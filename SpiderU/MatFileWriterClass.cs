using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace SpiderU {
	class MatFileWriterClass : FileWriterClass {
		private FileStream FStream;

		public MatFileWriterClass(string FileName)
			: base(FileName) {
			try {
				FStream = new FileStream(FileName, FileMode.Create);

			}
			catch (ArgumentException) {
				WarningDialog WDialog = new WarningDialog("UIMSGARGEXCEPTION", "in MatFileWriter");
			}
			catch (System.Security.SecurityException) {
				WarningDialog WDialog = new WarningDialog("UIMSGSECURITYEXCEPTION", "in MatFileWriter");
			}
			catch (PathTooLongException) {
				WarningDialog WDialog = new WarningDialog("UIMSGIOEXCEPTION", FileName + " in MatFileWriter");
			}
			catch (IOException) {
				WarningDialog WDialog = new WarningDialog("UIMSGIOEXCEPTION", "in MatFileWriter");
			}
		}

		private void WriteHeader(){
			const string HeaderString = "MAT-file 5.0 Created by SpiderU";
			byte[] HeaderBuffer = new byte[128];	// MAT-file level 5 uses 128 byte length header
			for(int i = 0; i < HeaderBuffer.Length; i++){
				HeaderBuffer[i] = 0;
			}
			ASCIIEncoding AEncoder = new ASCIIEncoding();
			AEncoder.GetBytes(HeaderString,0,HeaderString.Length,HeaderBuffer,0);
			HeaderBuffer[124] = 0x00;
			HeaderBuffer[125] = 0x01;
			AEncoder.GetBytes("MI", 0, 2, HeaderBuffer, 126);

			FStream.Write(HeaderBuffer, 0, 128);

		}

		public override void WriteFile() {

			WriteHeader();
			List<ScopeClass> SList = ScopeManager.ScopeList;

			try {
				if (Properties.Settings.Default.syncAllScope) {

					if (Properties.Settings.Default.addComment) {
					}

					if (Properties.Settings.Default.addHeader) {
					}
					double STime = 0.0;
					for (int DIndex = 0; DIndex < SList[0].DataLength; DIndex++) {
					}
				} else {
					for (int SIndex = 1; SIndex < SList.Count; SIndex++) {
						ScopeClass Scope = SList[SIndex];

						if (Properties.Settings.Default.addComment) {
						}

						if (Properties.Settings.Default.addHeader) {
						}

						double STime = 0.0;
						for (int DIndex = 0; DIndex < Scope.DataLength; DIndex++) {
						}
					}
				}
			}
			catch (IOException) {
				WarningDialog WDialog = new WarningDialog("UIMSGIOEXCEPTION", " in CSVFileWriteClass.WriteFile");
			}
		}

		public override void Close() {
			FStream.Close();
		}


	}

}
