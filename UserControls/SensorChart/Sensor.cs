using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor_Project_Hydrogen_Compression_WinForms.UserControls
{
    public class Sensor
    {
        public enum SensorType { Temperature, Pressure, Volume, Unknown }

        public SensorType Type { get; set; }

        public string Name { get; set; }

        public List<float> Readings = new List<float>();

        public int MaxReadingsCount { get; set; } = 600;


        public Sensor(string name, SensorType type)
        {
            this.Name = name;
            this.Type = type;
        }

        public Sensor(string name, SensorType type, int maxReadingsCount)
        {
            this.Name = name;
            this.Type = type;
            this.MaxReadingsCount = maxReadingsCount;
        }

        public void AddReading(float value)
        {
            if(Readings.Count >= MaxReadingsCount)
            {
                Readings.RemoveAt(0);
            }

            Readings.Add(value);

            //DataLogger.SaveAndAppendLogData("log_file", $"{Name} - {value}");
        }
    }
}
