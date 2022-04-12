using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Bachelor_Project_Hydrogen_Compression_WinForms.UserControls.Device.CompressorComponent;

namespace Bachelor_Project_Hydrogen_Compression_WinForms.UserControls.Device
{
    public static class CompressorDeviceRules
    {
        public static Dictionary<string, Image> GetComponentImages(ComponentType type)
        {
            switch (type)
            {
                case ComponentType.Valve:
                    return CompressorDeviceRules.ValveImages;
                case ComponentType.Reservoir:
                    return CompressorDeviceRules.ReservoirImages;
                case ComponentType.Pump:
                    return CompressorDeviceRules.PumpImages;
                case ComponentType.CounterTrigger:
                    return CompressorDeviceRules.CounterImages;
                case ComponentType.OpticalSensor:
                    return CompressorDeviceRules.OpticalSensorImages;
                default:
                    return new Dictionary<string, Image>();
            }
        }

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

        public static Dictionary<string, Image> GetPipeImages(CompressorPipe.PipeType type)
        {
            switch (type)
            {
                case CompressorPipe.PipeType.Gas:
                    return CompressorDeviceRules.GasPipeImages;
                case CompressorPipe.PipeType.Oil:
                    return CompressorDeviceRules.OilPipeImages;
                default:
                    return new Dictionary<string, Image>();
            }
        }

        public static Dictionary<string, Image> GasPipeImages = new Dictionary<string, Image>()
        {
            ["StraightHorizontalEmpty"] = Properties.Resources.horizontal_line,
            ["StraightVerticalEmpty"] = Properties.Resources.vertical_line,
            ["CrossEmpty"] = Properties.Resources.C_pluspose_line,
            ["T_LeftEmpty"] = Properties.Resources.TL_tpose_line,
            ["T_UpEmpty"] = Properties.Resources.TU_tpose_line,
            ["T_RightEmpty"] = Properties.Resources.TR_tpose_line,
            ["T_DownEmpty"] = Properties.Resources.TD_tpose_line,
            ["LU_CornerEmpty"] = Properties.Resources.LU_corner_line,
            ["RU_CornerEmpty"] = Properties.Resources.RU_corner_line,
            ["LD_CornerEmpty"] = Properties.Resources.LD_corner_line,
            ["RD_CornerEmpty"] = Properties.Resources.RD_corner_line,
        };

        public static Dictionary<string, Image> OilPipeImages = new Dictionary<string, Image>()
        {
            ["StraightHorizontalEmpty"] = Properties.Resources.horizontal_line,
            ["StraightVerticalEmpty"] = Properties.Resources.vertical_line,
            ["CrossEmpty"] = Properties.Resources.C_pluspose_line,
            ["T_LeftEmpty"] = Properties.Resources.TL_tpose_line,
            ["T_UpEmpty"] = Properties.Resources.TU_tpose_line,
            ["T_RightEmpty"] = Properties.Resources.TR_tpose_line,
            ["T_DownEmpty"] = Properties.Resources.TD_tpose_line,
            ["LU_CornerEmpty"] = Properties.Resources.LU_corner_line,
            ["RU_CornerEmpty"] = Properties.Resources.RU_corner_line,
            ["LD_CornerEmpty"] = Properties.Resources.LD_corner_line,
            ["RD_CornerEmpty"] = Properties.Resources.RD_corner_line,
        };
    }
}
