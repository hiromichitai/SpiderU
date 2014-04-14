using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiderU {
	class FileWriterCreator {
		private static readonly FileWriterCreator instance = new FileWriterCreator();

		private FileWriterCreator(){
		
		}

		public static FileWriterCreator Instance {
			get {
				return instance;
			}
		}

		public static FileWriterClass CreateFileWriter(string FileName){
			switch (System.IO.Path.GetExtension(FileName).ToLower()) {
				case(".xls"):
					return new ExcelFileWriter(FileName);
				default:
					return null;
					
			}
		}

	}
}
