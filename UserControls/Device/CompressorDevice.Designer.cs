﻿
namespace Bachelor_Project.UserControls.Device
{
    partial class CompressorDevice
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // CompressorDevice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.Name = "CompressorDevice";
            this.Size = new System.Drawing.Size(400, 400);
            this.Load += new System.EventHandler(this.CompressorDevice_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.CompressorDevice_Paint);
            this.Resize += new System.EventHandler(this.CompressorDevice_Resize);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
