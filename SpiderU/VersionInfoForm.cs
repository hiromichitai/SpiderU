using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace SpiderU {
	public partial class VersionInfoForm : Form {
		public VersionInfoForm() {
			InitializeComponent();


			Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
			
			object[] objCopyright =
			  asm.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
			if ((objCopyright != null) && (objCopyright.Length > 0)) {
				copyrightLabel.Text = 
				  ((AssemblyCopyrightAttribute)objCopyright[0]).Copyright;
			}
			
			System.Version ver = asm.GetName().Version;
			versionLabel.Text = ver.ToString();


		}

		private void label3_Click(object sender, EventArgs e) {

		}


	}
}
