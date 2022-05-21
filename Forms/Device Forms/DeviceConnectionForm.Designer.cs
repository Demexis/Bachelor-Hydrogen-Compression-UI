
namespace Bachelor_Project
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
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBox_SensorSets = new System.Windows.Forms.ComboBox();
            this.comboBox_DeviceSchemes = new System.Windows.Forms.ComboBox();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.checkBox_SimulationMode = new System.Windows.Forms.CheckBox();
            this.checkBox_GenerateReadings = new System.Windows.Forms.CheckBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer6 = new System.Windows.Forms.SplitContainer();
            this.splitContainer7 = new System.Windows.Forms.SplitContainer();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).BeginInit();
            this.splitContainer6.Panel2.SuspendLayout();
            this.splitContainer6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer7)).BeginInit();
            this.splitContainer7.Panel1.SuspendLayout();
            this.splitContainer7.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox_PortsList
            // 
            this.comboBox_PortsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox_PortsList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_PortsList.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox_PortsList.FormattingEnabled = true;
            this.comboBox_PortsList.ItemHeight = 37;
            this.comboBox_PortsList.Location = new System.Drawing.Point(0, 0);
            this.comboBox_PortsList.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox_PortsList.Name = "comboBox_PortsList";
            this.comboBox_PortsList.Size = new System.Drawing.Size(350, 45);
            this.comboBox_PortsList.TabIndex = 5;
            this.comboBox_PortsList.SelectedIndexChanged += new System.EventHandler(this.comboBox_PortsList_SelectedIndexChanged);
            // 
            // button_RefreshPorts
            // 
            this.button_RefreshPorts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(91)))), ((int)(((byte)(122)))));
            this.button_RefreshPorts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_RefreshPorts.FlatAppearance.BorderSize = 0;
            this.button_RefreshPorts.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(61)))), ((int)(((byte)(82)))));
            this.button_RefreshPorts.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(111)))), ((int)(((byte)(162)))));
            this.button_RefreshPorts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_RefreshPorts.ForeColor = System.Drawing.Color.Gainsboro;
            this.button_RefreshPorts.Location = new System.Drawing.Point(0, 0);
            this.button_RefreshPorts.Margin = new System.Windows.Forms.Padding(4);
            this.button_RefreshPorts.Name = "button_RefreshPorts";
            this.button_RefreshPorts.Size = new System.Drawing.Size(172, 45);
            this.button_RefreshPorts.TabIndex = 4;
            this.button_RefreshPorts.Text = "Refresh Ports";
            this.button_RefreshPorts.UseVisualStyleBackColor = false;
            this.button_RefreshPorts.Click += new System.EventHandler(this.button_RefreshPorts_Click);
            // 
            // button_ConnectToPort
            // 
            this.button_ConnectToPort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(91)))), ((int)(((byte)(122)))));
            this.button_ConnectToPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_ConnectToPort.FlatAppearance.BorderSize = 0;
            this.button_ConnectToPort.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(61)))), ((int)(((byte)(82)))));
            this.button_ConnectToPort.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(111)))), ((int)(((byte)(162)))));
            this.button_ConnectToPort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_ConnectToPort.ForeColor = System.Drawing.Color.Gainsboro;
            this.button_ConnectToPort.Location = new System.Drawing.Point(0, 0);
            this.button_ConnectToPort.Margin = new System.Windows.Forms.Padding(4);
            this.button_ConnectToPort.Name = "button_ConnectToPort";
            this.button_ConnectToPort.Size = new System.Drawing.Size(526, 58);
            this.button_ConnectToPort.TabIndex = 3;
            this.button_ConnectToPort.Text = "Connect";
            this.button_ConnectToPort.UseVisualStyleBackColor = false;
            this.button_ConnectToPort.Click += new System.EventHandler(this.button_ConnectToPort_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.panel1.Controls.Add(this.splitContainer3);
            this.panel1.Location = new System.Drawing.Point(0, 105);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(526, 250);
            this.panel1.TabIndex = 6;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.splitContainer4);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainer3.Size = new System.Drawing.Size(526, 250);
            this.splitContainer3.SplitterDistance = 139;
            this.splitContainer3.TabIndex = 7;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.splitContainer5);
            this.splitContainer4.Size = new System.Drawing.Size(526, 139);
            this.splitContainer4.SplitterDistance = 103;
            this.splitContainer4.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 175F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.comboBox_SensorSets, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.comboBox_DeviceSchemes, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(526, 103);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(3, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 52);
            this.label1.TabIndex = 3;
            this.label1.Text = "Device Scheme:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label10.Location = new System.Drawing.Point(3, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(169, 51);
            this.label10.TabIndex = 2;
            this.label10.Text = "Sensor Set:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBox_SensorSets
            // 
            this.comboBox_SensorSets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox_SensorSets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_SensorSets.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox_SensorSets.FormattingEnabled = true;
            this.comboBox_SensorSets.ItemHeight = 37;
            this.comboBox_SensorSets.Location = new System.Drawing.Point(179, 4);
            this.comboBox_SensorSets.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox_SensorSets.Name = "comboBox_SensorSets";
            this.comboBox_SensorSets.Size = new System.Drawing.Size(343, 45);
            this.comboBox_SensorSets.TabIndex = 6;
            this.comboBox_SensorSets.SelectedIndexChanged += new System.EventHandler(this.comboBox_SensorSets_SelectedIndexChanged);
            // 
            // comboBox_DeviceSchemes
            // 
            this.comboBox_DeviceSchemes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox_DeviceSchemes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_DeviceSchemes.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox_DeviceSchemes.FormattingEnabled = true;
            this.comboBox_DeviceSchemes.ItemHeight = 37;
            this.comboBox_DeviceSchemes.Location = new System.Drawing.Point(179, 55);
            this.comboBox_DeviceSchemes.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox_DeviceSchemes.Name = "comboBox_DeviceSchemes";
            this.comboBox_DeviceSchemes.Size = new System.Drawing.Size(343, 45);
            this.comboBox_DeviceSchemes.TabIndex = 7;
            this.comboBox_DeviceSchemes.SelectedIndexChanged += new System.EventHandler(this.comboBox_DeviceSchemes_SelectedIndexChanged);
            // 
            // splitContainer5
            // 
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.Location = new System.Drawing.Point(0, 0);
            this.splitContainer5.Name = "splitContainer5";
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.checkBox_SimulationMode);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.checkBox_GenerateReadings);
            this.splitContainer5.Size = new System.Drawing.Size(526, 32);
            this.splitContainer5.SplitterDistance = 263;
            this.splitContainer5.TabIndex = 1;
            // 
            // checkBox_SimulationMode
            // 
            this.checkBox_SimulationMode.AutoSize = true;
            this.checkBox_SimulationMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBox_SimulationMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox_SimulationMode.ForeColor = System.Drawing.Color.Gainsboro;
            this.checkBox_SimulationMode.Location = new System.Drawing.Point(0, 0);
            this.checkBox_SimulationMode.Name = "checkBox_SimulationMode";
            this.checkBox_SimulationMode.Size = new System.Drawing.Size(263, 32);
            this.checkBox_SimulationMode.TabIndex = 0;
            this.checkBox_SimulationMode.Text = "Simulation Mode";
            this.checkBox_SimulationMode.UseVisualStyleBackColor = true;
            this.checkBox_SimulationMode.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox_GenerateReadings
            // 
            this.checkBox_GenerateReadings.AutoSize = true;
            this.checkBox_GenerateReadings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBox_GenerateReadings.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox_GenerateReadings.ForeColor = System.Drawing.Color.Gainsboro;
            this.checkBox_GenerateReadings.Location = new System.Drawing.Point(0, 0);
            this.checkBox_GenerateReadings.Name = "checkBox_GenerateReadings";
            this.checkBox_GenerateReadings.Size = new System.Drawing.Size(259, 32);
            this.checkBox_GenerateReadings.TabIndex = 1;
            this.checkBox_GenerateReadings.Text = "Generate Readings";
            this.checkBox_GenerateReadings.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.button_ConnectToPort);
            this.splitContainer1.Size = new System.Drawing.Size(526, 107);
            this.splitContainer1.SplitterDistance = 45;
            this.splitContainer1.TabIndex = 7;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.comboBox_PortsList);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.button_RefreshPorts);
            this.splitContainer2.Size = new System.Drawing.Size(526, 45);
            this.splitContainer2.SplitterDistance = 350;
            this.splitContainer2.TabIndex = 0;
            // 
            // splitContainer6
            // 
            this.splitContainer6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer6.Location = new System.Drawing.Point(0, 0);
            this.splitContainer6.Name = "splitContainer6";
            // 
            // splitContainer6.Panel2
            // 
            this.splitContainer6.Panel2.Controls.Add(this.splitContainer7);
            this.splitContainer6.Size = new System.Drawing.Size(684, 461);
            this.splitContainer6.SplitterDistance = 76;
            this.splitContainer6.TabIndex = 7;
            // 
            // splitContainer7
            // 
            this.splitContainer7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer7.Location = new System.Drawing.Point(0, 0);
            this.splitContainer7.Name = "splitContainer7";
            // 
            // splitContainer7.Panel1
            // 
            this.splitContainer7.Panel1.Controls.Add(this.panel1);
            this.splitContainer7.Size = new System.Drawing.Size(604, 461);
            this.splitContainer7.SplitterDistance = 529;
            this.splitContainer7.TabIndex = 0;
            // 
            // DeviceConnectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(684, 461);
            this.Controls.Add(this.splitContainer6);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DeviceConnectionForm";
            this.Text = "DeviceControlForm";
            this.panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel1.PerformLayout();
            this.splitContainer5.Panel2.ResumeLayout(false);
            this.splitContainer5.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
            this.splitContainer5.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer6.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).EndInit();
            this.splitContainer6.ResumeLayout(false);
            this.splitContainer7.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer7)).EndInit();
            this.splitContainer7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_PortsList;
        private System.Windows.Forms.Button button_RefreshPorts;
        private System.Windows.Forms.Button button_ConnectToPort;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox checkBox_SimulationMode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBox_SensorSets;
        private System.Windows.Forms.ComboBox comboBox_DeviceSchemes;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private System.Windows.Forms.CheckBox checkBox_GenerateReadings;
        private System.Windows.Forms.SplitContainer splitContainer6;
        private System.Windows.Forms.SplitContainer splitContainer7;
    }
}