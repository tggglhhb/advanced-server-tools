using CommandSystem;
using System;

namespace AdvSrvTools.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    class Template : ICommand
    {
        public string Command { get; } = "";

        public string[] Aliases { get; } = { "", "" };

        public string Description { get; } = "";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            throw new NotImplementedException();
        }
    }
}