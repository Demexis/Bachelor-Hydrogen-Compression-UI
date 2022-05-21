using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor_Project.UserControls.CustomConsole
{
    public abstract class CustomConsoleCommand
    {
        public string Name;
        protected CustomConsole _console;

        public abstract bool Execute(params string[] commandParams);

        public abstract string GetCommandSyntax();
        public abstract string GetCommandInfo();

        public CustomConsoleCommand(CustomConsole customConsole) 
        {
            _console = customConsole;
        }
    }
}
