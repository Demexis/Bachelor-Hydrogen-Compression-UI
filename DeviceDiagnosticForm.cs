using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bachelor_Project_Hydrogen_Compression_WinForms
{
    public partial class DeviceDiagnosticForm : Form
    {
        public DeviceDiagnosticForm()
        {
            InitializeComponent();

            MainForm.Instance.SerialPortDataSender += Update;
        }

        delegate void SetUpdateCallback(string text);

        public void Update(string data)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.chart_TemperatureGraph.InvokeRequired)
            {
                SetUpdateCallback d = new SetUpdateCallback(Update);
                this.Invoke(d, new object[] { data });
            }
            else
            {
                string[] values = data.Split('_');

                if (values.Length >= 1) this.chart_TemperatureGraph.Series[0].Points.AddY(values[0]);
                if (DeviceGovernForm.Instance.EnabledChannels)
                {
                    if (values.Length >= 2) this.chart1.Series[0].Points.AddY(values[1]);
                    if (values.Length >= 3) this.chart2.Series[0].Points.AddY(values[2]);
                    if (values.Length >= 4) this.chart3.Series[0].Points.AddY(values[3]);
                }
            }

            Console.WriteLine(data);
        }
    }
}
