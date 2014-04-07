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
	public partial class NewScope : Form {

		private byte[] PrimaryAddressArray;
		private byte[] SecondaryAddressArray;
		private Device NewDevice;

		public NewScope() {
			InitializeComponent();
		}

		public NewScope(DeviceListClass GPIBDeviceList) {
			InitializeComponent();
			PrimaryAddressArray = new byte[GPIBDeviceList.NumFreeDevice()];
			SecondaryAddressArray = new byte[GPIBDeviceList.NumFreeDevice()];

			try {
				deviceListBox.Items.Clear();
				for (int Index = 0; Index < GPIBDeviceList.NumFreeDevice(); Index++) {
					PrimaryAddressArray[Index] = GPIBDeviceList.FreeDevicePrimaryAddress(Index);
					SecondaryAddressArray[Index] = GPIBDeviceList.FreeDeviceSecondaryAddress(Index);
					Device GPIBDevice = new Device(0, PrimaryAddressArray[Index], SecondaryAddressArray[Index]);
					GPIBDevice.Write("*IDN?");
					string IDString = GPIBDevice.ReadString();
					string ModelString = GetID(IDString);
					string DisplayString = ModelString + "(" + GPIBDevice.PrimaryAddress.ToString() + ")";
					deviceListBox.Items.Add(DisplayString);
					GPIBDevice.GoToLocal();
					GPIBDevice.Dispose();
				}
			}
			catch (NationalInstruments.NI4882.GpibException Ex) {
				ErrorDialog DialogForm = new ErrorDialog(Ex.ToString());
			}
			catch (Exception Ex) {
				ErrorDialog DialogForm = new ErrorDialog(Ex.ToString());
			}
		}

		public Device CreatedDevice() {
			return (NewDevice);
		}


		private string GetID(string IDNString) {
			if (IDNString.Contains("YOKOGAWA")) {
				if (IDNString.IndexOf("7015") == 9) { // YOKOGAWA's DL1500 series returns YOKOGAWA,7015XX 
					return "YOKOGAWA DL1500";
				}
				if (IDNString.IndexOf("7016") == 9) { // YOKOGAWA's DL1600 series returns YOKOGAWA,7016XX 
					return "YOKOGAWA DL1600";
				}
				if (IDNString.IndexOf("7017") == 9) { // YOKOGAWA's DL1700 series returns YOKOGAWA,7017XX 
					return "YOKOGAWA DL1700";
				}
				if (IDNString.IndexOf("7101") == 9) { // YOKOGAWA's DL2000 series returns YOKOGAWA,7101XX 
					return "YOKOGAWA DL2000";
				}
				if (IDNString.IndexOf("DL750") == 9) { // YOKOGAWA's DL750 series returns YOKOGAWA,DL750XX 
					return "YOKOGAWA DL750";
				}

			}
			if (IDNString.Contains("LECROY")) {
				return "LECROY";
			}
			return "Unknown";
		}

		private void OKButton_Click(object sender, EventArgs e) {
			if (deviceListBox.SelectedIndex != -1) {
				int SelectedIndex = deviceListBox.SelectedIndex;
				NewDevice = new Device(0, PrimaryAddressArray[SelectedIndex],
					SecondaryAddressArray[SelectedIndex]);
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
