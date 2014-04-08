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
	public partial class ErrorDialog : Form {
		public ErrorDialog() {
			InitializeComponent();
		}

		public ErrorDialog(string messageString) {
			InitializeComponent();
			messageTextBox.Text = messageString;
			ShowDialog();
		}

		public ErrorDialog(string messageString, Boolean doConvert) {
			InitializeComponent();
			if (doConvert) {
				ResourceManager rm = new ResourceManager("SpiderU.UIMessageResource",typeof(WarningDialog).Assembly);
				messageString = rm.GetString(messageString);
			}
			messageTextBox.Text = messageString;
			ShowDialog();
		}


	}
}
