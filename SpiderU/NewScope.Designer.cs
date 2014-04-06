namespace SpiderU {
	partial class NewScope {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewScope));
			this.deviceListBox = new System.Windows.Forms.ListBox();
			this.OKButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// deviceListBox
			// 
			resources.ApplyResources(this.deviceListBox, "deviceListBox");
			this.deviceListBox.FormattingEnabled = true;
			this.deviceListBox.Items.AddRange(new object[] {
            resources.GetString("deviceListBox.Items")});
			this.deviceListBox.Name = "deviceListBox";
			this.deviceListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
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
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// NewScope
			// 
			this.AcceptButton = this.OKButton;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.OKButton);
			this.Controls.Add(this.deviceListBox);
			this.Name = "NewScope";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox deviceListBox;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.Button cancelButton;
	}
}