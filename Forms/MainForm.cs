using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using Bachelor_Project.Handlers;
using Bachelor_Project.Forms.Editor_Forms;
using Bachelor_Project.Forms.Editor_Forms.Device_Scheme_Editor_Forms;
using Bachelor_Project.Forms.Editor_Forms.Cyclogram_Editor_Form;

namespace Bachelor_Project
{
    public partial class MainForm : Form
    {
        private SensorReadingHelper _sensorReadingHelper = new SensorReadingHelper(); // move to somewhere else
        public SensorReadingHelper SensorReadingHelper => _sensorReadingHelper;

        public static MainForm Instance; // Singleton

        public bool TimerEnabled { 
            get { return timer_ToSendSerialData.Enabled; } 
            set { timer_ToSendSerialData.Enabled = value; } 
        }

        public Dictionary<Button, Panel> Submenus = new Dictionary<Button, Panel>();
        public Dictionary<Button, Form> ChildForms = new Dictionary<Button, Form>();


        public MainForm()
        {
            InitializeComponent();

            if (Instance != null)
                this.Close();
            else
                Instance = this;

            InitializeSubmenus();
            InitializeChildForms();
            InitializeNotifyIconContext();

            SetCurrentSubmenuFromButton(null);

            COM_Handler.MainSerialPort = this.serialPort_Main;

            ManageButtonsOnConnection(false);
        }

        private void InitializeNotifyIconContext()
        {
            this.notifyIcon1.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            this.notifyIcon1.ContextMenuStrip.Items.Add("Exit", null, (x, y) => { this.Close(); });
        }

        private void InitializeChildForms()
        {
            ChildForms = new Dictionary<Button, Form>()
            {
                [this.button_ChildForm_Connect] = new DeviceConnectionForm(),
                [this.button_ChildForm_Govern] = new DeviceGovernForm(),
                [this.button_ChildForm_Diagnostic] = new DeviceDiagnosticForm(),
                [this.button_ChildForm_Device] = new DeviceForm(),
                [this.button_ChildForm_Sensors] = new SensorsEditorForm(),
                [this.button_ChildForm_DeviceSchemes] = new DeviceSchemeEditorForm(),
                [this.button_ChildForm_Cyclograms] = new CyclogramEditorForm()
            };
        }

        private void InitializeSubmenus()
        {
            Submenus = new Dictionary<Button, Panel>()
            {
                [this.button_SideMenu_Control] = this.panel_ControlSubmenu,
                [this.button_SideMenu_Editors] = this.panel_EditorsSubmenu,
                [this.button_SideMenu_Options] = this.panel_OptionsSubmenu
            };
        }

        #region SideMenuSubmenus
        private void SetCurrentSubmenuFromButton(Button button)
        {
            foreach(KeyValuePair<Button, Panel> submenu in Submenus)
            {
                submenu.Value.Visible = (submenu.Key == button && !submenu.Value.Visible);
            }
        }

        private void SideMenuButton_Click(object sender, EventArgs e)
        {
            if(sender is Button button) SetCurrentSubmenuFromButton(button);
        }
        #endregion


        #region ChildForms
        private Form _activeForm = null;
        private void SetChildForm(Button button)
        {
            _activeForm?.Hide();

            Form openForm = null;
            foreach (KeyValuePair<Button, Form> childForm in ChildForms)
            {
                if (childForm.Key == button)
                {
                    openForm = childForm.Value;
                    break;
                }
            }

            _activeForm = openForm;

            if(_activeForm != null)
            {
                _activeForm.TopLevel = false;
                _activeForm.FormBorderStyle = FormBorderStyle.None;
                _activeForm.Dock = DockStyle.Fill;
                panel_ChildForm.Controls.Add(_activeForm);
                panel_ChildForm.Tag = _activeForm;
                _activeForm.BringToFront();
                _activeForm.Show();
            }
        }
        
        private void ChildFormButton_Click(object sender, EventArgs e)
        {
            if(sender is Button button) SetChildForm(button);
        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            //SerialPortDataSender?.Invoke(_serialData);
        }

        private string _serialData = string.Empty;
        private void MainSerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string temp = serialPort_Main.ReadLine();

            _serialData = temp;

            Console.WriteLine(temp);

            COM_Handler.SerialPortDataSender?.Invoke(_serialData);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            AppPreferences.ApplicationIsTerminating = true;
            DeviceForm.TempTestingFlag = false;
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }

        public void ManageButtonsOnConnection(bool connectionStatus)
        {
            button_ChildForm_Device.Enabled = connectionStatus;
            //button_ChildForm_Diagnostic.Enabled = connectionStatus;
            button_ChildForm_Govern.Enabled = connectionStatus;

            button_ChildForm_Sensors.Enabled = !connectionStatus;
            button_ChildForm_DeviceSchemes.Enabled = !connectionStatus;
            button_ChildForm_Cyclograms.Enabled = !connectionStatus;
        }
    }
}
