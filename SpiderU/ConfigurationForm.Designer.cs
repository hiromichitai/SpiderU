namespace SpiderU {
	partial class ConfigurationForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigurationForm));
			this.OKButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.outputFileFormatComboBox = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.label2 = new System.Windows.Forms.Label();
			this.useAutoFileNameCheckBox = new System.Windows.Forms.CheckBox();
			this.label11 = new System.Windows.Forms.Label();
			this.autoFileNamePrefixInitialValueTextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.autoFileNamePrefixTextBox = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.autoFileNumberDigitsNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.label10 = new System.Windows.Forms.Label();
			this.autoFileNameSuffixTextBox = new System.Windows.Forms.TextBox();
			this.autoFileNumberValueNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.autoFileNameSampleLabel = new System.Windows.Forms.Label();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.addCommentLineCheckBox = new System.Windows.Forms.CheckBox();
			this.label7 = new System.Windows.Forms.Label();
			this.includeModelNameCheckBox = new System.Windows.Forms.CheckBox();
			this.includeDateTimeCheckBox = new System.Windows.Forms.CheckBox();
			this.label8 = new System.Windows.Forms.Label();
			this.headerLineCheckBox = new System.Windows.Forms.CheckBox();
			this.label6 = new System.Windows.Forms.Label();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.syncAllScopeCheckBox = new System.Windows.Forms.CheckBox();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.autoFileNumberDigitsNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.autoFileNumberValueNumericUpDown)).BeginInit();
			this.tabPage3.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.tabPage4.SuspendLayout();
			this.SuspendLayout();
			// 
			// OKButton
			// 
			resources.ApplyResources(this.OKButton, "OKButton");
			this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKButton.Name = "OKButton";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// cancelButton
			// 
			resources.ApplyResources(this.cancelButton, "cancelButton");
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// tabControl1
			// 
			resources.ApplyResources(this.tabControl1, "tabControl1");
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.outputFileFormatComboBox);
			this.tabPage1.Controls.Add(this.label1);
			resources.ApplyResources(this.tabPage1, "tabPage1");
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// outputFileFormatComboBox
			// 
			resources.ApplyResources(this.outputFileFormatComboBox, "outputFileFormatComboBox");
			this.outputFileFormatComboBox.FormattingEnabled = true;
			this.outputFileFormatComboBox.Name = "outputFileFormatComboBox";
			this.outputFileFormatComboBox.SelectedIndexChanged += new System.EventHandler(this.outputFileFormatComboBox_SelectedIndexChanged);
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.tableLayoutPanel2);
			resources.ApplyResources(this.tabPage2, "tabPage2");
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel2
			// 
			resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
			this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.useAutoFileNameCheckBox, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.label11, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.autoFileNamePrefixInitialValueTextBox, 1, 1);
			this.tableLayoutPanel2.Controls.Add(this.label3, 0, 2);
			this.tableLayoutPanel2.Controls.Add(this.autoFileNamePrefixTextBox, 1, 2);
			this.tableLayoutPanel2.Controls.Add(this.label4, 0, 3);
			this.tableLayoutPanel2.Controls.Add(this.autoFileNumberDigitsNumericUpDown, 1, 3);
			this.tableLayoutPanel2.Controls.Add(this.label10, 0, 4);
			this.tableLayoutPanel2.Controls.Add(this.autoFileNameSuffixTextBox, 1, 5);
			this.tableLayoutPanel2.Controls.Add(this.autoFileNumberValueNumericUpDown, 1, 4);
			this.tableLayoutPanel2.Controls.Add(this.label5, 0, 5);
			this.tableLayoutPanel2.Controls.Add(this.label9, 0, 6);
			this.tableLayoutPanel2.Controls.Add(this.autoFileNameSampleLabel, 1, 6);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			// 
			// useAutoFileNameCheckBox
			// 
			resources.ApplyResources(this.useAutoFileNameCheckBox, "useAutoFileNameCheckBox");
			this.useAutoFileNameCheckBox.Checked = global::SpiderU.Properties.Settings.Default.useAutoFileName;
			this.useAutoFileNameCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.useAutoFileNameCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::SpiderU.Properties.Settings.Default, "useAutoFileName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.useAutoFileNameCheckBox.Name = "useAutoFileNameCheckBox";
			this.useAutoFileNameCheckBox.UseVisualStyleBackColor = true;
			this.useAutoFileNameCheckBox.CheckedChanged += new System.EventHandler(this.useAutoFileNameCheckBox_CheckedChanged);
			// 
			// label11
			// 
			resources.ApplyResources(this.label11, "label11");
			this.label11.Name = "label11";
			// 
			// autoFileNamePrefixInitialValueTextBox
			// 
			resources.ApplyResources(this.autoFileNamePrefixInitialValueTextBox, "autoFileNamePrefixInitialValueTextBox");
			this.autoFileNamePrefixInitialValueTextBox.Name = "autoFileNamePrefixInitialValueTextBox";
			this.autoFileNamePrefixInitialValueTextBox.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.autoFileNamePrefixInitialValueTextBox_HelpRequested);
			// 
			// label3
			// 
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			// 
			// autoFileNamePrefixTextBox
			// 
			this.autoFileNamePrefixTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::SpiderU.Properties.Settings.Default, "autoFileNamePrefix", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			resources.ApplyResources(this.autoFileNamePrefixTextBox, "autoFileNamePrefixTextBox");
			this.autoFileNamePrefixTextBox.Name = "autoFileNamePrefixTextBox";
			this.autoFileNamePrefixTextBox.Text = global::SpiderU.Properties.Settings.Default.autoFileNamePrefix;
			// 
			// label4
			// 
			resources.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			this.label4.Click += new System.EventHandler(this.label4_Click);
			// 
			// autoFileNumberDigitsNumericUpDown
			// 
			resources.ApplyResources(this.autoFileNumberDigitsNumericUpDown, "autoFileNumberDigitsNumericUpDown");
			this.autoFileNumberDigitsNumericUpDown.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
			this.autoFileNumberDigitsNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.autoFileNumberDigitsNumericUpDown.Name = "autoFileNumberDigitsNumericUpDown";
			this.autoFileNumberDigitsNumericUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
			this.autoFileNumberDigitsNumericUpDown.ValueChanged += new System.EventHandler(this.autoFileNumberDigitsNumericUpDown_ValueChanged_1);
			// 
			// label10
			// 
			resources.ApplyResources(this.label10, "label10");
			this.label10.Name = "label10";
			// 
			// autoFileNameSuffixTextBox
			// 
			this.autoFileNameSuffixTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::SpiderU.Properties.Settings.Default, "autoFileNameSuffix", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			resources.ApplyResources(this.autoFileNameSuffixTextBox, "autoFileNameSuffixTextBox");
			this.autoFileNameSuffixTextBox.Name = "autoFileNameSuffixTextBox";
			this.autoFileNameSuffixTextBox.Text = global::SpiderU.Properties.Settings.Default.autoFileNameSuffix;
			// 
			// autoFileNumberValueNumericUpDown
			// 
			resources.ApplyResources(this.autoFileNumberValueNumericUpDown, "autoFileNumberValueNumericUpDown");
			this.autoFileNumberValueNumericUpDown.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
			this.autoFileNumberValueNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.autoFileNumberValueNumericUpDown.Name = "autoFileNumberValueNumericUpDown";
			this.autoFileNumberValueNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label5
			// 
			resources.ApplyResources(this.label5, "label5");
			this.label5.Name = "label5";
			// 
			// label9
			// 
			resources.ApplyResources(this.label9, "label9");
			this.label9.Name = "label9";
			// 
			// autoFileNameSampleLabel
			// 
			resources.ApplyResources(this.autoFileNameSampleLabel, "autoFileNameSampleLabel");
			this.autoFileNameSampleLabel.Name = "autoFileNameSampleLabel";
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.tableLayoutPanel1);
			resources.ApplyResources(this.tabPage3, "tabPage3");
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel1
			// 
			resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
			this.tableLayoutPanel1.Controls.Add(this.addCommentLineCheckBox, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.label7, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.includeModelNameCheckBox, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.includeDateTimeCheckBox, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.label8, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.headerLineCheckBox, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.label6, 0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			// 
			// addCommentLineCheckBox
			// 
			resources.ApplyResources(this.addCommentLineCheckBox, "addCommentLineCheckBox");
			this.addCommentLineCheckBox.Checked = global::SpiderU.Properties.Settings.Default.addComment;
			this.addCommentLineCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.addCommentLineCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::SpiderU.Properties.Settings.Default, "addComment", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.addCommentLineCheckBox.Name = "addCommentLineCheckBox";
			this.addCommentLineCheckBox.UseVisualStyleBackColor = true;
			this.addCommentLineCheckBox.CheckedChanged += new System.EventHandler(this.commentLineCheckBox_CheckedChanged);
			// 
			// label7
			// 
			resources.ApplyResources(this.label7, "label7");
			this.label7.Name = "label7";
			// 
			// includeModelNameCheckBox
			// 
			resources.ApplyResources(this.includeModelNameCheckBox, "includeModelNameCheckBox");
			this.includeModelNameCheckBox.Checked = global::SpiderU.Properties.Settings.Default.includeModelName;
			this.includeModelNameCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.includeModelNameCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::SpiderU.Properties.Settings.Default, "includeModelName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.includeModelNameCheckBox.Name = "includeModelNameCheckBox";
			this.includeModelNameCheckBox.UseVisualStyleBackColor = true;
			// 
			// includeDateTimeCheckBox
			// 
			resources.ApplyResources(this.includeDateTimeCheckBox, "includeDateTimeCheckBox");
			this.includeDateTimeCheckBox.Checked = global::SpiderU.Properties.Settings.Default.includeDateTime;
			this.includeDateTimeCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.includeDateTimeCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::SpiderU.Properties.Settings.Default, "includeDateTime", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.includeDateTimeCheckBox.Name = "includeDateTimeCheckBox";
			this.includeDateTimeCheckBox.UseVisualStyleBackColor = true;
			// 
			// label8
			// 
			resources.ApplyResources(this.label8, "label8");
			this.label8.Name = "label8";
			// 
			// headerLineCheckBox
			// 
			resources.ApplyResources(this.headerLineCheckBox, "headerLineCheckBox");
			this.headerLineCheckBox.Checked = global::SpiderU.Properties.Settings.Default.addHeader;
			this.headerLineCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.headerLineCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::SpiderU.Properties.Settings.Default, "addHeader", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.headerLineCheckBox.Name = "headerLineCheckBox";
			this.headerLineCheckBox.UseVisualStyleBackColor = true;
			// 
			// label6
			// 
			resources.ApplyResources(this.label6, "label6");
			this.label6.Name = "label6";
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.syncAllScopeCheckBox);
			resources.ApplyResources(this.tabPage4, "tabPage4");
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.UseVisualStyleBackColor = true;
			// 
			// syncAllScopeCheckBox
			// 
			resources.ApplyResources(this.syncAllScopeCheckBox, "syncAllScopeCheckBox");
			this.syncAllScopeCheckBox.Checked = global::SpiderU.Properties.Settings.Default.syncAllScope;
			this.syncAllScopeCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.syncAllScopeCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::SpiderU.Properties.Settings.Default, "syncAllScope", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.syncAllScopeCheckBox.Name = "syncAllScopeCheckBox";
			this.syncAllScopeCheckBox.UseVisualStyleBackColor = true;
			// 
			// ConfigurationForm
			// 
			this.AcceptButton = this.OKButton;
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.CancelButton = this.cancelButton;
			resources.ApplyResources(this, "$this");
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.OKButton);
			this.HelpButton = true;
			this.Name = "ConfigurationForm";
			this.TopMost = true;
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.autoFileNumberDigitsNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.autoFileNumberValueNumericUpDown)).EndInit();
			this.tabPage3.ResumeLayout(false);
			this.tabPage3.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tabPage4.ResumeLayout(false);
			this.tabPage4.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.ComboBox outputFileFormatComboBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.CheckBox addCommentLineCheckBox;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.CheckBox includeModelNameCheckBox;
		private System.Windows.Forms.CheckBox includeDateTimeCheckBox;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.CheckBox headerLineCheckBox;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox useAutoFileNameCheckBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox autoFileNamePrefixTextBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NumericUpDown autoFileNumberDigitsNumericUpDown;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.NumericUpDown autoFileNumberValueNumericUpDown;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox autoFileNameSuffixTextBox;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox autoFileNamePrefixInitialValueTextBox;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label autoFileNameSampleLabel;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.CheckBox syncAllScopeCheckBox;
	}
}