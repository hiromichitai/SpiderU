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
		ConfigurationForm CForm;

		public MainForm() {
			InitializeComponent();
			rm = new ResourceManager("SpiderU.UIMessageResoure",typeof(MainForm).Assembly);
			CForm = new ConfigurationForm();
			
		}

		private void exitXToolStripMenuItem_Click(object sender, EventArgs e) {
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

		private void acquisitionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ScopeManager.GetWaveform();

		}

		private void settingToolStripMenuItem_Click(object sender, EventArgs e) {
			CForm.ShowDialog();
		}
	}
}
