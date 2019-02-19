﻿namespace Sander.DirLister.UI.App
{
	partial class ProgressForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgressForm));
			this.ProgressBar = new System.Windows.Forms.ProgressBar();
			this.ProgressLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// ProgressBar
			// 
			this.ProgressBar.BackColor = System.Drawing.SystemColors.ButtonShadow;
			this.ProgressBar.ForeColor = System.Drawing.Color.Black;
			this.ProgressBar.Location = new System.Drawing.Point(12, 44);
			this.ProgressBar.Name = "ProgressBar";
			this.ProgressBar.Size = new System.Drawing.Size(387, 12);
			this.ProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.ProgressBar.TabIndex = 0;
			// 
			// ProgressLabel
			// 
			this.ProgressLabel.AutoSize = true;
			this.ProgressLabel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.ProgressLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ProgressLabel.ForeColor = System.Drawing.Color.White;
			this.ProgressLabel.Location = new System.Drawing.Point(12, 13);
			this.ProgressLabel.Name = "ProgressLabel";
			this.ProgressLabel.Size = new System.Drawing.Size(74, 20);
			this.ProgressLabel.TabIndex = 1;
			this.ProgressLabel.Text = "Waiting...";
			// 
			// ProgressForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.ClientSize = new System.Drawing.Size(411, 68);
			this.Controls.Add(this.ProgressLabel);
			this.Controls.Add(this.ProgressBar);
			this.Cursor = System.Windows.Forms.Cursors.AppStarting;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ProgressForm";
			this.Opacity = 0.8D;
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "ProgressForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		internal System.Windows.Forms.ProgressBar ProgressBar;
		private System.Windows.Forms.Label ProgressLabel;
	}
}