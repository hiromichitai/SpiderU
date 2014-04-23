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
	public partial class ConfigurationForm : Form {
		public ConfigurationForm() {
			InitializeComponent();
		}

		public FileWriterClass.FileFormatEnum FileFormat() {
			switch (outputFileFormatComboBox.SelectedIndex) {
				case(-1) :
					return FileWriterClass.FileFormatEnum.INVALID;
				case(0):
					return FileWriterClass.FileFormatEnum.CSVFILE;
				case (1):
					return FileWriterClass.FileFormatEnum.LD1FILE;
				case (2):
					return FileWriterClass.FileFormatEnum.HDF5FILE;
			}
			return FileWriterClass.FileFormatEnum.INVALID;
		}


		private void OKButton_Click(object sender, EventArgs e) {
			Properties.Settings.Default.Save();
		}

		private void useAutoFileNameCheckBox_CheckedChanged(object sender, EventArgs e) {
			if (useAutoFileNameCheckBox.Checked) {

			}
		}

	}
}
