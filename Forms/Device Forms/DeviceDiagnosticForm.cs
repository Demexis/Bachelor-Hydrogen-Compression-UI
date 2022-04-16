using Bachelor_Project_Hydrogen_Compression_WinForms.Handlers;
using Bachelor_Project_Hydrogen_Compression_WinForms.UserControls;
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

using CompressorComponent = Bachelor_Project_Hydrogen_Compression_WinForms.UserControls.Device.CompressorComponent;
using ComponentType = Bachelor_Project_Hydrogen_Compression_WinForms.UserControls.Device.CompressorComponent.ComponentType;
using ComponentOrientation = Bachelor_Project_Hydrogen_Compression_WinForms.UserControls.Device.CompressorComponent.ComponentOrientation;
using Bachelor_Project_Hydrogen_Compression_WinForms.UserControls.Device;

namespace Bachelor_Project_Hydrogen_Compression_WinForms
{
    public partial class DeviceDiagnosticForm : Form
    {
        public List<(string timeInfo, string sensorName, float sensorValue, bool cyclogramActiveStatus, int timeStampValue)> logRecords = new List<(string timeInfo, string sensorName, float sensorValue, bool cyclogramActiveStatus, int timeStampValue)>();
        private SensorReadingHelper _sensorReadingHelper = new SensorReadingHelper();

        public DeviceDiagnosticForm()
        {
            InitializeComponent();

            COM_Handler.SerialPortDataSender += Update;

            InitializeSensors();

            InitializeCompressorDeviceScheme();

            foreach (string fileName in Directory.GetFiles("Logs"))
            {
                comboBox_LogFilesList.Items.Add(fileName);
            }

            if (comboBox_LogFilesList.Items.Count > 0) comboBox_LogFilesList.SelectedItem = comboBox_LogFilesList.Items[0];

            this.cyclogram1.OnComponentStatusChange += SetComponentStatus;
        }

        private void SetComponentStatus(string componentWord, string statusWord)
        {
            CompressorComponent.ComponentStatus status;

            if (statusWord.Equals("active"))
            {
                status = CompressorComponent.ComponentStatus.Active;

                compressorDevice1.SetComponentStatus(componentWord, status);
            }
            else if (statusWord.Equals("inactive"))
            {
                status = CompressorComponent.ComponentStatus.Inactive;

                compressorDevice1.SetComponentStatus(componentWord, status);
            }
        }

        delegate void SetUpdateCallback(string text);

        public void Update(string data)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            //if (this.chart_TemperatureGraph.InvokeRequired)
            //{
            //    SetUpdateCallback d = new SetUpdateCallback(Update);
            //    this.Invoke(d, new object[] { data });
            //}
            //else
            //{
            //    JObject json = JsonConvert.DeserializeObject<JObject>(data);

            //    try
            //    {
            //        this.chart1.Series[0].Points.AddY(int.Parse(json["arduino"]["termistor"].ToString()));
            //    }
            //    catch (Exception)
            //    {
            //        // ...
            //    }

            //    //string[] values = data.Split('_');

            //    //if (values.Length >= 1) this.chart_TemperatureGraph.Series[0].Points.AddY(values[0]);
            //    //if (DeviceGovernForm.Instance.EnabledChannels)
            //    //{
            //    //    if (values.Length >= 2) this.chart1.Series[0].Points.AddY(values[1]);
            //    //    if (values.Length >= 3) this.chart2.Series[0].Points.AddY(values[2]);
            //    //    if (values.Length >= 4) this.chart3.Series[0].Points.AddY(values[3]);
            //    //}
            //}

            Console.WriteLine(data);
        }

        private void InitializeSensors()
        {
            string sensorsConfigFile = @"Configs\sensors.json";

            if (!File.Exists(sensorsConfigFile))
            {
                MessageBox.Show($"Can't find \"{sensorsConfigFile}\".");
            }
            else
            {
                string json = File.ReadAllText(@"Configs\sensors.json");

                JSON_Handler.InitializeSensorReaderWithJson(_sensorReadingHelper, json);
            }

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

        private void InitializeCompressorDeviceScheme()
        {
            compressorDevice1.InitializeRoadmap(new Size(9, 14));

            compressorDevice1.InitializePipes(
                "000010000\n" +
                "001111110\n" +
                "001010000\n" +
                "001011100\n" +
                "001000100\n" +
                "111111111\n" +
                "010010010\n" +
                "010010010\n" +
                "010000010\n" +
                "010000010\n" +
                "011111110\n" +
                "010101010\n" +
                "011111110\n" +
                "000101000",
                CompressorPipe.PipeType.Gas);

            compressorDevice1.InitializeDeviceComponents(
                "XXXXrXXXX\n" +
                "XXX1X2XXX\n" +
                "XXXXXXXXX\n" +
                "XXXXX3XXX\n" +
                "XXXXXXXXX\n" +
                "XX4XX5XXX\n" +
                "XXXXXXXXX\n" +
                "XqXXwXXeX\n" +
                "XXXXXXXXX\n" +
                "XaXXXXXsX\n" +
                "XX6XXX7XX\n" +
                "X9XpX8X0X\n" +
                "XXdXXXfXX\n" +
                "XXXXXXXXX",

                new Dictionary<char, CompressorComponent>()
                {
                    ['1'] = new CompressorComponent(
                        "s1",
                        ComponentType.Valve,
                        ComponentOrientation.Horizontal, CompressorComponent.ComponentStatus.Active),
                    ['2'] = new CompressorComponent(
                        "s2",
                        ComponentType.Valve,
                        ComponentOrientation.Horizontal),
                    ['3'] = new CompressorComponent(
                        "s3",
                        ComponentType.Valve,
                        ComponentOrientation.Horizontal),
                    ['4'] = new CompressorComponent(
                        "s4",
                        ComponentType.Valve,
                        ComponentOrientation.Horizontal),
                    ['5'] = new CompressorComponent(
                        "s5",
                        ComponentType.Valve,
                        ComponentOrientation.Horizontal),
                    ['6'] = new CompressorComponent(
                        "s6",
                        ComponentType.Valve,
                        ComponentOrientation.Horizontal),
                    ['7'] = new CompressorComponent(
                        "s7",
                        ComponentType.Valve,
                        ComponentOrientation.Horizontal),
                    ['8'] = new CompressorComponent(
                        "s8",
                        ComponentType.Valve,
                        ComponentOrientation.Vertical),
                    ['9'] = new CompressorComponent(
                        "s9",
                        ComponentType.Valve,
                        ComponentOrientation.Vertical),
                    ['0'] = new CompressorComponent(
                        "s10",
                        ComponentType.Valve,
                        ComponentOrientation.Vertical),
                    ['q'] = new CompressorComponent(
                        "v1",
                        ComponentType.Reservoir,
                        ComponentOrientation.Vertical,
                        CompressorComponent.ComponentStatus.Active, 0.3f),
                    ['w'] = new CompressorComponent(
                        "h1",
                        ComponentType.Reservoir,
                        ComponentOrientation.Vertical,
                        CompressorComponent.ComponentStatus.Active, 0.5f),
                    ['e'] = new CompressorComponent(
                        "v2",
                        ComponentType.Reservoir,
                        ComponentOrientation.Vertical,
                        CompressorComponent.ComponentStatus.Active, 0.8f),
                    ['r'] = new CompressorComponent(
                        "buffer",
                        ComponentType.Reservoir,
                        ComponentOrientation.Vertical,
                        CompressorComponent.ComponentStatus.Active, 1f),
                    ['p'] = new CompressorComponent(
                        "p1",
                        ComponentType.Pump,
                        ComponentOrientation.Vertical),
                    ['a'] = new CompressorComponent(
                        "d1",
                        ComponentType.CounterTrigger,
                        ComponentOrientation.Vertical),
                    ['s'] = new CompressorComponent(
                        "d2",
                        ComponentType.CounterTrigger,
                        ComponentOrientation.Vertical),
                    ['d'] = new CompressorComponent(
                        "d3",
                        ComponentType.OpticalSensor,
                        ComponentOrientation.Vertical),
                    ['f'] = new CompressorComponent(
                        "d4",
                        ComponentType.OpticalSensor,
                        ComponentOrientation.Vertical)
                });
        }


        private void button_Open_Click(object sender, EventArgs e)
        {
            if(comboBox_LogFilesList.SelectedItem != null)
            {
                logRecords.Clear();

                string fileName = comboBox_LogFilesList.SelectedItem.ToString();

                if (File.Exists(fileName))
                {
                    string cyclogramFileName = "Cyclograms\\" + fileName.Split('_')[1];

                    if (!File.Exists(cyclogramFileName)) return;

                    this.cyclogram1.Stop();
                    this.cyclogram1.Clear();

                    string json = File.ReadAllText(cyclogramFileName);

                    JSON_Handler.InitializeCyclogramWithJson(cyclogram1, json);

                    using (StreamReader file = new StreamReader(fileName))
                    {
                        while(!file.EndOfStream)
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
                else
                {
                    MessageBox.Show("File doesn't exist.");
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
                    if(sensors[p2.sensorName].Count >= _sensorReadingHelper.Sensors.First((x) => x.Name.Equals(p2.sensorName)).MaxReadingsCount)
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
    }
}
