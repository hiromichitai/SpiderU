namespace SpiderU {
	partial class ScopeSettingForm {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScopeSettingForm));
			this.chComboBox = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.c = new System.Windows.Forms.Label();
			this.labelTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.unitTextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.multiplierTextBox = new System.Windows.Forms.TextBox();
			this.OKButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// chComboBox
			// 
			this.chComboBox.FormattingEnabled = true;
			resources.ApplyResources(this.chComboBox, "chComboBox");
			this.chComboBox.Name = "chComboBox";
			this.chComboBox.SelectionChangeCommitted += new System.EventHandler(this.chComboBox_SelectionChangeCommitted);
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			// 
			// c
			// 
			resources.ApplyResources(this.c, "c");
			this.c.Name = "c";
			// 
			// labelTextBox
			// 
			resources.ApplyResources(this.labelTextBox, "labelTextBox");
			this.labelTextBox.Name = "labelTextBox";
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// unitTextBox
			// 
			resources.ApplyResources(this.unitTextBox, "unitTextBox");
			this.unitTextBox.Name = "unitTextBox";
			// 
			// label3
			// 
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			// 
			// multiplierTextBox
			// 
			resources.ApplyResources(this.multiplierTextBox, "multiplierTextBox");
			this.multiplierTextBox.Name = "multiplierTextBox";
			// 
			// OKButton
			// 
			this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			resources.ApplyResources(this.OKButton, "OKButton");
			this.OKButton.Name = "OKButton";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			resources.ApplyResources(this.cancelButton, "cancelButton");
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// ScopeSettingForm
			// 
			this.AcceptButton = this.OKButton;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.OKButton);
			this.Controls.Add(this.multiplierTextBox);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.unitTextBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.labelTextBox);
			this.Controls.Add(this.c);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.chComboBox);
			this.Name = "ScopeSettingForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox chComboBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label c;
		private System.Windows.Forms.TextBox labelTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox unitTextBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox multiplierTextBox;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.Button cancelButton;
	}
}