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
	public partial class MainForm : Form {
		DeviceListClass DeviceList;
		ResourceManager rm;

		public MainForm() {
			InitializeComponent();
			Properties.Settings.Default.autoFileNameSerialNumber = 1;	// reset serial number
			Properties.Settings.Default.autoFileNamePrefix = DateTime.Now.ToString("yyMMdd-");
			Properties.Settings.Default.Save();
			saveFileDialog1.Filter = FileWriterClass.ExtFilter();

			rm = new ResourceManager("SpiderU.UIMessageResoure", typeof(MainForm).Assembly);
			
		}

		private void exitXToolStripMenuItem_Click(object sender, EventArgs e) {
			Properties.Settings.Default.Save();
			Close();
		}

		private void scopeSettingToolStripMenuItem_Click(object sender, EventArgs e) {
			string ScopeID = sender.ToString();
			ScopeClass Scope = ScopeManager.GetScopeFromID(ScopeID);
			ScopeSettingForm SettingForm = new ScopeSettingForm(Scope);
		}

		private void scanToolStripMenuItem_Click(object sender, EventArgs e) {
			if (DeviceList == null) {
				try {
					DeviceList = new DeviceListClass();
					NewScopeForm NScopeForm = new NewScopeForm(DeviceList);
					if (NScopeForm.ShowDialog() == DialogResult.OK) {
						 ScopeClass NewScope = ScopeManager.CreateNewScope(NScopeForm.CreatedDevice());
						 ToolStripItem newScopeSettingItem = new ToolStripMenuItem();
						 newScopeSettingItem.Text = NewScope.ScopeID;
						 newScopeSettingItem.Click +=  scopeSettingToolStripMenuItem_Click;
						 scopeToolStripMenuItem.DropDownItems.Add((newScopeSettingItem));
					}
				}
				catch (System.Exception Ex) {
					ErrorDialog Dialog = new ErrorDialog(Ex.ToString());
				}
			}
		}

		private string getAutoFileName() {
			string FileName = Properties.Settings.Default.autoFileNamePrefix;
			FileName += Properties.Settings.Default.autoFileNameSerialNumber.ToString("D"+Properties.Settings.Default.autoFileNameSerialDigits.ToString("D"));
			FileName += Properties.Settings.Default.autoFileNameSuffix;
			return FileName;
		}

		private void acquisitionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ScopeManager.GetWaveform();
			string FileName = "";
			if (Properties.Settings.Default.useAutoFileName) {
				FileName =getAutoFileName();
			}
			saveFileDialog1.DefaultExt = FileWriterClass.DefaultExtention(Properties.Settings.Default.outputFileFormatID);
			saveFileDialog1.FileName = FileName;
			if (saveFileDialog1.ShowDialog() == DialogResult.OK) {
				FileWriterClass fWriter = FileWriterCreator.CreateFileWriter(saveFileDialog1.FileName);
				fWriter.WriteFile();
				
			}

		}

		private void settingToolStripMenuItem_Click(object sender, EventArgs e) {
			ConfigurationForm CForm = new ConfigurationForm();
			CForm.ShowDialog();
		}
	}
}
