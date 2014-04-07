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

		private int NumChannel;
		private int[] ChannelNumber;
		private ScopeClass TargetScope;
		private string[] UnitString;
		private string[] LabelString;
		private double[] Multiplier;
		private int CurrentChannelIndex;
		private int comboBoxPreSelectionIndex;

		public ScopeSettingForm() {
			InitializeComponent();
		}

		private void refreshForm(){
			if (chComboBox.SelectedIndex != -1) {
				int chIndex = chComboBox.SelectedIndex;
				labelTextBox.Text = LabelString[chIndex];
				unitTextBox.Text = UnitString[chIndex];
				multiplierTextBox.Text = Multiplier[chIndex].ToString();
			}
		}

		private void storeChangedValue(){
			LabelString[comboBoxPreSelectionIndex] = labelTextBox.Text;
			UnitString[comboBoxPreSelectionIndex] = unitTextBox.Text;
			Multiplier[comboBoxPreSelectionIndex] = Convert.ToDouble(multiplierTextBox.Text);
		}

		public ScopeSettingForm(ScopeClass Scope) {
			InitializeComponent();
			TargetScope = Scope;

			NumChannel = Scope.NumOnChannel;
			ChannelNumber = new int[NumChannel];
			UnitString = new string[NumChannel];
			LabelString = new string[NumChannel];
			Multiplier = new double[NumChannel];

			for(int chIndex = 0; chIndex < NumChannel; chIndex++ ){
				ChannelNumber[chIndex] = Scope.NthOnChannelNumber(chIndex);
				LabelString[chIndex] = Scope.ChannelLabel(ChannelNumber[chIndex]);
				UnitString[chIndex] = Scope.ChannelUnit(ChannelNumber[chIndex]);
				Multiplier[chIndex] = Scope.ChannelMultiplier(ChannelNumber[chIndex]);
				chComboBox.Items.Add(string.Format("Ch{0:D1}",ChannelNumber[chIndex]));
			}
			chComboBox.SelectedIndex = 0;
			comboBoxPreSelectionIndex = 0;
			refreshForm();
			ShowDialog();

		}


		private void chComboBox_SelectionChangeCommitted(object sender, EventArgs e)
		{
			storeChangedValue();
			refreshForm();
		}

		private void OKButton_Click(object sender, EventArgs e)
		{
			storeChangedValue();
			for(int chIndex = 0; chIndex < NumChannel; chIndex++ ){
				TraceClass trace = TargetScope.Channel[ChannelNumber[chIndex]];
				trace.TraceLabel = LabelString[chIndex];
				trace.TraceUnit = UnitString[chIndex];
				trace.Multiplier = Multiplier[chIndex];
			}
			DialogResult = DialogResult.OK;
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		
		}
	}
}
