using CommandSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvSrvTools.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    class UpdateAST: ICommand
    {
        public string Command { get; } = "updateast";

        public string[] Aliases { get; } = { "uast", "ua" };

        public string Description { get; } = "Advanced Server Tools updater";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Task task = Updater.RunUpdater();

            response = "Updater launched, check server console for details";
            return true;
        }
    }
}
