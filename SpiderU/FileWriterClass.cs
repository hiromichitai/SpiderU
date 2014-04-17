using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace SpiderU
{
    public abstract class FileWriterClass
    {
		public enum FileFormatEnum {
			INVALID = -1, CSVFILE = 0, XLSFILE = 1, HDF5FILE = 2, LD1FILE = 3
		}

		public enum EncodingEnum {
			INVALID = -1, ASCII = 0, UTF8 = 1, UNICODE = 2
		}

		public static string DefaultExtention(int FileFormatID) {
			switch(FileFormatID){
				case -1:
					return "";
				case 0:
					return ".csv";
				case 1:
					return ".xls";
				case 2:
					return ".h5";
				case 3:
					return ".ld1";
				default:
					return "";
			}
		}

		public static string EncodingString(int EncodingID) {
			switch(EncodingID){
				case 0:
					return "ASCII";
				case 1:
					return "UTF-8";
				case 2:
					return "Unicode";
				default:
					return "";

			}
		}

		public System.Text.Encoding GetEncoding() {
			switch (Properties.Settings.Default.outputEncodingID) {
				case((int)EncodingEnum.ASCII):
					return new System.Text.ASCIIEncoding();
				case((int)EncodingEnum.UTF8):
					return new System.Text.UTF8Encoding();
				case((int)EncodingEnum.UNICODE):
					return new System.Text.UnicodeEncoding();
				default:
					return null;
			}
		}

		public static string ExtFilter() {
			return "csv files (*.csv)|*.csv|Excel files (*.xls;*.xlsx)|*.xls;*.xlsx|HDF5 files (*.h5)|*.h5|LD1 file (*.ld1)|*.ld1| All files (*.*)|*.* ";
		}


		public FileWriterClass(string FileName) {
			if(File.Exists(FileName)){
				WarningDialog WDialog = new WarningDialog("UIMessageUTF8ConvError");
				if (WDialog.DialogResult != DialogResult.OK) { 
					throw(new System.Exception("User abort"));
				}
			}
			if (Properties.Settings.Default.syncAllScope) {
				if (!ScopeManager.AllScopeSyncable()) {
					WarningDialog WDialog = new WarningDialog("UIMSGNOTSYNCABLE", true);
					return;
				}

			}

		}

		public abstract void WriteFile();

		public abstract void Close();
		

    }
}
