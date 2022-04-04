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

namespace Bachelor_Project_Hydrogen_Compression_WinForms
{
    public partial class DeviceForm : Form
    {
        public DeviceForm()
        {
            InitializeComponent();

            string json = File.ReadAllText("testCyclogram.json");

            JSON_Loader.InitializeCyclogramWithJson(cyclogram1, json);

            cyclogram1.PlayMode = Cyclogram.CyclogramPlayMode.Loop;

            compressorDevice1.InitializeRoadMap(
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
                "000101000");

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

            this.cyclogram1.OnComponentStatusChange += SetComponentStatus;

            this.controlPanel1.OnPlayPauseClick += this.cyclogram1.Play;
            this.controlPanel1.OnStepForwardClick += this.cyclogram1.StepForward;
            this.controlPanel1.OnStepBackwardClick += this.cyclogram1.StepBackward;
            this.controlPanel1.OnRightEndClick += this.cyclogram1.SetRightEnd;
            this.controlPanel1.OnLeftEndClick += this.cyclogram1.SetLeftEnd;
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
    }
}
