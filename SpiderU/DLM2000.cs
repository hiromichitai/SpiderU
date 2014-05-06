using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalInstruments.NI4882;

namespace SpiderU {
	class DLM2000 : ScopeClass {

		private double[] VperDiv;

		public DLM2000(ComDeviceClass MyDevice,string ModelName,int NumChannel)	: base(MyDevice,ModelName,NumChannel) {

			if (ComDevice.IDString.Substring(0,8) != "YOKOGAWA") { // check manufacturer 
				WarningDialog DialogForm = new WarningDialog("Internal Error in DLM2000 constructor");
				Exception Ex = new Exception("Internal Error");
				throw(Ex);
			}
			for (int Index = 0; Index < NumberOfChannel; Index++) {
				int Channel = Index + 1;
				TraceClass Trace = TraceList[Index];
				Trace.TraceLabel = String.Format("Ch{0:D}", Channel);
				Trace.TraceUnit = "V";
				Trace.Multiplier = 1.0;
			}
			VperDiv = new double[NumberOfChannel];

		}

		public override int NumChannel() {
			return NumberOfChannel;
		}


		public override void GetSettings(){

			ComDevice.Write(":COMMUNICATE:HEADER OFF");
			ComDevice.Write(":ACQUIRE:RLENGTH?");
			string RecordLengthStr = ComDevice.ReadString();
			RecordLength = Convert.ToInt32(RecordLengthStr);
			ComDevice.Write(":TIMEBASE:SRATE?");
			string SamplingRateStr = ComDevice.ReadString();
			SamplingTime = 1.0 / Convert.ToDouble(SamplingRateStr);
			for (int TraceIndex = 0; TraceIndex < NumberOfChannel; TraceIndex++) {
				int Channel = TraceIndex+1;
				string CommandString = String.Format(":CHANNEL{0:D}:DISPLAY?",Channel);
				ComDevice.Write(CommandString);
				string ResultString = ComDevice.ReadString();
				TraceList[TraceIndex].IsOn = (ResultString.Substring(0,1) == "1");

				CommandString = String.Format(":CHANNEL{0:D}:VDIV?", Channel);
				ComDevice.Write(CommandString);
				ResultString = ComDevice.ReadString();
				
				VperDiv[TraceIndex] = Convert.ToDouble(ResultString);

			}
		}

		protected override void GetData(){
			const int BUFFERLENGTH = 10000;		// maximu block length of DLM2000 is 20000

		  	ComDevice.Write(":STOP");
			for(int TraceIndex = 0; TraceIndex < NumberOfChannel; TraceIndex++){
				if(TraceList[TraceIndex].IsOn) {
					if(TraceList[TraceIndex].DataLength != RecordLength) {
						TraceList[TraceIndex].DataLength = RecordLength;
					}

					string CommandString = string.Format(":WAVEFORM:TRACE {0:D}", TraceIndex + 1);
					ComDevice.Write(CommandString);
					ComDevice.Write(":WAVEFORM:FORMAT BYTE");
					ComDevice.Write(":WAVEFORM:BYTEORDER LSBFIRST");
		
					ComDevice.Write(":WAVEFORM:START 0");
					CommandString = string.Format(":WAVEFORM:END {0:D}", RecordLength - 1);
					ComDevice.Write(CommandString);
					ComDevice.Write(":WAVEFORM:OFFSET?");
					string Response = ComDevice.ReadString();
					double Offset = Convert.ToDouble(Response);

					ComDevice.Write(":WAVEFORM:RANGE?");
					Response = ComDevice.ReadString();
					double Range = Convert.ToDouble(Response);

					ComDevice.Write(":WAVEFORM:SEND?");
					byte[] HeaderLengthByte = ComDevice.ReadByteArray(2);
					int HeaderLength = HeaderLengthByte[1] - '0';
					
					byte[] BlockLengthStrByte = ComDevice.ReadByteArray(HeaderLength);
					string BlockLengthStr = System.Text.Encoding.ASCII.GetString(BlockLengthStrByte);
					int BlockLength = Convert.ToInt32(BlockLengthStr);
					int BytesLeft = BlockLength;
					int BytesToRead = 0;
					byte[] RawData = new byte[BlockLength];
					byte[] InputBuffer = new byte[BUFFERLENGTH];
					int BOffset = 0;
					while (BytesLeft > 0) {
						if(BytesLeft > BUFFERLENGTH){
							BytesToRead = BUFFERLENGTH;
						} else {
							BytesToRead = BytesLeft;
						}
						InputBuffer = ComDevice.ReadByteArray(BytesToRead);
						for (int BIndex = 0; BIndex < BytesToRead; BIndex++) {
							RawData[BIndex+BOffset] = InputBuffer[BIndex];
						}
						BOffset += BytesToRead;
						BytesLeft -= BytesToRead;
					}
					byte[] Garbage = ComDevice.ReadByteArray(1);	// read one byte of EOS

					double[] TraceData = TraceList[TraceIndex].Data();
					for (int Index = 0; Index < RecordLength; Index++) {
						TraceData[Index] = TraceList[TraceIndex].Multiplier*(Range*RawData[Index]/12.5+Offset);
					}
				}
			}
//			if AqOptionForm.TrigSingleCheckBox.Checked then begin
//				ComDevice.Write(":START");
//			end;
			ComDevice.GoToLocal();
		}

	}
}
