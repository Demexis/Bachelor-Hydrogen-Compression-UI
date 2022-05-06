using Bachelor_Project.Handlers;
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
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bachelor_Project.Forms.Editor_Forms.Cyclogram_Editor_Form
{
    public partial class CyclogramEditorForm : Form
    {
        private Dictionary<Control, Action> _controlEvents;
        private Dictionary<Control, Action> _controlsStatuses;

        public CyclogramEditorForm()
        {
            InitializeComponent();
            InitializeControlEventsAndStatuses();
            InitializeControlPanel();
            InitializeCyclogram();
            InitializeCompressorScheme();

            ResetAllImportantControls();

            this.VisibleChanged += (s, e) => { ResetSchemeSelectionComboBox(); };

            this.cyclogramComponentStatusList1.OnRadioButtonCheckedChanged += () =>
            {
                SerializeAll();
                this.cyclogram1.Refresh();
            };
        }

        #region Initialization

        private void InitializeControlEventsAndStatuses()
        {
            _controlEvents = new Dictionary<Control, Action>()
            {
                [this.comboBox_SelectDeviceScheme] = () => { DeviceSchemeSelectionChanged(); UpdateCyclogramComboBox(); },
                [this.button_NewCyclogram] = () => { CreateNewCyclogram(); UpdateCyclogramComboBox(); },
                [this.comboBox_SelectCyclogram] = () => { CyclogramSelectionChanged(); },
                [this.button_AddStep] = () => { AddNewStep(); UpdateCyclogramComboBox(); UpdateStepsListBox(); SerializeAll(); },
                [this.listBox_Steps] = () => { StepsListBoxSelectionChange(); },
                [this.button_SaveStep] = () => { SaveEditedStep(); UpdateStepsListBox(); SerializeAll(); },
                [this.button_RemoveStep] = () => { RemoveSelectedStep(); UpdateStepsListBox(); SerializeAll(); },
                [this.button_MoveUpEnd] = () => { MoveSelectedStepUpEnd(); SerializeAll(); },
                [this.button_MoveUp] = () => { MoveSelectedStepUp(); SerializeAll(); },
                [this.button_MoveDown] = () => { MoveSelectedStepDown(); SerializeAll(); },
                [this.button_MoveDownEnd] = () => { MoveSelectedStepDownEnd(); SerializeAll(); },
                [this.button_DeleteCyclogram] = () => { if (DeleteCurrentCyclogram()) { UpdateCyclogramComboBox(); } }
            };

            _controlsStatuses = new Dictionary<Control, Action>()
            {
                [this.comboBox_SelectDeviceScheme] = () =>
                {
                    if (comboBox_SelectDeviceScheme.SelectedItem != null)
                    {
                        this.comboBox_SelectCyclogram.Enabled = true;
                        this.button_NewCyclogram.Enabled = true;
                        this.button_DeleteCyclogram.Enabled = true;
                    }
                    else
                    {
                        ResetAllImportantControls();
                    }
                },
                [this.button_NewCyclogram] = () =>
                {

                },
                [this.comboBox_SelectCyclogram] = () =>
                {
                    textBox_AddStep_Name.Enabled = (comboBox_SelectCyclogram.SelectedItem != null);
                    button_AddStep.Enabled = (comboBox_SelectCyclogram.SelectedItem != null);

                    button_RemoveStep.Enabled = false;
                    button_SaveStep.Enabled = false;
                },
                [this.button_AddStep] = () =>
                {

                },
                [this.listBox_Steps] = () =>
                {
                    button_SaveStep.Enabled = (listBox_Steps.SelectedItem != null);
                    button_RemoveStep.Enabled = (listBox_Steps.SelectedItem != null);
                },
                [this.button_SaveStep] = () =>
                {
                    button_SaveStep.Enabled = (listBox_Steps.SelectedItem != null);
                },
                [this.button_RemoveStep] = () =>
                {
                    button_RemoveStep.Enabled = (listBox_Steps.SelectedItem != null);
                },
                [this.button_MoveUpEnd] = () =>
                {

                },
                [this.button_MoveUp] = () =>
                {

                },
                [this.button_MoveDown] = () =>
                {

                },
                [this.button_MoveDownEnd] = () =>
                {

                }
            };

        }

        private void InitializeCyclogram()
        {
            this.cyclogram1.CyclogramName = "Preview Cyclogram";
            this.cyclogram1.OnComponentStatusChange += (c, s) =>
            {
                foreach (CompressorElement element in this.compressorDevice1.Layers[CompressorLayer.LayerType.Components].GetElements)
                {
                    if (element is CompressorComponent component)
                    {
                        if (component.Name.Equals((c as CyclogramComponentElement).Name))
                        {
                            if (Enum.TryParse((s as CyclogramStatusElement).Name, out CompressorComponent.ComponentStatus status))
                            {
                                component.Status = status;
                            }
                        }
                    }
                }

                this.compressorDevice1.Refresh();
            };
        }

        private void InitializeCompressorScheme()
        {
            this.compressorDevice1.Alignment = UserControls.Device.CompressorDevice.DeviceAlignment.Middle;
        }

        private void InitializeControlPanel()
        {
            this.cyclogram1.OnSingleExecutionEnd += () => this.controlPanel1.PlayButtonStatus = false;

            this.controlPanel1.OnPlayPauseClick += this.cyclogram1.Play;
            this.controlPanel1.OnStepForwardClick += this.cyclogram1.StepForward;
            this.controlPanel1.OnStepBackwardClick += this.cyclogram1.StepBackward;
            this.controlPanel1.OnRightEndClick += this.cyclogram1.SetRightEnd;
            this.controlPanel1.OnLeftEndClick += this.cyclogram1.SetLeftEnd;
        }

        #endregion

        private void ResetAllImportantControls()
        {
            comboBox_SelectCyclogram.Enabled = false;

            textBox_AddStep_Name.Enabled = false;

            button_NewCyclogram.Enabled = false;
            button_DeleteCyclogram.Enabled = false;
            button_AddStep.Enabled = false;
            button_SaveStep.Enabled = false;
            button_RemoveStep.Enabled = false;
        }


        private void ResetSchemeSelectionComboBox()
        {
            comboBox_SelectDeviceScheme.DataSource = null;

            comboBox_SelectDeviceScheme.DataSource = FileManager.GetDeviceSchemeFiles();

            if (comboBox_SelectDeviceScheme.Items.Count != 0)
            {
                comboBox_SelectDeviceScheme.SelectedIndex = 0;
            }
        }

        private void control_Event(object sender, EventArgs e)
        {
            if (sender is Control control)
            {
                if (_controlEvents.ContainsKey(control)) this._controlEvents[control]?.Invoke();
                if (_controlsStatuses.ContainsKey(control)) this._controlsStatuses[control]?.Invoke();
            }
        }

        private void DeviceSchemeSelectionChanged()
        {
            this.cyclogram1.Components.Clear();

            if (comboBox_SelectDeviceScheme.SelectedItem != null)
            {
                string json = File.ReadAllText((string)this.comboBox_SelectDeviceScheme.SelectedItem);
                JSON_Handler.InitializeCompressorDeviceWithJson(this.compressorDevice1, json);

                foreach (CompressorComponent component in this.compressorDevice1.GetComponentsArray())
                {
                    List<CyclogramStatusElement> statuses = new List<CyclogramStatusElement>();

                    foreach (CompressorComponent.ComponentStatus statusEnum in Enum.GetValues(typeof(CompressorComponent.ComponentStatus)))
                    {
                        statuses.Add(new CyclogramStatusElement() { Name = statusEnum.ToString() });
                    }

                    this.cyclogram1.Components.Add(new CyclogramComponentElement() { Name = component.Name, Statuses = statuses });
                }

                this.cyclogramComponentStatusList1.Cyclogram = this.cyclogram1;
            }
        }

        private void CyclogramSelectionChanged()
        {
            if (comboBox_SelectCyclogram.SelectedItem != null)
            {
                JSON_Handler.InitializeCyclogramWithJson(this.cyclogram1, File.ReadAllText((string)comboBox_SelectCyclogram.SelectedItem));
            }

            UpdateStepsListBox();
        }

        private void UpdateCyclogramComboBox()
        {
            if (comboBox_SelectDeviceScheme.SelectedItem == null) return;

            List<string> filePaths = Directory.GetFiles(@"Cyclograms", "*.*", SearchOption.AllDirectories)
            .Where(s => s.EndsWith(".json")).ToList();

            {
                List<string> toDelete = new List<string>();
                foreach (string item in comboBox_SelectCyclogram.Items)
                {
                    if (!filePaths.Contains(item))
                    {
                        toDelete.Add(item);
                    }
                }
                toDelete.ForEach(x => comboBox_SelectCyclogram.Items.Remove(x));
            }

            foreach (string filePath in filePaths)
            {
                string json = File.ReadAllText(filePath);

                JsonDocument doc = JsonDocument.Parse(json);
                JsonElement root = doc.RootElement;

                if (root.TryGetProperty("cyclogram", out JsonElement cyclogramProperty))
                {
                    if (cyclogramProperty.TryGetProperty("deviceScheme", out JsonElement deviceSchemeProperty))
                    {
                        if (((string)comboBox_SelectDeviceScheme.SelectedItem).Equals(deviceSchemeProperty.GetString()))
                        {
                            if (!comboBox_SelectCyclogram.Items.Contains(filePath))
                            {
                                comboBox_SelectCyclogram.Items.Add(filePath);
                            }

                            continue;
                        }
                    }
                }

                if (comboBox_SelectCyclogram.Items.Contains(filePath))
                {
                    comboBox_SelectCyclogram.Items.Remove(filePath);
                }
            }

            if (comboBox_SelectCyclogram.SelectedItem == null)
            {
                if (comboBox_SelectCyclogram.Items.Count != 0)
                {
                    comboBox_SelectCyclogram.SelectedIndex = 0;
                }
            }

        }

        private void UpdateStepsListBox()
        {
            listBox_Steps.Items.Clear();
            cyclogram1.Steps.ForEach(x => listBox_Steps.Items.Add(x));

            listBox_Steps.Enabled = (cyclogram1.Steps.Count != 0);

            listBox_Steps.SelectedItem = null;

            cyclogram1.Refresh();
        }

        private void StepsListBoxSelectionChange()
        {
            if (listBox_Steps.SelectedItem != null && listBox_Steps.SelectedItem is CyclogramStepElement step)
            {
                textBox_AddStep_Name.Text = step.Name;
                textBox_AddStep_LengthInMilliseconds.Text = step.LengthMilliseconds.ToString();

                this.cyclogramComponentStatusList1.CurrentStep = step;
            }
            else
            {
                this.cyclogramComponentStatusList1.CurrentStep = null;
            }
        }

        private void RemoveSelectedStep()
        {
            if (listBox_Steps.SelectedItem != null)
            {
                try
                {
                    if (listBox_Steps.SelectedItem is CyclogramStepElement step)
                    {
                        try
                        {
                            cyclogram1.Steps.Remove(step);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void SaveEditedStep()
        {
            if (listBox_Steps.SelectedItem != null)
            {
                if (listBox_Steps.SelectedItem is CyclogramStepElement step)
                {
                    try
                    {
                        step.Name = textBox_AddStep_Name.Text;
                        step.LengthMilliseconds = int.Parse(textBox_AddStep_LengthInMilliseconds.Text);

                        Console.WriteLine($"Contains? {cyclogram1.Steps.Contains(step)}");
                        Console.WriteLine($"Sequences Count: " + step.Sequences.Count);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

        }

        private void AddNewStep()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox_AddStep_Name.Text))
                {
                    throw new Exception("Name field can't be empty or filled only with white space.");
                }

                string name = textBox_AddStep_Name.Text;
                int lengthMilliseconds = int.Parse(textBox_AddStep_LengthInMilliseconds.Text);
                if (lengthMilliseconds <= 0) throw new Exception("Length should be greater than 0 ms.");

                foreach (CyclogramStepElement step in cyclogram1.Steps)
                {
                    if (step.Name.Equals(name))
                    {
                        throw new Exception("The cyclogram has a step with the same name.");
                    }
                }

                CyclogramStepElement newStep = new CyclogramStepElement(name, lengthMilliseconds);

                cyclogram1.Steps.Add(newStep);

                cyclogram1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void MoveSelectedStepUpEnd()
        {
            if (listBox_Steps.SelectedItem != null)
            {
                if (listBox_Steps.SelectedItem is CyclogramStepElement step)
                {
                    int index = cyclogram1.Steps.FindIndex(a => a == step);

                    CyclogramStepElement tmp = cyclogram1.Steps[0];
                    cyclogram1.Steps[0] = step;
                    cyclogram1.Steps[index] = tmp;

                    UpdateStepsListBox();

                    listBox_Steps.SelectedItem = step;
                }
            }
        }

        private void MoveSelectedStepUp()
        {
            if (listBox_Steps.SelectedItem != null)
            {
                if (listBox_Steps.SelectedItem is CyclogramStepElement step)
                {
                    int index = cyclogram1.Steps.FindIndex(a => a == step);

                    if (index > 0)
                    {
                        CyclogramStepElement tmp = cyclogram1.Steps[index - 1];
                        cyclogram1.Steps[index - 1] = step;
                        cyclogram1.Steps[index] = tmp;

                        UpdateStepsListBox();

                        listBox_Steps.SelectedItem = step;
                    }
                }
            }
        }

        private void MoveSelectedStepDown()
        {
            if (listBox_Steps.SelectedItem != null)
            {
                if (listBox_Steps.SelectedItem is CyclogramStepElement step)
                {
                    int index = cyclogram1.Steps.FindIndex(a => a == step);

                    if (index < cyclogram1.Steps.Count - 1)
                    {
                        CyclogramStepElement tmp = cyclogram1.Steps[index + 1];
                        cyclogram1.Steps[index + 1] = step;
                        cyclogram1.Steps[index] = tmp;

                        UpdateStepsListBox();

                        listBox_Steps.SelectedItem = step;
                    }
                }
            }
        }

        private void MoveSelectedStepDownEnd()
        {
            if (listBox_Steps.SelectedItem != null)
            {
                if (listBox_Steps.SelectedItem is CyclogramStepElement step)
                {
                    int index = cyclogram1.Steps.FindIndex(a => a == step);

                    CyclogramStepElement tmp = cyclogram1.Steps[cyclogram1.Steps.Count - 1];
                    cyclogram1.Steps[cyclogram1.Steps.Count - 1] = step;
                    cyclogram1.Steps[index] = tmp;

                    UpdateStepsListBox();

                    listBox_Steps.SelectedItem = step;
                }
            }
        }

        private bool DeleteCurrentCyclogram()
        {
            if (this.comboBox_SelectCyclogram.SelectedItem != null)
            {
                switch (MessageBox.Show("Are you really sure that you want to delete current cyclogram?", "Delete Confirmation", MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Yes:
                        string filePath = this.comboBox_SelectCyclogram.SelectedItem.ToString();

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

        private void CreateNewCyclogram()
        {
            JObject rootObj = new JObject();
            JObject cyclogramObj = new JObject();

            cyclogramObj.Add("steps", new JArray());

            cyclogramObj.Add("deviceScheme", (string)this.comboBox_SelectDeviceScheme.SelectedItem);

            rootObj.Add("cyclogram", cyclogramObj);

            string jsonString = rootObj.ToString();

            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            string deviceSchemeName = Path.GetFileNameWithoutExtension((string)this.comboBox_SelectDeviceScheme.SelectedItem);

            int i;
            for (i = 1; File.Exists(@"Cyclograms\" + deviceSchemeName + $"_cyclogram{i}.json"); i++) ;

            saveFileDialog1.FileName = deviceSchemeName + $"_cyclogram{i}.json";
            saveFileDialog1.Filter = "JSON (*.json)|*.json|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.InitialDirectory = Path.GetFullPath(@"Cyclograms");
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

        private void SerializeAll()
        {
            try
            {
                JObject rootObj = new JObject();
                JObject cyclogramObj = new JObject();
                JArray stepsArray = new JArray();

                foreach (CyclogramStepElement step in cyclogram1.Steps)
                {
                    JObject stepObj = new JObject();
                    stepObj.Add("name", step.Name);
                    stepObj.Add("lengthMilliseconds", step.LengthMilliseconds);

                    JArray sequencesArray = new JArray();
                    foreach (CyclogramSequenceElement sequence in step.Sequences)
                    {
                        JObject sequenceObj = new JObject();

                        sequenceObj.Add("componentName", sequence.ComponentName);
                        sequenceObj.Add("statusName", sequence.StatusName);

                        sequencesArray.Add(sequenceObj);
                    }

                    stepObj.Add("sequences", sequencesArray);

                    stepsArray.Add(stepObj);
                }

                cyclogramObj.Add("steps", stepsArray);

                cyclogramObj.Add("deviceScheme", (string)this.comboBox_SelectDeviceScheme.SelectedItem);

                rootObj.Add("cyclogram", cyclogramObj);

                string jsonString = rootObj.ToString();

                string filePath = (string)comboBox_SelectCyclogram.SelectedItem;

                // Creating backup
                using (StreamWriter file = File.CreateText(filePath + ".bak"))
                {
                    file.Write(File.ReadAllText(filePath));
                }

                // serialize JSON to a string and then write string to a file
                File.WriteAllText(filePath, jsonString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
