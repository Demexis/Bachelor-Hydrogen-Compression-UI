
namespace Bachelor_Project_Hydrogen_Compression_WinForms
{
    partial class DeviceConnectionForm
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
            this.comboBox_PortsList = new System.Windows.Forms.ComboBox();
            this.button_RefreshPorts = new System.Windows.Forms.Button();
            this.button_ConnectToPort = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox_PortsList
            // 
            this.comboBox_PortsList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_PortsList.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox_PortsList.FormattingEnabled = true;
            this.comboBox_PortsList.Location = new System.Drawing.Point(5, 6);
            this.comboBox_PortsList.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox_PortsList.Name = "comboBox_PortsList";
            this.comboBox_PortsList.Size = new System.Drawing.Size(195, 45);
            this.comboBox_PortsList.TabIndex = 5;
            this.comboBox_PortsList.SelectedIndexChanged += new System.EventHandler(this.comboBox_PortsList_SelectedIndexChanged);
            // 
            // button_RefreshPorts
            // 
            this.button_RefreshPorts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_RefreshPorts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(91)))), ((int)(((byte)(122)))));
            this.button_RefreshPorts.FlatAppearance.BorderSize = 0;
            this.button_RefreshPorts.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(61)))), ((int)(((byte)(82)))));
            this.button_RefreshPorts.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(111)))), ((int)(((byte)(162)))));
            this.button_RefreshPorts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_RefreshPorts.ForeColor = System.Drawing.Color.Gainsboro;
            this.button_RefreshPorts.Location = new System.Drawing.Point(208, 4);
            this.button_RefreshPorts.Margin = new System.Windows.Forms.Padding(4);
            this.button_RefreshPorts.Name = "button_RefreshPorts";
            this.button_RefreshPorts.Size = new System.Drawing.Size(100, 47);
            this.button_RefreshPorts.TabIndex = 4;
            this.button_RefreshPorts.Text = "Refresh Ports";
            this.button_RefreshPorts.UseVisualStyleBackColor = false;
            this.button_RefreshPorts.Click += new System.EventHandler(this.button_RefreshPorts_Click);
            // 
            // button_ConnectToPort
            // 
            this.button_ConnectToPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_ConnectToPort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(91)))), ((int)(((byte)(122)))));
            this.button_ConnectToPort.FlatAppearance.BorderSize = 0;
            this.button_ConnectToPort.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(61)))), ((int)(((byte)(82)))));
            this.button_ConnectToPort.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(111)))), ((int)(((byte)(162)))));
            this.button_ConnectToPort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_ConnectToPort.ForeColor = System.Drawing.Color.Gainsboro;
            this.button_ConnectToPort.Location = new System.Drawing.Point(5, 59);
            this.button_ConnectToPort.Margin = new System.Windows.Forms.Padding(4);
            this.button_ConnectToPort.Name = "button_ConnectToPort";
            this.button_ConnectToPort.Size = new System.Drawing.Size(303, 47);
            this.button_ConnectToPort.TabIndex = 3;
            this.button_ConnectToPort.Text = "Connect";
            this.button_ConnectToPort.UseVisualStyleBackColor = false;
            this.button_ConnectToPort.Click += new System.EventHandler(this.button_ConnectToPort_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.panel1.Controls.Add(this.button_RefreshPorts);
            this.panel1.Controls.Add(this.comboBox_PortsList);
            this.panel1.Controls.Add(this.button_ConnectToPort);
            this.panel1.Location = new System.Drawing.Point(195, 171);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(312, 112);
            this.panel1.TabIndex = 6;
            // 
            // DeviceConnectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(684, 461);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DeviceConnectionForm";
            this.Text = "DeviceControlForm";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_PortsList;
        private System.Windows.Forms.Button button_RefreshPorts;
        private System.Windows.Forms.Button button_ConnectToPort;
        private System.Windows.Forms.Panel panel1;
    }
}