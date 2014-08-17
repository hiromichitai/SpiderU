using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace SpiderU {
	class MatFileWriterClass : FileWriterClass {
		private enum miDataType:int { 
			miINT8 = 1, 
			miUINT8 = 2,
			miINT16 = 3,
			miUINT16 = 4,
			miINT32 = 5,
			miUINT32 = 6,
			miSINGLE = 7,
			miDOUBLE = 9,
			miINT64 = 12,
			miUINT64 = 13,
			miMATRIX = 14,
			miCOMPRESSED = 15,
			miUTF8 = 16,
			miUTF16 = 17,
			miUTF32 = 18		
		}

		private enum mxArrayType : int {
			mxCELL_CLASS = 1,
			mxSTRUCT_CLASS = 2,
			mxOBJECT_CLASS = 3,
			mxCHAR_CLASS = 4,
			mxSPARSE_CLASS = 5,
			mxDOUBLE_CLASS = 6,
			mxSINGLE_CLASS = 7,
			mxINT8_CLASS = 8,
			mxUINT8_CLASS = 9,
			mxINT16_CLASS = 10
		}

		private FileStream FStream;

		private class miMatrixClass {
			private byte[] byteBuffer;

			public miMatrixClass(mxArrayType ArrayType) {
				byteBuffer = new byte[24]; // for miMATRIX tag and array flags, type
				BitConverter.GetBytes((int)miDataType.miMATRIX).CopyTo(byteBuffer, 0);
				BitConverter.GetBytes((int)miDataType.miUINT32).CopyTo(byteBuffer, 8);
				byteBuffer[16] = 0;
				byteBuffer[17] = 0;
				byteBuffer[18] = 0;		// flags clear
				byteBuffer[19] = (byte)ArrayType;
				byteBuffer[20] = 0;
				byteBuffer[21] = 0;
				byteBuffer[22] = 0;
				byteBuffer[23] = 0;
			}

			public void setDimension(int[] dimensionArray){
				int numDimensions = dimensionArray.Length;
				int numTagSize = numDimensions + (numDimensions % 2);
				
				Array.Resize(ref byteBuffer,byteBuffer.Length+numTagSize*4);
				for (int Index = 0; Index < numDimensions; Index++) {
					BitConverter.GetBytes(dimensionArray[Index]).CopyTo(byteBuffer, 24 + Index * 4);
				}
			
			}

			public void setArrayName(string arrayName) {
/*
				int numDimensions = dimensionArray.Length;
				int numTagSize = numDimensions + (numDimensions % 2);

				Array.Resize(ref byteBuffer, byteBuffer.Length + numTagSize * 4);
				for (int Index = 0; Index < numDimensions; Index++) {
					BitConverter.GetBytes(dimensionArray[Index]).CopyTo(byteBuffer, 24 + Index * 4);
				}
*/
			}
		}

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

			UTF8Encoding U8Encoder = new UTF8Encoding();
			WriteHeader();
			List<ScopeClass> SList = ScopeManager.ScopeList;

			try {
				if (Properties.Settings.Default.syncAllScope) {
					int LabelByteCount = 0;
					int UnitByteCount = 0;
					for (int SIndex = 0; SIndex < SList.Count; SIndex++) {
						List<TraceClass> TList = SList[SIndex].OnTrace;
						for (int TIndex = 0; TIndex < TList.Count; TIndex++) {
							LabelByteCount += U8Encoder.GetByteCount(TList[TIndex].TraceLabel);
							UnitByteCount += U8Encoder.GetByteCount(TList[TIndex].TraceUnit);
						}
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
