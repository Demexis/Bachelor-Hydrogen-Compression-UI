using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor_Project_Hydrogen_Compression_WinForms
{
    public static class COM_Handler
    {
        public static bool ConnectedToPort { get; set; }

        public static SerialPort MainSerialPort { get; set; }

        public static Action<string> SerialPortDataSender;

        
    }
}
