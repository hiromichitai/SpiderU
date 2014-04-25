using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalInstruments.NI4882;

namespace SpiderU {
	class WaveRunner6050 : ScopeClass {

		private double[] VperDiv;

		public WaveRunner6050(Device MyDevice,string ModelName,int NumChannel)	: base(MyDevice,ModelName,NumChannel) {

			GPIBDevice.Write("*IDN?");	// to make sure what am I?
			string IDString = GPIBDevice.ReadString();
			if (IDString.Substring(0,19) != "*IDN LECROY,WR6050A") { // check model name 
				WarningDialog DialogForm = new WarningDialog("UIMSGILLEGALID", IDString);
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
			GPIBDevice.Write("COMM_HEADER OFF");			
			GPIBDevice.Write("COMM_ORDER LO");	// LSB first
			GPIBDevice.Write("COMM_FORMAT DEF9,BYTE,BIN");
			GPIBDevice.Write("WFSU SP,0,NP,0,FP,0,SN,0");
			VperDiv = new double[NumberOfChannel];
			/*
				GPIBWrite('TRIG_MODE?');
				GPIBReadString(255,TrigModeString);

				GPIBWrite('STOP');
				for TraceIndex := 0 to MaxNumTrace-1 do begin
					if FTraceProperty[TraceIndex].SwitchOn then begin

						CommandString := Format('C%1d:WAVEFORM? DESC',[TraceIndex+1]);
						GPIBWrite(CommandString);
						ibeos(UnitDescriptor,0);
						ibrd(UnitDescriptor,Dummy,5);
						ibrd(UnitDescriptor,BlockLenStr,11);
						BlockLen := StrToInt(Copy(BlockLenStr,3,9));
						ibrd(UnitDescriptor,RawData,BlockLen+1);
						ibeos(UnitDescriptor,$040a);
						BytePtr := @FloatBuf;
						BytePtr^ := RawData[156];
						inc(BytePtr);
						BytePtr^ := RawData[157];
						inc(BytePtr);
						BytePtr^ := RawData[158];
						inc(BytePtr);
						BytePtr^ := RawData[159];
						VGain := FloatBuf;
						BytePtr := @FloatBuf;
						BytePtr^ := RawData[160];
						inc(BytePtr);
						BytePtr^ := RawData[161];
						inc(BytePtr);
						BytePtr^ := RawData[162];
						inc(BytePtr);
						BytePtr^ := RawData[163];
						VOffset := FloatBuf;
						BytePtr := @FloatBuf;
						BytePtr^ := RawData[176];
						inc(BytePtr);
						BytePtr^ := RawData[177];
						inc(BytePtr);
						BytePtr^ := RawData[178];
						inc(BytePtr);
						BytePtr^ := RawData[179];
						SamplingTime := FloatBuf;

						BytePtr := @FloatBuf;
						BytePtr^ := RawData[328];
						inc(BytePtr);
						BytePtr^ := RawData[329];
						inc(BytePtr);
						BytePtr^ := RawData[330];
						inc(BytePtr);
						BytePtr^ := RawData[331];
						ProbeATT := FloatBuf;

						FTraceProperty[TraceIndex].VperDiv := ScopeVperDiv(RawData[332])*ProbeATT;

						CommandString := Format('C%1d:WAVEFORM? DAT1',[TraceIndex+1]);
						GPIBWrite(CommandString);
						ibeos(UnitDescriptor,0);
						ibrd(UnitDescriptor,Dummy,5);
						ibrd(UnitDescriptor,BlockLenStr,11);
						FRecordLength := StrToInt(Copy(BlockLenStr,3,9));
						SetLength(FYData[TraceIndex],FRecordLength);
						BytesLeft := FRecordLength;
						while BytesLeft > 0 do begin
							if BytesLeft > BUFFERLENGTH then begin
								ReadLength := BUFFERLENGTH;
							end else begin
								ReadLength := BytesLeft;
							end;
							ibrd(UnitDescriptor,RawData,ReadLength);
							for i := 0 to ReadLength-1 do begin
								FYData[TraceIndex,FRecordLength - BytesLeft + i] :=
									(VGain * ShortInt(RawData[i]) - VOffset)
									* FTraceProperty[TraceIndex].YMagnitude;
							end;
							BytesLeft := BytesLeft - ReadLength;
						end;
						ibrd(UnitDescriptor,Dummy,1);
						ibeos(UnitDescriptor,$040a);
					end;
				end;
				if AqOptionForm.TrigSingleCheckBox.Checked then begin
					if TrigModeString = 'STOP' then begin
						TrigModeString := 'SINGLE';
					end;
				end;
				GPIBWrite('TRIG_MODE '+TrigModeString);
				Local;
				DataActive := true;
 
			 * */
		}

		public override int NumChannel() {
			return NumberOfChannel;
		}


		public override void GetSettings(){
			for (int TraceIndex = 0; TraceIndex < NumberOfChannel; TraceIndex++) {
				int Channel = TraceIndex + 1;
				string CommandString = String.Format("C{0:D}:TRACE?", Channel);
				GPIBDevice.Write(CommandString);
				string ResultString = GPIBDevice.ReadString();
				TraceList[TraceIndex].IsOn = (ResultString.Substring(0, 2) == "ON");
				CommandString = String.Format("C{0:D}:VDIV?", Channel);
				GPIBDevice.Write(CommandString);
				ResultString = GPIBDevice.ReadString();
				VperDiv[TraceIndex] = Convert.ToDouble(ResultString);
			}
			string CommString = String.Format("C%1d:WAVEFORM? DESC",1);	// use channel 1
			GPIBDevice.Write(CommString);
			GPIBDevice.ReadByteArray(5);
			byte[] BlockLenStr = GPIBDevice.ReadByteArray(11);
			int BlockLength = Convert.ToInt32(Convert.ToString(BlockLenStr).Substring(2,9));
			byte[] BlockData = GPIBDevice.ReadByteArray(BlockLength);
			double VGain = System.BitConverter.ToSingle(BlockData,156);
			double VOffset = System.BitConverter.ToSingle(BlockData,160);
			SamplingTime = System.BitConverter.ToSingle(BlockData,176);

		}

		protected override void GetData(){
			const int BUFFERLENGTH = 10000;		// maximu block length of WaveRunner6050 is 20000

		  	GPIBDevice.Write(":STOP");
			for(int TraceIndex = 0; TraceIndex < NumberOfChannel; TraceIndex++){
				if(TraceList[TraceIndex].IsOn) {
				}
			}
			GPIBDevice.GoToLocal();
		}

	}
}
