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
			autoFileNumberDigitsNumericUpDown.Value = Properties.Settings.Default.autoFileNameSerialDigits;
			autoFileNumberValueNumericUpDown.Value = Properties.Settings.Default.autoFileNameSerialNumber;
			autoFileNameSuffixTextBox.Text = Properties.Settings.Default.autoFileNamePrefix;
			includeModelNameCheckBox.Checked = Properties.Settings.Default.includeModelName;
			includeDateTimeCheckBox.Checked = Properties.Settings.Default.includeDateTime;
			headerLineCheckBox.Checked = Properties.Settings.Default.addHeader;
			autoFileNamePrefixTextBox.Enabled = useAutoFileNameCheckBox.Checked;
			autoFileNumberDigitsNumericUpDown.Enabled = useAutoFileNameCheckBox.Checked;
			autoFileNameSuffixTextBox.Enabled = useAutoFileNameCheckBox.Checked;
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
			Properties.Settings.Default.outputFileFormatID = outputFileFormatComboBox.SelectedIndex;
			Properties.Settings.Default.useAutoFileName = useAutoFileNameCheckBox.Checked;
			Properties.Settings.Default.autoFileNamePrefix = autoFileNamePrefixTextBox.Text;
			Properties.Settings.Default.autoFileNameSerialDigits = (int)autoFileNumberDigitsNumericUpDown.Value;
			Properties.Settings.Default.autoFileNameSuffix = autoFileNameSuffixTextBox.Text;
			Properties.Settings.Default.autoFileNameSerialNumber = (int)autoFileNumberValueNumericUpDown.Value;
			Properties.Settings.Default.includeModelName = includeModelNameCheckBox.Checked;
			Properties.Settings.Default.includeDateTime = includeDateTimeCheckBox.Checked;
			Properties.Settings.Default.addHeader = headerLineCheckBox.Checked;

			Properties.Settings.Default.Save();
		}

		private string GetSampleAutoFileName() {
			string digitFormat = "{0:D" + string.Format("{0,1:D}",(int)(autoFileNumberDigitsNumericUpDown.Value)) + "}";
			return autoFileNamePrefixTextBox.Text
				+ string.Format(digitFormat, (int)(autoFileNumberValueNumericUpDown.Value))
				+ autoFileNameSuffixTextBox.Text + FileWriterClass.DefaultExtention((int)FileFormat());
		}

		private void useAutoFileNameCheckBox_CheckedChanged(object sender, EventArgs e) {
			if (useAutoFileNameCheckBox.Checked) {
				autoFileNamePrefixTextBox.Enabled = true;
				autoFileNumberDigitsNumericUpDown.Enabled = true;
				autoFileNumberValueNumericUpDown.Enabled = true;
				autoFileNameSuffixTextBox.Enabled = true;
			} else {
				Properties.Settings.Default.useAutoFileName = false;
				autoFileNamePrefixTextBox.Enabled = false;
				autoFileNumberDigitsNumericUpDown.Enabled = false;
				autoFileNumberValueNumericUpDown.Enabled = false;
				autoFileNameSuffixTextBox.Enabled = false;
			}
			autoFileNameSampleLabel.Text = GetSampleAutoFileName();
		}

		private void outputFileFormatComboBox_SelectedIndexChanged(object sender, EventArgs e) {
//			Properties.Settings.Default.outputFileFormatID = (int)FileFormat();
		}

		private void includeModelNameCheckBox_CheckedChanged(object sender, EventArgs e) {
//			Properties.Settings.Default.includeModelName = includeModelNameCheckBox.Checked;
		}

		private void includeDateTimeCheckBox_CheckedChanged(object sender, EventArgs e) {
//			Properties.Settings.Default.includeDateTime = includeDateTimeCheckBox.Checked;
		}

		private void headerLineCheckBox_CheckedChanged(object sender, EventArgs e) {
//			Properties.Settings.Default.addHeader = headerLineCheckBox.Checked;
		}

		private void autoFileNumberDigitsNumericUpDown_ValueChanged(object sender, EventArgs e) {
			autoFileNameSampleLabel.Text = GetSampleAutoFileName();
		}

		private void autoFileNumberValueNumericUpDown_ValueChanged(object sender, EventArgs e) {
			autoFileNameSampleLabel.Text = GetSampleAutoFileName();

		}

		private void autoFileNameSuffixTextBox_TextChanged(object sender, EventArgs e) {
			autoFileNameSampleLabel.Text = GetSampleAutoFileName();

		}

		private void autoFileNamePrefixTextBox_TextChanged(object sender, EventArgs e) {
			autoFileNameSampleLabel.Text = GetSampleAutoFileName();

		}

	}
}
