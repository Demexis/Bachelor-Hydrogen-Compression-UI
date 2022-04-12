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
using Bachelor_Project_Hydrogen_Compression_WinForms.UserControls.Device;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using CompressorComponent = Bachelor_Project_Hydrogen_Compression_WinForms.UserControls.Device.CompressorComponent;
using ComponentType = Bachelor_Project_Hydrogen_Compression_WinForms.UserControls.Device.CompressorComponent.ComponentType;
using ComponentOrientation = Bachelor_Project_Hydrogen_Compression_WinForms.UserControls.Device.CompressorComponent.ComponentOrientation;
using Bachelor_Project_Hydrogen_Compression_WinForms.UserControls;
using Bachelor_Project_Hydrogen_Compression_WinForms.Miscellaneous;
using Bachelor_Project_Hydrogen_Compression_WinForms.Handlers;

namespace Bachelor_Project_Hydrogen_Compression_WinForms
{
    public partial class DeviceForm : Form
    {
        private SensorReadingHelper _sensorReadingHelper = new SensorReadingHelper();

        private string _previousFile = string.Empty;

        public static bool TempTestingFlag = true;

        public DeviceForm()
        {
            InitializeComponent();

            InitializeSensors();

            InitializeCompressorDeviceScheme();

            InitializeCyclogram();

            InitializeControlPanel();


            SendTestData();
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

        private void InitializeControlPanel()
        {
            this.controlPanel1.OnPlayPauseClick += this.cyclogram1.Play;
            this.controlPanel1.OnStepForwardClick += this.cyclogram1.StepForward;
            this.controlPanel1.OnStepBackwardClick += this.cyclogram1.StepBackward;
            this.controlPanel1.OnRightEndClick += this.cyclogram1.SetRightEnd;
            this.controlPanel1.OnLeftEndClick += this.cyclogram1.SetLeftEnd;
        }

        private void InitializeCyclogram()
        {
            foreach (string file in Directory.GetFiles("Cyclograms"))
            {
                this.comboBox_CyclogramList.Items.Add(file);
            }

            if (this.comboBox_CyclogramList.Items.Count > 0)
            {
                this.comboBox_CyclogramList.SelectedIndex = 0;
            }

            this.cyclogram1.OnComponentStatusChange += SetComponentStatus;
            this.cyclogram1.OnSingleExecutionEnd += () => { this.controlPanel1.PlayButtonStatus = false; };
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

        private async void SendTestData()
        {
            while(true && TempTestingFlag && comboBox_CyclogramList.SelectedItem != null)
            {
                if(TempTestingFlag)
                {
                    string name;
                    float value;

                    name = "d1";
                    value = (float)new Random().NextDouble() * 3 + 10;
                    _sensorReadingHelper.AddReadingToTheSensor(name, value);
                    DataLogger.SaveAndAppendLogData($"Logs\\logfile_{comboBox_CyclogramList.SelectedItem.ToString().Split('\\').Last()}", $"{name}_{value}_{cyclogram1.Active}_{cyclogram1.CurrentTimeStamp}");
                    await Task.Delay(1);

                    name = "d2";
                    value = (float)new Random().NextDouble() * 15 + 25;
                    _sensorReadingHelper.AddReadingToTheSensor(name, value);
                    DataLogger.SaveAndAppendLogData($"Logs\\logfile_{comboBox_CyclogramList.SelectedItem.ToString().Split('\\').Last()}", $"{name}_{value}_{cyclogram1.Active}_{cyclogram1.CurrentTimeStamp}");
                    await Task.Delay(1);

                    name = "d3";
                    value = (float)new Random().NextDouble() * 5 + 15;
                    _sensorReadingHelper.AddReadingToTheSensor(name, value);
                    DataLogger.SaveAndAppendLogData($"Logs\\logfile_{comboBox_CyclogramList.SelectedItem.ToString().Split('\\').Last()}", $"{name}_{value}_{cyclogram1.Active}_{cyclogram1.CurrentTimeStamp}");
                    await Task.Delay(1);

                    name = "d4";
                    value = (float)new Random().NextDouble() * 10 + 45;
                    _sensorReadingHelper.AddReadingToTheSensor(name, value);
                    DataLogger.SaveAndAppendLogData($"Logs\\logfile_{comboBox_CyclogramList.SelectedItem.ToString().Split('\\').Last()}", $"{name}_{value}_{cyclogram1.Active}_{cyclogram1.CurrentTimeStamp}");
                    await Task.Delay(1);

                    name = "d5";
                    value = (float)new Random().NextDouble() * 200 + 100;
                    _sensorReadingHelper.AddReadingToTheSensor(name, value);
                    DataLogger.SaveAndAppendLogData($"Logs\\logfile_{comboBox_CyclogramList.SelectedItem.ToString().Split('\\').Last()}", $"{name}_{value}_{cyclogram1.Active}_{cyclogram1.CurrentTimeStamp}");
                    await Task.Delay(1);

                    name = "d6";
                    value = (float)new Random().NextDouble() * 500 + 1000;
                    _sensorReadingHelper.AddReadingToTheSensor(name, value);
                    DataLogger.SaveAndAppendLogData($"Logs\\logfile_{comboBox_CyclogramList.SelectedItem.ToString().Split('\\').Last()}", $"{name}_{value}_{cyclogram1.Active}_{cyclogram1.CurrentTimeStamp}");

                }


                await Task.Delay(100);
            }
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

        private void comboBox_CyclogramList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_previousFile.Equals((string)this.comboBox_CyclogramList.SelectedItem)) return;

            SetCyclogramData((string)this.comboBox_CyclogramList.SelectedItem);

            _previousFile = (string)this.comboBox_CyclogramList.SelectedItem;
        }

        private void SetCyclogramData(string fileName)
        {
            if (!File.Exists(fileName)) return;

            this.cyclogram1.Stop();
            this.cyclogram1.Clear();

            string json = File.ReadAllText(fileName);

            JSON_Handler.InitializeCyclogramWithJson(cyclogram1, json);
        }

        private void tableLayoutPanel1_Resize(object sender, EventArgs e)
        {
            tableLayoutPanel1.Size = new Size(tableLayoutPanel1.Width, panel1.Size.Height * tableLayoutPanel1.RowCount);
        }
    }
}
