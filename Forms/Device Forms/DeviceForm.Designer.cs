
namespace Bachelor_Project_Hydrogen_Compression_WinForms
{
    partial class DeviceForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.controlPanel1 = new Bachelor_Project_Hydrogen_Compression_WinForms.UserControls.ControlPanel.ControlPanel();
            this.comboBox_CyclogramList = new System.Windows.Forms.ComboBox();
            this.cyclogram1 = new Bachelor_Project_Hydrogen_Compression_WinForms.Cyclogram();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.compressorDevice1 = new Bachelor_Project_Hydrogen_Compression_WinForms.UserControls.Device.CompressorDevice();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer4);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(523, 567);
            this.splitContainer1.SplitterDistance = 468;
            this.splitContainer1.TabIndex = 7;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.splitContainer5);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.cyclogram1);
            this.splitContainer4.Size = new System.Drawing.Size(468, 567);
            this.splitContainer4.SplitterDistance = 48;
            this.splitContainer4.TabIndex = 2;
            // 
            // splitContainer5
            // 
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer5.Location = new System.Drawing.Point(0, 0);
            this.splitContainer5.Name = "splitContainer5";
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.controlPanel1);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.comboBox_CyclogramList);
            this.splitContainer5.Size = new System.Drawing.Size(468, 48);
            this.splitContainer5.SplitterDistance = 232;
            this.splitContainer5.TabIndex = 1;
            // 
            // controlPanel1
            // 
            this.controlPanel1.BackColor = System.Drawing.Color.Transparent;
            this.controlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlPanel1.Location = new System.Drawing.Point(0, 0);
            this.controlPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.controlPanel1.Name = "controlPanel1";
            this.controlPanel1.Size = new System.Drawing.Size(232, 48);
            this.controlPanel1.TabIndex = 0;
            // 
            // comboBox_CyclogramList
            // 
            this.comboBox_CyclogramList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.comboBox_CyclogramList.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox_CyclogramList.FormattingEnabled = true;
            this.comboBox_CyclogramList.ItemHeight = 37;
            this.comboBox_CyclogramList.Location = new System.Drawing.Point(0, 3);
            this.comboBox_CyclogramList.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox_CyclogramList.Name = "comboBox_CyclogramList";
            this.comboBox_CyclogramList.Size = new System.Drawing.Size(232, 45);
            this.comboBox_CyclogramList.TabIndex = 6;
            // 
            // cyclogram1
            // 
            this.cyclogram1.Active = false;
            this.cyclogram1.CurrentTimeStamp = 0;
            this.cyclogram1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cyclogram1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cyclogram1.Location = new System.Drawing.Point(0, 0);
            this.cyclogram1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cyclogram1.Name = "cyclogram1";
            this.cyclogram1.Size = new System.Drawing.Size(468, 515);
            this.cyclogram1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitContainer2.Size = new System.Drawing.Size(51, 567);
            this.splitContainer2.SplitterDistance = 118;
            this.splitContainer2.TabIndex = 7;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.compressorDevice1);
            this.splitContainer3.Size = new System.Drawing.Size(912, 567);
            this.splitContainer3.SplitterDistance = 523;
            this.splitContainer3.TabIndex = 9;
            // 
            // compressorDevice1
            // 
            this.compressorDevice1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.compressorDevice1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.compressorDevice1.Location = new System.Drawing.Point(0, 0);
            this.compressorDevice1.Name = "compressorDevice1";
            this.compressorDevice1.Size = new System.Drawing.Size(385, 567);
            this.compressorDevice1.TabIndex = 8;
            // 
            // DeviceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(912, 567);
            this.Controls.Add(this.splitContainer3);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DeviceForm";
            this.Text = "DeviceForm";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
            this.splitContainer5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Cyclogram cyclogram1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private UserControls.Device.CompressorDevice compressorDevice1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private UserControls.ControlPanel.ControlPanel controlPanel1;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private System.Windows.Forms.ComboBox comboBox_CyclogramList;
    }
}