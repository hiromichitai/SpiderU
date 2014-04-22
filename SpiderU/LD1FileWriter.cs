using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SpiderU {

	class LD1FileWriter: FileWriterClass {
		private FileStream FStream;
		private System.Text.Encoding FEncoding;

		public LD1FileWriter(string FileName) : base(FileName) {
			try {
				FStream = new FileStream(FileName, FileMode.Create);
				FEncoding = GetEncoding();
			}
			catch (ArgumentException) {
				WarningDialog WDialog = new WarningDialog("UIMSGARGEXCEPTION", "in CSVFileWriter");
			}
			catch (System.Security.SecurityException) {
				WarningDialog WDialog = new WarningDialog("UIMSGSECURITYEXCEPTION", "in CSVFileWriter");
			}
			catch (PathTooLongException) {
				WarningDialog WDialog = new WarningDialog("UIMSGIOEXCEPTION", FileName + " in CSVFileWriter");
			}
			catch (IOException) {
				WarningDialog WDialog = new WarningDialog("UIMSGIOEXCEPTION", "in CSVFileWriter");
			}
			
		}

		private void PutInt(int intData){
			byte[] intBuffer = BitConverter.GetBytes(intData);
			FStream.Write(intBuffer,0,4);
		}

		private void PutFloat(double doubleData) {	// LD1 format uses 4byte float(not double)
			byte[] intBuffer = BitConverter.GetBytes((float)doubleData);
			FStream.Write(intBuffer, 0, 4);
		}

		private void PutString(string stringData) {
			byte[] stringBuffer = Encoding.Unicode.GetBytes(stringData);
			PutInt(stringBuffer.Length);
			FStream.Write(stringBuffer, 0, stringBuffer.Length);
		}

		public override void WriteFile() {
			List<ScopeClass> SList = ScopeManager.ScopeList;

			try {
				if (Properties.Settings.Default.syncAllScope) {
					string commentLine = SList[0].Comment;
					for (int SIndex = 1; SIndex < SList.Count; SIndex++) {
						commentLine += "," + SList[SIndex].Comment;
					}					
					PutString(commentLine);

					PutInt(1);	// All scopes are syncronized, so number of signal block is 1

					PutInt(SList[0].DataLength);

					int NumTotalOnChannel = 0;
					for (int SIndex = 0; SIndex < SList.Count; SIndex++) {
						NumTotalOnChannel += SList[SIndex].NumOnChannel;
					}
					PutInt(NumTotalOnChannel+1);	// +1 for time axis
					PutString("time");
					PutString("s");
					for (int SIndex = 0; SIndex < SList.Count; SIndex++) {
						for(int TIndex = 0; TIndex < SList[SIndex].NumOnChannel; TIndex++){
							PutString(SList[SIndex].NthOnChannel(TIndex).TraceLabel);
							PutString(SList[SIndex].NthOnChannel(TIndex).TraceUnit);
						}
					}

	
					for (int DIndex = 0; DIndex < SList[0].DataLength; DIndex++) {
						for (int SIndex = 0; SIndex < SList.Count; SIndex++) {
							ScopeClass Scope = SList[SIndex];
							for (int TIndex = 0; TIndex < Scope.NumOnChannel; TIndex++) {
								PutFloat(Scope.NthOnChannel(TIndex).Data()[DIndex]);
							}
						}
					}
				} else {
					PutString(" ");

					PutInt(SList.Count);

					for (int SIndex = 0; SIndex < SList.Count; SIndex++) {
						ScopeClass Scope = SList[SIndex];
						PutInt(Scope.DataLength);

						PutInt(Scope.NumOnChannel + 1);	// +1 for time axis
						PutString("time");
						PutString("s");
						for (int TIndex = 0; TIndex < SList[SIndex].NumOnChannel; TIndex++) {
							PutString(Scope.NthOnChannel(TIndex).TraceLabel);
							PutString(Scope.NthOnChannel(TIndex).TraceUnit);
						}


						for (int DIndex = 0; DIndex < SList[0].DataLength; DIndex++) {
							for (int TIndex = 0; TIndex < Scope.NumOnChannel; TIndex++) {
								PutFloat(Scope.NthOnChannel(TIndex).Data()[DIndex]);
							}
						}
					}

				}
			}
			catch (IOException) {
				WarningDialog WDialog = new WarningDialog("UIMSGIOEXCEPTION", " in CSVFileWriteClass.WriteFile");
			}
						
		}

		public override void Close() {

		}

		/*
		 * 


for ln := 0 to RecordLength-1 do begin
		DFile.PutFloat(XData[0,ln]);
		for ch := 0 to NumActiveTrace-1 do begin
			DFile.PutFloat(YData[ch,ln]);
		end;
	end;
		 * 
		 * 
		 */

	}
}
