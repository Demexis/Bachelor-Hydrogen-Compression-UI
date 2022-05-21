using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor_Project.UserControls.CustomConsole.ConsoleCommands
{
    class CustomConsoleSendDataToSerialPortCommand : CustomConsoleCommand
    {
        public CustomConsoleSendDataToSerialPortCommand(CustomConsole customConsole) : base(customConsole)
        {
            Name = "/send";
        }

        public override bool Execute(params string[] commandParams)
        {
            if (commandParams.Length == 1)
            {
                if(COM_Handler.ConnectedToPort && COM_Handler.MainSerialPort.IsOpen)
                {
                    try
                    {
                        COM_Handler.MainSerialPort.WriteLine(commandParams[0]);
                    }
                    catch(Exception ex)
                    {
                        _console.LogError($"Could not send data to COM port. Error message: {ex.Message}");
                    }
                }
                else
                {
                    _console.LogError("The COM port is not available or closed.");
                }

                return true;
            }

            _console.LogWarning("The /send command can take only 1 argument. Type '/help' to find more information.");

            return false;
        }

        public override string GetCommandInfo()
        {
            return $"sends the message specified in the first argument to the COM port. For example: \"{Name} trigger_42.1\".";
        }

        public override string GetCommandSyntax()
        {
            string msg = string.Empty;

            msg += $"{Name} [message]";

            return msg;
        }
    }
}
