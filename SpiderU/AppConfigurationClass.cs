using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace SpiderU {
	public class AppConfigurationClass : ApplicationSettingsBase {
	 
		private static readonly AppConfigurationClass instance = new AppConfigurationClass();

	   [UserScopedSetting()]

		private static Boolean addComment;
		private static Boolean addHeader;
		private static Boolean useAutoFileName;
		private static string autoFileNamePrefix;
		private static string autoFileNameSuffix;
		private static int autoFileNameSerialDigits;
		private static int autoFileNameSerialNumber;

		private AppConfigurationClass() {

		}

		public static AppConfigurationClass Instance {
			get {
				return instance;
			}
		}


		public bool AddComment {
			get {
				return commentLineCheckBox.Checked;
			}
		}

		public string CommentLine {
			get {
				return commentLineTextBox.Text;
			}
		}

		public bool AddHeader {
			get {
				return headerLineCheckBox.Checked;
			}
		}


	}
}
