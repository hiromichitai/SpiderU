namespace SpiderU {
	partial class WarningDialog {
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
			this.messageTextBox = new System.Windows.Forms.TextBox();
			this.okButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// messageTextBox
			// 
			this.messageTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.messageTextBox.Location = new System.Drawing.Point(61, 37);
			this.messageTextBox.Multiline = true;
			this.messageTextBox.Name = "messageTextBox";
			this.messageTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.messageTextBox.Size = new System.Drawing.Size(276, 155);
			this.messageTextBox.TabIndex = 0;
			this.messageTextBox.UseWaitCursor = true;
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.okButton.Location = new System.Drawing.Point(166, 220);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 1;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			// 
			// WarningDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(419, 267);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.messageTextBox);
			this.Name = "WarningDialog";
			this.Text = "Warning";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox messageTextBox;
		private System.Windows.Forms.Button okButton;
	}
}