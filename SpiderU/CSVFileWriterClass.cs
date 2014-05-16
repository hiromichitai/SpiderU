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
						SWriter.Write(SList[0].Comment);
						for (int SIndex = 1; SIndex < SList.Count; SIndex++) {
							SWriter.Write("," + SList[SIndex].Comment);
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
					double STime = 0.0;
					for (int DIndex = 0; DIndex < SList[0].DataLength; DIndex++) {
						SWriter.Write(string.Format("{0:G}", STime));
						STime += SList[0].SampleTime;
						for (int SIndex = 0; SIndex < SList.Count; SIndex++) {
							ScopeClass Scope = SList[SIndex];
							for (int TIndex = 0; TIndex < Scope.NumOnChannel; TIndex++) {
								SWriter.Write(",");
								SWriter.Write(string.Format("{0:G}", Scope.NthOnChannel(TIndex).Data()[DIndex]));
							}
						}
						SWriter.WriteLine();
					}
				} else {
					for (int SIndex = 1; SIndex < SList.Count; SIndex++) {
						ScopeClass Scope = SList[SIndex];

						if (Properties.Settings.Default.addComment) {
							SWriter.WriteLine(Scope.Comment);
						}

						if (Properties.Settings.Default.addHeader) {
							SWriter.Write("Time(s),");
							for (int TIndex = 0; TIndex < Scope.NumOnChannel; TIndex++) {
								SWriter.Write(Scope.NthOnChannel(TIndex).TraceLabel + "(" + Scope.NthOnChannel(TIndex).TraceUnit + ")");
							}
						}
						SWriter.WriteLine();

						double STime = 0.0;
						for (int DIndex = 0; DIndex < Scope.DataLength; DIndex++) {
							SWriter.Write(string.Format("{0:G}",STime));
							STime += Scope.SampleTime;
							for (int TIndex = 0; TIndex < Scope.NumOnChannel; TIndex++) {
								SWriter.Write(",");
								SWriter.Write(string.Format("{0:G}", Scope.NthOnChannel(TIndex).Data()[DIndex]));
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
