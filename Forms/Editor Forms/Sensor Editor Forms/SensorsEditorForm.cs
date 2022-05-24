using Bachelor_Project.Extensions;
using Bachelor_Project.Forms.Options_Forms;
using Bachelor_Project.Handlers;
using Bachelor_Project.UserControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bachelor_Project.Forms.Editor_Forms
{
    public partial class SensorsEditorForm : Form
    {
        public List<Sensor> GetSensors { get { return MainForm.Instance.SensorReadingHelper.Sensors; } }

        private Sensor _lastEditedSensor;

        private Dictionary<Button, Action> _buttonEvents;

        public SensorsEditorForm()
        {
            InitializeComponent();

            dataGridView1.Columns.Add("Name", "Name");
            dataGridView1.Columns.Add("Type", "Type");
            dataGridView1.Columns.Add("MaxReadingsCount", "Max Readings Count");
            dataGridView1.Columns.Add("MinimumValue", "Minimum Value");
            dataGridView1.Columns.Add("MaximumValue", "Maximum Value");

            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            VisibleChanged += (s, e) => { RefreshSensorSetFilesComboBox(); };

            RefreshSensorSetFilesComboBox();

            textBox_AddSensor_MaxRecords.Text = Sensor.DefaultReadingsCount.ToString();

            comboBox_EditSensor_Type.Items.AddRange(Enum.GetNames(typeof(Sensor.SensorType)));
            comboBox_AddSensor_Type.Items.AddRange(Enum.GetNames(typeof(Sensor.SensorType)));
            if (comboBox_AddSensor_Type.Items.Count > 0) comboBox_AddSensor_Type.SelectedIndex = 0;

            InitializeButtonEvents();

            UpdateSensorEditingPanel();

            AppearanceOptionsForm.OnColorPaletteChange += SetColorPaletteForControls;
        }

        private void InitializeButtonEvents()
        {
            _buttonEvents = new Dictionary<Button, Action>()
            {
                [this.button_AddSensor_Add] = () => { AddSensorRecord(); SerializeAll(); },
                [this.button_Remove] = () => { RemoveSensorRecord(); SerializeAll(); },
                [this.button_MoveUp] = () => { MoveSensorRecordUp(); SerializeAll(); },
                [this.button_MoveUpEnd] = () => { MoveSensorRecordToTheTop(); SerializeAll(); },
                [this.button_MoveDown] = () => { MoveSensorRecordDown(); SerializeAll(); },
                [this.button_MoveDownEnd] = () => { MoveSensorRecordToTheBottom(); SerializeAll(); },
                [this.button_EditSensor_Save] = () => { TryToSaveEditedSensor(); SerializeAll(); },
                [this.button_NewSensorSet] = () => { if (CreateNewSensorSetFile()) { RefreshSensorSetFilesComboBox(); } },
                [this.button_DeleteSensorSet] = () => { if(DeleteCurrentSensorSet()) RefreshSensorSetFilesComboBox(); }
            };
        }

        private void button_ClickEvent(object sender, EventArgs e)
        {
            _buttonEvents[(Button)sender]?.Invoke();
        }

        private void RefreshSensorSetFilesComboBox()
        {
            try
            {
                try
                {
                    if(this.dataGridView1.Rows.Count > 0)
                    {
                        this.dataGridView1.Rows.Clear();
                    }
                }
                catch (Exception ex)
                { }

                this.comboBox_SelectSensor.Items.Clear();

                foreach (string file in FileManager.GetFiles(FileManager.JsonFileStructure.Sensors))
                {
                    this.comboBox_SelectSensor.Items.Add(file);
                }

                if (this.comboBox_SelectSensor.Items.Count > 0)
                {
                    this.comboBox_SelectSensor.SelectedIndex = 0; // UpdateSensorsDatabase();
                }
            }
            catch(Exception ex)
            {

            }
        }
        
        private void UpdateSensorsDatabase()
        {
            if(dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
            }

            foreach(Sensor sensor in GetSensors)
            {
                dataGridView1.Rows.Add(sensor.Name, sensor.Type, sensor.MaxReadingsCount, sensor.MinimumValue, sensor.MaximumValue);
            }

            //dataGridView1.Update();
            //dataGridView1.Refresh();

            UpdateSensorEditingPanel();
            MainForm.Instance.SensorReadingHelper.OnSensorsChanged?.Invoke();
        }

        private void AddSensorRecord()
        {
            try
            {
                string name = textBox_AddSensor_Name.Text;
                Sensor.SensorType sensorType = (Sensor.SensorType)Enum.Parse(typeof(Sensor.SensorType), comboBox_AddSensor_Type.Text);
                int maxRecords = int.Parse(textBox_AddSensor_MaxRecords.Text);

                Sensor sensor = new Sensor(name, sensorType, maxRecords);

                if (MainForm.Instance.SensorReadingHelper.Sensors.Any((x) => x.Name.Equals(name)))
                {
                    throw new Exception("There is already a sensor with the same name.");
                }

                float minValue = float.Parse(textBox_AddSensor_MinValue.Text);
                float maxValue = float.Parse(textBox_AddSensor_MaxValue.Text);

                if(minValue > maxValue)
                {
                    throw new Exception("The minimum value cannot be greater than the maximum.");
                }

                sensor.MinimumValue = minValue;
                sensor.MaximumValue = maxValue;

                MainForm.Instance.SensorReadingHelper.Sensors?.Add(sensor);

                UpdateSensorsDatabase();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RemoveSensorRecord()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    for (int i = MainForm.Instance.SensorReadingHelper.Sensors.Count - 1; i >= 0; i--)
                    {
                        if (MainForm.Instance.SensorReadingHelper.Sensors[i].Name.Equals(row.Cells[0].Value.ToString()))
                        {
                            switch (MessageBox.Show("Do you really want to delete sensor?", "Confirmation", MessageBoxButtons.YesNoCancel))
                            {
                                case DialogResult.OK:
                                case DialogResult.Yes:
                                    MainForm.Instance.SensorReadingHelper.Sensors.RemoveAt(i);
                                    UpdateSensorsDatabase();
                                    break;
                                default:
                                    break;
                            }

                            return;
                        }
                    }
                }
            }
        }

        #region ChangeRecordIndexPosition

        private void MoveSensorRecordUp()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    for (int i = MainForm.Instance.SensorReadingHelper.Sensors.Count - 1; i > 0; i--)
                    {
                        if (MoveRowAndUpdate(i, row, i - 1, i - 1)) break;
                    }
                }
            }
        }

        private void MoveSensorRecordDown()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    for (int i = MainForm.Instance.SensorReadingHelper.Sensors.Count - 2; i >= 0; i--)
                    {
                        if (MoveRowAndUpdate(i, row, i + 1, i + 1)) break;
                    }
                }
            }
        }

        private void MoveSensorRecordToTheTop()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    for (int i = MainForm.Instance.SensorReadingHelper.Sensors.Count - 1; i > 0; i--)
                    {
                        if (MoveRowAndUpdate(i, row, 0, 0)) break;
                    }
                }
            }
        }

        private void MoveSensorRecordToTheBottom()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    for (int i = MainForm.Instance.SensorReadingHelper.Sensors.Count - 2; i >= 0; i--)
                    {
                        if (MoveRowAndUpdate(i, row, GetSensors.Count - 1, GetSensors.Count - 1)) break;
                    }
                }
            }
        }

        private bool MoveRowAndUpdate(int index, DataGridViewRow row, int insertIndex, int selectRowIndex)
        {
            if (GetSensors[index].Name.Equals(row.Cells[0].Value.ToString()))
            {
                Sensor sensor = GetSensors[index];

                GetSensors.RemoveAt(index);
                GetSensors.Insert(insertIndex, sensor);

                UpdateSensorsDatabase();

                dataGridView1.Rows[selectRowIndex].Selected = true;

                return true;
            }

            return false;
        }

        #endregion

        private void UpdateSensorEditingPanel()
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DataGridViewRow row = dataGridView1.SelectedRows[0];

                    _lastEditedSensor = GetSensors.First((x) => x.Name.Equals(row.Cells[0].Value?.ToString()));

                    textBox_EditSensor_Name.Text = row.Cells[0].Value.ToString();

                    if (Enum.TryParse(row.Cells[1].Value.ToString(), out Sensor.SensorType type))
                    {
                        comboBox_EditSensor_Type.SelectedItem = type.ToString();
                    }

                    if (int.TryParse(row.Cells[2].Value.ToString(), out int maxRecords))
                    {
                        textBox_EditSensor_MaxRecords.Text = maxRecords.ToString();
                    }

                    if (float.TryParse(row.Cells[3].Value.ToString(), out float minValue))
                    {
                        textBox_EditSensor_MinValue.Text = minValue.ToString();
                    }

                    if (float.TryParse(row.Cells[4].Value.ToString(), out float maxValue))
                    {
                        textBox_EditSensor_MaxValue.Text = maxValue.ToString();
                    }

                    textBox_EditSensor_Name.Enabled = true;
                    comboBox_EditSensor_Type.Enabled = true;
                    textBox_EditSensor_MaxRecords.Enabled = true;

                    textBox_EditSensor_MinValue.Enabled = true;
                    textBox_EditSensor_MaxValue.Enabled = true;

                    button_EditSensor_Save.Enabled = true;
                }
                else
                {
                    _lastEditedSensor = null;

                    textBox_EditSensor_Name.Text = string.Empty;
                    comboBox_EditSensor_Type.SelectedItem = null;
                    textBox_EditSensor_MaxRecords.Text = string.Empty;

                    textBox_EditSensor_MinValue.Text = string.Empty;
                    textBox_EditSensor_MaxValue.Text = string.Empty;

                    textBox_EditSensor_Name.Enabled = false;
                    comboBox_EditSensor_Type.Enabled = false;
                    textBox_EditSensor_MaxRecords.Enabled = false;

                    textBox_EditSensor_MinValue.Enabled = false;
                    textBox_EditSensor_MaxValue.Enabled = false;

                    button_EditSensor_Save.Enabled = false;
                }
            }
            catch(Exception ex) { }
        }

        private void TryToSaveEditedSensor()
        {
            try
            {
                Sensor sensor = _lastEditedSensor;

                DataGridViewRow row = dataGridView1.SelectedRows[0];

                string name = textBox_EditSensor_Name.Text;
                Sensor.SensorType type = (Sensor.SensorType)Enum.Parse(typeof(Sensor.SensorType), comboBox_EditSensor_Type.Text);
                int maxRecords = int.Parse(textBox_EditSensor_MaxRecords.Text);

                _lastEditedSensor.Name = name;
                _lastEditedSensor.Type = type;
                _lastEditedSensor.MaxReadingsCount = maxRecords;

                float minValue = float.Parse(textBox_EditSensor_MinValue.Text);
                float maxValue = float.Parse(textBox_EditSensor_MaxValue.Text);

                if (minValue > maxValue)
                {
                    throw new Exception("The minimum value cannot be greater than the maximum.");
                }

                _lastEditedSensor.MinimumValue = minValue;
                _lastEditedSensor.MaximumValue = maxValue;

                UpdateSensorsDatabase();

                SerializeAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox_SelectSensor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sensorsConfigFile = (string)this.comboBox_SelectSensor.SelectedItem;

            if (!File.Exists(sensorsConfigFile))
            {
                MessageBox.Show($"Can't find \"{sensorsConfigFile}\".");
            }
            else
            {
                string json = File.ReadAllText(sensorsConfigFile);

                MainForm.Instance.SensorReadingHelper.Sensors.Clear();
                JSON_Handler.InitializeSensorReaderWithJson(MainForm.Instance.SensorReadingHelper, json);
            }

            UpdateSensorsDatabase();
        }

        private void SerializeAll()
        {
            try
            {
                JObject json = new JObject();

                JArray jArray = new JArray();
                foreach (Sensor sensor in GetSensors)
                {
                    JObject keyValuePairs = new JObject();

                    keyValuePairs.Add(new JProperty(nameof(sensor.Name).ToLower(), sensor.Name));
                    keyValuePairs.Add(new JProperty(nameof(sensor.Type).ToLower(), sensor.Type.ToString()));

                    string maxReadingsCountStr = nameof(sensor.MaxReadingsCount)[0].ToString().ToLower() + nameof(sensor.MaxReadingsCount).Substring(1);

                    keyValuePairs.Add(new JProperty(maxReadingsCountStr, sensor.MaxReadingsCount));

                    string minValueStr = nameof(sensor.MinimumValue)[0].ToString().ToLower() + nameof(sensor.MinimumValue).Substring(1);

                    keyValuePairs.Add(new JProperty(minValueStr, sensor.MinimumValue));

                    string maxValueStr = nameof(sensor.MaximumValue)[0].ToString().ToLower() + nameof(sensor.MaximumValue).Substring(1);

                    keyValuePairs.Add(new JProperty(maxValueStr, sensor.MaximumValue));

                    jArray.Add(keyValuePairs);
                }

                json.Add("sensors", jArray);

                string jsonString = json.ToString();

                string filePath = (string)comboBox_SelectSensor.SelectedItem;

                // Creating backup
                using (StreamWriter file = File.CreateText(filePath + ".bak"))
                {
                    file.Write(File.ReadAllText(filePath));
                }

                // serialize JSON to a string and then write string to a file
                File.WriteAllText(filePath, jsonString);

                MainForm.Instance.SensorReadingHelper.OnSensorsChanged?.Invoke();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private bool CreateNewSensorSetFile()
        {
            try
            {
                string json = string.Empty;

                JObject rootObj = new JObject();
                JArray sensorsArray = new JArray();

                rootObj.Add("sensors", sensorsArray);

                json = rootObj.ToString();

                Stream myStream;
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                int i;
                for (i = 1; File.Exists(@"Sensors\sensors" + $"{i}.json"); i++) ;

                saveFileDialog1.FileName = $"sensors{i}.json";
                saveFileDialog1.Filter = "JSON (*.json)|*.json|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.InitialDirectory = Path.GetFullPath(@"Sensors");
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.CheckPathExists = true;

                if(saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if ((myStream = saveFileDialog1.OpenFile()) != null)
                    {
                        byte[] data = Encoding.ASCII.GetBytes(json);

                        myStream.Write(data, 0, data.Length);

                        myStream.Close();

                        return true;
                    }
                }

                return false;
                
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        private bool DeleteCurrentSensorSet()
        {
            if(this.comboBox_SelectSensor.SelectedItem != null)
            {
                switch(MessageBox.Show("Are you really sure that you want to delete current sensor set?", "Delete Confirmation", MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Yes:
                        string filePath = this.comboBox_SelectSensor.SelectedItem.ToString();

                        if (File.Exists(filePath))
                        {
                            try
                            {
                                File.Delete(filePath);

                                if (File.Exists(filePath + ".bak"))
                                {
                                    File.Delete(filePath + ".bak");
                                }
                            }
                            catch(Exception ex)
                            {
                                return false;
                            }
                        }

                        return true;
                    default:
                        break;
                }

            }

            return false;
        }

        public void SetColorPaletteForControls(Dictionary<FormColorVariant, Color> colorPalette)
        {
            this.BackColor = colorPalette[FormColorVariant.DarkFirst];

            foreach (Button button in this.GetAllControlsRecusrvive<Button>())
            {
                button.BackColor = colorPalette[FormColorVariant.BrightSecond];
                button.ForeColor = colorPalette[FormColorVariant.TextColorFirst];

                button.FlatAppearance.MouseDownBackColor = colorPalette[FormColorVariant.ButtonMouseDown];
                button.FlatAppearance.MouseOverBackColor = colorPalette[FormColorVariant.ButtonMouseOver];

            }

            this.dataGridView1.BackgroundColor = colorPalette[FormColorVariant.DarkSecond];
            this.dataGridView1.GridColor = colorPalette[FormColorVariant.Outline];

            this.dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = colorPalette[FormColorVariant.DarkSecond];
            this.dataGridView1.AlternatingRowsDefaultCellStyle.ForeColor = colorPalette[FormColorVariant.TextColorSecond];
            this.dataGridView1.AlternatingRowsDefaultCellStyle.SelectionBackColor = colorPalette[FormColorVariant.BrightSecond];
            this.dataGridView1.AlternatingRowsDefaultCellStyle.SelectionForeColor = colorPalette[FormColorVariant.TextColorFirst];

            this.dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = colorPalette[FormColorVariant.NormalFirst];
            this.dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = colorPalette[FormColorVariant.TextColorSecond];
            this.dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = colorPalette[FormColorVariant.NormalSecond];
            this.dataGridView1.ColumnHeadersDefaultCellStyle.SelectionForeColor = colorPalette[FormColorVariant.TextColorFirst];

            this.dataGridView1.RowHeadersDefaultCellStyle.BackColor = colorPalette[FormColorVariant.NormalSecond];
            this.dataGridView1.RowHeadersDefaultCellStyle.ForeColor = colorPalette[FormColorVariant.TextColorSecond];
            this.dataGridView1.RowHeadersDefaultCellStyle.SelectionBackColor = colorPalette[FormColorVariant.NormalSecond];
            this.dataGridView1.RowHeadersDefaultCellStyle.SelectionForeColor = colorPalette[FormColorVariant.TextColorFirst];

            this.dataGridView1.RowsDefaultCellStyle.BackColor = colorPalette[FormColorVariant.Outline];
            this.dataGridView1.RowsDefaultCellStyle.ForeColor = colorPalette[FormColorVariant.TextColorSecond];
            this.dataGridView1.RowsDefaultCellStyle.SelectionBackColor = colorPalette[FormColorVariant.BrightSecond];
            this.dataGridView1.RowsDefaultCellStyle.SelectionForeColor = colorPalette[FormColorVariant.TextColorFirst];

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            UpdateSensorEditingPanel();
        }
    }
}
