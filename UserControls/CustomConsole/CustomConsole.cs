using Bachelor_Project.Extensions;
using Bachelor_Project.Forms.Options_Forms;
using Bachelor_Project.UserControls.CustomConsole.ConsoleCommands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bachelor_Project.UserControls.CustomConsole
{
    public partial class CustomConsole : UserControl
    {
        public static CustomConsole Instance;

        public bool DebugMode = false;

        [Category("Appearance"), Description("The maximum number of messages that the console can display. If the number of messages is exceeded, the previous ones are deleted.")]
        public uint MaxMessagesCount { get; set; } = 50;
        private List<CustomConsoleMessage> _customConsoleMessages = new List<CustomConsoleMessage>();

        public List<CustomConsoleCommand> CustomConsoleCommands { get; private set; } = new List<CustomConsoleCommand>();

        public CustomConsole()
        {
            InitializeComponent();

            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                this.Parent.Controls.Remove(this);
            }

            CustomConsoleCommands = new List<CustomConsoleCommand>()
            {
                new CustomConsoleHelpCommand(this),
                new CustomConsoleDebugCommand(this),
                new CustomConsoleSendDataToSerialPortCommand(this)
            };

            AppearanceOptionsForm.OnColorPaletteChange += SetColorPaletteForControls;
        }

        public void Log(string message)
        {
            Log(message, Color.FromArgb(175, 175, 175));
        }

        public void Log(string message, Color color)
        {
            if (_customConsoleMessages.Count >= MaxMessagesCount)
            {
                _customConsoleMessages.RemoveAt(0);
            }

            _customConsoleMessages.Add(new CustomConsoleMessage() { Text = message, Color = color });

            UpdateConsole();
        }

        public void LogWarning(string message)
        {
            if (_customConsoleMessages.Count >= MaxMessagesCount)
            {
                _customConsoleMessages.RemoveAt(0);
            }

            _customConsoleMessages.Add(new CustomConsoleMessage() { Text = message, Color = Color.Orange });

            UpdateConsole();
        }

        public void LogError(string message)
        {
            if (_customConsoleMessages.Count >= MaxMessagesCount)
            {
                _customConsoleMessages.RemoveAt(0);
            }

            _customConsoleMessages.Add(new CustomConsoleMessage() { Text = message, Color = Color.Red });

            UpdateConsole();
        }

        public void UpdateConsole()
        {
            try
            {
                richTextBox_Console.Invoke(new Action(() =>
                {
                    richTextBox_Console.Clear();

                    foreach (CustomConsoleMessage msg in _customConsoleMessages)
                    {
                        richTextBox_Console.AppendText(msg.Text, msg.Color);
                        richTextBox_Console.AppendText(System.Environment.NewLine);
                    }

                    richTextBox_Console.ScrollToCaret();
                }));
            }
            catch(Exception ex)
            {
                // Skip...
                // Window hasn't been created yet...
            }

        }

        private void button_Send_Click(object sender, EventArgs e)
        {
            string commandText = textBox.Text;

            if(!string.IsNullOrWhiteSpace(commandText))
            {
                this.Log($">: {commandText}", Color.Gainsboro);

                string[] words = commandText.Split(' ');

                string commandName = words[0];

                string[] commandParams = new string[words.Length - 1];
                Array.Copy(commandText.Split(' '), 1, commandParams, 0, words.Length - 1);

                for (int i = 0; i < commandParams.Length; i++)
                {
                    commandParams[i] = commandParams[i].ToLower();
                }


                bool found = false;
                foreach (CustomConsoleCommand command in CustomConsoleCommands)
                {
                    if (command.Name.ToLower() == commandName.ToLower())
                    {
                        if (!command.Execute(commandParams))
                        {
                            this.LogError(command.GetCommandSyntax());
                        }

                        found = true;
                        break;
                    }
                }

                if(!found) this.LogWarning("No such command: " + commandName);

                this.textBox.Text = string.Empty;
            }

            UpdateConsole();
        }

        public void SetColorPaletteForControls(Dictionary<FormColorVariant, Color> colorPalette)
        {
            this.BackColor = colorPalette[FormColorVariant.DarkSecond];
            this.richTextBox_Console.BackColor = colorPalette[FormColorVariant.DarkSecond];
            this.richTextBox_Console.ForeColor = colorPalette[FormColorVariant.TextColorFirst];

            foreach (Button button in this.GetAllControlsRecusrvive<Button>())
            {
                button.BackColor = colorPalette[FormColorVariant.BrightSecond];
                button.ForeColor = colorPalette[FormColorVariant.TextColorFirst];

                button.FlatAppearance.MouseDownBackColor = colorPalette[FormColorVariant.ButtonMouseDown];
                button.FlatAppearance.MouseOverBackColor = colorPalette[FormColorVariant.ButtonMouseOver];

            }
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                button_Send_Click(sender, e);
                e.SuppressKeyPress = true;
            }
        }
    }
}
