using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor_Project.Handlers
{
    public static class FileManager
    {
        public static string[] GetSensorFiles()
        {
            if (!Directory.Exists("Sensors"))
            {
                Directory.CreateDirectory("Sensors");
            }

            return Directory.GetFiles("Sensors", "*.*", SearchOption.AllDirectories)
            .Where(s => s.EndsWith(".json")).ToArray();
        }

        public static string[] GetDeviceSchemeFiles()
        {
            if (!Directory.Exists("DeviceSchemes"))
            {
                Directory.CreateDirectory("DeviceSchemes");
            }

            return Directory.GetFiles("DeviceSchemes", "*.*", SearchOption.AllDirectories)
            .Where(s => s.EndsWith(".json")).ToArray();
        }

    }
}
