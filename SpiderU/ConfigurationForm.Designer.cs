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
			this.autoFileNameSuffixTextBox = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.autoFileNamePrefixTextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.useAutoFileNameCheckBox = new System.Windows.Forms.CheckBox();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.label7 = new System.Windows.Forms.Label();
			this.headerLineCheckBox = new System.Windows.Forms.CheckBox();
			this.label8 = new System.Windows.Forms.Label();
			this.includeDateTimeCheckBox = new System.Windows.Forms.CheckBox();
			this.includeModelNameCheckBox = new System.Windows.Forms.CheckBox();
			this.commentLineCheckBox = new System.Windows.Forms.CheckBox();
			this.label6 = new System.Windows.Forms.Label();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			this.tabPage3.SuspendLayout();
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
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			// 
			// tabPage1
			// 
			resources.ApplyResources(this.tabPage1, "tabPage1");
			this.tabPage1.Controls.Add(this.outputFileFormatComboBox);
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// outputFileFormatComboBox
			// 
			resources.ApplyResources(this.outputFileFormatComboBox, "outputFileFormatComboBox");
			this.outputFileFormatComboBox.FormattingEnabled = true;
			this.outputFileFormatComboBox.Items.AddRange(new object[] {
            resources.GetString("outputFileFormatComboBox.Items"),
            resources.GetString("outputFileFormatComboBox.Items1"),
            resources.GetString("outputFileFormatComboBox.Items2"),
            resources.GetString("outputFileFormatComboBox.Items3")});
			this.outputFileFormatComboBox.Name = "outputFileFormatComboBox";
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// tabPage2
			// 
			resources.ApplyResources(this.tabPage2, "tabPage2");
			this.tabPage2.Controls.Add(this.autoFileNameSuffixTextBox);
			this.tabPage2.Controls.Add(this.label5);
			this.tabPage2.Controls.Add(this.numericUpDown1);
			this.tabPage2.Controls.Add(this.label4);
			this.tabPage2.Controls.Add(this.autoFileNamePrefixTextBox);
			this.tabPage2.Controls.Add(this.label3);
			this.tabPage2.Controls.Add(this.label2);
			this.tabPage2.Controls.Add(this.useAutoFileNameCheckBox);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// autoFileNameSuffixTextBox
			// 
			resources.ApplyResources(this.autoFileNameSuffixTextBox, "autoFileNameSuffixTextBox");
			this.autoFileNameSuffixTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::SpiderU.Properties.Settings.Default, "autoFileNameSuffix", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.autoFileNameSuffixTextBox.Name = "autoFileNameSuffixTextBox";
			this.autoFileNameSuffixTextBox.Text = global::SpiderU.Properties.Settings.Default.autoFileNameSuffix;
			// 
			// label5
			// 
			resources.ApplyResources(this.label5, "label5");
			this.label5.Name = "label5";
			// 
			// numericUpDown1
			// 
			resources.ApplyResources(this.numericUpDown1, "numericUpDown1");
			this.numericUpDown1.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
			this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
			// 
			// label4
			// 
			resources.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			// 
			// autoFileNamePrefixTextBox
			// 
			resources.ApplyResources(this.autoFileNamePrefixTextBox, "autoFileNamePrefixTextBox");
			this.autoFileNamePrefixTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::SpiderU.Properties.Settings.Default, "autoFileNamePrefix", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.autoFileNamePrefixTextBox.Name = "autoFileNamePrefixTextBox";
			this.autoFileNamePrefixTextBox.Text = global::SpiderU.Properties.Settings.Default.autoFileNamePrefix;
			// 
			// label3
			// 
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
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
			// 
			// tabPage3
			// 
			resources.ApplyResources(this.tabPage3, "tabPage3");
			this.tabPage3.Controls.Add(this.label7);
			this.tabPage3.Controls.Add(this.headerLineCheckBox);
			this.tabPage3.Controls.Add(this.label8);
			this.tabPage3.Controls.Add(this.includeDateTimeCheckBox);
			this.tabPage3.Controls.Add(this.includeModelNameCheckBox);
			this.tabPage3.Controls.Add(this.commentLineCheckBox);
			this.tabPage3.Controls.Add(this.label6);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// label7
			// 
			resources.ApplyResources(this.label7, "label7");
			this.label7.Name = "label7";
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
			// label8
			// 
			resources.ApplyResources(this.label8, "label8");
			this.label8.Name = "label8";
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
			// includeModelNameCheckBox
			// 
			resources.ApplyResources(this.includeModelNameCheckBox, "includeModelNameCheckBox");
			this.includeModelNameCheckBox.Checked = global::SpiderU.Properties.Settings.Default.includeModelName;
			this.includeModelNameCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.includeModelNameCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::SpiderU.Properties.Settings.Default, "includeModelName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.includeModelNameCheckBox.Name = "includeModelNameCheckBox";
			this.includeModelNameCheckBox.UseVisualStyleBackColor = true;
			// 
			// commentLineCheckBox
			// 
			resources.ApplyResources(this.commentLineCheckBox, "commentLineCheckBox");
			this.commentLineCheckBox.Checked = global::SpiderU.Properties.Settings.Default.addComment;
			this.commentLineCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.commentLineCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::SpiderU.Properties.Settings.Default, "addComment", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.commentLineCheckBox.Name = "commentLineCheckBox";
			this.commentLineCheckBox.UseVisualStyleBackColor = true;
			// 
			// label6
			// 
			resources.ApplyResources(this.label6, "label6");
			this.label6.Name = "label6";
			// 
			// ConfigurationForm
			// 
			this.AcceptButton = this.OKButton;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.OKButton);
			this.Name = "ConfigurationForm";
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			this.tabPage3.ResumeLayout(false);
			this.tabPage3.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.ComboBox outputFileFormatComboBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TextBox autoFileNameSuffixTextBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox autoFileNamePrefixTextBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox useAutoFileNameCheckBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.CheckBox headerLineCheckBox;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.CheckBox includeDateTimeCheckBox;
		private System.Windows.Forms.CheckBox includeModelNameCheckBox;
		private System.Windows.Forms.CheckBox commentLineCheckBox;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
	}
}