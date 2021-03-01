using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using RemoteAdmin;
using Respawning;
using System;
using System.Diagnostics;

namespace AdvSrvTools.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    internal class SpawnChaos : ICommand
    {
        public string Command { get; } = "spawnchaos";

        public string[] Aliases { get; } = { "sci" };

        public string Description { get; } = "Spawns Chaos with the van";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("ast.respawning"))
            {
                response = "You can't spawn chaos, you don't have \"ast.respawning\" permission.";
                return false;
            }

            if (Respawn.IsSpawning)
            {
                response = "A team is currently spawning, please try again.";
                return false;
            }
            sender.Respond("Starting...");
            Respawn.SummonChaosInsurgencyVan(true);
            Stopwatch stopwatch = Stopwatch.StartNew();
            while (true)
            {
                //some other processing to do possible
                if (stopwatch.ElapsedMilliseconds >= 10000)
                {
                    break;
                }
            }
            RespawnManager.Singleton.ForceSpawnTeam(SpawnableTeamType.ChaosInsurgency);
            if (sender is PlayerCommandSender player)
            {
                response = $"Chaos with van spawned by {player.Nickname}";
                return true;
            }
            else
            {
                response = "Chaos with van spawned by server console";
                return true;
            }
        }
    }
}