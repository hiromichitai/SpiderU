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
			INVALID, CSVFILE, LD1FILE, HDF5FILE
		}
		protected FileStream FStream;
		private static UTF8Encoding UTF8Encoder = new UTF8Encoding();

		protected static byte[] GetUTF8Bytes(string OriginalString) {
			try {
				return UTF8Encoder.GetBytes(OriginalString);
			}
			catch (Exception e) {
				WarningDialog WDialog = new WarningDialog("UIMessageUTF8ConvError");
				return null;
			}

		}

		public FileWriterClass(string FileName) {
			if(File.Exists(FileName)){
				WarningDialog WDialog = new WarningDialog("UIMessageUTF8ConvError");
				if (WDialog.DialogResult != DialogResult.OK) { 
					throw(new System.Exception("User abort"));
				} else {
					FStream = new FileStream(FileName,FileMode.CreateNew);
				}
			}
		}

		public abstract void WriteFile() ;
		

    }
}
