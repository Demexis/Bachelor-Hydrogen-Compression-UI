using Bachelor_Project.UserControls.Device;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bachelor_Project.UserControls.CyclogramComponentStatusList
{
    public partial class CyclogramComponentStatusList : UserControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Cyclogram Cyclogram
        {
            get { return _cyclogram; }
            set 
            { 
                _cyclogram = value; 
                RecreateTableLayoutRows();
                RecreateGroupBoxesAndRadioButtons();
            }
        }
        private Cyclogram _cyclogram;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CyclogramStepElement CurrentStep
        {
            get { return _currentStep; }
            set 
            { 
                _currentStep = value;
                ResetRadioButtons();

                this.label_SelectedStep.Text = $"Selected Step: " + (_currentStep != null ? _currentStep.Name : "");
            }
        }
        private CyclogramStepElement _currentStep;

        private Dictionary<RadioButton, (CyclogramComponentElement, CyclogramStatusElement)> _radioButtonComponentStatuses;

        public Action OnRadioButtonCheckedChanged;

        public int GroupBoxHeight { get; set; } = 60;

        public CyclogramComponentStatusList()
        {
            InitializeComponent();
        }

        private void RecreateTableLayoutRows()
        {
            tableLayoutPanel_ComponentsAndStatuses.Controls.Clear();
            tableLayoutPanel_ComponentsAndStatuses.RowStyles.Clear();

            if (Cyclogram == null) return;

            tableLayoutPanel_ComponentsAndStatuses.RowCount = Cyclogram.Components.Count;
            tableLayoutPanel_ComponentsAndStatuses.Size = new Size(tableLayoutPanel_ComponentsAndStatuses.Width, GroupBoxHeight * tableLayoutPanel_ComponentsAndStatuses.RowCount);

            for (int i = 0; i < tableLayoutPanel_ComponentsAndStatuses.RowCount; i++)
            {
                tableLayoutPanel_ComponentsAndStatuses.RowStyles.Add(new RowStyle(SizeType.Percent));
            }

            foreach (RowStyle rowStyle in tableLayoutPanel_ComponentsAndStatuses.RowStyles)
            {
                rowStyle.SizeType = SizeType.Percent;
                rowStyle.Height = 1f / tableLayoutPanel_ComponentsAndStatuses.RowStyles.Count;
            }
            
        }

        private void RecreateGroupBoxesAndRadioButtons()
        {
            _radioButtonComponentStatuses = new Dictionary<RadioButton, (CyclogramComponentElement, CyclogramStatusElement)>();

            if (Cyclogram == null) return;

            Console.WriteLine("Components count " + Cyclogram.Components.Count.ToString());

            for (int i = 0; i < Cyclogram.Components.Count; i++)
            {
                CyclogramComponentElement component = Cyclogram.Components[i];

                GroupBox groupBox = new GroupBox()
                {
                    ForeColor = this.label_SelectedStep.ForeColor,
                    Font = this.label_SelectedStep.Font,
                    Text = Cyclogram.Components[i].Name,
                    Dock = DockStyle.Fill,
                    AutoSize = true
                };

                FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel()
                {
                    Dock = DockStyle.Top,
                    FlowDirection = FlowDirection.LeftToRight,
                    Size = new Size(Size.Width, 20 * component.Statuses.Count)
                };

                foreach (CyclogramStatusElement status in component.Statuses)
                {
                    RadioButton radioButton = new RadioButton()
                    {
                        ForeColor = this.label_SelectedStep.ForeColor,
                        Font = this.label_SelectedStep.Font,
                        Text = status.Name
                    };
                    
                    _radioButtonComponentStatuses.Add(radioButton, (component, status));

                    radioButton.CheckedChanged += (s, e) =>
                    {
                        if(((RadioButton)s).Checked)
                        {
                            CyclogramComponentElement comp;
                            CyclogramStatusElement compStatus;

                            (comp, compStatus) = _radioButtonComponentStatuses[(RadioButton)s];

                            if (CurrentStep != null)
                            {
                                CurrentStep.Sequences.RemoveAll(x => x.ComponentName.Equals(comp.Name) && !x.StatusName.Equals(compStatus.Name));
                                if (((RadioButton)s).Checked)
                                    CurrentStep.Sequences.Add(new CyclogramSequenceElement(comp.Name, compStatus.Name));

                                OnRadioButtonCheckedChanged?.Invoke();
                            }
                        }

                    };


                    flowLayoutPanel.Controls.Add(radioButton);
                }

                groupBox.Controls.Add(flowLayoutPanel);

                tableLayoutPanel_ComponentsAndStatuses.Controls.Add(groupBox, 0, i);
            }
        }

        private void ResetRadioButtons()
        {
            if(_radioButtonComponentStatuses != null)
            {
                foreach (RadioButton radioButton in _radioButtonComponentStatuses.Keys)
                {
                    radioButton.Enabled = (CurrentStep != null);
                }
            }

            if (CurrentStep != null)
            {
                Dictionary<RadioButton, bool> _radioChecked = new Dictionary<RadioButton, bool>();


                foreach (RadioButton radioButton in _radioButtonComponentStatuses.Keys)
                {
                    radioButton.Checked = false;

                    CyclogramComponentElement component;
                    CyclogramStatusElement status;

                    (component, status) = _radioButtonComponentStatuses[radioButton];

                    foreach (CyclogramSequenceElement sequence in CurrentStep.Sequences)
                    {
                        if (sequence.ComponentName.Equals(component.Name) && sequence.StatusName.Equals(status.Name))
                        {
                            _radioChecked.Add(radioButton, true);

                            break;
                        }
                    }

                }

                foreach (RadioButton radio in _radioChecked.Keys)
                {
                    radio.Checked = _radioChecked[radio];
                }

            }

        }
    }
}
