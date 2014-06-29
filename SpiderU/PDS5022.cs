using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibUsbDotNet;
using LibUsbDotNet.Main;


namespace SpiderU {
	class PDS5022 : ScopeClass {

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
				int ChannelNum = Convert.ToInt32(ChannelString.Substring(2));
				int TraceIndex = ChannelNum - 1;

				if (ChannelNum <= NumChannel) {
					TraceList[TraceIndex].IsOn = true;
				}
				ByteOffset += ChannelDataBytes + 3;	// 3 bytes for channel name "CHx"
			}
		}

		private void GetOneChannelData(byte[] ChannelData) {
			const int WaveDataStartAddress = 0x33;

			Encoding AsciiEnc = Encoding.GetEncoding("us-ascii");
			string ChannelString = AsciiEnc.GetString(ChannelData, 0, 3);
			int DataLength = BitConverter.ToInt32(ChannelData, 0x07);
			int TimePerDivCode = BitConverter.ToInt32(ChannelData, 0x13);
			int PositionCode = BitConverter.ToInt32(ChannelData, 0x17);
			int VoltPerDivCode = BitConverter.ToInt32(ChannelData, 0x1B);
			int AttenuationCode = BitConverter.ToInt32(ChannelData, 0x1F);
			int ChannelNum = Convert.ToInt32(ChannelString.Substring(2,1));
			double TimePerDiv = GetTimePerDiv(TimePerDivCode);
			double VoltPerDiv = GetVoltPerDiv(VoltPerDivCode);
			double ProbeAttenuation = GetProbeAttenuation(AttenuationCode);
			int TraceIndex = ChannelNum - 1;

			if (ChannelNum < NumChannel) {
				TraceList[TraceIndex].IsOn = true;
			}
			TraceList[TraceIndex].DataLength = DataLength;
			SamplingTime = TimePerDiv * 10.0 / DataLength;

			for (int DIndex = 0; DIndex < DataLength; DIndex++) {
				TraceList[TraceIndex][DIndex] = ProbeAttenuation * VoltPerDiv / 25.0 * BitConverter.ToInt16(ChannelData, WaveDataStartAddress + DIndex * 2);
			}

		}

		protected override void GetData() {
			const int BlockHeaderLength = 12;
			const int SPBVHeaderLength = 10;

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

			int ByteOffset = SPBVHeaderLength;
			int InfoBlockOffset = BitConverter.ToInt32(BlockBuffer, 6);

			while (ByteOffset < InfoBlockOffset) {
				int ChannelDataBytes = BitConverter.ToInt32(BlockBuffer, ByteOffset + 0x03) + 3;
				byte[] ChannelData = new byte[ChannelDataBytes];
				Array.Copy(BlockBuffer, ByteOffset, ChannelData, 0, ChannelDataBytes);
				GetOneChannelData(ChannelData);
				ByteOffset += ChannelDataBytes;
			}
		}



	}
}
