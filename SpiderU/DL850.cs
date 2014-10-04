using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalInstruments.NI4882;

namespace SpiderU {
	class DL850 : ScopeClass {
		private enum ModuleTypeEnum {
			NoModule, VoltageModule, TemperatureModule, StrainModule, AccelarationModule, FrequencyModule
		}
		private int[] ModuleTypeID;


		public DL850(ComPortClass MyDevice,string ModelName,int NumChannel)	: base(MyDevice,ModelName,NumChannel) {

			if (ComPort.IDString.Substring(0,8) != "YOKOGAWA") { // check manufacturer 
				WarningDialog DialogForm = new WarningDialog("Internal Error in DL850 constructor");
				Exception Ex = new Exception("Internal Error");
				throw(Ex);
			}
			ModuleTypeID = new int[NumChannel];
			for (int Index = 0; Index < NumberOfChannel; Index++) {
				int Channel = Index + 1;
				TraceClass Trace = TraceList[Index];
				Trace.TraceLabel = String.Format("Ch{0:D}", Channel);
				Trace.TraceUnit = "V";
				Trace.Multiplier = 1.0;
			}

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
				string CommandString = String.Format(":CHANNEL{0:D}:MODULE?", Channel);
				ComPort.Write(CommandString);
				string ResultString = ComPort.ReadString();
				switch (ResultString) {
					case("NOMODULE"): 
						ModuleTypeID[TraceIndex] = (int)ModuleTypeEnum.NoModule;
						break;
					case ("M701250"):		// 701250(HS10M12)
					case ("M701251"):		//  701251(HS1M16)
					case ("M701255"): 		// 701255(NONISO_10M12)
					case ("M701260"): 		// 701260(HV(with RMS)
					case ("M720210"):		// 720210(100MHz module)
					case("M720220"):		// 720220(16Ch voltage input)
					case("M720221"): 		// 720221(16ch temp/voltage module)
						ModuleTypeID[TraceIndex] = (int)ModuleTypeEnum.VoltageModule;
						break;
					case ("M701261"): 		// 701261(UNIV)
					case ("M701262"): 		// 701262(UNIV_AAF)
					case ("M701265"): 		// 701265(TEMP/HPV)
					case ("M720230"): 		// 720230(logic)
					case ("M720240"): 		// 720240(CAN bus monitor)
					case ("M720241"): 		// 720241(CAN&LIN bus monitor)
						ModuleTypeID[TraceIndex] = (int)ModuleTypeEnum.TemperatureModule;
						break;
					case ("M701270"): 		// 701270(STRAIN_NDIS)
					case ("M701271"): 		// 701271(STRAIN_DSUB)
						ModuleTypeID[TraceIndex] = (int)ModuleTypeEnum.StrainModule;
						break;
					case ("M701275"): 		// 701275(ACCL/VOLT)
						ModuleTypeID[TraceIndex] = (int)ModuleTypeEnum.AccelarationModule;
						break;
					case ("M701280"): 		// 701280(FREQ)
						ModuleTypeID[TraceIndex] = (int)ModuleTypeEnum.FrequencyModule;
						break;
				}
				if (ModuleTypeID[TraceIndex] != (int)ModuleTypeEnum.NoModule) {
					CommandString = String.Format(":CHANNEL{0:D}:DISPLAY?", Channel);
					ComPort.Write(CommandString);
					ResultString = ComPort.ReadString();
					TraceList[TraceIndex].IsOn = (ResultString.Substring(0, 1) == "1");
				} else {
					TraceList[TraceIndex].IsOn = false;
				}
			}
			ComPort.GoToLocal();
		}

		protected override void GetData(){
			const int BUFFERLENGTH = 10000;		// maximu block length of DL850 is 20000

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

					double Divider = 24000.0;
					switch ((ModuleTypeEnum)ModuleTypeID[TraceIndex]) {
						case (ModuleTypeEnum.TemperatureModule):
							Offset = 0.0;
							Divider = 100.0;
							break;
						case (ModuleTypeEnum.StrainModule):
							Divider = 48000.0;
							break;
					}
	
					ComPort.Write(":WAVEFORM:SEND?");
					byte[] HeaderLengthByte = ComPort.ReadByteArray(2);
					int HeaderLength = HeaderLengthByte[1] - '0';
					
					byte[] BlockLengthStrByte = ComPort.ReadByteArray(HeaderLength);
					string BlockLengthStr = System.Text.Encoding.ASCII.GetString(BlockLengthStrByte);
					int BlockLength = Convert.ToInt32(BlockLengthStr);
					int BytesLeft = BlockLength;
					int BytesToRead = 0;
					Int16[] RawData = new Int16[BlockLength];
					byte[] InputBuffer = new byte[BUFFERLENGTH];
					int BOffset = 0;
					while (BytesLeft > 0) {
						if(BytesLeft > BUFFERLENGTH){
							BytesToRead = BUFFERLENGTH;
						} else {
							BytesToRead = BytesLeft;
						}
						InputBuffer = ComPort.ReadByteArray(BytesToRead);
						for (int BIndex = 0; BIndex < BytesToRead; BIndex++) {
							RawData[BIndex+BOffset] = InputBuffer[BIndex];
						}
						BOffset += BytesToRead;
						BytesLeft -= BytesToRead;
					}
					byte[] Garbage = ComPort.ReadByteArray(1);	// read one byte of EOS
	
					for (int Index = 0; Index < RecordLength; Index++) {
						TraceList[TraceIndex][Index] = TraceList[TraceIndex].Multiplier * (Range * RawData[Index] * 10.0 / Divider + Offset);
					}
				}
			}
			ComPort.GoToLocal();
		}

	}
}
