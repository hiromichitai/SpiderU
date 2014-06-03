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
	public partial class ScopeSettingForm : Form {

		private int NumOnChannel;
		private int[] ChannelNumber;
		private ScopeClass TargetScope;
		private string commentString;
		private string[] UnitString;
		private string[] LabelString;
		private double[] Multiplier;
		private int comboBoxPreSelectionIndex;

		public ScopeSettingForm() {
			InitializeComponent();
		}

		private void refreshForm(){
			commentTextBox.Text = commentString;
			if (chComboBox.SelectedIndex != -1) {
				int chIndex = chComboBox.SelectedIndex;
				labelTextBox.Text = LabelString[chIndex];
				unitTextBox.Text = UnitString[chIndex];
				multiplierTextBox.Text = Multiplier[chIndex].ToString();
			}
		}

		private void storeChangedValue(){
			commentString = commentTextBox.Text;
			LabelString[comboBoxPreSelectionIndex] = labelTextBox.Text;
			UnitString[comboBoxPreSelectionIndex] = unitTextBox.Text;
			Multiplier[comboBoxPreSelectionIndex] = Convert.ToDouble(multiplierTextBox.Text);
		}

		public ScopeSettingForm(ScopeClass Scope) {
			InitializeComponent();
			TargetScope = Scope;

			NumOnChannel = Scope.OnTrace.Count;
			ChannelNumber = new int[NumOnChannel];
			UnitString = new string[NumOnChannel];
			LabelString = new string[NumOnChannel];
			Multiplier = new double[NumOnChannel];

			for(int OnTraceIndex = 0; OnTraceIndex < NumOnChannel; OnTraceIndex++ ){
				ChannelNumber[OnTraceIndex] = Scope.OnTrace[OnTraceIndex].ChannelNo;
				LabelString[OnTraceIndex] = Scope.OnTrace[OnTraceIndex].TraceLabel;
				UnitString[OnTraceIndex] = Scope.OnTrace[OnTraceIndex].TraceUnit;
				Multiplier[OnTraceIndex] = Scope.OnTrace[OnTraceIndex].Multiplier;
				chComboBox.Items.Add(string.Format("Ch{0:D1}",ChannelNumber[OnTraceIndex]));
			}
			chComboBox.SelectedIndex = 0;
			comboBoxPreSelectionIndex = 0;
			refreshForm();
			ShowDialog();

		}


		private void chComboBox_SelectionChangeCommitted(object sender, EventArgs e)
		{
			storeChangedValue();
			comboBoxPreSelectionIndex = chComboBox.SelectedIndex;

			refreshForm();
		}

		private void OKButton_Click(object sender, EventArgs e)
		{
			storeChangedValue();
			TargetScope.Comment = commentString;
			for (int OnTraceIndex = 0; OnTraceIndex < NumOnChannel; OnTraceIndex++) {
				TargetScope.OnTrace[OnTraceIndex].TraceLabel = LabelString[OnTraceIndex];
				TargetScope.OnTrace[OnTraceIndex].TraceUnit = UnitString[OnTraceIndex];
				TargetScope.OnTrace[OnTraceIndex].Multiplier = Multiplier[OnTraceIndex];
			}

			DialogResult = DialogResult.OK;
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}
	}
}
