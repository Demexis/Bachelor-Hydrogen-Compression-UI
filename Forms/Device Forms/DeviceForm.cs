using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bachelor_Project.UserControls.Device;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using CompressorComponent = Bachelor_Project.UserControls.Device.CompressorComponent;
using ComponentType = Bachelor_Project.UserControls.Device.CompressorComponent.ComponentType;
using ComponentOrientation = Bachelor_Project.UserControls.Device.CompressorComponent.ComponentOrientation;
using Bachelor_Project.UserControls;
using Bachelor_Project.Miscellaneous;
using Bachelor_Project.Handlers;
using System.Text.Json;

namespace Bachelor_Project
{
    public partial class DeviceForm : Form
    {
        public static DeviceForm Instance;

        private SensorReadingHelper _sensorReadingHelper;

        private string _previousFile = string.Empty;

        public static bool TempTestingFlag = true;

        public DeviceForm()
        {
            InitializeComponent();

            if (Instance == null) Instance = this;
            else this.Close();

            _sensorReadingHelper = MainForm.Instance.SensorReadingHelper;
            _sensorReadingHelper.OnSensorsChanged += ReinitializeSensorCharts;

            InitializeControlPanel();

            //SendTestData();

            this.VisibleChanged += (s, e) =>
            {
                ReinitializeSensorCharts();
            };
        }

        public void ReinitializeSensorCharts()
        {
            tableLayoutPanel1.Controls.Clear();

            tableLayoutPanel1.RowCount = _sensorReadingHelper.Sensors.Count / tableLayoutPanel1.ColumnCount + (_sensorReadingHelper.Sensors.Count % tableLayoutPanel1.ColumnCount == 0 ? 0 : 1);
            tableLayoutPanel1.Size = new Size(tableLayoutPanel1.Width, panel1.Size.Height * tableLayoutPanel1.RowCount);
            for (int i = 0; i < _sensorReadingHelper.Sensors.Count; i++)
            {
                tableLayoutPanel1.Controls.Add(new SensorChart(_sensorReadingHelper.Sensors[i]) { Dock = DockStyle.Fill, AutoSize = true }, i % 4, i / 4);
            }

            for (int i = 0; i < tableLayoutPanel1.RowCount - 1; i++)
            {
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent));
            }

            foreach (RowStyle rowStyle in tableLayoutPanel1.RowStyles)
            {
                rowStyle.SizeType = SizeType.Percent;
                rowStyle.Height = 1f / tableLayoutPanel1.RowStyles.Count;
            }

            foreach (SensorChart sensorChart in this.tableLayoutPanel1.Controls.OfType<SensorChart>().ToArray())
            {
                _sensorReadingHelper.AddSensorChart(sensorChart);
            }

        }

        private void InitializeControlPanel()
        {
            this.controlPanel1.OnPlayPauseClick += this.cyclogram1.Play;
            this.controlPanel1.OnStepForwardClick += this.cyclogram1.StepForward;
            this.controlPanel1.OnStepBackwardClick += this.cyclogram1.StepBackward;
            this.controlPanel1.OnRightEndClick += this.cyclogram1.SetRightEnd;
            this.controlPanel1.OnLeftEndClick += this.cyclogram1.SetLeftEnd;
        }

        public void ReinitializeCyclogram()
        {
            List<string> filePaths = Directory.GetFiles(@"Cyclograms", "*.*", SearchOption.AllDirectories)
            .Where(s => s.EndsWith(".json")).ToList();

            {
                List<string> toDelete = new List<string>();
                foreach (string item in comboBox_SelectCyclogram.Items)
                {
                    if (!filePaths.Contains(item))
                    {
                        toDelete.Add(item);
                    }
                }
                toDelete.ForEach(x => comboBox_SelectCyclogram.Items.Remove(x));
            }

            foreach (string filePath in filePaths)
            {
                string json = File.ReadAllText(filePath);

                JsonDocument doc = JsonDocument.Parse(json);
                JsonElement root = doc.RootElement;

                if (root.TryGetProperty("cyclogram", out JsonElement cyclogramProperty))
                {
                    if (cyclogramProperty.TryGetProperty("deviceScheme", out JsonElement deviceSchemeProperty))
                    {
                        if ((DeviceConnectionForm.Instance.SelectedDeviceScheme).Equals(deviceSchemeProperty.GetString()))
                        {
                            if (!comboBox_SelectCyclogram.Items.Contains(filePath))
                            {
                                comboBox_SelectCyclogram.Items.Add(filePath);
                            }

                            continue;
                        }
                    }
                }

                if (comboBox_SelectCyclogram.Items.Contains(filePath))
                {
                    comboBox_SelectCyclogram.Items.Remove(filePath);
                }
            }

            if (comboBox_SelectCyclogram.SelectedItem == null)
            {
                if (comboBox_SelectCyclogram.Items.Count != 0)
                {
                    comboBox_SelectCyclogram.SelectedIndex = 0;
                }
            }

            this.cyclogram1.OnComponentStatusChange += (c, s) =>
            {
                foreach (CompressorElement element in this.compressorDevice1.Layers[CompressorLayer.LayerType.Components].GetElements)
                {
                    if (element is CompressorComponent component)
                    {
                        if (component.Name.Equals((c as CyclogramComponentElement).Name))
                        {
                            if (Enum.TryParse((s as CyclogramStatusElement).Name, out CompressorComponent.ComponentStatus status))
                            {
                                component.Status = status;
                            }
                        }
                    }
                }

                this.compressorDevice1.Refresh();
            };

            this.cyclogram1.Refresh();
            this.cyclogram1.OnSingleExecutionEnd += () => { this.controlPanel1.PlayButtonStatus = false; };
        }

        public void ReinitializeCompressorDeviceScheme()
        {
            compressorDevice1.Alignment = CompressorDevice.DeviceAlignment.Middle;

            //this.cyclogram1.Components.Clear();

            string json = File.ReadAllText(DeviceConnectionForm.Instance.SelectedDeviceScheme);
            JSON_Handler.InitializeCompressorDeviceWithJson(this.compressorDevice1, json);

            foreach (CompressorComponent component in this.compressorDevice1.GetComponentsArray())
            {
                List<CyclogramStatusElement> statuses = new List<CyclogramStatusElement>();

                foreach (CompressorComponent.ComponentStatus statusEnum in Enum.GetValues(typeof(CompressorComponent.ComponentStatus)))
                {
                    statuses.Add(new CyclogramStatusElement() { Name = statusEnum.ToString() });
                }

                this.cyclogram1.Components.Add(new CyclogramComponentElement() { Name = component.Name, Statuses = statuses });
            }

        }

        public async void SendTestData()
        {
            TempTestingFlag = true;

            while (true && TempTestingFlag && comboBox_SelectCyclogram.SelectedItem != null)
            {
                if(TempTestingFlag)
                {
                    string name;
                    float value;

                    name = "d1";
                    value = (float)new Random().NextDouble() * 3 + 10;
                    _sensorReadingHelper.AddReadingToTheSensor(name, value);
                    DataLogger.SaveAndAppendLogData($"Logs\\logfile_{DeviceConnectionForm.Instance.SelectedSensorSet.Split('\\').Last()}_{comboBox_SelectCyclogram.SelectedItem.ToString().Split('\\').Last()}", $"{name}_{value}_{cyclogram1.Active}_{cyclogram1.CurrentTimeStamp}");
                    await Task.Delay(1);

                    name = "d2";
                    value = (float)new Random().NextDouble() * 15 + 25;
                    _sensorReadingHelper.AddReadingToTheSensor(name, value);
                    DataLogger.SaveAndAppendLogData($"Logs\\logfile_{DeviceConnectionForm.Instance.SelectedSensorSet.Split('\\').Last()}_{comboBox_SelectCyclogram.SelectedItem.ToString().Split('\\').Last()}", $"{name}_{value}_{cyclogram1.Active}_{cyclogram1.CurrentTimeStamp}");
                    await Task.Delay(1);

                    name = "d3";
                    value = (float)new Random().NextDouble() * 5 + 15;
                    _sensorReadingHelper.AddReadingToTheSensor(name, value);
                    DataLogger.SaveAndAppendLogData($"Logs\\logfile_{DeviceConnectionForm.Instance.SelectedSensorSet.Split('\\').Last()}_{comboBox_SelectCyclogram.SelectedItem.ToString().Split('\\').Last()}", $"{name}_{value}_{cyclogram1.Active}_{cyclogram1.CurrentTimeStamp}");
                    await Task.Delay(1);

                    name = "d4";
                    value = (float)new Random().NextDouble() * 10 + 45;
                    _sensorReadingHelper.AddReadingToTheSensor(name, value);
                    DataLogger.SaveAndAppendLogData($"Logs\\logfile_{DeviceConnectionForm.Instance.SelectedSensorSet.Split('\\').Last()}_{comboBox_SelectCyclogram.SelectedItem.ToString().Split('\\').Last()}", $"{name}_{value}_{cyclogram1.Active}_{cyclogram1.CurrentTimeStamp}");
                    await Task.Delay(1);

                    name = "d5";
                    value = (float)new Random().NextDouble() * 200 + 100;
                    _sensorReadingHelper.AddReadingToTheSensor(name, value);
                    DataLogger.SaveAndAppendLogData($"Logs\\logfile_{DeviceConnectionForm.Instance.SelectedSensorSet.Split('\\').Last()}_{comboBox_SelectCyclogram.SelectedItem.ToString().Split('\\').Last()}", $"{name}_{value}_{cyclogram1.Active}_{cyclogram1.CurrentTimeStamp}");
                    await Task.Delay(1);

                    name = "d6";
                    value = (float)new Random().NextDouble() * 500 + 1000;
                    _sensorReadingHelper.AddReadingToTheSensor(name, value);
                    DataLogger.SaveAndAppendLogData($"Logs\\logfile_{DeviceConnectionForm.Instance.SelectedSensorSet.Split('\\').Last()}_{comboBox_SelectCyclogram.SelectedItem.ToString().Split('\\').Last()}", $"{name}_{value}_{cyclogram1.Active}_{cyclogram1.CurrentTimeStamp}");

                }


                await Task.Delay(100);
            }
        }

        private void comboBox_CyclogramList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (_previousFile.Equals((string)this.comboBox_SelectCyclogram.SelectedItem)) return;

            SetCyclogramData((string)this.comboBox_SelectCyclogram.SelectedItem);

            //_previousFile = (string)this.comboBox_SelectCyclogram.SelectedItem;
        }

        private void SetCyclogramData(string fileName)
        {
            if (!File.Exists(fileName)) return;

            this.cyclogram1.Stop();

            string json = File.ReadAllText(fileName);

            JSON_Handler.InitializeCyclogramWithJson(cyclogram1, json);

            this.cyclogram1.Refresh();
        }

        private void tableLayoutPanel1_Resize(object sender, EventArgs e)
        {
            tableLayoutPanel1.Size = new Size(tableLayoutPanel1.Width, panel1.Size.Height * tableLayoutPanel1.RowCount);
        }

        public void Clear()
        {
            TempTestingFlag = false;

            this.cyclogram1.Stop();
        }
    }
}
