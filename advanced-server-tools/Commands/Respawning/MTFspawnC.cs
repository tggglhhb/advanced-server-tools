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
    internal class SpawnMTF : ICommand
    {
        public string Command { get; } = "spawnmtf";

        public string[] Aliases { get; } = { "smtf" };

        public string Description { get; } = "Spawns MTF with the chopper";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("ast.respawning"))
            {
                response = "You can't spawn MTF, you don't have \"ast.respawning\" permission.";
                return false;
            }

            if (Respawn.IsSpawning)
            {
                response = "A team is currently spawning, please try again.";
                return false;
            }
            sender.Respond("Starting...");
            Respawn.SummonNtfChopper();
            //            RespawnEffectsController.ExecuteAllEffects(RespawnEffectsController.EffectType.Selection, SpawnableTeamType.NineTailedFox);
            Stopwatch stopwatch = Stopwatch.StartNew();
            while (true)
            {
                //some other processing to do possible
                if (stopwatch.ElapsedMilliseconds >= 15000)
                {
                    break;
                }
            }
            RespawnManager.Singleton.ForceSpawnTeam(SpawnableTeamType.NineTailedFox);
            if (sender is PlayerCommandSender player)
            {
                response = $"MTF and chopper spawned by {player.Nickname}";
                return true;
            }
            else
            {
                response = "MTF and chopper spawned by server console";
                return true;
            }
        }
    }
}