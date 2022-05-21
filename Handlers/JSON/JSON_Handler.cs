using Bachelor_Project.Extensions;
using Bachelor_Project.Handlers;
using Bachelor_Project.UserControls;
using Bachelor_Project.UserControls.Device;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bachelor_Project
{
    public static class JSON_Handler
    {
        #region Cyclogram

        public static void InitializeCyclogramWithJson(Cyclogram cyclogram, string json)
        {
            cyclogram.Steps.Clear();
            //cyclogram.Components.Clear();
            //cyclogram.Statuses.Clear();

            JsonDocument doc = JsonDocument.Parse(json);
            JsonElement root = doc.RootElement;

            if(root.TryGetProperty("cyclogram", out JsonElement cyclogramObj))
            {
                if (cyclogramObj.TryGetProperty("steps", out JsonElement stepsArray))
                {
                    foreach (JsonElement stepElement in stepsArray.EnumerateArray())
                    {
                        cyclogram.Steps.Add(PrepareStep(stepElement));
                    }
                }

            }

            cyclogram.HorizontalVisionPos = 0;
            cyclogram.VerticalScrollerPos = 0;

            cyclogram.Refresh();
        }


        private static CyclogramStepElement PrepareStep(JsonElement childElement)
        {
            string name = "";

            if (childElement.TryGetProperty("name", out JsonElement nameProperty))
            {
                name = nameProperty.GetString();
            }

            int lengthMilliseconds = 0;
            if (childElement.TryGetProperty("lengthMilliseconds", out JsonElement lengthProperty))
            {
                lengthMilliseconds = lengthProperty.GetInt32();
            }

            if (lengthMilliseconds == 0) lengthMilliseconds = 1;

            List<CyclogramSequenceElement> sequences = new List<CyclogramSequenceElement>();

            if (childElement.TryGetProperty("sequences", out JsonElement sequencesArray))
            {
                foreach (JsonElement sequenceElement in sequencesArray.EnumerateArray())
                {
                    sequences.Add(PrepareSequence(sequenceElement));
                }
            }

            CyclogramStepElement step = new CyclogramStepElement(name, lengthMilliseconds) { Sequences = sequences };

            return step;
        }


        private static CyclogramSequenceElement PrepareSequence(JsonElement childElement)
        {
            string componentName = string.Empty;
            string statusName = string.Empty;

            if (childElement.TryGetProperty("componentName", out JsonElement componentNameProperty))
            {
                componentName = componentNameProperty.GetString();
            }

            if (childElement.TryGetProperty("statusName", out JsonElement statusNameProperty))
            {
                statusName = statusNameProperty.GetString();
            }

            return new CyclogramSequenceElement(componentName, statusName);
        }


        #endregion

        #region Sensors

        public static void InitializeSensorReaderWithJson(SensorReadingHelper sensorReadingHelper, string json)
        {
            JsonDocument doc = JsonDocument.Parse(json);
            JsonElement root = doc.RootElement;

            if (root.TryGetProperty("sensors", out JsonElement categoryMainProperty))
            {
                foreach (JsonElement categoryElement in categoryMainProperty.EnumerateArray())
                {
                    try
                    {
                        sensorReadingHelper.Sensors.Add(PrepareSensor(categoryElement));
                    }
                    catch { } // Skip...
                }
            }

        }

        private static Sensor PrepareSensor(JsonElement childElement)
        {
            string name = "";

            if (childElement.TryGetProperty("name", out JsonElement nameProperty))
            {
                name = nameProperty.GetString();
            }

            string type = "";
            Sensor.SensorType sensorType = Sensor.SensorType.Unknown;

            if (childElement.TryGetProperty("type", out JsonElement typeProperty))
            {
                type = typeProperty.GetString();

                try
                {
                    type = type.FirstCharToUpper();
                }
                catch(Exception ex)
                {
                    // Skip...
                }

                if (!Enum.TryParse<Sensor.SensorType>(type, out sensorType))
                {
                    MessageBox.Show($"Can't parse sensor type for {name} - {type}.");
                }
            }

            int maxReadingsCount = -1;

            if (childElement.TryGetProperty("maxReadingsCount", out JsonElement maxReadingsCountProperty))
            {
                maxReadingsCount = maxReadingsCountProperty.GetInt32();
            }

            float minValue = 0;
            float maxValue = 0;

            if (childElement.TryGetProperty("minimumValue", out JsonElement minValueProperty))
            {
                minValue = minValueProperty.GetSingle();
            }

            if (childElement.TryGetProperty("maximumValue", out JsonElement maxValueProperty))
            {
                maxValue = maxValueProperty.GetSingle();
            }

            if(minValue > maxValue)
            {
                float temp = minValue;

                minValue = maxValue;
                maxValue = temp;
            }

            Sensor sensor;

            if (maxReadingsCount <= 0)
            {
                sensor = new Sensor(name, sensorType);
            }
            else
            {
                sensor = new Sensor(name, sensorType, maxReadingsCount);
            }

            sensor.MinimumValue = minValue;
            sensor.MaximumValue = maxValue;

            return sensor;
        }

        #endregion

        #region DeviceScheme

        public static void InitializeCompressorDeviceWithJson(CompressorDevice compressorDevice, string json)
        {
            JsonDocument doc = JsonDocument.Parse(json);
            JsonElement root = doc.RootElement;

            Console.WriteLine($"Reading TilemapSize in JSON.");

            if (root.TryGetProperty("deviceScheme", out JsonElement deviceSchemeProperty))
            {
                if (deviceSchemeProperty.TryGetProperty("tilemapSize", out JsonElement tilemapSizeProperty))
                {
                    Size tilemapSize = default(Size);

                    int i = 0;

                    int X = 0, Y = 0;

                    foreach (JsonElement layerElement in tilemapSizeProperty.EnumerateArray())
                    {
                        switch (i)
                        {
                            case 0:
                                X = layerElement.GetInt32();
                                break;

                            case 1:
                                Y = layerElement.GetInt32();
                                break;

                            default:
                                break;
                        }

                        i++;
                    }

                    tilemapSize = new Size(X, Y);
                    compressorDevice.TilemapSize = tilemapSize;
                }

                Console.WriteLine($"TilemapSize in JSON: {compressorDevice.TilemapSize}");

                Dictionary<CompressorLayer.LayerTypeEnum, CompressorLayer> layers = new Dictionary<CompressorLayer.LayerTypeEnum, CompressorLayer>()
                {
                    [CompressorLayer.LayerTypeEnum.Editor] = new CompressorLayer(compressorDevice.TilemapSize, CompressorLayer.LayerTypeEnum.Editor),
                    [CompressorLayer.LayerTypeEnum.GasPipes] = new CompressorLayer(compressorDevice.TilemapSize, CompressorLayer.LayerTypeEnum.GasPipes),
                    [CompressorLayer.LayerTypeEnum.OilPipes] = new CompressorLayer(compressorDevice.TilemapSize, CompressorLayer.LayerTypeEnum.OilPipes),
                    [CompressorLayer.LayerTypeEnum.Components] = new CompressorLayer(compressorDevice.TilemapSize, CompressorLayer.LayerTypeEnum.Components)
                };

                if (deviceSchemeProperty.TryGetProperty("layers", out JsonElement layersProperty))
                {
                    foreach (JsonElement layerElement in layersProperty.EnumerateArray())
                    {
                        if(layerElement.TryGetProperty("layerType", out JsonElement layerTypeProperty))
                        {
                            if(Enum.TryParse(layerTypeProperty.GetString(), out CompressorLayer.LayerTypeEnum layerType))
                            {
                                if(layerElement.TryGetProperty("elements", out JsonElement elementsProperty))
                                {
                                    foreach (JsonElement elementObj in elementsProperty.EnumerateArray())
                                    {
                                        try
                                        {
                                            CompressorElement compressorElement;
                                            Point elementPos;

                                            (elementPos, compressorElement) = PrepareSchemeElement(elementObj, layerType);

                                            layers[layerType].SetElement(compressorElement, elementPos.Y, elementPos.X);
                                        }
                                        catch { } // Skip...

                                    }
                                }
                            }
                        }

                    }
                }

                compressorDevice.Layers[CompressorLayer.LayerTypeEnum.GasPipes].SetElements(layers[CompressorLayer.LayerTypeEnum.GasPipes].GetElements);
                compressorDevice.Layers[CompressorLayer.LayerTypeEnum.OilPipes].SetElements(layers[CompressorLayer.LayerTypeEnum.OilPipes].GetElements);
                compressorDevice.Layers[CompressorLayer.LayerTypeEnum.Components].SetElements(layers[CompressorLayer.LayerTypeEnum.Components].GetElements);

                foreach(CompressorLayer layer in compressorDevice.Layers.Values)
                {
                    layer.ApplyRulesToTheLayer();
                }

            }

        }

        private static (Point, CompressorElement) PrepareSchemeElement(JsonElement childElement, CompressorLayer.LayerTypeEnum layerType)
        {
            Point elementPos = default(Point);
            CompressorElement element;

            if (childElement.TryGetProperty("position", out JsonElement positionProperty))
            {
                int i = 0;

                int X = 0, Y = 0;

                foreach (JsonElement layerElement in positionProperty.EnumerateArray())
                {
                    switch (i)
                    {
                        case 0:
                            X = layerElement.GetInt32();
                            break;

                        case 1:
                            Y = layerElement.GetInt32();
                            break;

                        default:
                            break;
                    }

                    i++;
                }

                elementPos = new Point(X, Y);
            }

            switch (layerType)
            {
                case CompressorLayer.LayerTypeEnum.Components:

                    string name;
                    if (childElement.TryGetProperty("name", out JsonElement nameProperty))
                    {
                        name = nameProperty.GetString();
                    }
                    else
                    {
                        throw new Exception("Can't instantiate compressor element. Name field is empty.");
                    }

                    CompressorComponent.ComponentType componentType = CompressorComponent.ComponentType.Valve;
                    if (childElement.TryGetProperty("componentType", out JsonElement componentTypeProperty))
                    {
                        componentType = (CompressorComponent.ComponentType)Enum.Parse(typeof(CompressorComponent.ComponentType), componentTypeProperty.GetString());
                    }

                    CompressorComponent.ComponentOrientation componentOrientation = CompressorComponent.ComponentOrientation.Horizontal;
                    if (childElement.TryGetProperty("componentOrientation", out JsonElement componentOrientationProperty))
                    {
                        componentOrientation = (CompressorComponent.ComponentOrientation)Enum.Parse(typeof(CompressorComponent.ComponentOrientation), componentOrientationProperty.GetString());
                    }

                    string sensorName = null;
                    if (childElement.TryGetProperty("sensorName", out JsonElement sensorNameProperty))
                    {
                        sensorName = sensorNameProperty.GetString();
                    }

                    element = new CompressorComponent(name, componentType, componentOrientation) { SensorName = sensorName };

                    break;

                case CompressorLayer.LayerTypeEnum.GasPipes:
                case CompressorLayer.LayerTypeEnum.OilPipes:

                    CompressorPipe.PipeOrientation pipeOrientation = CompressorPipe.PipeOrientation.Cross;
                    if (childElement.TryGetProperty("pipeOrientation", out JsonElement pipeOrientationProperty))
                    {
                        pipeOrientation = (CompressorPipe.PipeOrientation)Enum.Parse(typeof(CompressorPipe.PipeOrientation), pipeOrientationProperty.GetString());
                    }

                    element = new CompressorPipe(CompressorPipe.PipeType.Gas, pipeOrientation, CompressorPipe.PipeStatus.Empty);

                    break;

                default:
                    throw new Exception("Can't instantiate compressor element.");
            }

            return (elementPos, element);
        }

        #endregion


    }
}
