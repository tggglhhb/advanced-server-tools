using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using Respawning;
using System;

namespace AdvSrvTools.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    internal class NextSpawn : ICommand
    {
        public string Command { get; } = "nextspawn";

        public string[] Aliases { get; } = { "ns" };

        public string Description { get; } = "Shows Next Spawn stats";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("ast.respawning"))
            {
                response = "You can't spawn chaos, you don't have \"ast.respawning\" permission.";
                return false;
            }
            var nkt = RespawnManager.Singleton.NextKnownTeam;
            int TimeUntilRespawn = Respawn.TimeUntilRespawn;
            response = $"{nkt} will spawn in {TimeUntilRespawn}s";
            return true;
        }
    }
}