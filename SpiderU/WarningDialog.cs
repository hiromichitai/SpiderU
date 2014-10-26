using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Resources;

namespace SpiderU {
	public partial class WarningDialog : Form {
		public WarningDialog() {
			InitializeComponent();
		}

		public WarningDialog(string messageString) {
			InitializeComponent();
			messageTextBox.Text = messageString;
			ActiveControl = OKButton;
			ShowDialog();
		}

		public WarningDialog(string messageString,Boolean doConvert) {
			InitializeComponent();
			if (doConvert) {
				ResourceManager rm = new ResourceManager("SpiderU.UIMessageResource",typeof(WarningDialog).Assembly);
				messageString = rm.GetString(messageString);
			}
			messageTextBox.Text = messageString;
			ActiveControl = OKButton;
			ShowDialog();
		}

		public WarningDialog(string messageString1,string messageString2) {
			InitializeComponent();
			ResourceManager rm = new ResourceManager("SpiderU.UIMessageResource", typeof(WarningDialog).Assembly);
			if (messageString1 != "") {
				string rString = rm.GetString(messageString1);
				if (rString != null) {
					messageString1 = rString;
				}
			}
			if (messageString2 != "") {
				string rString = rm.GetString(messageString2);
				if (rString != null) {
					messageString2 = rString;
				}
			}
			messageTextBox.Text = messageString1 + " " + messageString2;
			ActiveControl = OKButton;
			ShowDialog();
		}

	}
}
