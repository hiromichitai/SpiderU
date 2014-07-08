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
	public partial class AboutForm : Form {
		public AboutForm() {
			InitializeComponent();

			System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
			System.Version ver = asm.GetName().Version;
			versionLabel.Text = ver.ToString();

		}

		private void label1_Click(object sender, EventArgs e) {

		}
	}
}
