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

        public static void InitializeCyclogramWithJsonTEMP(Cyclogram cyclogram, string json)
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

            cyclogram.Refresh();
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

        public static void InitializeCyclogramWithJson(Cyclogram cyclogram, string json)
        {
            JsonDocument doc = JsonDocument.Parse(json);
            JsonElement root = doc.RootElement;

            //if (root.TryGetProperty("length", out JsonElement lengthProperty))
            //{
            //    cyclogram.Length = lengthProperty.GetInt32();
            //}

            //if (root.TryGetProperty("categories", out JsonElement categoryMainProperty))
            //{
            //    foreach (JsonElement categoryElement in categoryMainProperty.EnumerateArray())
            //    {
            //        cyclogram.Components.Add(PrepareCategory(categoryElement));
            //    }
            //}

            //if (root.TryGetProperty("titles", out JsonElement titleMainProperty))
            //{
            //    foreach (JsonElement titleElement in titleMainProperty.EnumerateArray())
            //    {
            //        cyclogram.Statuses.Add(PrepareTitle(titleElement, cyclogram));
            //    }
            //}

            //if (root.TryGetProperty("operations", out JsonElement operationMainProperty))
            //{
            //    foreach (JsonElement operationElement in operationMainProperty.EnumerateArray())
            //    {
            //        CyclogramStepElement step = PrepareStep(operationElement);

            //        if (operationElement.TryGetProperty("sequences", out JsonElement sequenceMainProperty))
            //        {
            //            foreach (JsonElement sequenceElement in sequenceMainProperty.EnumerateArray())
            //            {
            //                step.Sequences.Add(PrepareSequence(sequenceElement, cyclogram));
            //            }
            //        }

            //        cyclogram.Steps.Add(step);
            //    }
            //}

            cyclogram.Refresh();
        }

        
        //private static CyclogramComponentElement PrepareCategory(JsonElement childElement)
        //{
        //    string name = "";

        //    if (childElement.TryGetProperty("name", out JsonElement nameProperty))
        //    {
        //        name = nameProperty.GetString();
        //    }

        //    CyclogramComponentElement category = new CyclogramComponentElement() { Name = name };

        //    return category;
        //}

        //private static CyclogramStatusElement PrepareTitle(JsonElement childElement, Cyclogram cyclogram)
        //{
        //    string idValue = "";

        //    if (childElement.TryGetProperty("titleId", out JsonElement idProperty))
        //    {
        //        idValue = idProperty.GetString();
        //    }

        //    string textValue = "";

        //    if (childElement.TryGetProperty("text", out JsonElement textProperty))
        //    {
        //        textValue = textProperty.GetString();
        //    }

        //    string categoryValue = "";
            
        //    if (childElement.TryGetProperty("category", out JsonElement categoryProperty))
        //    {
        //        categoryValue = categoryProperty.GetString();
        //    }

        //    CyclogramComponentElement category = null;
        //    foreach(CyclogramComponentElement categoryElement in cyclogram.Components)
        //    {
        //        if(categoryElement.Name.Equals(categoryValue))
        //        {
        //            category = categoryElement;
        //            break;
        //        }
        //    }

        //    CyclogramStatusElement title = new CyclogramStatusElement(idValue, textValue, category);
        //    title.Category?.Statuses.Add(title);

        //    return title;
        //}

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

        private static CyclogramSequenceElement PrepareSequence(JsonElement childElement, Cyclogram cyclogram)
        {
            string sequenceId = "";

            if (childElement.TryGetProperty("sequence_id", out JsonElement element))
            {
                sequenceId = element.GetString();
            }

            string titleId = "";

            if (childElement.TryGetProperty("title_id", out JsonElement element2))
            {
                titleId = element2.GetString();
            }

            //int length = 0;

            //if (childElement.TryGetProperty("length", out JsonElement element3))
            //{
            //    length = element3.GetInt16();
            //}

            //string afterId = "";

            //if (childElement.TryGetProperty("after_id", out JsonElement element4))
            //{
            //    afterId = element4.GetString();
            //}

            //int pos = 0;
            //foreach (CyclogramSequenceElement sequence in cyclogram.Sequences)
            //{
            //    if (sequence.SequenceID.Equals(afterId))
            //    {
            //        pos = sequence.Pos + sequence.Length;
            //        break;
            //    }
            //}

            CyclogramSequenceElement sequenceObj = new CyclogramSequenceElement(sequenceId, titleId);

            return sequenceObj;
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

            Sensor sensor;

            if (maxReadingsCount <= 0)
            {
                sensor = new Sensor(name, sensorType);
            }
            else
            {
                sensor = new Sensor(name, sensorType, maxReadingsCount);
            }

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

                Dictionary<CompressorLayer.LayerType, CompressorLayer> layers = new Dictionary<CompressorLayer.LayerType, CompressorLayer>()
                {
                    [CompressorLayer.LayerType.Editor] = new CompressorLayer(compressorDevice.TilemapSize, CompressorLayer.LayerType.Editor),
                    [CompressorLayer.LayerType.GasPipes] = new CompressorLayer(compressorDevice.TilemapSize, CompressorLayer.LayerType.GasPipes),
                    [CompressorLayer.LayerType.OilPipes] = new CompressorLayer(compressorDevice.TilemapSize, CompressorLayer.LayerType.OilPipes),
                    [CompressorLayer.LayerType.Components] = new CompressorLayer(compressorDevice.TilemapSize, CompressorLayer.LayerType.Components)
                };

                if (deviceSchemeProperty.TryGetProperty("layers", out JsonElement layersProperty))
                {
                    foreach (JsonElement layerElement in layersProperty.EnumerateArray())
                    {
                        if(layerElement.TryGetProperty("layerType", out JsonElement layerTypeProperty))
                        {
                            if(Enum.TryParse(layerTypeProperty.GetString(), out CompressorLayer.LayerType layerType))
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

                compressorDevice.Layers[CompressorLayer.LayerType.GasPipes].SetElements(layers[CompressorLayer.LayerType.GasPipes].GetElements);
                compressorDevice.Layers[CompressorLayer.LayerType.OilPipes].SetElements(layers[CompressorLayer.LayerType.OilPipes].GetElements);
                compressorDevice.Layers[CompressorLayer.LayerType.Components].SetElements(layers[CompressorLayer.LayerType.Components].GetElements);

                foreach(CompressorLayer layer in compressorDevice.Layers.Values)
                {
                    layer.ApplyRulesToTheLayer();
                }

            }

        }

        private static (Point, CompressorElement) PrepareSchemeElement(JsonElement childElement, CompressorLayer.LayerType layerType)
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
                case CompressorLayer.LayerType.Components:

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

                    element = new CompressorComponent(name, componentType, componentOrientation);

                    break;

                case CompressorLayer.LayerType.GasPipes:
                case CompressorLayer.LayerType.OilPipes:

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
