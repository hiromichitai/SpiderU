using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NationalInstruments.NI4882;

namespace SpiderU {
	public partial class NewScopeForm : Form {

		private ComPortListClass ComPortList;
		private ComPortClass NewDevice;

		public NewScopeForm() {
			InitializeComponent();
		}

		public NewScopeForm(ComPortListClass MyComPortList) {
			InitializeComponent();
			ComPortList = MyComPortList;

			try {
				deviceListBox.Items.Clear();
				for (int Index = 0; Index < ComPortList.NumFreeDevice(); Index++) {
					ComPortClass ComPort = ComPortList[Index];
					ComPort.InitializeComPort();
					deviceListBox.Items.Add(ComPort.IDString);
					ComPort.Close();
				}
			}
			catch (NationalInstruments.NI4882.GpibException Ex) {
				ErrorDialog DialogForm = new ErrorDialog(Ex.ToString());
			}
			catch (Exception Ex) {
				ErrorDialog DialogForm = new ErrorDialog(Ex.ToString());
			}
		}

		public ComPortClass CreatedDevice() {
			return (NewDevice);
		}


		private void OKButton_Click(object sender, EventArgs e) {
			if (deviceListBox.SelectedIndex != -1) {
				int SelectedIndex = deviceListBox.SelectedIndex;
				NewDevice = ComPortList[SelectedIndex];
				DialogResult = DialogResult.OK;
			} else {
				DialogResult = DialogResult.Cancel;
			}
			Close();

		}

		private void cancelButton_Click(object sender, EventArgs e) {
			DialogResult = DialogResult.Cancel;
			Close();

		}


	}
}
