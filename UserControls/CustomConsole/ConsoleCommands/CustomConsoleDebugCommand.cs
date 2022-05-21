using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor_Project.UserControls.CustomConsole.ConsoleCommands
{
    public class CustomConsoleDebugCommand : CustomConsoleCommand
    {
        public CustomConsoleDebugCommand(CustomConsole customConsole) : base(customConsole)
        {
            Name = "/debug";
        }

        public override bool Execute(params string[] commandParams)
        {
            if (commandParams.Length == 0)
            {
                _console.DebugMode = !_console.DebugMode;

                _console.Log($"Debug mode is now " + (_console.DebugMode ? "active" : "inactive") + ".");

                return true;
            }

            _console.LogWarning("The /debug command cannot take arguments.");

            return false;
        }

        public override string GetCommandInfo()
        {
            return "allows to get more information while the application is running. Necessary for the developer to debug the release version of the program.";
        }

        public override string GetCommandSyntax()
        {
            string msg = string.Empty;

            msg += $"{Name}";

            return msg;
        }
    }
}
