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
		private ComPortListClass ComPortList;
		private ResourceManager rm;
		private string saveDialogInitialDir = "";

		public MainForm() {
			InitializeComponent();
			Properties.Settings.Default.autoFileNameSerialNumber = 1;	// reset serial number
			try {
				Properties.Settings.Default.autoFileNamePrefix = DateTime.Now.ToString(Properties.Settings.Default.autoFileNamePrefixInitialValue);
			}
			catch (System.FormatException) {
				Properties.Settings.Default.autoFileNamePrefix = Properties.Settings.Default.autoFileNamePrefixInitialValue;
			}

			Properties.Settings.Default.Save();
			saveFileDialog1.Filter = FileWriterClass.ExtFilter();
			rm = new ResourceManager("SpiderU.UIMessageResource", typeof(MainForm).Assembly);
		}

		private string GetUIString(string KeyString) {
			return rm.GetString(KeyString);
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

		private async void scanToolStripMenuItem_Click(object sender, EventArgs e) {
			if (ComPortList == null) {
				ComPortList = new ComPortListClass();
			}
			try {
				toolStripStatusLabel1.Text = GetUIString("UIMSGSCANSCOPE");

				if (ComPortList == null) {
					ErrorDialog EDialog = new ErrorDialog("UIMSGNOSCOPE");
					return;
				}
				NewScopeForm NScopeForm = new NewScopeForm(ComPortList);
				if (NScopeForm.ShowDialog() == DialogResult.OK) {
					ComPortList.UseDevice(NScopeForm.CreatedDevice());
					ScopeClass NewScope = ScopeManager.CreateNewScope(NScopeForm.CreatedDevice());
					ToolStripItem newScopeSettingItem = new ToolStripMenuItem();
					newScopeSettingItem.Text = NewScope.ID;
					newScopeSettingItem.Click += scopeSettingToolStripMenuItem_Click;
					scopeToolStripMenuItem.DropDownItems.Add((newScopeSettingItem));
					PictureBox NewScopePictureBox = new PictureBox();
					NewScopePictureBox.Width = flowLayoutPanel1.Width / ScopeManager.ScopeList.Count;
					NewScopePictureBox.Height = flowLayoutPanel1.Height;
					flowLayoutPanel1.Controls.Add(NewScopePictureBox);
					foreach (PictureBox ScopePictureBox in flowLayoutPanel1.Controls) {
						ScopePictureBox.Width = flowLayoutPanel1.Width / flowLayoutPanel1.Controls.Count;
					}
					NewScopePictureBox.Paint += NewScope.DrawScope;
					foreach (PictureBox ScopePictureBox in flowLayoutPanel1.Controls) {
						ScopePictureBox.Refresh();
					}

				}
				toolStripStatusLabel1.Text = GetUIString("UIMSGREADYSTATE");
			}
			catch (System.Exception Ex) {
				ErrorDialog Dialog = new ErrorDialog(Ex.ToString());
			}
		}


		private async void acquisitionToolStripMenuItem_Click(object sender, EventArgs e) {
			toolStripStatusLabel1.Text = GetUIString("UIMSGCAPTURESTATE");
			bool GWResult = await ScopeManager.GetWaveform();
			if (GWResult) {
				foreach (PictureBox ScopePictureBox in flowLayoutPanel1.Controls) {
					ScopePictureBox.Refresh();
				}
				toolStripStatusLabel1.Text = GetUIString("UIMSGWRITESTATE");
				string FileName = "";
				if (Properties.Settings.Default.useAutoFileName) {
					FileName = FileWriterCreator.autoFileName;
				}
				saveFileDialog1.FilterIndex = Properties.Settings.Default.outputFileFormatID+1;		// FilterIndex starts from 1, not 0
				saveFileDialog1.FileName = FileName;
				saveFileDialog1.InitialDirectory = saveDialogInitialDir;
				if (saveFileDialog1.ShowDialog() == DialogResult.OK) {
					FileWriterClass fWriter = FileWriterCreator.CreateFileWriter(saveFileDialog1.FileName);
					fWriter.WriteFile();
					fWriter.Close();
					if (Properties.Settings.Default.useAutoFileName) {
						FileWriterCreator.incAutoFileNameNumber();
					}
					saveDialogInitialDir = System.IO.Path.GetDirectoryName(saveFileDialog1.FileName);
				}
			} else {
				WarningDialog WDialog = new WarningDialog("UIMSGNOSCOPECONNECT"," in acquisitionToolStripMenuItem_Click");
			}
			toolStripStatusLabel1.Text = GetUIString("UIMSGREADYSTATE");
		}

		private void settingToolStripMenuItem_Click(object sender, EventArgs e) {
			ConfigurationForm CForm = new ConfigurationForm();
			CForm.ShowDialog();
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
			VersionInfoForm AForm = new VersionInfoForm();
			AForm.ShowDialog();
		}

		private void showHelp() {
			ResourceManager rm = new ResourceManager("SpiderU.FileNames", typeof(MainForm).Assembly);
			string HelpURI = rm.GetString("HELPFILE");
			Help.ShowHelp(this, HelpURI);
		}


		private void showHelpToolStripMenuItem_Click(object sender, EventArgs e) {
			showHelp();
		}

		private void MainForm_HelpRequested(object sender, HelpEventArgs hlpevent) {
			showHelp();
		}


	}
}
