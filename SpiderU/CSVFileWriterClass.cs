using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SpiderU {
	class CSVFileWriterClass : FileWriterClass {
		private FileStream FStream;
		private StreamWriter SWriter;

		public CSVFileWriterClass(string FileName) : base(FileName){
			try {
				FStream = new FileStream(FileName, FileMode.Create);
				System.Text.Encoding FEncoding = GetEncoding();
				SWriter = new StreamWriter(FStream, FEncoding);

			}
			catch (ArgumentException) {
				WarningDialog WDialog = new WarningDialog("UIMSGARGEXCEPTION","in CSVFileWriter");
			}
			catch (System.Security.SecurityException) {
				WarningDialog WDialog = new WarningDialog("UIMSGSECURITYEXCEPTION", "in CSVFileWriter");
			}
			catch (PathTooLongException) {
				WarningDialog WDialog = new WarningDialog("UIMSGIOEXCEPTION", FileName+" in CSVFileWriter");
			}
			catch (IOException) {
				WarningDialog WDialog = new WarningDialog("UIMSGIOEXCEPTION", "in CSVFileWriter");
			}
		}

		public override void WriteFile(){

			List<ScopeClass> SList = ScopeManager.ScopeList;

			try { 
			if (Properties.Settings.Default.syncAllScope) {

				if (Properties.Settings.Default.addComment) {
					SWriter.Write(SList[0].ScopeTitle);
					for (int SIndex = 1; SIndex < SList.Count; SIndex++) {
						SWriter.Write("," + SList[SIndex].ScopeTitle);
					}
					SWriter.WriteLine();
				}

				if (Properties.Settings.Default.addHeader) {
					SWriter.Write("Time(s),");
					for (int SIndex = 0; SIndex < SList.Count; SIndex++) {
						ScopeClass Scope = SList[SIndex];
						for (int TIndex = 0; TIndex < Scope.NumOnChannel; TIndex++) {
							SWriter.Write(Scope.NthOnChannel(TIndex).TraceLabel + "(" + Scope.NthOnChannel(TIndex).TraceUnit + ")");

						}
					}
					SWriter.WriteLine();					
				}
				for (int DIndex = 0; DIndex < SList[0].DataLength; DIndex++) {
					for (int SIndex = 0; SIndex < SList.Count; SIndex++) {
						ScopeClass Scope = SList[SIndex];
						for (int TIndex = 0; TIndex < Scope.NumOnChannel; TIndex++) {
							if ((SIndex != 0) || (TIndex != 0)) {	// then not the first column
								SWriter.Write(",");
							}
							SWriter.Write(string.Format("%f",Scope.NthOnChannel(TIndex).Data()[DIndex]));
						}
					}
					SWriter.WriteLine();	
				}
			} else {
				for (int SIndex = 1; SIndex < SList.Count; SIndex++) {
					ScopeClass Scope = SList[SIndex];

					if (Properties.Settings.Default.addComment) {
						SWriter.WriteLine(Scope.ScopeTitle);
					}

					if (Properties.Settings.Default.addHeader) {
						SWriter.Write("Time(s),");
						for (int TIndex = 0; TIndex < Scope.NumOnChannel; TIndex++) {
							SWriter.Write(Scope.NthOnChannel(TIndex).TraceLabel + "(" + Scope.NthOnChannel(TIndex).TraceUnit + ")");
						}
					}
					SWriter.WriteLine();					

					for (int DIndex = 0; DIndex < SList[0].DataLength; DIndex++) {
						for (int TIndex = 0; TIndex < Scope.NumOnChannel; TIndex++) {
							if (TIndex != 0) {	// then not the first column
								SWriter.Write(",");
							}
							SWriter.Write(string.Format("%f",Scope.NthOnChannel(TIndex).Data()[DIndex]));
						}
					}
					SWriter.WriteLine();	
				}
			}
			}
			catch (IOException) {
				WarningDialog WDialog = new WarningDialog("UIMSGIOEXCEPTION", " in CSVFileWriteClass.WriteFile");
			}
		}

		public override void Close(){
			SWriter.Close();
		}

	}
}
