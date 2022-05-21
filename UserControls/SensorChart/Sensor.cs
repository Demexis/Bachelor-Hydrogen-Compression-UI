using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor_Project.UserControls
{
    public class Sensor
    {
        public string Name { get; set; }

        public enum SensorType { Temperature, Pressure, Volume, Unknown }
        public SensorType Type { get; set; }

        public const int DefaultReadingsCount = 600;
        public int MaxReadingsCount { get; set; } = DefaultReadingsCount;

        public List<float> Readings = new List<float>();

        public float MinimumValue { get; set; }
        public float MaximumValue { get; set; }


        public Sensor(string name, SensorType type)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("Name is null or white space.");
            }

            this.Name = name;
            this.Type = type;
        }

        public Sensor(string name, SensorType type, int maxReadingsCount)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("Name is null or white space.");
            }

            if(maxReadingsCount <= 0)
            {
                throw new Exception("Max Readings should be greater than zero.");
            }

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

        public override string ToString()
        {
            return this.Name;
        }
    }
}
