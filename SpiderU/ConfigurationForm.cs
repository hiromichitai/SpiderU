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
			InitializeForm();
		}

		private void InitializeForm(){
			outputFileFormatComboBox.SelectedIndex = Properties.Settings.Default.outputFileFormatID;
			useAutoFileNameCheckBox.Checked = Properties.Settings.Default.useAutoFileName;
			autoFileNamePrefixTextBox.Text = Properties.Settings.Default.autoFileNamePrefix;
			autoFileNamePrefixTextBox.Enabled = useAutoFileNameCheckBox.Checked;
			autoFileNumberDigitsNumericUpDown.Value = Properties.Settings.Default.autoFileNameSerialDigits;
			autoFileNumberDigitsNumericUpDown.Enabled = useAutoFileNameCheckBox.Checked;
			autoFileNameSuffixTextBox.Text = Properties.Settings.Default.autoFileNamePrefix;
			autoFileNameSuffixTextBox.Enabled = useAutoFileNameCheckBox.Checked;
			includeModelNameCheckBox.Checked = Properties.Settings.Default.includeModelName;
			includeDateTimeCheckBox.Checked = Properties.Settings.Default.includeDateTime;
			headerLineCheckBox.Checked = Properties.Settings.Default.addHeader;

		}

		private FileWriterClass.FileFormatEnum FileFormat() {
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

		private string GetSampleAutoFileName() {
			return autoFileNamePrefixTextBox.Text
				+ string.Format(autoFileNumberDigitsNumericUpDown.Value.ToString("D"), 1)
				+ autoFileNameSuffixTextBox.Text + FileWriterClass.DefaultExtention((int)FileFormat());

		}

		private void useAutoFileNameCheckBox_CheckedChanged(object sender, EventArgs e) {
			if (useAutoFileNameCheckBox.Checked) {
				Properties.Settings.Default.useAutoFileName = true;
				Properties.Settings.Default.autoFileNamePrefix = autoFileNamePrefixTextBox.Text;
				Properties.Settings.Default.autoFileNameSerialDigits = (int)autoFileNumberDigitsNumericUpDown.Value;
				Properties.Settings.Default.autoFileNamePrefix = autoFileNameSuffixTextBox.Text;
				autoFileNameSampleLabel.Text = GetSampleAutoFileName();
			} else {
				Properties.Settings.Default.useAutoFileName = false;
				autoFileNamePrefixTextBox.Enabled = false;
				autoFileNumberDigitsNumericUpDown.Enabled = false;
				autoFileNameSuffixTextBox.Enabled = false;
				autoFileNameSampleLabel.Text = "";
			}
		}

		private void outputFileFormatComboBox_SelectedIndexChanged(object sender, EventArgs e) {
			Properties.Settings.Default.outputFileFormatID = (int)FileFormat();
		}

		private void includeModelNameCheckBox_CheckedChanged(object sender, EventArgs e) {
			Properties.Settings.Default.includeModelName = includeModelNameCheckBox.Checked;

		}

		private void includeDateTimeCheckBox_CheckedChanged(object sender, EventArgs e) {
			Properties.Settings.Default.includeDateTime = includeDateTimeCheckBox.Checked;

		}

		private void headerLineCheckBox_CheckedChanged(object sender, EventArgs e) {
			Properties.Settings.Default.addHeader = headerLineCheckBox.Checked;

		}

	}
}
