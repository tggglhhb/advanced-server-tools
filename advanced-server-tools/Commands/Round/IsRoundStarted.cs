using CommandSystem;
using System;

namespace AdvSrvTools.Commands.Round
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    class IsRoundStarted : ICommand
    {
        public string Command { get; } = "isroundstarted";

        public string[] Aliases { get; } = { "irs" };

        public string Description { get; } = "Check if round is started";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            bool isr = Exiled.API.Features.Round.IsStarted;
            string ed;
            if (isr) ed = "";
            else ed = "n't";
            response = $"The round is{ed} started";
            return true;
        }
    }
}