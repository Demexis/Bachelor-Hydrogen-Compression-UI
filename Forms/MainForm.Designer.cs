
namespace Bachelor_Project
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panel_SideMenu = new System.Windows.Forms.Panel();
            this.panel_OptionsSubmenu = new System.Windows.Forms.Panel();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button_SideMenu_Options = new System.Windows.Forms.Button();
            this.panel_EditorsSubmenu = new System.Windows.Forms.Panel();
            this.button_ChildForm_DeviceSchemes = new System.Windows.Forms.Button();
            this.button_ChildForm_Cyclograms = new System.Windows.Forms.Button();
            this.button_ChildForm_Sensors = new System.Windows.Forms.Button();
            this.button_SideMenu_Editors = new System.Windows.Forms.Button();
            this.panel_ControlSubmenu = new System.Windows.Forms.Panel();
            this.button_ChildForm_Diagnostic = new System.Windows.Forms.Button();
            this.button_ChildForm_Govern = new System.Windows.Forms.Button();
            this.button_ChildForm_Device = new System.Windows.Forms.Button();
            this.button_ChildForm_Connect = new System.Windows.Forms.Button();
            this.button_SideMenu_Control = new System.Windows.Forms.Button();
            this.panel_Logo = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.serialPort_Main = new System.IO.Ports.SerialPort(this.components);
            this.timer_ToSendSerialData = new System.Windows.Forms.Timer(this.components);
            this.panel_ChildForm = new System.Windows.Forms.Panel();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.panel_SideMenu.SuspendLayout();
            this.panel_OptionsSubmenu.SuspendLayout();
            this.panel_EditorsSubmenu.SuspendLayout();
            this.panel_ControlSubmenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_SideMenu
            // 
            this.panel_SideMenu.AutoScroll = true;
            this.panel_SideMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(13)))), ((int)(((byte)(24)))));
            this.panel_SideMenu.Controls.Add(this.panel_OptionsSubmenu);
            this.panel_SideMenu.Controls.Add(this.button_SideMenu_Options);
            this.panel_SideMenu.Controls.Add(this.panel_EditorsSubmenu);
            this.panel_SideMenu.Controls.Add(this.button_SideMenu_Editors);
            this.panel_SideMenu.Controls.Add(this.panel_ControlSubmenu);
            this.panel_SideMenu.Controls.Add(this.button_SideMenu_Control);
            this.panel_SideMenu.Controls.Add(this.panel_Logo);
            this.panel_SideMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_SideMenu.Location = new System.Drawing.Point(0, 0);
            this.panel_SideMenu.Name = "panel_SideMenu";
            this.panel_SideMenu.Size = new System.Drawing.Size(250, 561);
            this.panel_SideMenu.TabIndex = 0;
            // 
            // panel_OptionsSubmenu
            // 
            this.panel_OptionsSubmenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(32)))), ((int)(((byte)(39)))));
            this.panel_OptionsSubmenu.Controls.Add(this.button6);
            this.panel_OptionsSubmenu.Controls.Add(this.button7);
            this.panel_OptionsSubmenu.Controls.Add(this.button8);
            this.panel_OptionsSubmenu.Controls.Add(this.button9);
            this.panel_OptionsSubmenu.Controls.Add(this.button10);
            this.panel_OptionsSubmenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_OptionsSubmenu.Location = new System.Drawing.Point(0, 536);
            this.panel_OptionsSubmenu.Name = "panel_OptionsSubmenu";
            this.panel_OptionsSubmenu.Size = new System.Drawing.Size(233, 241);
            this.panel_OptionsSubmenu.TabIndex = 5;
            // 
            // button6
            // 
            this.button6.Dock = System.Windows.Forms.DockStyle.Top;
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(48)))), ((int)(((byte)(64)))));
            this.button6.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(40)))), ((int)(((byte)(48)))));
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.ForeColor = System.Drawing.Color.LightGray;
            this.button6.Location = new System.Drawing.Point(0, 160);
            this.button6.Name = "button6";
            this.button6.Padding = new System.Windows.Forms.Padding(35, 0, 0, 0);
            this.button6.Size = new System.Drawing.Size(233, 40);
            this.button6.TabIndex = 4;
            this.button6.Text = "button6";
            this.button6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Dock = System.Windows.Forms.DockStyle.Top;
            this.button7.FlatAppearance.BorderSize = 0;
            this.button7.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(48)))), ((int)(((byte)(64)))));
            this.button7.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(40)))), ((int)(((byte)(48)))));
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.ForeColor = System.Drawing.Color.LightGray;
            this.button7.Location = new System.Drawing.Point(0, 120);
            this.button7.Name = "button7";
            this.button7.Padding = new System.Windows.Forms.Padding(35, 0, 0, 0);
            this.button7.Size = new System.Drawing.Size(233, 40);
            this.button7.TabIndex = 3;
            this.button7.Text = "button7";
            this.button7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Dock = System.Windows.Forms.DockStyle.Top;
            this.button8.FlatAppearance.BorderSize = 0;
            this.button8.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(48)))), ((int)(((byte)(64)))));
            this.button8.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(40)))), ((int)(((byte)(48)))));
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.ForeColor = System.Drawing.Color.LightGray;
            this.button8.Location = new System.Drawing.Point(0, 80);
            this.button8.Name = "button8";
            this.button8.Padding = new System.Windows.Forms.Padding(35, 0, 0, 0);
            this.button8.Size = new System.Drawing.Size(233, 40);
            this.button8.TabIndex = 2;
            this.button8.Text = "button8";
            this.button8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button8.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            this.button9.Dock = System.Windows.Forms.DockStyle.Top;
            this.button9.FlatAppearance.BorderSize = 0;
            this.button9.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(48)))), ((int)(((byte)(64)))));
            this.button9.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(40)))), ((int)(((byte)(48)))));
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button9.ForeColor = System.Drawing.Color.LightGray;
            this.button9.Location = new System.Drawing.Point(0, 40);
            this.button9.Name = "button9";
            this.button9.Padding = new System.Windows.Forms.Padding(35, 0, 0, 0);
            this.button9.Size = new System.Drawing.Size(233, 40);
            this.button9.TabIndex = 1;
            this.button9.Text = "button9";
            this.button9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button9.UseVisualStyleBackColor = true;
            // 
            // button10
            // 
            this.button10.Dock = System.Windows.Forms.DockStyle.Top;
            this.button10.FlatAppearance.BorderSize = 0;
            this.button10.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(48)))), ((int)(((byte)(64)))));
            this.button10.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(40)))), ((int)(((byte)(48)))));
            this.button10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button10.ForeColor = System.Drawing.Color.LightGray;
            this.button10.Location = new System.Drawing.Point(0, 0);
            this.button10.Name = "button10";
            this.button10.Padding = new System.Windows.Forms.Padding(35, 0, 0, 0);
            this.button10.Size = new System.Drawing.Size(233, 40);
            this.button10.TabIndex = 0;
            this.button10.Text = "button10";
            this.button10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button10.UseVisualStyleBackColor = true;
            // 
            // button_SideMenu_Options
            // 
            this.button_SideMenu_Options.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_SideMenu_Options.FlatAppearance.BorderSize = 0;
            this.button_SideMenu_Options.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_SideMenu_Options.ForeColor = System.Drawing.Color.Gainsboro;
            this.button_SideMenu_Options.Location = new System.Drawing.Point(0, 491);
            this.button_SideMenu_Options.Name = "button_SideMenu_Options";
            this.button_SideMenu_Options.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.button_SideMenu_Options.Size = new System.Drawing.Size(233, 45);
            this.button_SideMenu_Options.TabIndex = 3;
            this.button_SideMenu_Options.Text = "Options";
            this.button_SideMenu_Options.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_SideMenu_Options.UseVisualStyleBackColor = true;
            this.button_SideMenu_Options.Click += new System.EventHandler(this.SideMenuButton_Click);
            // 
            // panel_EditorsSubmenu
            // 
            this.panel_EditorsSubmenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(32)))), ((int)(((byte)(39)))));
            this.panel_EditorsSubmenu.Controls.Add(this.button_ChildForm_DeviceSchemes);
            this.panel_EditorsSubmenu.Controls.Add(this.button_ChildForm_Cyclograms);
            this.panel_EditorsSubmenu.Controls.Add(this.button_ChildForm_Sensors);
            this.panel_EditorsSubmenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_EditorsSubmenu.Location = new System.Drawing.Point(0, 361);
            this.panel_EditorsSubmenu.Name = "panel_EditorsSubmenu";
            this.panel_EditorsSubmenu.Size = new System.Drawing.Size(233, 130);
            this.panel_EditorsSubmenu.TabIndex = 7;
            // 
            // button_ChildForm_DeviceSchemes
            // 
            this.button_ChildForm_DeviceSchemes.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_ChildForm_DeviceSchemes.FlatAppearance.BorderSize = 0;
            this.button_ChildForm_DeviceSchemes.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(48)))), ((int)(((byte)(64)))));
            this.button_ChildForm_DeviceSchemes.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(40)))), ((int)(((byte)(48)))));
            this.button_ChildForm_DeviceSchemes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_ChildForm_DeviceSchemes.ForeColor = System.Drawing.Color.LightGray;
            this.button_ChildForm_DeviceSchemes.Location = new System.Drawing.Point(0, 80);
            this.button_ChildForm_DeviceSchemes.Name = "button_ChildForm_DeviceSchemes";
            this.button_ChildForm_DeviceSchemes.Padding = new System.Windows.Forms.Padding(35, 0, 0, 0);
            this.button_ChildForm_DeviceSchemes.Size = new System.Drawing.Size(233, 40);
            this.button_ChildForm_DeviceSchemes.TabIndex = 0;
            this.button_ChildForm_DeviceSchemes.Text = "Device Schemes";
            this.button_ChildForm_DeviceSchemes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_ChildForm_DeviceSchemes.UseVisualStyleBackColor = true;
            this.button_ChildForm_DeviceSchemes.Click += new System.EventHandler(this.ChildFormButton_Click);
            // 
            // button_ChildForm_Cyclograms
            // 
            this.button_ChildForm_Cyclograms.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_ChildForm_Cyclograms.FlatAppearance.BorderSize = 0;
            this.button_ChildForm_Cyclograms.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(48)))), ((int)(((byte)(64)))));
            this.button_ChildForm_Cyclograms.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(40)))), ((int)(((byte)(48)))));
            this.button_ChildForm_Cyclograms.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_ChildForm_Cyclograms.ForeColor = System.Drawing.Color.LightGray;
            this.button_ChildForm_Cyclograms.Location = new System.Drawing.Point(0, 40);
            this.button_ChildForm_Cyclograms.Name = "button_ChildForm_Cyclograms";
            this.button_ChildForm_Cyclograms.Padding = new System.Windows.Forms.Padding(35, 0, 0, 0);
            this.button_ChildForm_Cyclograms.Size = new System.Drawing.Size(233, 40);
            this.button_ChildForm_Cyclograms.TabIndex = 1;
            this.button_ChildForm_Cyclograms.Text = "Cyclograms";
            this.button_ChildForm_Cyclograms.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_ChildForm_Cyclograms.UseVisualStyleBackColor = true;
            this.button_ChildForm_Cyclograms.Click += new System.EventHandler(this.ChildFormButton_Click);
            // 
            // button_ChildForm_Sensors
            // 
            this.button_ChildForm_Sensors.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_ChildForm_Sensors.FlatAppearance.BorderSize = 0;
            this.button_ChildForm_Sensors.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(48)))), ((int)(((byte)(64)))));
            this.button_ChildForm_Sensors.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(40)))), ((int)(((byte)(48)))));
            this.button_ChildForm_Sensors.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_ChildForm_Sensors.ForeColor = System.Drawing.Color.LightGray;
            this.button_ChildForm_Sensors.Location = new System.Drawing.Point(0, 0);
            this.button_ChildForm_Sensors.Name = "button_ChildForm_Sensors";
            this.button_ChildForm_Sensors.Padding = new System.Windows.Forms.Padding(35, 0, 0, 0);
            this.button_ChildForm_Sensors.Size = new System.Drawing.Size(233, 40);
            this.button_ChildForm_Sensors.TabIndex = 2;
            this.button_ChildForm_Sensors.Text = "Sensors";
            this.button_ChildForm_Sensors.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_ChildForm_Sensors.UseVisualStyleBackColor = true;
            this.button_ChildForm_Sensors.Click += new System.EventHandler(this.ChildFormButton_Click);
            // 
            // button_SideMenu_Editors
            // 
            this.button_SideMenu_Editors.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_SideMenu_Editors.FlatAppearance.BorderSize = 0;
            this.button_SideMenu_Editors.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_SideMenu_Editors.ForeColor = System.Drawing.Color.Gainsboro;
            this.button_SideMenu_Editors.Location = new System.Drawing.Point(0, 316);
            this.button_SideMenu_Editors.Name = "button_SideMenu_Editors";
            this.button_SideMenu_Editors.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.button_SideMenu_Editors.Size = new System.Drawing.Size(233, 45);
            this.button_SideMenu_Editors.TabIndex = 6;
            this.button_SideMenu_Editors.Text = "Editors";
            this.button_SideMenu_Editors.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_SideMenu_Editors.UseVisualStyleBackColor = true;
            this.button_SideMenu_Editors.Click += new System.EventHandler(this.SideMenuButton_Click);
            // 
            // panel_ControlSubmenu
            // 
            this.panel_ControlSubmenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(32)))), ((int)(((byte)(39)))));
            this.panel_ControlSubmenu.Controls.Add(this.button_ChildForm_Diagnostic);
            this.panel_ControlSubmenu.Controls.Add(this.button_ChildForm_Govern);
            this.panel_ControlSubmenu.Controls.Add(this.button_ChildForm_Device);
            this.panel_ControlSubmenu.Controls.Add(this.button_ChildForm_Connect);
            this.panel_ControlSubmenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_ControlSubmenu.Location = new System.Drawing.Point(0, 145);
            this.panel_ControlSubmenu.Name = "panel_ControlSubmenu";
            this.panel_ControlSubmenu.Size = new System.Drawing.Size(233, 171);
            this.panel_ControlSubmenu.TabIndex = 2;
            // 
            // button_ChildForm_Diagnostic
            // 
            this.button_ChildForm_Diagnostic.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_ChildForm_Diagnostic.FlatAppearance.BorderSize = 0;
            this.button_ChildForm_Diagnostic.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(48)))), ((int)(((byte)(64)))));
            this.button_ChildForm_Diagnostic.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(40)))), ((int)(((byte)(48)))));
            this.button_ChildForm_Diagnostic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_ChildForm_Diagnostic.ForeColor = System.Drawing.Color.LightGray;
            this.button_ChildForm_Diagnostic.Location = new System.Drawing.Point(0, 120);
            this.button_ChildForm_Diagnostic.Name = "button_ChildForm_Diagnostic";
            this.button_ChildForm_Diagnostic.Padding = new System.Windows.Forms.Padding(35, 0, 0, 0);
            this.button_ChildForm_Diagnostic.Size = new System.Drawing.Size(233, 40);
            this.button_ChildForm_Diagnostic.TabIndex = 2;
            this.button_ChildForm_Diagnostic.Text = "Diagnostic";
            this.button_ChildForm_Diagnostic.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_ChildForm_Diagnostic.UseVisualStyleBackColor = true;
            this.button_ChildForm_Diagnostic.Click += new System.EventHandler(this.ChildFormButton_Click);
            // 
            // button_ChildForm_Govern
            // 
            this.button_ChildForm_Govern.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_ChildForm_Govern.FlatAppearance.BorderSize = 0;
            this.button_ChildForm_Govern.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(48)))), ((int)(((byte)(64)))));
            this.button_ChildForm_Govern.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(40)))), ((int)(((byte)(48)))));
            this.button_ChildForm_Govern.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_ChildForm_Govern.ForeColor = System.Drawing.Color.LightGray;
            this.button_ChildForm_Govern.Location = new System.Drawing.Point(0, 80);
            this.button_ChildForm_Govern.Name = "button_ChildForm_Govern";
            this.button_ChildForm_Govern.Padding = new System.Windows.Forms.Padding(35, 0, 0, 0);
            this.button_ChildForm_Govern.Size = new System.Drawing.Size(233, 40);
            this.button_ChildForm_Govern.TabIndex = 1;
            this.button_ChildForm_Govern.Text = "Govern";
            this.button_ChildForm_Govern.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_ChildForm_Govern.UseVisualStyleBackColor = true;
            this.button_ChildForm_Govern.Click += new System.EventHandler(this.ChildFormButton_Click);
            // 
            // button_ChildForm_Device
            // 
            this.button_ChildForm_Device.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_ChildForm_Device.FlatAppearance.BorderSize = 0;
            this.button_ChildForm_Device.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(48)))), ((int)(((byte)(64)))));
            this.button_ChildForm_Device.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(40)))), ((int)(((byte)(48)))));
            this.button_ChildForm_Device.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_ChildForm_Device.ForeColor = System.Drawing.Color.LightGray;
            this.button_ChildForm_Device.Location = new System.Drawing.Point(0, 40);
            this.button_ChildForm_Device.Name = "button_ChildForm_Device";
            this.button_ChildForm_Device.Padding = new System.Windows.Forms.Padding(35, 0, 0, 0);
            this.button_ChildForm_Device.Size = new System.Drawing.Size(233, 40);
            this.button_ChildForm_Device.TabIndex = 3;
            this.button_ChildForm_Device.Text = "Device";
            this.button_ChildForm_Device.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_ChildForm_Device.UseVisualStyleBackColor = true;
            this.button_ChildForm_Device.Click += new System.EventHandler(this.ChildFormButton_Click);
            // 
            // button_ChildForm_Connect
            // 
            this.button_ChildForm_Connect.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_ChildForm_Connect.FlatAppearance.BorderSize = 0;
            this.button_ChildForm_Connect.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(48)))), ((int)(((byte)(64)))));
            this.button_ChildForm_Connect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(40)))), ((int)(((byte)(48)))));
            this.button_ChildForm_Connect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_ChildForm_Connect.ForeColor = System.Drawing.Color.LightGray;
            this.button_ChildForm_Connect.Location = new System.Drawing.Point(0, 0);
            this.button_ChildForm_Connect.Name = "button_ChildForm_Connect";
            this.button_ChildForm_Connect.Padding = new System.Windows.Forms.Padding(35, 0, 0, 0);
            this.button_ChildForm_Connect.Size = new System.Drawing.Size(233, 40);
            this.button_ChildForm_Connect.TabIndex = 0;
            this.button_ChildForm_Connect.Text = "Connect";
            this.button_ChildForm_Connect.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_ChildForm_Connect.UseVisualStyleBackColor = true;
            this.button_ChildForm_Connect.Click += new System.EventHandler(this.ChildFormButton_Click);
            // 
            // button_SideMenu_Control
            // 
            this.button_SideMenu_Control.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_SideMenu_Control.FlatAppearance.BorderSize = 0;
            this.button_SideMenu_Control.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_SideMenu_Control.ForeColor = System.Drawing.Color.Gainsboro;
            this.button_SideMenu_Control.Location = new System.Drawing.Point(0, 100);
            this.button_SideMenu_Control.Name = "button_SideMenu_Control";
            this.button_SideMenu_Control.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.button_SideMenu_Control.Size = new System.Drawing.Size(233, 45);
            this.button_SideMenu_Control.TabIndex = 1;
            this.button_SideMenu_Control.Text = "Control";
            this.button_SideMenu_Control.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_SideMenu_Control.UseVisualStyleBackColor = true;
            this.button_SideMenu_Control.Click += new System.EventHandler(this.SideMenuButton_Click);
            // 
            // panel_Logo
            // 
            this.panel_Logo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.panel_Logo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel_Logo.BackgroundImage")));
            this.panel_Logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel_Logo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Logo.Location = new System.Drawing.Point(0, 0);
            this.panel_Logo.Name = "panel_Logo";
            this.panel_Logo.Size = new System.Drawing.Size(233, 100);
            this.panel_Logo.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(250, 461);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(684, 100);
            this.panel1.TabIndex = 1;
            // 
            // serialPort_Main
            // 
            this.serialPort_Main.BaudRate = 250000;
            this.serialPort_Main.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.MainSerialPort_DataReceived);
            // 
            // timer_ToSendSerialData
            // 
            this.timer_ToSendSerialData.Interval = 1000;
            this.timer_ToSendSerialData.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel_ChildForm
            // 
            this.panel_ChildForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.panel_ChildForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel_ChildForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_ChildForm.Location = new System.Drawing.Point(250, 0);
            this.panel_ChildForm.Name = "panel_ChildForm";
            this.panel_ChildForm.Size = new System.Drawing.Size(684, 461);
            this.panel_ChildForm.TabIndex = 2;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipTitle = "Info";
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "App";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(934, 561);
            this.Controls.Add(this.panel_ChildForm);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel_SideMenu);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(950, 600);
            this.Name = "MainForm";
            this.Text = "App";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.panel_SideMenu.ResumeLayout(false);
            this.panel_OptionsSubmenu.ResumeLayout(false);
            this.panel_EditorsSubmenu.ResumeLayout(false);
            this.panel_ControlSubmenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel_SideMenu;
        private System.Windows.Forms.Button button_SideMenu_Control;
        private System.Windows.Forms.Panel panel_Logo;
        private System.Windows.Forms.Panel panel_ControlSubmenu;
        private System.Windows.Forms.Button button_ChildForm_Diagnostic;
        private System.Windows.Forms.Button button_ChildForm_Govern;
        private System.Windows.Forms.Button button_ChildForm_Connect;
        private System.Windows.Forms.Button button_SideMenu_Options;
        private System.Windows.Forms.Panel panel_OptionsSubmenu;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel_ChildForm;
        private System.IO.Ports.SerialPort serialPort_Main;
        private System.Windows.Forms.Timer timer_ToSendSerialData;
        private System.Windows.Forms.Button button_ChildForm_Device;
        private System.Windows.Forms.Panel panel_EditorsSubmenu;
        private System.Windows.Forms.Button button_ChildForm_Cyclograms;
        private System.Windows.Forms.Button button_ChildForm_DeviceSchemes;
        private System.Windows.Forms.Button button_ChildForm_Sensors;
        private System.Windows.Forms.Button button_SideMenu_Editors;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}

