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
/*
			try {
				List<ScopeClass> SList = ScopeManager.ScopeList;
				for (int SIndex = 0; SIndex < SList.Count; SIndex++) {
					ScopeClass Scope = SList[SIndex];
					sheet.Name = Scope.ScopeID;
					int Row = 1;
					if (SpiderU.Properties.Settings.Default.addComment) {
					for (int ChIndex = 0; ChIndex < Scope.NumOnChannel; ChIndex++) {
						((Excel.Range)sheet.Cells[Row, 1]).Value = Scope.ScopeTitle;
					}
					Row++;
				}

				if (SpiderU.Properties.Settings.Default.addHeader) {
					((Excel.Range)sheet.Cells[Row, 1]).Value = "Time(s)";

					for (int ChIndex = 0; ChIndex < Scope.NumOnChannel; ChIndex++) {
						((Excel.Range)sheet.Cells[Row, ChIndex+2]).Value = Scope.NthOnChannel(ChIndex).TraceLabel + "(" + Scope.NthOnChannel(ChIndex).TraceUnit + ")";
					}
					Row++;
				}

				for (int RIndex = 0; RIndex < Scope.DataLength; RIndex++) {
					((Excel.Range)sheet.Cells[RIndex+Row,1]).Value = Scope.STime(RIndex);
					for(int ChIndex = 0; ChIndex < Scope.NumOnChannel; ChIndex++){
						((Excel.Range)sheet.Cells[RIndex+Row,ChIndex+2]).Value = Scope.NthOnChannel(ChIndex).Data()[RIndex];
					}
				}
			}
			wkbk.Save();
			}
*/
		}

		public override void Close(){
		}

	}
}
