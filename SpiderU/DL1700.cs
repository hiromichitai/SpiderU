﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalInstruments.NI4882;

namespace SpiderU {
	class DL1700 : ScopeClass {

		public DL1700(ComPortClass MyDevice,string ModelName,int NumChannel)	: base(MyDevice,ModelName,NumChannel) {

			if (ComPort.IDString.Substring(0,8) != "YOKOGAWA") { // check manufacturer 
				WarningDialog DialogForm = new WarningDialog("Internal Error in DL1700 constructor");
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

		}

		public override int NumChannel() {
			return NumberOfChannel;
		}


		public override void GetSettings(){

			ComPort.Write(":COMMUNICATE:HEADER OFF");
			ComPort.Write(":ACQUIRE:RLENGTH?");
			string RecordLengthStr = ComPort.ReadString();
			RecordLength = Convert.ToInt32(RecordLengthStr);
			ComPort.Write(":TIMEBASE:SRATE?");
			string SamplingRateStr = ComPort.ReadString();
			SamplingTime = 1.0 / Convert.ToDouble(SamplingRateStr);
			for (int TraceIndex = 0; TraceIndex < NumberOfChannel; TraceIndex++) {
				int Channel = TraceIndex+1;
				string CommandString = String.Format(":CHANNEL{0:D}:DISPLAY?",Channel);
				ComPort.Write(CommandString);
				string ResultString = ComPort.ReadString();
				TraceList[TraceIndex].IsOn = (ResultString.Substring(0,1) == "1");
			}
		}

		protected override void GetData(){
			const int BUFFERLENGTH = 10000;		// maximu block length of DL1700 is 20000

		  	ComPort.Write(":STOP");
			for(int TraceIndex = 0; TraceIndex < NumberOfChannel; TraceIndex++){
				if(TraceList[TraceIndex].IsOn) {
					if(TraceList[TraceIndex].DataLength != RecordLength) {
						TraceList[TraceIndex].DataLength = RecordLength;
					}

					string CommandString = string.Format(":WAVEFORM:TRACE {0:D}", TraceIndex + 1);
					ComPort.Write(CommandString);
					ComPort.Write(":WAVEFORM:FORMAT WORD");
					ComPort.Write(":WAVEFORM:BYTEORDER LSBFIRST");
		
					ComPort.Write(":WAVEFORM:START 0");
					CommandString = string.Format(":WAVEFORM:END {0:D}", RecordLength - 1);
					ComPort.Write(CommandString);
					ComPort.Write(":WAVEFORM:OFFSET?");
					string Response = ComPort.ReadString();
					double Offset = Convert.ToDouble(Response);

					ComPort.Write(":WAVEFORM:RANGE?");
					Response = ComPort.ReadString();
					double Range = Convert.ToDouble(Response);

					ComPort.Write(":WAVEFORM:SIGN?");
					Response = ComPort.ReadString();

					ComPort.Write(":WAVEFORM:SEND?");
					byte[] HeaderLengthByte = ComPort.ReadByteArray(2);
					int HeaderLength = HeaderLengthByte[1] - '0';
					
					byte[] BlockLengthStrByte = ComPort.ReadByteArray(HeaderLength);
					string BlockLengthStr = System.Text.Encoding.ASCII.GetString(BlockLengthStrByte);
					int BlockLength = Convert.ToInt32(BlockLengthStr);
					int WordsLeft = BlockLength;
					int WordsToRead = 0;
					Int16[] RawData = new Int16[BlockLength];
					byte[] InputBuffer = new byte[BUFFERLENGTH*sizeof(Int16)];
					int WOffset = 0;
					while (WordsLeft > 0) {
						if (WordsLeft > BUFFERLENGTH) {
							WordsToRead = BUFFERLENGTH;
						} else {
							WordsToRead = WordsLeft;
						}
						InputBuffer = ComPort.ReadByteArray(WordsToRead * sizeof(Int16));
						for (int BIndex = 0; BIndex < WordsToRead; BIndex++) {
							RawData[BIndex+WOffset] = BitConverter.ToInt16(InputBuffer,BIndex*sizeof(Int16));
						}
						WOffset += WordsToRead;
						WordsLeft -= WordsToRead;
					}
					byte[] Garbage = ComPort.ReadByteArray(1);	// read one byte of EOS

					for (int Index = 0; Index < RecordLength; Index++) {
						TraceList[TraceIndex][Index] = TraceList[TraceIndex].Multiplier * (Range * RawData[Index] / 3072 + Offset);
					}

				}
			}
//			if AqOptionForm.TrigSingleCheckBox.Checked then begin
//				ComDevice.Write(":START");
//			end;
			ComPort.GoToLocal();
		}

	}
}
