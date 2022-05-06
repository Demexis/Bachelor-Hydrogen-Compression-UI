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

        public Action OnSensorsChanged;

        public void AddSensorChart(SensorChart sensorChart)
        {
            _sensorCharts.Add(sensorChart);
        }


        public void AddReadingToTheSensor(string name, float value)
        {
            if(Sensors.Exists((x) => x.Name.Equals(name))) Sensors.First((x) => x.Name.Equals(name))?.AddReading(value);


            UpdateCharts();
        }

        public void UpdateCharts()
        {

            foreach(SensorChart sensorChart in _sensorCharts)
            {
                sensorChart.RefreshChart();
            }

            //List<Sensor> displayedSensors = new List<Sensor>();

            ////Console.WriteLine(this.tableLayoutPanel1.Controls.OfType<SensorChart>().Count());

            //foreach (SensorChart sensorChart in _sensorCharts)
            //{
            //    sensorChart.SensorChartUpdate();

            //    if (sensorChart.SelectedSensor != null)
            //    {
            //        displayedSensors.Add(sensorChart.SelectedSensor);
            //    }
            //}

            //foreach (SensorChart sensorChart in _sensorCharts)
            //{
            //    if (sensorChart.SelectedSensor == null)
            //    {
            //        foreach (Sensor sensor in Sensors)
            //        {
            //            if (!displayedSensors.Contains(sensor))
            //            {
            //                displayedSensors.Add(sensor);

            //                sensorChart.SelectedSensor = sensor.Name;
            //            }
            //        }

            //        displayedSensors.Add(sensorChart.SelectedSensor);
            //    }
            //}
        }

    }
}
