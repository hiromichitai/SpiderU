﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalInstruments.NI4882;

namespace SpiderU {
	class DLM2000 : ScopeClass {

		private double[] VperDiv;

		public DLM2000(ComPortClass MyDevice,string ModelName,int NumChannel)	: base(MyDevice,ModelName,NumChannel) {

			if (ComPort.IDString.Substring(0,8) != "YOKOGAWA") { // check manufacturer 
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
			ComPort.GoToLocal();
		}

		protected override void GetData(){
			const int BUFFERLENGTH = 10000;		// maximu block length of DLM2000 is 20000

			GetSettings();
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

					ComPort.Write(":WAVEFORM:SEND?");
					byte[] HeaderLengthByte = ComPort.ReadByteArray(2);
					int HeaderLength = HeaderLengthByte[1] - '0';
					
					byte[] BlockLengthStrByte = ComPort.ReadByteArray(HeaderLength);
					string BlockLengthStr = System.Text.Encoding.ASCII.GetString(BlockLengthStrByte);
					int BlockLength = Convert.ToInt32(BlockLengthStr);

					if (BlockLength != 2 * RecordLength) {
						ErrorDialog EDialog = new ErrorDialog("UIMSGDATAINCONSIST"," in GetData().");
						return;
					}

					int BytesLeft = BlockLength;
					int BytesToRead = 0;
					Int16[] RawData = new Int16[BlockLength];
					byte[] InputBuffer = new byte[BUFFERLENGTH];
					int WOffset = 0;
					while (BytesLeft > 0) {
						if(BytesLeft > BUFFERLENGTH){
							BytesToRead = BUFFERLENGTH;
						} else {
							BytesToRead = BytesLeft;
						}
						InputBuffer = ComPort.ReadByteArray(BytesToRead);
						for (int WIndex = 0; WIndex < BytesToRead/sizeof(Int16); WIndex++) {
							RawData[WIndex+WOffset] = BitConverter.ToInt16(InputBuffer,sizeof(Int16)*WIndex);
						}
						WOffset += BytesToRead/sizeof(Int16);
						BytesLeft -= BytesToRead;
					}
					byte[] Garbage = ComPort.ReadByteArray(1);	// read one byte of EOS

					for (int Index = 0; Index < RecordLength; Index++) {
						TraceList[TraceIndex][Index] = TraceList[TraceIndex].Multiplier * (Range * RawData[Index] / 3200.0 + Offset);
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
