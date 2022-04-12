using Bachelor_Project_Hydrogen_Compression_WinForms.Extensions;
using Bachelor_Project_Hydrogen_Compression_WinForms.Handlers;
using Bachelor_Project_Hydrogen_Compression_WinForms.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bachelor_Project_Hydrogen_Compression_WinForms
{
    public static class JSON_Handler
    {
        #region Cyclogram

        public static void InitializeCyclogramWithJson(Cyclogram cyclogram, string json)
        {
            JsonDocument doc = JsonDocument.Parse(json);
            JsonElement root = doc.RootElement;

            //if (root.TryGetProperty("length", out JsonElement lengthProperty))
            //{
            //    cyclogram.Length = lengthProperty.GetInt32();
            //}

            if (root.TryGetProperty("categories", out JsonElement categoryMainProperty))
            {
                foreach (JsonElement categoryElement in categoryMainProperty.EnumerateArray())
                {
                    cyclogram.Components.Add(PrepareCategory(categoryElement));
                }
            }

            if (root.TryGetProperty("titles", out JsonElement titleMainProperty))
            {
                foreach (JsonElement titleElement in titleMainProperty.EnumerateArray())
                {
                    cyclogram.Statuses.Add(PrepareTitle(titleElement, cyclogram));
                }
            }

            if (root.TryGetProperty("operations", out JsonElement operationMainProperty))
            {
                foreach (JsonElement operationElement in operationMainProperty.EnumerateArray())
                {
                    CyclogramStepElement step = PrepareOperation(operationElement);

                    if (operationElement.TryGetProperty("sequences", out JsonElement sequenceMainProperty))
                    {
                        foreach (JsonElement sequenceElement in sequenceMainProperty.EnumerateArray())
                        {
                            step.Sequences.Add(PrepareSequence(sequenceElement, cyclogram));
                        }
                    }

                    if (operationElement.TryGetProperty("length", out JsonElement lengthProperty))
                    {
                        step.LengthMilliseconds = lengthProperty.GetInt32();
                    }

                    if (step.LengthMilliseconds == 0) step.LengthMilliseconds = 1;


                    cyclogram.Steps.Add(step);
                }
            }

            cyclogram.Refresh();
        }

        
        private static CyclogramComponentElement PrepareCategory(JsonElement childElement)
        {
            string name = "";

            if (childElement.TryGetProperty("name", out JsonElement nameProperty))
            {
                name = nameProperty.GetString();
            }

            CyclogramComponentElement category = new CyclogramComponentElement() { Name = name };

            return category;
        }

        private static CyclogramStatusElement PrepareTitle(JsonElement childElement, Cyclogram cyclogram)
        {
            string idValue = "";

            if (childElement.TryGetProperty("titleId", out JsonElement idProperty))
            {
                idValue = idProperty.GetString();
            }

            string textValue = "";

            if (childElement.TryGetProperty("text", out JsonElement textProperty))
            {
                textValue = textProperty.GetString();
            }

            string categoryValue = "";
            
            if (childElement.TryGetProperty("category", out JsonElement categoryProperty))
            {
                categoryValue = categoryProperty.GetString();
            }

            CyclogramComponentElement category = null;
            foreach(CyclogramComponentElement categoryElement in cyclogram.Components)
            {
                if(categoryElement.Name.Equals(categoryValue))
                {
                    category = categoryElement;
                    break;
                }
            }

            CyclogramStatusElement title = new CyclogramStatusElement(idValue, textValue, category);
            title.Category?.Titles.Add(title);

            return title;
        }

        private static CyclogramStepElement PrepareOperation(JsonElement childElement)
        {
            string name = "";

            if (childElement.TryGetProperty("name", out JsonElement nameProperty))
            {
                name = nameProperty.GetString();
            }

            CyclogramStepElement operation = new CyclogramStepElement() { Name = name };

            return operation;
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
                    sensorReadingHelper.Sensors.Add(PrepareSensor(categoryElement));
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
    }
}
