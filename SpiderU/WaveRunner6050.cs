using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalInstruments.NI4882;

namespace SpiderU {
	class WaveRunner6050 : ScopeClass {

		public WaveRunner6050(ComPortClass MyDevice, string ModelName, int NumChannel)
			: base(MyDevice, ModelName, NumChannel) {

			ComPort.Write("*IDN?");	// to make sure what am I?
			string IDString = ComPort.ReadString();
			if (IDString.Substring(0, 19) != "*IDN LECROY,WR6050A") { // check model name 
				WarningDialog DialogForm = new WarningDialog("UIMSGILLEGALID", IDString);
				Exception Ex = new Exception("Internal Error");
				throw (Ex);
			}
			for (int Index = 0; Index < NumberOfChannel; Index++) {
				int Channel = Index + 1;
				TraceClass Trace = TraceList[Index];
				Trace.TraceLabel = String.Format("Ch{0:D}", Channel);
				Trace.TraceUnit = "V";
				Trace.Multiplier = 1.0;
			}
			ComPort.Write("COMM_HEADER OFF");
			ComPort.Write("COMM_ORDER LO");	// LSB first
			ComPort.Write("COMM_FORMAT DEF9,BYTE,BIN");
			ComPort.Write("WFSU SP,0,NP,0,FP,0,SN,0");
		}

		public override int NumChannel() {
			return NumberOfChannel;
		}

		public override void GetSettings() {
			for (int TraceIndex = 0; TraceIndex < NumberOfChannel; TraceIndex++) {
				int Channel = TraceIndex + 1;
				string CommandString = String.Format("C{0:D}:TRACE?", Channel);
				ComPort.Write(CommandString);
				string ResultString = ComPort.ReadString();
				TraceList[TraceIndex].IsOn = (ResultString.Substring(0, 2) == "ON");
			}
			string CommString = String.Format("C%1d:WAVEFORM? DESC", 1);	// use channel 1
			ComPort.Write(CommString);
			ComPort.ReadByteArray(5);
			byte[] BlockLenStr = ComPort.ReadByteArray(11);
			int BlockLength = Convert.ToInt32(Convert.ToString(BlockLenStr).Substring(2, 9));
			byte[] BlockData = ComPort.ReadByteArray(BlockLength);
			double VGain = System.BitConverter.ToSingle(BlockData, 156);
			double VOffset = System.BitConverter.ToSingle(BlockData, 160);
			SamplingTime = System.BitConverter.ToSingle(BlockData, 176);
		}

		protected override void GetData() {
			const int BUFFERLENGTH = 20000;		// maximu block length of WaveRunner6050 is 20000

			ComPort.Write(":STOP");
			for (int TraceIndex = 0; TraceIndex < NumberOfChannel; TraceIndex++) {
				if (TraceList[TraceIndex].IsOn) {
					int Channel = TraceIndex + 1;
					TraceClass Trace = TraceList[TraceIndex];
					string CommString = String.Format("C%1d:WAVEFORM? DESC", Channel);
					ComPort.Write(CommString);
					ComPort.ReadByteArray(5);
					byte[] BlockLenStr = ComPort.ReadByteArray(11);
					int BlockLength = Convert.ToInt32(Convert.ToString(BlockLenStr).Substring(2, 9));
					byte[] BlockData = ComPort.ReadByteArray(BlockLength);
					double VGain = System.BitConverter.ToSingle(BlockData, 156);
					double VOffset = System.BitConverter.ToSingle(BlockData, 160);
					SamplingTime = System.BitConverter.ToSingle(BlockData, 176);

					CommString = String.Format("C%1d:WAVEFORM? DAT1", Channel);
					ComPort.Write(CommString);
					ComPort.ReadByteArray(5);
					BlockLenStr = ComPort.ReadByteArray(11);
					BlockLength = Convert.ToInt32(Convert.ToString(BlockLenStr).Substring(2, 9));
					Trace.DataLength = BlockLength;
					int BytesLeft = BlockLength;
					int ReadLength = 0;
					while (BytesLeft > 0) {
						if (BytesLeft > BUFFERLENGTH) {
							ReadLength = BUFFERLENGTH;
						} else {
							ReadLength = BytesLeft;
						}
						byte[] RawData = ComPort.ReadByteArray(ReadLength);
						for (int BIndex = 0; BIndex < ReadLength; BIndex++) {
							Trace[BlockLength - BytesLeft + BIndex] =
								(VGain * RawData[BIndex] - VOffset) * Trace.Multiplier;
						}
						BytesLeft -= ReadLength;
					}
				}
				ComPort.ReadByteArray(1);
			}
			ComPort.GoToLocal();
		}
	}
}
