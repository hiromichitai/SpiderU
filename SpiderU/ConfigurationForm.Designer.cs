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
			this.label1 = new System.Windows.Forms.Label();
			this.outputFileFormatComboBox = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.useAutoFileNameCheckBox = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.autoFileNamePrefixTextBox = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.autoFileNameSuffixTextBox = new System.Windows.Forms.TextBox();
			this.OKButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.commentLineCheckBox = new System.Windows.Forms.CheckBox();
			this.label7 = new System.Windows.Forms.Label();
			this.commentLineTextBox = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.headerLineCheckBox = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// outputFileFormatComboBox
			// 
			this.outputFileFormatComboBox.FormattingEnabled = true;
			this.outputFileFormatComboBox.Items.AddRange(new object[] {
            resources.GetString("outputFileFormatComboBox.Items"),
            resources.GetString("outputFileFormatComboBox.Items1")});
			resources.ApplyResources(this.outputFileFormatComboBox, "outputFileFormatComboBox");
			this.outputFileFormatComboBox.Name = "outputFileFormatComboBox";
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			// 
			// useAutoFileNameCheckBox
			// 
			resources.ApplyResources(this.useAutoFileNameCheckBox, "useAutoFileNameCheckBox");
			this.useAutoFileNameCheckBox.Name = "useAutoFileNameCheckBox";
			this.useAutoFileNameCheckBox.UseVisualStyleBackColor = true;
			// 
			// label3
			// 
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			// 
			// autoFileNamePrefixTextBox
			// 
			resources.ApplyResources(this.autoFileNamePrefixTextBox, "autoFileNamePrefixTextBox");
			this.autoFileNamePrefixTextBox.Name = "autoFileNamePrefixTextBox";
			// 
			// label4
			// 
			resources.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
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
			// label5
			// 
			resources.ApplyResources(this.label5, "label5");
			this.label5.Name = "label5";
			// 
			// autoFileNameSuffixTextBox
			// 
			resources.ApplyResources(this.autoFileNameSuffixTextBox, "autoFileNameSuffixTextBox");
			this.autoFileNameSuffixTextBox.Name = "autoFileNameSuffixTextBox";
			// 
			// OKButton
			// 
			this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			resources.ApplyResources(this.OKButton, "OKButton");
			this.OKButton.Name = "OKButton";
			this.OKButton.UseVisualStyleBackColor = true;
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			resources.ApplyResources(this.cancelButton, "cancelButton");
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// label6
			// 
			resources.ApplyResources(this.label6, "label6");
			this.label6.Name = "label6";
			// 
			// commentLineCheckBox
			// 
			resources.ApplyResources(this.commentLineCheckBox, "commentLineCheckBox");
			this.commentLineCheckBox.Name = "commentLineCheckBox";
			this.commentLineCheckBox.UseVisualStyleBackColor = true;
			// 
			// label7
			// 
			resources.ApplyResources(this.label7, "label7");
			this.label7.Name = "label7";
			// 
			// commentLineTextBox
			// 
			resources.ApplyResources(this.commentLineTextBox, "commentLineTextBox");
			this.commentLineTextBox.Name = "commentLineTextBox";
			// 
			// label8
			// 
			resources.ApplyResources(this.label8, "label8");
			this.label8.Name = "label8";
			// 
			// headerLineCheckBox
			// 
			resources.ApplyResources(this.headerLineCheckBox, "headerLineCheckBox");
			this.headerLineCheckBox.Checked = true;
			this.headerLineCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.headerLineCheckBox.Name = "headerLineCheckBox";
			this.headerLineCheckBox.UseVisualStyleBackColor = true;
			// 
			// ConfigurationForm
			// 
			this.AcceptButton = this.OKButton;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.Controls.Add(this.headerLineCheckBox);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.commentLineTextBox);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.commentLineCheckBox);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.OKButton);
			this.Controls.Add(this.autoFileNameSuffixTextBox);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.numericUpDown1);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.autoFileNamePrefixTextBox);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.useAutoFileNameCheckBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.outputFileFormatComboBox);
			this.Controls.Add(this.label1);
			this.Name = "ConfigurationForm";
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox outputFileFormatComboBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox useAutoFileNameCheckBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox autoFileNamePrefixTextBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox autoFileNameSuffixTextBox;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.CheckBox commentLineCheckBox;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox commentLineTextBox;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.CheckBox headerLineCheckBox;
	}
}