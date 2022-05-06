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

            VisibleChanged += (s, e) => { RefreshSensorSetFilesComboBox(); };

            RefreshSensorSetFilesComboBox();

            textBox_AddSensor_MaxRecords.Text = Sensor.DefaultReadingsCount.ToString();

            comboBox_EditSensor_Type.Items.AddRange(Enum.GetNames(typeof(Sensor.SensorType)));
            comboBox_AddSensor_Type.Items.AddRange(Enum.GetNames(typeof(Sensor.SensorType)));
            if (comboBox_AddSensor_Type.Items.Count > 0) comboBox_AddSensor_Type.SelectedIndex = 0;

            InitializeButtonEvents();
        }

        private void RefreshSensorSetFilesComboBox()
        {
            this.comboBox_SelectSensor.Items.Clear();

            foreach (string file in FileManager.GetSensorFiles())
            {
                this.comboBox_SelectSensor.Items.Add(file);
            }

            if (this.comboBox_SelectSensor.Items.Count > 0)
            {
                this.comboBox_SelectSensor.SelectedIndex = 0; // UpdateSensorsDatabase();
            }
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
                [this.button_NewSensorSet] = () => { CreateNewSensorSetFile(); RefreshSensorSetFilesComboBox(); SerializeAll(); },
                [this.button_DeleteSensorSet] = () => { if(DeleteCurrentSensorSet()) RefreshSensorSetFilesComboBox(); SerializeAll(); }
            };
        }

        private void button_ClickEvent(object sender, EventArgs e)
        {
            _buttonEvents[(Button)sender]?.Invoke();
        }
        
        private void UpdateSensorsDatabase()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = GetSensors;

            UpdateSensorEditingPanel();
            MainForm.Instance.SensorReadingHelper.OnSensorsChanged?.Invoke();
        }

        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            for (int i = 0; i <= dataGridView1.Columns.Count - 1; i++)
            {
                dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
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

                MainForm.Instance.SensorReadingHelper.Sensors?.Add(sensor);

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = MainForm.Instance.SensorReadingHelper.Sensors;
                dataGridView1.Update();
                dataGridView1.Refresh();
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
                            switch (MessageBox.Show("Are you really want to delete sensor(-s)?", "Confirmation", MessageBoxButtons.YesNoCancel))
                            {
                                case DialogResult.OK:
                                case DialogResult.Yes:
                                    MainForm.Instance.SensorReadingHelper.Sensors.RemoveAt(i);
                                    dataGridView1.DataSource = null;
                                    dataGridView1.DataSource = MainForm.Instance.SensorReadingHelper.Sensors;
                                    dataGridView1.Update();
                                    dataGridView1.Refresh();
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

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = GetSensors;

                dataGridView1.Rows[selectRowIndex].Selected = true;
                dataGridView1.Update();
                dataGridView1.Refresh();

                return true;
            }

            return false;
        }

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

                    textBox_EditSensor_Name.Enabled = true;
                    comboBox_EditSensor_Type.Enabled = true;
                    textBox_EditSensor_MaxRecords.Enabled = true;
                }
                else
                {
                    _lastEditedSensor = null;

                    textBox_EditSensor_Name.Text = string.Empty;
                    comboBox_EditSensor_Type.SelectedItem = null;
                    textBox_EditSensor_MaxRecords.Text = string.Empty;

                    textBox_EditSensor_Name.Enabled = false;
                    comboBox_EditSensor_Type.Enabled = false;
                    textBox_EditSensor_MaxRecords.Enabled = false;
                }
            }
            catch { }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            UpdateSensorEditingPanel();
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

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = GetSensors;

                dataGridView1.Update();
                dataGridView1.Refresh();

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

        private void CreateNewSensorSetFile()
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

            switch (saveFileDialog1.ShowDialog())
            {
                case DialogResult.OK:

                    if ((myStream = saveFileDialog1.OpenFile()) != null)
                    {
                        byte[] data = Encoding.ASCII.GetBytes(json);

                        myStream.Write(data, 0, data.Length);

                        myStream.Close();
                    }
                    break;
                default:
                    return;
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
                            File.Delete(filePath);

                            if (File.Exists(filePath + ".bak"))
                            {
                                File.Delete(filePath + ".bak");
                            }
                        }

                        return true;
                    default:
                        break;
                }

            }

            return false;
        }

    }
}
