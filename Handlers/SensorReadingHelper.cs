using Bachelor_Project.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor_Project.Handlers
{
    public class SensorReadingHelper
    {
        public List<Sensor> Sensors = new List<Sensor>();

        private List<SensorChart> _sensorCharts = new List<SensorChart>();

        public Action OnSensorsChanged { get; set; }

        public void AddSensorChart(SensorChart sensorChart)
        {
            _sensorCharts.Add(sensorChart);
        }


        public bool AddReadingToTheSensor(string name, float value)
        {
            bool result = false;

            if (Sensors.Exists((x) => x.Name.Equals(name)))
            {
                Sensors.First((x) => x.Name.Equals(name))?.AddReading(value);
                result = true;
            }

            UpdateCharts();

            return result;
        }

        public void UpdateCharts()
        {
            foreach(SensorChart sensorChart in _sensorCharts)
            {
                sensorChart.RefreshChart();
            }
        }

    }
}
