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

namespace Bachelor_Project_Hydrogen_Compression_WinForms
{
    public partial class MainForm : Form
    {
        public static MainForm Instance; // Singleton
        public static bool ConnectedToPort { get; set; }

        public SerialPort GetMainSerialPort { get { return this.serialPort_Main; } }
        public bool TimerEnabled { 
            get { return timer_ToSendSerialData.Enabled; } 
            set { timer_ToSendSerialData.Enabled = value; } 
        }

        public Dictionary<Button, Panel> Submenus = new Dictionary<Button, Panel>();
        public Dictionary<Button, Form> ChildForms = new Dictionary<Button, Form>();

        public Action<string> SerialPortDataSender;

        public MainForm()
        {
            InitializeComponent();

            if (Instance != null)
                this.Close();
            else
                Instance = this;

            InitializeSubmenus();
            InitializeChildForms();

            SetCurrentSubmenuFromButton(null);
        }

        private void InitializeChildForms()
        {
            ChildForms = new Dictionary<Button, Form>()
            {
                [this.button_ChildForm_Connect] = new DeviceConnectionForm(),
                [this.button_ChildForm_Govern] = new DeviceGovernForm(),
                [this.button_ChildForm_Diagnostic] = new DeviceDiagnosticForm(),
                [this.button_ChildForm_Device] = new DeviceForm()
            };
        }

        private void InitializeSubmenus()
        {
            Submenus = new Dictionary<Button, Panel>()
            {
                [this.button_SideMenu_Control] = this.panel_ControlSubmenu,
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

            SerialPortDataSender?.Invoke(_serialData);
        }
    }
}
