using Bachelor_Project.Extensions;
using Bachelor_Project.Forms.Options_Forms;
using Bachelor_Project.Handlers;
using Bachelor_Project.Processing;
using Bachelor_Project.UserControls;
using Bachelor_Project.UserControls.Device;
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

namespace Bachelor_Project.Forms.Editor_Forms.Device_Scheme_Editor_Forms
{
    public partial class DeviceSchemeEditorForm : Form
    {
        private bool _initialized = false;

        private List<ToolTip> _toolTips = new List<ToolTip>();

        private CompressorElement _selectedElement;

        private Dictionary<PictureBox, CompressorElement> _pictureBoxes = new Dictionary<PictureBox, CompressorElement>();

        private Dictionary<Control, Action> _controlEvents;


        public DeviceSchemeEditorForm()
        {
            InitializeComponent();

            DisableMainControls();

            this.compressorDevice1.Alignment = CompressorDevice.DeviceAlignment.Middle;
            this.compressorDevice1.EditorMode = true;

            InitializeButtonEvents();

            AppearanceOptionsForm.OnColorPaletteChange += SetColorPaletteForControls;
        }

        private void DeviceSchemeEditorForm_Load(object sender, EventArgs e)
        {
            this.compressorDevice1.MouseClick += (s2, e2) => 
            { 
                if(e2.Button == MouseButtons.Left) PutElementOnScheme(e2.Location);
                else if (e2.Button == MouseButtons.Right) RemoveElementFromScheme(e2.Location);
            };

            foreach (CompressorLayer.LayerTypeEnum layer in Enum.GetValues(typeof(CompressorLayer.LayerTypeEnum)))
            {
                if (layer == CompressorLayer.LayerTypeEnum.Editor) continue;

                comboBox_SelectLayer.Items.Add(layer);
            }

            comboBox_SelectLayer.SelectedIndex = 0;

            this.comboBox_SelectedComponent_Type.DataSource = Enum.GetValues(typeof(CompressorComponent.ComponentType));
            this.comboBox_SelectedComponent_Orientation.DataSource = Enum.GetValues(typeof(CompressorComponent.ComponentOrientation));

            RefreshDeviceSchemesFilesComboBox();
            _initialized = true;
        }


        private void InitializeButtonEvents()
        {
            _controlEvents = new Dictionary<Control, Action>()
            {
                [this.comboBox_SelectLayer] = () => { AfterLayerChanged(); },
                [this.listBox_Components] = () => { ComponentsListBoxChanged(); },
                [this.button_SelectedComponent_Save] = () => 
                { 
                    SaveSelectedComponent(); 
                    UpdateComponentsList();
                    foreach (CompressorLayer layer in compressorDevice1.Layers.Values)
                    {
                        layer.ApplyRulesToTheLayer();
                    }
                    SerializeAll();
                },
                [this.button_NewScheme] = () => { CreateNewScheme(); RefreshDeviceSchemesFilesComboBox(); },
                [this.comboBox_SelectDeviceScheme] = () => 
                { 
                    SelectedSchemeChanged();
                    UpdateComponentsList();
                    foreach (CompressorLayer layer in compressorDevice1.Layers.Values)
                    {
                        layer.ApplyRulesToTheLayer();
                    }

                    this.compressorDevice1.Refresh();
                },
                [this.button_DeleteSensorSet] = () => { if(DeleteCurrentScheme()) RefreshDeviceSchemesFilesComboBox(); }
            };
        }

        private void control_Event(object sender, EventArgs e)
        {
            _controlEvents[(Control)sender]?.Invoke();
        }

        private void DisableMainControls()
        {
            this.comboBox_SelectLayer.Enabled = false;
            this.numericUpDown_Rows.Enabled = false;
            this.numericUpDown_Columns.Enabled = false;

            this.textBox_SelectedComponent_SensorName.Enabled = false;
            this.comboBox_SelectedComponent_Orientation.Enabled = false;
            this.comboBox_SelectedComponent_Type.Enabled = false;

            this.textBox_SelectedComponent_SensorName.Text = string.Empty;
            this.comboBox_SelectedComponent_Orientation.SelectedItem = null;
            this.comboBox_SelectedComponent_Type.SelectedItem = null;

            this.textBox_SelectedComponent_Name.Text = string.Empty;
            this.textBox_SelectedComponent_Name.Enabled = false;
        }

        private void RefreshDeviceSchemesFilesComboBox()
        {
            _initialized = false;

            this.comboBox_SelectDeviceScheme.Items.Clear();

            this.compressorDevice1.Layers.ToList().ForEach(x => x.Value.SetElements(new CompressorElement[compressorDevice1.TilemapSize.Width, compressorDevice1.TilemapSize.Height]));

            foreach (string file in FileManager.GetFiles(FileManager.JsonFileStructure.DeviceSchemes))
            {
                this.comboBox_SelectDeviceScheme.Items.Add(file);
            }

            if (this.comboBox_SelectDeviceScheme.Items.Count > 0)
            {
                this.comboBox_SelectDeviceScheme.SelectedIndex = 0;
            }

            if(this.comboBox_SelectDeviceScheme.SelectedItem == null)
            {
                DisableMainControls();
            }

            this.numericUpDown_Rows.Value = compressorDevice1.TilemapSize.Height;
            this.numericUpDown_Columns.Value = compressorDevice1.TilemapSize.Width;

            _initialized = true;
        }

        private void numericUpDown_TilemapSize_ValueChanged(object sender, EventArgs e)
        {
            if (!_initialized) return;

            Size size = new Size((int)numericUpDown_Columns.Value, (int)numericUpDown_Rows.Value);

            if (this.compressorDevice1.CanBeResized(size))
            {
                this.compressorDevice1.TilemapSize = size;
            }

            SerializeAll();
        }

        

        private void RemoveElementFromScheme(Point mousePos)
        {
            if (Enum.TryParse(comboBox_SelectLayer.SelectedItem.ToString(), out CompressorLayer.LayerTypeEnum layerType))
            {
                if (this.compressorDevice1.MousePosInsideTheGrid(mousePos))
                {
                    Point gridPos = this.compressorDevice1.MousePosToGridPos(mousePos);
                    //Console.WriteLine("Mouse: " + mousePos.ToString());
                    //Console.WriteLine("Grid Pos: " + gridPos.ToString());
                    this.compressorDevice1.Layers[layerType].SetElement(null, gridPos.X, gridPos.Y);
                    this.compressorDevice1.Layers[layerType].ApplyRulesToTheLayer();

                    this.compressorDevice1.Refresh();

                    UpdateComponentsList();
                    SerializeAll();
                }

            }
        }

        private void PutElementOnScheme(Point mousePos)
        {
            if (_selectedElement != null)
            {
                if (Enum.TryParse(comboBox_SelectLayer.SelectedItem.ToString(), out CompressorLayer.LayerTypeEnum layerType))
                {
                    if(this.compressorDevice1.MousePosInsideTheGrid(mousePos))
                    {
                        Point gridPos = this.compressorDevice1.MousePosToGridPos(mousePos);
                        //Console.WriteLine("Mouse: " + mousePos.ToString());
                        //Console.WriteLine("Grid Pos: " + gridPos.ToString());
                        this.compressorDevice1.Layers[layerType].SetElement((CompressorElement)_selectedElement.Clone(), gridPos.X, gridPos.Y);
                        this.compressorDevice1.Layers[layerType].ApplyRulesToTheLayer();

                        this.compressorDevice1.Refresh();

                        UpdateComponentsList();
                        SerializeAll();
                    }

                }
            }
        }

        private void UpdateComponentsList()
        {
            this.listBox_Components.Items.Clear();

            foreach(CompressorComponent component in compressorDevice1.Layers[CompressorLayer.LayerTypeEnum.Components].GetElements)
            {
                if(component != null)
                {
                    this.listBox_Components.Items.Add(component);
                }
            }
        }

        private void AfterLayerChanged()
        {
            _selectedElement = null;

            if (Enum.TryParse(comboBox_SelectLayer.SelectedItem.ToString(), out CompressorLayer.LayerTypeEnum layerType))
            {
                (string, Image)[] namesNimgs = this.compressorDevice1.Layers[layerType].GetImagesOgLayerElements();

                _pictureBoxes.Clear();

                tableLayoutPanel_Components.Controls.Clear();

                tableLayoutPanel_Components.RowCount = namesNimgs.Length / tableLayoutPanel_Components.ColumnCount + (namesNimgs.Length % tableLayoutPanel_Components.ColumnCount == 0 ? 0 : 1);
                tableLayoutPanel_Components.Size = new Size(tableLayoutPanel_Components.Width, tableLayoutPanel_Components.Size.Width / 3 * tableLayoutPanel_Components.RowCount);
                for (int i = 0; i < namesNimgs.Length; i++)
                {
                    int size = tableLayoutPanel_Components.Size.Width / 3 - 2;

                    //Size stretchedSize = new Size(tableLayoutPanel_Components.Size.Width / 3, tableLayoutPanel_Components.Size.Width / 3);
                    Size stretchedSize = new Size(size, size);
                    Image stretchedImg = BitmapProcessing.GetInterpolatedBitmap((Bitmap)namesNimgs[i].Item2, stretchedSize);

                    PictureBox pictureBox = new PictureBox() { Dock = DockStyle.Fill, AutoSize = true, Image = stretchedImg };
                    if (_toolTips.Count <= i)
                    {
                        _toolTips.Add(new ToolTip());
                        _toolTips[_toolTips.Count - 1].SetToolTip(pictureBox, namesNimgs[i].Item1);
                    }

                    switch (layerType)
                    {
                        case CompressorLayer.LayerTypeEnum.Components:
                            string orientationStr = namesNimgs[i].Item1.Split(' ')[0];
                            string typeStr = namesNimgs[i].Item1.Split(' ')[1];

                            if (Enum.TryParse(orientationStr, out CompressorComponent.ComponentOrientation orientation))
                            {
                                if (Enum.TryParse(typeStr, out CompressorComponent.ComponentType cType))
                                {
                                    _pictureBoxes.Add(pictureBox, new CompressorComponent($"default_{cType}", cType, orientation, CompressorComponent.ComponentStatus.Disabled, 1));
                                }
                            }
                            break;
                        case CompressorLayer.LayerTypeEnum.GasPipes:

                            _pictureBoxes.Add(pictureBox, new CompressorPipe(CompressorPipe.PipeType.Gas, CompressorPipe.PipeOrientation.Cross, CompressorPipe.PipeStatus.Empty));

                            break;
                        case CompressorLayer.LayerTypeEnum.OilPipes:

                            _pictureBoxes.Add(pictureBox, new CompressorPipe(CompressorPipe.PipeType.Gas, CompressorPipe.PipeOrientation.Cross, CompressorPipe.PipeStatus.Empty));

                            break;
                        default:
                            _selectedElement = null;
                            break;

                    }

                    pictureBox.Click += (s2, e2) =>
                    {
                        Console.WriteLine("Clicked Picture Box");
                        _selectedElement = _pictureBoxes[s2 as PictureBox];

                        foreach(PictureBox picture in _pictureBoxes.Keys)
                        {
                            picture.BackColor = 
                                (picture == s2 as PictureBox ? Color.Yellow : tableLayoutPanel_Components.BackColor);
                        }
                    };

                    tableLayoutPanel_Components.Controls.Add(pictureBox, i % 3, i / 3);
                }

                for (int i = 0; i < tableLayoutPanel_Components.RowCount - 1; i++)
                {
                    tableLayoutPanel_Components.RowStyles.Add(new RowStyle(SizeType.Percent));
                }

                foreach (RowStyle rowStyle in tableLayoutPanel_Components.RowStyles)
                {
                    rowStyle.SizeType = SizeType.Percent;
                    rowStyle.Height = 1f / tableLayoutPanel_Components.RowStyles.Count;
                }

            }
        }


        private void ComponentsListBoxChanged()
        {
            foreach(CompressorElement c in this.compressorDevice1.Layers[CompressorLayer.LayerTypeEnum.Components].GetElements)
            {
                if(c != null && c is CompressorComponent comp)
                {
                    comp.Selected = false;
                }
            }

            if (this.listBox_Components.SelectedItem != null)
            {
                CompressorComponent component = (CompressorComponent)this.listBox_Components.SelectedItem;

                this.textBox_SelectedComponent_Name.Text = component.Name;
                this.comboBox_SelectedComponent_Type.SelectedItem = component.Type;
                this.comboBox_SelectedComponent_Orientation.SelectedItem = component.Orientation;

                this.textBox_SelectedComponent_Name.Enabled = true;
                this.comboBox_SelectedComponent_Orientation.Enabled = true;
                this.comboBox_SelectedComponent_Type.Enabled = true;

                this.textBox_SelectedComponent_SensorName.Enabled = 
                    (component.Type == CompressorComponent.ComponentType.OpticalSensor) ||
                    (component.Type == CompressorComponent.ComponentType.Reservoir);

                this.textBox_SelectedComponent_SensorName.Text = component.SensorName;

                component.Selected = true;
            }
            else
            {
                this.textBox_SelectedComponent_Name.Text = string.Empty;
                this.textBox_SelectedComponent_SensorName.Text = string.Empty;
                this.comboBox_SelectedComponent_Type.SelectedIndex = 0;
                this.comboBox_SelectedComponent_Orientation.SelectedIndex = 0;

                this.textBox_SelectedComponent_Name.Enabled = false;
                this.comboBox_SelectedComponent_Orientation.Enabled = false;
                this.comboBox_SelectedComponent_Type.Enabled = false;
                this.textBox_SelectedComponent_SensorName.Enabled = false;

            }

            this.compressorDevice1.Refresh();
        }

        private void SaveSelectedComponent()
        {
            try
            {
                if (this.listBox_Components.SelectedItem != null)
                {
                    CompressorComponent component = (CompressorComponent)this.listBox_Components.SelectedItem;

                    if (string.IsNullOrWhiteSpace(this.textBox_SelectedComponent_Name.Text))
                    {
                        throw new Exception("Name text is empty or filled with white space.");
                    }

                    component.Name = this.textBox_SelectedComponent_Name.Text;
                    component.Type = (CompressorComponent.ComponentType)this.comboBox_SelectedComponent_Type.SelectedItem;
                    component.Orientation = (CompressorComponent.ComponentOrientation)this.comboBox_SelectedComponent_Orientation.SelectedItem;

                    if (!string.IsNullOrWhiteSpace(textBox_SelectedComponent_SensorName.Text))
                    {
                        component.SensorName = textBox_SelectedComponent_SensorName.Text;
                    }

                    this.listBox_Components.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CreateNewScheme()
        {
            JObject rootObj = new JObject();
            JObject deviceSchemeObj = new JObject();
            JArray layersArray = new JArray();


            foreach (CompressorLayer layer in compressorDevice1.Layers.Values)
            {
                if (layer.LayerType != CompressorLayer.LayerTypeEnum.Editor)
                {
                    JObject layerObj = new JObject();

                    JProperty layerTypeProperty = new JProperty("layerType", layer.LayerType.ToString());

                    layerObj.Add(layerTypeProperty);

                    JArray elementsArray = new JArray();

                    layerObj.Add("elements", elementsArray);
                    layersArray.Add(layerObj);
                }
            }

            deviceSchemeObj.Add("layers", layersArray);

            deviceSchemeObj.Add("tilemapSize", new JArray(this.compressorDevice1.TilemapSize.Width, this.compressorDevice1.TilemapSize.Height));

            rootObj.Add("deviceScheme", deviceSchemeObj);

            string jsonString = rootObj.ToString();

            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            int i;
            for (i = 1; File.Exists(@"DeviceSchemes\deviceScheme" + $"{i}.json"); i++) ;

            saveFileDialog1.FileName = $"deviceScheme{i}.json";
            saveFileDialog1.Filter = "JSON (*.json)|*.json|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.InitialDirectory = Path.GetFullPath(@"DeviceSchemes");
            saveFileDialog1.RestoreDirectory = true;

            switch (saveFileDialog1.ShowDialog())
            {
                case DialogResult.OK:

                    if ((myStream = saveFileDialog1.OpenFile()) != null)
                    {
                        byte[] data = Encoding.ASCII.GetBytes(jsonString);

                        myStream.Write(data, 0, data.Length);

                        myStream.Close();
                    }
                    break;
                default:
                    return;
            }

        }

        private bool DeleteCurrentScheme()
        {
            if (this.comboBox_SelectDeviceScheme.SelectedItem != null)
            {
                switch (MessageBox.Show("Are you really sure that you want to delete current scheme?", "Delete Confirmation", MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Yes:
                        string filePath = this.comboBox_SelectDeviceScheme.SelectedItem.ToString();

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

        private void SelectedSchemeChanged()
        {
            if (comboBox_SelectDeviceScheme.SelectedItem != null)
            {
                this.comboBox_SelectLayer.Enabled = true;
                this.numericUpDown_Rows.Enabled = true;
                this.numericUpDown_Columns.Enabled = true;

                this.textBox_SelectedComponent_SensorName.Enabled = false;
                this.comboBox_SelectedComponent_Orientation.Enabled = false;
                this.comboBox_SelectedComponent_Type.Enabled = false;

                this.textBox_SelectedComponent_SensorName.Text = string.Empty;
                this.comboBox_SelectedComponent_Orientation.SelectedItem = null;
                this.comboBox_SelectedComponent_Type.SelectedItem = null;

                this.textBox_SelectedComponent_Name.Text = string.Empty;
                this.textBox_SelectedComponent_Name.Enabled = false;

                Console.WriteLine("Before SetElements");

                foreach (CompressorLayer layer in compressorDevice1.Layers.Values)
                {
                    layer.SetElements(new CompressorElement[layer.GetElements.GetLength(0), layer.GetElements.GetLength(1)]);
                }
                Console.WriteLine("After SetElements");

                //string sensorsConfigFile = (string)this.comboBox_SelectDeviceScheme.SelectedItem;

                //if (!File.Exists(sensorsConfigFile))
                //{
                //    MessageBox.Show($"Can't find \"{sensorsConfigFile}\".");
                //}
                //else
                //{
                //    string json = File.ReadAllText(sensorsConfigFile);

                //    Console.WriteLine(this.compressorDevice1.TilemapSize);

                //    MainForm.Instance.SensorReadingHelper.Sensors.Clear();
                //    JSON_Handler.InitializeCompressorDeviceWithJson(this.compressorDevice1, json);
                //}

                string json = File.ReadAllText((string)this.comboBox_SelectDeviceScheme.SelectedItem);
                JSON_Handler.InitializeCompressorDeviceWithJson(this.compressorDevice1, json);

                Console.WriteLine("Success");
                Console.WriteLine(this.compressorDevice1.TilemapSize);

                _initialized = false;

                this.numericUpDown_Columns.Value = this.compressorDevice1.TilemapSize.Width;
                this.numericUpDown_Rows.Value = this.compressorDevice1.TilemapSize.Height;

                _initialized = true;
            }
        }

        private void SerializeAll()
        {
            try
            {
                JObject rootObj = new JObject();
                JObject deviceSchemeObj = new JObject();
                JArray layersArray = new JArray();


                foreach(CompressorLayer layer in compressorDevice1.Layers.Values)
                {
                    if(layer.LayerType != CompressorLayer.LayerTypeEnum.Editor)
                    {
                        JObject layerObj = new JObject();

                        JProperty layerTypeProperty = new JProperty("layerType", layer.LayerType.ToString());

                        layerObj.Add(layerTypeProperty);

                        JArray elementsArray = new JArray();

                        for(int i = 0; i < layer.GetElements.GetLength(0); i++)
                        {
                            for (int j = 0; j < layer.GetElements.GetLength(1); j++)
                            {
                                CompressorElement element = layer.GetElements[i, j];

                                switch (layer.LayerType)
                                {
                                    case CompressorLayer.LayerTypeEnum.Components:

                                        if (element is CompressorComponent component)
                                        {
                                            JObject componentObj = new JObject();

                                            componentObj.Add(nameof(component.Name).ToLower(), component.Name);

                                            componentObj.Add("position", new JArray(j, i));

                                            string componentTypeStr = nameof(CompressorComponent.ComponentType)[0].ToString().ToLower() + nameof(CompressorComponent.ComponentType).Substring(1);
                                            componentObj.Add(componentTypeStr, component.Type.ToString());

                                            string componentOrientationStr = nameof(CompressorComponent.ComponentOrientation)[0].ToString().ToLower() + nameof(CompressorComponent.ComponentOrientation).Substring(1);
                                            componentObj.Add(componentOrientationStr, component.Orientation.ToString());

                                            if (!string.IsNullOrEmpty(component.SensorName))
                                            {
                                                string sensorNameStr = nameof(component.SensorName)[0].ToString().ToLower() + nameof(component.SensorName).Substring(1);
                                                componentObj.Add(sensorNameStr, component.SensorName.ToString());
                                            }

                                            elementsArray.Add(componentObj);
                                        }

                                        break;

                                    case CompressorLayer.LayerTypeEnum.GasPipes:
                                    case CompressorLayer.LayerTypeEnum.OilPipes:

                                        if (element is CompressorPipe pipe)
                                        {
                                            JObject pipeObj = new JObject();

                                            pipeObj.Add("position", new JArray(j, i));

                                            string pipeOrientationStr = nameof(CompressorPipe.PipeOrientation)[0].ToString().ToLower() + nameof(CompressorPipe.PipeOrientation).Substring(1);
                                            pipeObj.Add(pipeOrientationStr, pipe.Orientation.ToString());

                                            elementsArray.Add(pipeObj);
                                        }

                                        break;

                                    default:
                                        break;
                                }
                            }
                        
                        }

                        layerObj.Add("elements", elementsArray);
                        layersArray.Add(layerObj);
                    }
                }

                deviceSchemeObj.Add("layers", layersArray);

                deviceSchemeObj.Add("tilemapSize", new JArray(this.compressorDevice1.TilemapSize.Width, this.compressorDevice1.TilemapSize.Height));

                rootObj.Add("deviceScheme", deviceSchemeObj);

                string jsonString = rootObj.ToString();

                string filePath = (string)comboBox_SelectDeviceScheme.SelectedItem;

                // Creating backup
                using (StreamWriter file = File.CreateText(filePath + ".bak"))
                {
                    file.Write(File.ReadAllText(filePath));
                }

                // serialize JSON to a string and then write string to a file
                File.WriteAllText(filePath, jsonString);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void SetColorPaletteForControls(Dictionary<FormColorVariant, Color> colorPalette)
        {
            this.BackColor = colorPalette[FormColorVariant.DarkFirst];

            this.splitContainer7.Panel2.BackColor = colorPalette[FormColorVariant.DarkSecond];
            this.compressorDevice1.BackColor = colorPalette[FormColorVariant.DarkSecond];

            foreach (Button button in this.GetAllControlsRecusrvive<Button>())
            {
                button.BackColor = colorPalette[FormColorVariant.BrightSecond];
                button.ForeColor = colorPalette[FormColorVariant.TextColorFirst];

                button.FlatAppearance.MouseDownBackColor = colorPalette[FormColorVariant.ButtonMouseDown];
                button.FlatAppearance.MouseOverBackColor = colorPalette[FormColorVariant.ButtonMouseOver];

            }

        }

    }
}
