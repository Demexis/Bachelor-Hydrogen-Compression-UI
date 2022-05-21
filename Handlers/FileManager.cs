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
        public enum JsonFileStructure { Sensors, DeviceSchemes, Cyclograms }

        public static string[] GetFiles(JsonFileStructure jsonFileStructure)
        {
            string folderName = jsonFileStructure.ToString();

            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(folderName);
            }

            return Directory.GetFiles(folderName, "*.*", SearchOption.AllDirectories)
            .Where(s => s.EndsWith(".json")).ToArray();
        }

    }
}
