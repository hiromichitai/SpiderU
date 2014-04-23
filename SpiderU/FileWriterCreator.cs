﻿using System;
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

		public static string autoFileName {
			get {
				string FileName = Properties.Settings.Default.autoFileNamePrefix;
				FileName += Properties.Settings.Default.autoFileNameSerialNumber.ToString("D" + Properties.Settings.Default.autoFileNameSerialDigits.ToString("D"));
				FileName += Properties.Settings.Default.autoFileNameSuffix;
				return FileName;
			}
		}

		public static void incAutoFileNameNumber() {
			Properties.Settings.Default.autoFileNameSerialNumber++;
		}


		public static FileWriterClass CreateFileWriter(string FileName){
			switch (System.IO.Path.GetExtension(FileName).ToLower()) {
				case(".xls"):
					return new ExcelFileWriterClass(FileName);
				default:
					return null;
					
			}
		}

	}
}
