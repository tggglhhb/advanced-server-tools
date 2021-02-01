using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using RemoteAdmin;
using System;

namespace AdvSrvTools.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    internal class ChopperCall : ICommand
    {
        public string Command { get; } = "choppercall";

        public string[] Aliases { get; } = { "chc" };

        public string Description { get; } = "Calls the MTF Chopper";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("ast.respawning"))
            {
                response = "You can't spawn the chopper, you don't have \"ast.respawning\" permission.";
                return false;
            }

            Respawn.SummonNtfChopper();
            //RespawnEffectsController.ExecuteAllEffects(RespawnEffectsController.EffectType.Selection, SpawnableTeamType.NineTailedFox);
            if (sender is PlayerCommandSender player)
            {
                response = $"Chopper called by {player.Nickname}";
                return true;
            }
            else
            {
                response = "Chopper called by server console";
                return true;
            }
        }
    }
}