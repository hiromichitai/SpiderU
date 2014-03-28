using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpiderU {
	public partial class MainForm : Form {
		DeviceListClass DeviceList;

		public MainForm() {
			InitializeComponent();
		}

		private void exitXToolStripMenuItem_Click(object sender, EventArgs e) {
			Close();
		}

		private void scanToolStripMenuItem_Click(object sender, EventArgs e) {
			if (DeviceList == null) {
				try {
					DeviceList = new DeviceListClass();
				}
				catch (System.Exception Ex) {
					ErrorDialog Dialog = new ErrorDialog(Ex.ToString());
				}
			}
		}

		private void acquisitionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ScopeManager.GetWaveform();
		}
	}
}
