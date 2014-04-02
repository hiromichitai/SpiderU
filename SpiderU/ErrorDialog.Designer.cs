namespace SpiderU {
	partial class ErrorDialog {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErrorDialog));
			this.messageTextBox = new System.Windows.Forms.TextBox();
			this.OKButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// messageTextBox
			// 
			resources.ApplyResources(this.messageTextBox, "messageTextBox");
			this.messageTextBox.Name = "messageTextBox";
			// 
			// OKButton
			// 
			resources.ApplyResources(this.OKButton, "OKButton");
			this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKButton.Name = "OKButton";
			this.OKButton.UseVisualStyleBackColor = true;
			// 
			// ErrorDialog
			// 
			this.AcceptButton = this.OKButton;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ControlBox = false;
			this.Controls.Add(this.OKButton);
			this.Controls.Add(this.messageTextBox);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ErrorDialog";
			this.TopMost = true;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox messageTextBox;
		private System.Windows.Forms.Button OKButton;
	}
}