using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;


namespace SpiderU {
	public class ExcelFileWriter : FileWriterClass {
		private Excel.Workbook wkbk;
		private Excel.Worksheet sheet;
		private Excel.Application excelApp;
		
		public ExcelFileWriter(string FileName) : base(FileName){
			List<ScopeClass> SList = ScopeManager.ScopeList;
			for (int SIndex = 0; SIndex < SList.Count; SIndex++) {
				ScopeClass Scope = SList[SIndex];
				if(Scope.DataLength > 1048576){ // Check if record length is larger than maximu rownumber for Excel2010
					WarningDialog WDialog = new WarningDialog("UIMSGEXCELRANGEOVER",true);
					return;				
				}
			}		
			excelApp = new Excel.Application();
	        wkbk = excelApp.Workbooks.Add();
			wkbk.SaveAs(FileName);
		}

		public override void WriteFile(){
			List<ScopeClass> SList = ScopeManager.ScopeList;
			for (int SIndex = 0; SIndex < SList.Count; SIndex++) {
				ScopeClass Scope = SList[SIndex];
				sheet = wkbk.Sheets.Add() as Excel.Worksheet;
				sheet.Name = Scope.ScopeID;
				int Row = 1;
				if (SpiderU.Properties.Settings.Default.addComment) {
					for (int ChIndex = 0; ChIndex < Scope.NumOnChannel; ChIndex++) {
						((Excel.Range)sheet.Cells[Row, 1]).Value = Scope.ScopeTitle;
					}
					Row++;
				}

				if (SpiderU.Properties.Settings.Default.addHeader) {
					for (int ChIndex = 0; ChIndex < Scope.NumOnChannel; ChIndex++) {
						((Excel.Range)sheet.Cells[Row, ChIndex]).Value = Scope.NthOnChannel(ChIndex).TraceLabel + "(" + Scope.NthOnChannel(ChIndex).TraceUnit + ")";
					}
					Row++;
				}

				for (int RIndex = 0; RIndex < Scope.DataLength; RIndex++) {
					for(int ChIndex = 0; ChIndex < Scope.NumOnChannel; ChIndex++){
						((Excel.Range)sheet.Cells[RIndex+Row,ChIndex]).Value = Scope.NthOnChannel(ChIndex).Data()[RIndex];
					}
				}
			}
			wkbk.Save();
		}
 	
	}

}
