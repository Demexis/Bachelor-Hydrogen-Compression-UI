using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bachelor_Project_Hydrogen_Compression_WinForms
{
    public partial class DeviceConnectionForm : Form
    {
        public DeviceConnectionForm()
        {
            InitializeComponent();

            button_ConnectToPort.Enabled = false;
        }

        private void button_ConnectToPort_Click(object sender, EventArgs e)
        {
            SerialPort serialPort = MainForm.Instance.GetMainSerialPort;

            if (!MainForm.ConnectedToPort)
            {
                try
                {
                    serialPort.PortName = comboBox_PortsList.Text;
                    serialPort.Open();
                    comboBox_PortsList.Enabled = false;
                    button_RefreshPorts.Enabled = false;
                    button_ConnectToPort.Text = "Disconnect";

                    MainForm.Instance.TimerEnabled = true;
                    MainForm.ConnectedToPort = !MainForm.ConnectedToPort;
                }
                catch
                {
                    MessageBox.Show("Connection Error");
                }
            }
            else
            {
                MainForm.Instance.TimerEnabled = false;
                MainForm.ConnectedToPort = !MainForm.ConnectedToPort;


                try
                {
                    serialPort.Close();
                    comboBox_PortsList.Enabled = true;
                    button_RefreshPorts.Enabled = true;
                    button_ConnectToPort.Text = "Connect";
                }
                catch
                {
                    MessageBox.Show("Disconnection Error");
                }
            }
        }

        private void button_RefreshPorts_Click(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            comboBox_PortsList.Text = "";
            comboBox_PortsList.Items.Clear();

            if (ports.Length != 0)
            {
                comboBox_PortsList.Items.AddRange(ports);
                comboBox_PortsList.SelectedIndex = 0;
            }
        }

        private void comboBox_PortsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            button_ConnectToPort.Enabled = (((ComboBox)sender).SelectedItem != null);
        }
    }
}
