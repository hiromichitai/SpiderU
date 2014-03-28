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
	public partial class ScopeSetting : Form {

		private int NumChannel;
		private int[] ChannelNumber;
		private ScopeClass TargetScope;
		private string[] UnitString;
		private string[] LabelString;
		private double[] MultiplyValue;
		private int CurrentChannelIndex;

		public ScopeSetting() {
			InitializeComponent();
		}
	}
}
