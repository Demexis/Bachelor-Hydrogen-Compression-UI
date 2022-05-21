using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor_Project.UserControls.CustomConsole.ConsoleCommands
{
    public class CustomConsoleHelpCommand : CustomConsoleCommand
    {

        public CustomConsoleHelpCommand(CustomConsole customConsole) : base(customConsole)
        {
            Name = "/help";
        }

        public override bool Execute(params string[] commandParams)
        {
            string msg =
            "List of commands:" + System.Environment.NewLine +
            "-------------------" + System.Environment.NewLine; 

            foreach(CustomConsoleCommand command in CustomConsole.Instance.CustomConsoleCommands)
            {
                msg += $"{command.GetCommandSyntax()} - {command.GetCommandInfo()} {System.Environment.NewLine}";
            }

            msg += "-------------------";

            if (commandParams.Length == 0)
            {
                _console.Log(msg);

                return true;
            }

            _console.LogWarning("The /help command cannot take arguments.");
            _console.Log(msg);

            return true;
        }

        public override string GetCommandInfo()
        {
            return "get help information.";
        }

        public override string GetCommandSyntax()
        {
            return "/help";
        }
    }
}
