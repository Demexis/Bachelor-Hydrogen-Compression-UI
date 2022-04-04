using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor_Project_Hydrogen_Compression_WinForms.UserControls.Device
{
    public static class CompressorDeviceRules
    {
        public static Dictionary<string, Image> ValveImages = new Dictionary<string, Image>()
        {
            ["HorizontalDisabled"] = Properties.Resources.valve_horizontal,
            ["HorizontalActive"] = Properties.Resources.valve_horizontal_active,
            ["HorizontalInactive"] = Properties.Resources.valve_horizontal_inactive,

            ["VerticalDisabled"] = Properties.Resources.valve_vertical,
            ["VerticalActive"] = Properties.Resources.valve_vertical_active,
            ["VerticalInactive"] = Properties.Resources.valve_vertical_inactive,
        };

        public static Dictionary<string, Image> ReservoirImages = new Dictionary<string, Image>()
        {
            ["HorizontalDisabled"] = Properties.Resources.reservoir_horizontal,
            ["HorizontalActive"] = Properties.Resources.reservoir_horizontal_active,
            ["HorizontalInactive"] = Properties.Resources.reservoir_horizontal_inactive,

            ["VerticalDisabled"] = Properties.Resources.reservoir_vertical,
            ["VerticalActive"] = Properties.Resources.reservoir_vertical_active,
            ["VerticalInactive"] = Properties.Resources.reservoir_vertical_inactive,
        };

        public static Dictionary<string, Image> PumpImages = new Dictionary<string, Image>()
        {
            ["HorizontalDisabled"] = Properties.Resources.pump_horizontal,
            ["HorizontalActive"] = Properties.Resources.pump_horizontal_active,
            ["HorizontalInactive"] = Properties.Resources.pump_horizontal_inactive,

            ["VerticalDisabled"] = Properties.Resources.pump_vertical,
            ["VerticalActive"] = Properties.Resources.pump_vertical_active,
            ["VerticalInactive"] = Properties.Resources.pump_vertical_inactive,
        };

        public static Dictionary<string, Image> CounterImages = new Dictionary<string, Image>()
        {
            ["HorizontalDisabled"] = Properties.Resources.counter_horizontal,
            ["HorizontalActive"] = Properties.Resources.counter_horizontal_active,
            ["HorizontalInactive"] = Properties.Resources.counter_horizontal_inactive,

            ["VerticalDisabled"] = Properties.Resources.counter_vertical,
            ["VerticalActive"] = Properties.Resources.counter_vertical_active,
            ["VerticalInactive"] = Properties.Resources.counter_vertical_inactive,
        };

        public static Dictionary<string, Image> OpticalSensorImages = new Dictionary<string, Image>()
        {
            ["HorizontalDisabled"] = Properties.Resources.opticalSensor_horizontal,
            ["HorizontalActive"] = Properties.Resources.opticalSensor_horizontal_active,
            ["HorizontalInactive"] = Properties.Resources.opticalSensor_horizontal_inactive,

            ["VerticalDisabled"] = Properties.Resources.opticalSensor_vertical,
            ["VerticalActive"] = Properties.Resources.opticalSensor_vertical_active,
            ["VerticalInactive"] = Properties.Resources.opticalSensor_vertical_inactive,
        };

        public static Dictionary<char, Image> RoadImages = new Dictionary<char, Image>()
        {
            ['1'] = Properties.Resources.vertical_line,
            ['2'] = Properties.Resources.horizontal_line,
            ['3'] = Properties.Resources.LU_corner_line,
            ['4'] = Properties.Resources.RU_corner_line,
            ['5'] = Properties.Resources.RD_corner_line,
            ['6'] = Properties.Resources.LD_corner_line,
            ['7'] = Properties.Resources.TD_tpose_line,
            ['8'] = Properties.Resources.TL_tpose_line,
            ['9'] = Properties.Resources.TU_tpose_line,
            ['0'] = Properties.Resources.TR_tpose_line,
            ['+'] = Properties.Resources.C_pluspose_line
        };
    }
}
