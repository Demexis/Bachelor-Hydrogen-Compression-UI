using Bachelor_Project.Handlers;
using Bachelor_Project.UserControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CompressorComponent = Bachelor_Project.UserControls.Device.CompressorComponent;
using ComponentType = Bachelor_Project.UserControls.Device.CompressorComponent.ComponentType;
using ComponentOrientation = Bachelor_Project.UserControls.Device.CompressorComponent.ComponentOrientation;
using Bachelor_Project.UserControls.Device;
using Bachelor_Project.Forms.Options_Forms;
using Bachelor_Project.Extensions;

namespace Bachelor_Project
{
    public partial class DeviceDiagnosticForm : Form
    {
        public DeviceDiagnosticForm Instance;

        public List<(string timeInfo, string sensorName, float sensorValue, bool cyclogramActiveStatus, int timeStampValue)> logRecords = new List<(string timeInfo, string sensorName, float sensorValue, bool cyclogramActiveStatus, int timeStampValue)>();
        private SensorReadingHelper _sensorReadingHelper = new SensorReadingHelper();

        public DeviceDiagnosticForm()
        {
            InitializeComponent();

            if (Instance == null) Instance = this;
            else this.Close();


            this.VisibleChanged += (s, e) =>
            {
                comboBox_LogFilesList.Items.Clear();

                foreach (string fileName in Directory.GetFiles("Logs"))
                {
                    comboBox_LogFilesList.Items.Add(fileName);
                }

                if (comboBox_LogFilesList.Items.Count > 0) comboBox_LogFilesList.SelectedItem = comboBox_LogFilesList.Items[0];
            };

            this.cyclogram1.CyclogramName = "Cyclogram";
            this.cyclogram1.OnComponentStatusChange += (c, s) =>
            {
                foreach (CompressorElement element in this.compressorDevice1.Layers[CompressorLayer.LayerTypeEnum.Components].GetElements)
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

            AppearanceOptionsForm.OnColorPaletteChange += SetColorPaletteForControls;
        }

        private void RecreateSensorCharts()
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

            //Console.WriteLine("TEMP1: " + _sensorReadingHelper.Sensors.Count);
            //Console.WriteLine("TEMP3: " + tableLayoutPanel1.RowCount);
            //foreach (RowStyle rowStyle in tableLayoutPanel1.RowStyles)
            //{
            //    Console.WriteLine("TEMP2: " + rowStyle.Height);
            //    //rowStyle.Height = 1f / tableLayoutPanel1.RowStyles.Count;
            //}
        }

        private void button_Open_Click(object sender, EventArgs e)
        {
            if(comboBox_LogFilesList.SelectedItem != null)
            {
                try
                {
                    logRecords.Clear();

                    string fileName = comboBox_LogFilesList.SelectedItem.ToString();

                    if (!File.Exists(fileName)) throw new Exception("File doesn't exist.");

                    string sensorSetFileName = "Sensors\\" + fileName.Split('_')[1];

                    if (!File.Exists(sensorSetFileName)) throw new Exception("Sensor file doesn't exist.");

                    string jsonSensorSet = File.ReadAllText(sensorSetFileName);

                    _sensorReadingHelper.Sensors.Clear();
                    JSON_Handler.InitializeSensorReaderWithJson(_sensorReadingHelper, jsonSensorSet);
                    RecreateSensorCharts();


                    string cyclogramFileName = "Cyclograms\\" + fileName.Split('_')[2] + "_" + fileName.Split('_')[3];

                    if (!File.Exists(cyclogramFileName)) throw new Exception("Cyclogram file doesn't exist.");

                    this.cyclogram1.Stop();

                    string jsonCyclogram = File.ReadAllText(cyclogramFileName);

                    this.cyclogram1.Components.Clear();
                    JSON_Handler.InitializeCyclogramWithJson(cyclogram1, jsonCyclogram);
                    this.cyclogram1.Refresh();


                    string deviceSchemeFileName = "DeviceSchemes\\" + fileName.Split('_')[2] + ".json";

                    if (!File.Exists(deviceSchemeFileName)) throw new Exception("DeviceScheme file doesn't exist.");


                    string jsonDeviceScheme = File.ReadAllText(deviceSchemeFileName);

                    JSON_Handler.InitializeCompressorDeviceWithJson(compressorDevice1, jsonDeviceScheme);


                    // Initialize components and statuses of a cyclogram
                    foreach (CompressorComponent component in this.compressorDevice1.GetComponentsArray())
                    {
                        List<CyclogramStatusElement> statuses = new List<CyclogramStatusElement>();

                        foreach (CompressorComponent.ComponentStatus statusEnum in Enum.GetValues(typeof(CompressorComponent.ComponentStatus)))
                        {
                            statuses.Add(new CyclogramStatusElement() { Name = statusEnum.ToString() });
                        }

                        this.cyclogram1.Components.Add(new CyclogramComponentElement() { Name = component.Name, Statuses = statuses });
                    }

                    this.cyclogram1.Refresh();


                    using (StreamReader file = new StreamReader(fileName))
                    {
                        while (!file.EndOfStream)
                        {
                            string line = file.ReadLine();

                            string[] logParts = line.Split(':');

                            line = logParts.Last();

                            string timeInfo = string.Empty;

                            for (int i = 0; i < logParts.Length - 1; i++)
                            {
                                timeInfo += logParts[i];
                            }

                            // Remove space
                            line = line.Remove(0, 1);

                            // Remove '(' and space
                            line = line.Remove(0, 2);

                            // Remove space and ')'
                            line = line.Remove(line.Length - 2, 2);

                            string[] args = line.Split('_');

                            logRecords.Add((timeInfo, args[0], float.Parse(args[1]), bool.Parse(args[2]), int.Parse(args[3])));
                        }

                    }

                    trackBar1.Value = 0;
                    trackBar1.Maximum = logRecords.Count - 1;

                    textBox1.Text = $"{logRecords[0].timeInfo} : ( {logRecords[0].sensorName}, {logRecords[0].sensorValue}, {logRecords[0].cyclogramActiveStatus}, {logRecords[0].timeStampValue} )";
                    
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

        }

        private void tableLayoutPanel1_Resize(object sender, EventArgs e)
        {
            tableLayoutPanel1.Size = new Size(tableLayoutPanel1.Width, panel1.Size.Height * tableLayoutPanel1.RowCount);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int trackbarValue = trackBar1.Value;

            (string timeInfo, string sensorName, float sensorValue, bool cyclogramActiveStatus, int timeStampValue) p = logRecords[trackbarValue];

            textBox1.Text = $"{p.timeInfo} : ( {p.sensorName}, {p.sensorValue}, {p.cyclogramActiveStatus}, {p.timeStampValue} )";

            if (p.cyclogramActiveStatus)
            {
                cyclogram1.SetCurrentTimeStamp(p.timeStampValue);
            }
            else if (p.timeStampValue != cyclogram1.CurrentTimeStamp)
            {
                cyclogram1.SetCurrentTimeStamp(p.timeStampValue); // TODO: Silent Time Stamp
            }

            Dictionary<string, List<float>> sensors = new Dictionary<string, List<float>>();

            foreach((string timeInfo, string sensorName, float sensorValue, bool cyclogramActiveStatus, int timeStampValue) p2 in logRecords.GetRange(0, trackbarValue + 1))
            {
                if(sensors.ContainsKey(p2.sensorName))
                {
                    if (sensors[p2.sensorName].Count >= _sensorReadingHelper.Sensors.First((x) => x.Name.Equals(p2.sensorName)).MaxReadingsCount)
                    {
                        sensors[p2.sensorName].RemoveAt(0);
                    }
                    sensors[p2.sensorName].Add(p2.sensorValue);
                }
                else
                {
                    sensors.Add(p2.sensorName, new List<float>() { p2.sensorValue });
                }
            }

            foreach(Sensor sensor in _sensorReadingHelper.Sensors)
            {
                if(sensors.ContainsKey(sensor.Name))
                {
                    sensor.Readings = sensors[sensor.Name];
                }
            }

            _sensorReadingHelper.UpdateCharts();
        }

        public void SetColorPaletteForControls(Dictionary<FormColorVariant, Color> colorPalette)
        {
            this.BackColor = colorPalette[FormColorVariant.DarkFirst];
            this.compressorDevice1.BackColor = colorPalette[FormColorVariant.DarkSecond];

            foreach (Button button in this.GetAllControlsRecusrvive<Button>())
            {
                button.BackColor = colorPalette[FormColorVariant.BrightSecond];
                button.ForeColor = colorPalette[FormColorVariant.TextColorFirst];

                button.FlatAppearance.MouseDownBackColor = colorPalette[FormColorVariant.ButtonMouseDown];
                button.FlatAppearance.MouseOverBackColor = colorPalette[FormColorVariant.ButtonMouseOver];

            }

        }
    }
}
