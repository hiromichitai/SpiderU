using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibUsbDotNet;
using LibUsbDotNet.Main;


namespace SpiderU {
	class PDS5022 : ScopeClass {
/*
// owondump.h - linux USB userspace driver for the owon PDS digital storage scopes
// Copyright 2009 Michael Murphy <ee07m060@elec.qmul.ac.uk>

#define OWON_START_DATA_CMD "START"
#define BULK_WRITE_ENDPOINT 0x03
#define BULK_READ_ENDPOINT 0x81
#define DEFAULT_INTERFACE 0x00
#define DEFAULT_CONFIGURATION 0x01
#define DEFAULT_TIMEOUT	500				  // 500mS for USB timeouts
#define DEFAULT_BITMAP_READ_TIMEOUT 3000  // allow Owon the extra time needed to fill USB buffer for bitmap data
#define MAX_USB_LOCKS 10				  // allow multiple scopes to slave to same PC host
#define MAX_HEADER_LENGTH 0x40
#define VECTORGRAM_FILE_HEADER_LENGTH 10  // for vectorgrams, the data header begins 10 bytes after file header
#define VECTORGRAM_BLOCK_HEADER_LENGTH 51
#define VECTORGRAM_BLOCK_HEADER_CHNAMELEN 3	// "CH1", "CH2", "CHA", etc.

// this is the structure of the header that precedes each block of channel data sent by the Owon

struct channelHeader {
	char channelname[3];	// 3 bytes ( {"CH1", "CH2", "CHA", "CHB", "CHC", "CHD"} )
	long int blocklength;	// 4 bytes (little endian long int holding data block length)
	long int samplecount1;	// 4 bytes (little endian long int holding the count of samples)
	long int samplecount2;	// 4 bytes (little endian long int holding the count of samples)
	long int unknown3;		// 4 bytes (purpose unknown)
	long int timebasecode;	// 4 bytes (little endian long int holding timebase code - 0x00000000 (5ns) to 0x000000ff (100s)
	long int unknown4;		// 4 bytes (purpose unknown)
	long int vertsenscode;	// 4 bytes (little endian long int holding vertical sensitivity code - 0x00000001 (5mV) to 0x0000000A (5V)
	long int unknown5;		// 4 bytes (purpose unknown)
	long int unknown6;		// 4 bytes (purpose unknown)
	long int unknown7;		// 4 bytes (purpose unknown)
	long int unknown8;		// 4 bytes (purpose unknown)
	long int unknown9;		// 4 bytes (purpose unknown)

// these last three should be in a separate structure since they are not contained in the binary header
	int vertSensitivity;	// 5mV through 5000mV (5V)
	long long int timeBase; // in nanoseconds (10E-9)
};

	*/
		
		private double[] VoltPerDiv;
		private double[] ProbeAttenuation;

		public PDS5022(ComPortClass MyComPort, string ModelName, int NumChannel)
			: base(MyComPort, ModelName, NumChannel) {

			if (ComPort.IDString.Substring(0, 4) != "OWON") { // check manufacturer 
				ErrorDialog EDialog = new ErrorDialog("PDS5022 constructor");
				return;
			}
			for (int Index = 0; Index < NumberOfChannel; Index++) {
				int Channel = Index + 1;
				TraceClass Trace = TraceList[Index];
				Trace.TraceLabel = String.Format("Ch{0:D}", Channel);
				Trace.TraceUnit = "V";
				Trace.Multiplier = 1.0;
			}
		}

		private double GetTimePerDiv(int TimePerDivCode) {
			switch (TimePerDivCode) {
				case 0: return 5e-9;
				case 1: return 10e-9;
				case 2: return 25e-9;
				case 3: return 50e-9;
				case 4: return 100e-9;
				case 5: return 250e-9;
				case 6: return 500e-9;
				case 7: return 1.0e-6;
				case 8: return 2.5e-6;
				case 9: return 5.0e-6;
				case 10: return 10.0e-6;
				case 11: return 25.0e-6;
				case 12: return 50.0e-6;
				case 13: return 100.0e-6;
				case 14: return 250.0e-6;
				case 15: return 500.0e-6;
				case 16: return 1.0e-3;
				case 17: return 2.5e-3;
				case 18: return 5.0e-3;
				case 19: return 10.0e-3;
				case 20: return 25.0e-3;
				case 21: return 50.0e-3;
				case 22: return 100e-3;
				case 23: return 250e-3;
				case 24: return 500e-3;
				case 25: return 1.0;
				case 26: return 2.5;
				case 27: return 5.0;
				case 28: return 10.0;
				case 29: return 25.0;
				case 30: return 50.0;
				case 31: return 100.0;
			}
			WarningDialog WDialog = new WarningDialog("UIMSGOSCWRONGDATA", "in GetTimePerDiv");
			return 0.0;
		}

		private double GetVoltPerDiv(int VoltPerDivCode) {
			switch (VoltPerDivCode) {
				case 1: return 5.0e-3;
				case 2: return 10.0e-3;
				case 3: return 20.0e-3;
				case 4: return 50.0e-3;
				case 5: return 100.0e-3;
				case 6: return 200.0e-3;
				case 7: return 500.0e-3;
				case 8: return 1.0;
				case 9: return 2.0;
				case 10: return 5.0;
				case 11: return 10.0;
				case 12: return 20.0;
				case 13: return 50.0;
			}
			WarningDialog WDialog = new WarningDialog("UIMSGOSCWRONGDATA", "in GetVoltPerDiv");
			return 0.0;
		}

		private double GetProbeAttenuation(int ATTCode) {
			switch (ATTCode) {
				case 0: return 1.0;
				case 1: return 10.0;
				case 2: return 100.0;
				case 3: return 1000.0;
			}
			WarningDialog WDialog = new WarningDialog("UIMSGOSCWRONGDATA", "in GetProbeAttenuation");
			return 0.0;
		}

		public override void GetSettings() {
			const int BlockHeaderLength = 12;
			const int SPBVHeaderLength = 10;
			const int WaveDataStartAddress = 0x33;

			ComPort.Write("STARTBIN");
			byte[] BHeaderBuffer = ComPort.ReadByteArray(BlockHeaderLength);
			int BlockLength = BitConverter.ToInt32(BHeaderBuffer, 0);
			byte[] BlockBuffer = ComPort.ReadByteArray(BlockLength);
			Encoding AsciiEnc = Encoding.GetEncoding("us-ascii");
			string TopTwoChar = AsciiEnc.GetString(BlockBuffer, 0, 2);
			if (TopTwoChar != "SP") {
				WarningDialog WDialog = new WarningDialog("UIMSGPDSBMP",true);
				return;
			}
			byte[] ChannelDataList = new byte[BlockLength - SPBVHeaderLength];
			Array.Copy(BlockBuffer, SPBVHeaderLength, ChannelDataList, 0, BlockLength - SPBVHeaderLength);
			int InfoBlockOffset = BitConverter.ToInt32(BlockBuffer, 6) - SPBVHeaderLength;

			int ByteOffset = 0;
			while (ByteOffset < InfoBlockOffset) {
				string ChannelString = AsciiEnc.GetString(ChannelDataList, ByteOffset + 0, 3);
				int ChannelDataBytes = BitConverter.ToInt32(ChannelDataList, ByteOffset + 0x03);
				int DataLength = BitConverter.ToInt32(ChannelDataList, ByteOffset + 0x07);
				int TimePerDivCode = BitConverter.ToInt32(ChannelDataList, ByteOffset + 0x13);
				int PositionCode = BitConverter.ToInt32(ChannelDataList, ByteOffset + 0x17);
				int VoltPerDivCode = BitConverter.ToInt32(ChannelDataList, ByteOffset + 0x1B);
				int AttenuationCode = BitConverter.ToInt32(ChannelDataList, ByteOffset + 0x1F);
				int ChannelNum = Convert.ToInt32(ChannelString.Substring(2));
				double TimePerDiv = GetTimePerDiv(TimePerDivCode);
				double VoltPerDiv = GetVoltPerDiv(VoltPerDivCode);
				double ProbeAttenuation = GetProbeAttenuation(AttenuationCode);

				if (ChannelNum <= NumChannel) {
					int TraceIndex = ChannelNum - 1;
					TraceList[TraceIndex].IsOn = true;
				}
				ByteOffset += ChannelDataBytes + 3;	// 3 bytes for channel name "CHx"
			}


		}

		protected override void GetData() {
			const int BlockHeaderLength = 12;
			const int SPBVHeaderLength = 10;
			const int WaveDataStartAddress = 0x33;

			ComPort.Write("STARTBIN");
			byte[] BHeaderBuffer = ComPort.ReadByteArray(BlockHeaderLength);
			int BlockLength = BitConverter.ToInt32(BHeaderBuffer, 0);
			byte[] BlockBuffer = ComPort.ReadByteArray(BlockLength);
			Encoding AsciiEnc = Encoding.GetEncoding("us-ascii");
			string TopTwoChar = AsciiEnc.GetString(BlockBuffer, 0, 2);
			if (TopTwoChar != "SP") {
				WarningDialog WDialog = new WarningDialog("UIMSGPDSBMP", true);
				return;
			}
			byte[] ChannelDataList = new byte[BlockLength - SPBVHeaderLength];
			Array.Copy(BlockBuffer, SPBVHeaderLength, ChannelDataList, 0, BlockLength - SPBVHeaderLength);
			int InfoBlockOffset = BitConverter.ToInt32(BlockBuffer, 6) - SPBVHeaderLength;

			int ByteOffset = 0;
			while (ByteOffset < InfoBlockOffset) {
				string ChannelString = AsciiEnc.GetString(BlockBuffer, ByteOffset + 0, 3);
				int ChannelDataBytes = BitConverter.ToInt32(BlockBuffer, ByteOffset + 0x03);
				int DataLength = BitConverter.ToInt32(BlockBuffer, ByteOffset + 0x07);
				int TimePerDivCode = BitConverter.ToInt32(BlockBuffer, ByteOffset + 0x13);
				int PositionCode = BitConverter.ToInt32(BlockBuffer, ByteOffset + 0x17);
				int VoltPerDivCode = BitConverter.ToInt32(BlockBuffer, ByteOffset + 0x1B);
				int AttenuationCode = BitConverter.ToInt32(BlockBuffer, ByteOffset + 0x1F);
				int ChannelNum = Convert.ToInt32(ChannelString[2]);
				double TimePerDiv = GetTimePerDiv(TimePerDivCode);
				double VoltPerDiv = GetVoltPerDiv(VoltPerDivCode);
				double ProbeAttenuation = GetProbeAttenuation(AttenuationCode);

				if (ChannelNum < NumChannel) {
					int TraceIndex = ChannelNum - 1;
					TraceList[TraceIndex].IsOn = true;
				}

			}
		}



	}
}
