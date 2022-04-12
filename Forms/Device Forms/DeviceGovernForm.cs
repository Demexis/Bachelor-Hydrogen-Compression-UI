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
    public partial class DeviceGovernForm : Form
    {
        public static DeviceGovernForm Instance;

        public bool EnabledChannels = false;

        public DeviceGovernForm()
        {
            InitializeComponent();

            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                this.Close();
            }
        }

        private void button_ActivateChannels_Click(object sender, EventArgs e)
        {
            COM_Handler.MainSerialPort.WriteLine(!EnabledChannels ? "a" : "b");
            button_ActivateChannels.Text = EnabledChannels ? "Activate Channels" : "Deactivate Channels";
            EnabledChannels = !EnabledChannels;
        }
    }
}
