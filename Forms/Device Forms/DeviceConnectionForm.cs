using Bachelor_Project.Extensions;
using Bachelor_Project.Forms.Options_Forms;
using Bachelor_Project.Handlers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bachelor_Project
{
    public partial class DeviceConnectionForm : Form
    {
        public static DeviceConnectionForm Instance;

        public string SelectedSensorSet = string.Empty;
        public string SelectedDeviceScheme = string.Empty;

        public bool GeneratingReadingsStatus => checkBox_SimulationMode.Checked && checkBox_GenerateReadings.Checked;

        private bool _connected = false;

        public DeviceConnectionForm()
        {
            InitializeComponent();

            if (Instance == null) Instance = this;
            else this.Close();

            button_ConnectToPort.Enabled = false;
            checkBox_GenerateReadings.Enabled = checkBox_SimulationMode.Checked;

            RefreshPorts();

            this.VisibleChanged += (s, e) =>
            {
                //comboBox_SensorSets.Items.Clear();
                //comboBox_DeviceSchemes.Items.Clear();

                List<string> sensorFiles = FileManager.GetFiles(FileManager.JsonFileStructure.Sensors).ToList();
                List<string> deviceSchemeFiles = FileManager.GetFiles(FileManager.JsonFileStructure.DeviceSchemes).ToList();

                List<string> sensorsToDelete = new List<string>();
                foreach(string sensor in comboBox_SensorSets.Items)
                {
                    if (!sensorFiles.Contains(sensor)) sensorsToDelete.Add(sensor);
                }
                sensorsToDelete.ForEach(x => comboBox_SensorSets.Items.Remove(x));

                List<string> deviceSchemesToDelete = new List<string>();
                foreach (string deviceScheme in comboBox_DeviceSchemes.Items)
                {
                    if (!deviceSchemeFiles.Contains(deviceScheme)) deviceSchemesToDelete.Add(deviceScheme);
                }
                deviceSchemesToDelete.ForEach(x => comboBox_DeviceSchemes.Items.Remove(x));

                sensorFiles.ForEach(x => { if (!comboBox_SensorSets.Items.Contains(x)) comboBox_SensorSets.Items.Add(x); });
                deviceSchemeFiles.ForEach(x => { if (!comboBox_DeviceSchemes.Items.Contains(x)) comboBox_DeviceSchemes.Items.Add(x); });

                if(comboBox_SensorSets.SelectedItem == null && comboBox_SensorSets.Items.Count != 0)
                {
                    comboBox_SensorSets.SelectedIndex = 0;
                }

                if (comboBox_DeviceSchemes.SelectedItem == null && comboBox_DeviceSchemes.Items.Count != 0)
                {
                    comboBox_DeviceSchemes.SelectedIndex = 0;
                }
            };

            AppearanceOptionsForm.OnColorPaletteChange += SetColorPaletteForControls;
        }

        private void button_ConnectToPort_Click(object sender, EventArgs e)
        {
            if(checkBox_SimulationMode.Checked)
            {
                _connected = !_connected;
                checkBox_SimulationMode.Enabled = !_connected;

                button_ConnectToPort.Text = _connected ? "Disconnect" : "Connect";
                comboBox_DeviceSchemes.Enabled = !_connected;
                comboBox_SensorSets.Enabled = !_connected;

                if(_connected)
                {
                    DeviceForm.Instance.ReinitializeCompressorDeviceScheme();
                    DeviceForm.Instance.ReinitializeCyclogram();
                    if (checkBox_GenerateReadings.Checked) DeviceForm.Instance.SendTestData();
                }
                else
                {
                    DeviceForm.Instance.Clear();
                }

                MainForm.Instance.ManageButtonsOnConnection(_connected);

                return;
            }

            SerialPort serialPort = COM_Handler.MainSerialPort;

            if (!COM_Handler.ConnectedToPort)
            {
                try
                {
                    serialPort.PortName = comboBox_PortsList.Text;
                    serialPort.Open();
                    comboBox_PortsList.Enabled = false;
                    comboBox_DeviceSchemes.Enabled = false;
                    comboBox_SensorSets.Enabled = false;
                    button_RefreshPorts.Enabled = false;
                    button_ConnectToPort.Text = "Disconnect";

                    MainForm.Instance.TimerEnabled = true;
                    COM_Handler.ConnectedToPort = !COM_Handler.ConnectedToPort;
                    _connected = COM_Handler.ConnectedToPort;

                    if (_connected)
                    {
                        DeviceForm.Instance.ReinitializeCompressorDeviceScheme();
                        DeviceForm.Instance.ReinitializeCyclogram();
                        if(checkBox_GenerateReadings.Checked) DeviceForm.Instance.SendTestData();
                    }
                    else
                    {
                        DeviceForm.Instance.Clear();
                    }

                    MainForm.Instance.ManageButtonsOnConnection(_connected);
                }
                catch
                {
                    MessageBox.Show("Connection Error");
                }
            }
            else
            {
                MainForm.Instance.TimerEnabled = false;
                COM_Handler.ConnectedToPort = !COM_Handler.ConnectedToPort;
                _connected = COM_Handler.ConnectedToPort;

                if(!_connected)
                {
                    DeviceForm.Instance.Clear();
                }

                MainForm.Instance.ManageButtonsOnConnection(_connected);

                try
                {
                    serialPort.Close();
                    comboBox_PortsList.Enabled = true;
                    comboBox_DeviceSchemes.Enabled = true;
                    comboBox_SensorSets.Enabled = true;
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
            RefreshPorts();
        }

        private void RefreshPorts()
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
            button_ConnectToPort.Enabled = CheckButtonConnectAvailability();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            comboBox_PortsList.Enabled = !((CheckBox)sender).Checked;
            button_RefreshPorts.Enabled = !((CheckBox)sender).Checked;

            checkBox_GenerateReadings.Enabled = ((CheckBox)sender).Checked;

            button_ConnectToPort.Enabled = CheckButtonConnectAvailability();
        }

        private void comboBox_SensorSets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(sender is ComboBox comboBox)
            {
                SelectedSensorSet = comboBox.SelectedItem != null ? (string)comboBox.SelectedItem : string.Empty;

                string json = File.ReadAllText(FileManager.GetFiles(FileManager.JsonFileStructure.Sensors).First(x => x.Equals(SelectedSensorSet)));
                MainForm.Instance.SensorReadingHelper.Sensors.Clear();
                JSON_Handler.InitializeSensorReaderWithJson(MainForm.Instance.SensorReadingHelper, json);

                button_ConnectToPort.Enabled = CheckButtonConnectAvailability();
            }
        }

        private void comboBox_DeviceSchemes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                SelectedDeviceScheme = comboBox.SelectedItem != null ? (string)comboBox.SelectedItem : string.Empty;

                button_ConnectToPort.Enabled = CheckButtonConnectAvailability();
            }
        }

        private bool CheckButtonConnectAvailability()
        {
            return
            (
                (checkBox_SimulationMode.Checked || comboBox_PortsList.SelectedItem != null) &&
                comboBox_SensorSets.SelectedItem != null &&
                comboBox_DeviceSchemes.SelectedItem != null
            );
        }

        public void SetColorPaletteForControls(Dictionary<FormColorVariant, Color> colorPalette)
        {
            this.BackColor = colorPalette[FormColorVariant.DarkFirst];
            this.panel1.BackColor = colorPalette[FormColorVariant.DarkSecond];
            
            foreach(Button button in this.GetAllControlsRecusrvive<Button>())
            {
                button.BackColor = colorPalette[FormColorVariant.BrightSecond];
                button.ForeColor = colorPalette[FormColorVariant.TextColorFirst];

                button.FlatAppearance.MouseDownBackColor = colorPalette[FormColorVariant.ButtonMouseDown];
                button.FlatAppearance.MouseOverBackColor = colorPalette[FormColorVariant.ButtonMouseOver];

            }

        }
    }
}
