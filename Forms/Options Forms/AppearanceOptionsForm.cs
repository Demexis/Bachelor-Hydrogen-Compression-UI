using Bachelor_Project.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bachelor_Project.Forms.Options_Forms
{
    public enum FormColorVariant 
    { 
        Outline, TextColorFirst, TextColorSecond,
        ButtonMouseDown, ButtonMouseOver, 
        BrightFirst, BrightSecond, 
        NormalFirst, NormalSecond, 
        DarkFirst, DarkSecond,
        SidePanelBright, SidePanelNormal, SidePanelDark, SidePanelButtonDown, SidePanelButtonOver
    };


    public partial class AppearanceOptionsForm : Form
    {
        public static AppearanceOptionsForm Instance;

        public static Action<Dictionary<FormColorVariant, Color>> OnColorPaletteChange;

        public static Dictionary<FormColorVariant, Color> SelectedColorPalette { get; private set; }

        public static Dictionary<string, Dictionary<FormColorVariant, Color>> ColorPalettes = new Dictionary<string, Dictionary<FormColorVariant, Color>>()
        {
            ["Blackberry"] = new Dictionary<FormColorVariant, Color>()
            {
                [FormColorVariant.Outline] = Color.FromArgb(32, 30, 55),
                [FormColorVariant.TextColorFirst] = Color.FromArgb(220, 220, 220),
                [FormColorVariant.TextColorSecond] = Color.FromArgb(180, 160, 220),

                [FormColorVariant.ButtonMouseDown] = Color.FromArgb(63, 61, 82),
                [FormColorVariant.ButtonMouseOver] = Color.FromArgb(113, 111, 162),

                [FormColorVariant.BrightFirst] = Color.FromArgb(250, 250, 250),
                [FormColorVariant.BrightSecond] = Color.FromArgb(93, 91, 122),
                [FormColorVariant.NormalFirst] = Color.FromArgb(42, 40, 65),
                [FormColorVariant.NormalSecond] = Color.FromArgb(52, 50, 75), 
                [FormColorVariant.DarkFirst] = Color.FromArgb(32, 30, 45),
                [FormColorVariant.DarkSecond] = Color.FromArgb(23, 21, 32),

                [FormColorVariant.SidePanelBright] = Color.FromArgb(35, 32, 39),
                [FormColorVariant.SidePanelNormal] = Color.FromArgb(32, 21, 32),
                [FormColorVariant.SidePanelDark] = Color.FromArgb(24, 13, 24),

                [FormColorVariant.SidePanelButtonDown] = Color.FromArgb(72, 48, 64),
                [FormColorVariant.SidePanelButtonOver] = Color.FromArgb(52, 40, 48)
            },
            ["Gray"] = new Dictionary<FormColorVariant, Color>()
            {
                [FormColorVariant.Outline] = Color.FromArgb(55, 55, 55),
                [FormColorVariant.TextColorFirst] = Color.FromArgb(220, 220, 220),
                [FormColorVariant.TextColorSecond] = Color.FromArgb(220, 220, 220),

                [FormColorVariant.ButtonMouseDown] = Color.FromArgb(82, 82, 82),
                [FormColorVariant.ButtonMouseOver] = Color.FromArgb(162, 162, 162),

                [FormColorVariant.BrightFirst] = Color.FromArgb(250, 250, 250),
                [FormColorVariant.BrightSecond] = Color.FromArgb(122, 122, 122),
                [FormColorVariant.NormalFirst] = Color.FromArgb(65, 65, 65),
                [FormColorVariant.NormalSecond] = Color.FromArgb(75, 75, 75),
                [FormColorVariant.DarkFirst] = Color.FromArgb(45, 45, 45),
                [FormColorVariant.DarkSecond] = Color.FromArgb(32, 32, 32),

                [FormColorVariant.SidePanelBright] = Color.FromArgb(39, 39, 39),
                [FormColorVariant.SidePanelNormal] = Color.FromArgb(32, 32, 32),
                [FormColorVariant.SidePanelDark] = Color.FromArgb(24, 24, 24),

                [FormColorVariant.SidePanelButtonDown] = Color.FromArgb(72, 72, 72),
                [FormColorVariant.SidePanelButtonOver] = Color.FromArgb(52, 52, 52)
            }
        };

        public AppearanceOptionsForm()
        {
            InitializeComponent();

            if (Instance == null) Instance = this;
            else this.Close();

            this.comboBox_ColorThemes.DataSource = ColorPalettes.Keys.ToList();

            if (this.comboBox_ColorThemes.Items.Count > 0) this.comboBox_ColorThemes.SelectedIndex = 0;

            OnColorPaletteChange += SetColorPaletteForControls;
        }

        private void comboBox_ColorThemes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox_ColorThemes.SelectedItem != null)
            {
                SelectedColorPalette = ColorPalettes[comboBox_ColorThemes.SelectedItem.ToString()];

                OnColorPaletteChange?.Invoke(SelectedColorPalette);
            }
        }

        public void SetColorPaletteForControls(Dictionary<FormColorVariant, Color> colorPalette)
        {
            this.BackColor = colorPalette[FormColorVariant.DarkFirst];
            this.panel1.BackColor = colorPalette[FormColorVariant.DarkSecond];

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
