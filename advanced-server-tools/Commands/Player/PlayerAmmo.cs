using CommandSystem;
using Exiled.Permissions.Extensions;
using System;
using P = Exiled.API.Features.Player;

namespace AdvSrvTools.Commands.Player
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    internal class PlayerAmmo : ICommand
    {
        public string Command { get; } = "playerammmo";

        public string[] Aliases { get; } = { "pa" };

        public string Description { get; } = "A command for managing player ammo. Run the command without any arguments to check the subcommands.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("ast.playermgm"))
            {
                response = "You can't use this command, you don't have \"ast.playermgm\" permission.";
                return false;
            }
            if (arguments.Count < 1)
            {
                response = "Subcommands: clear (c), drop (d), reset (r). Will soon add the option to drop specific ammo types";
                return false;
            }
            else if (arguments.At(0) == "clear" || arguments.At(0) == "c")
            {
                if (arguments.Count > 1)
                {
                    P Ply = P.Get(arguments.At(1));
                    if (Ply == null)
                    {
                        response = $"Player not found: {arguments.At(1)}";
                        return false;
                    }
                    if (Ply.Role == RoleType.Spectator || Ply.Role == RoleType.None || Ply.Team == Team.SCP)
                    {
                        response = "You cannot clear the ammo of this player.";
                        return false;
                    }
                    Ply.Ammo.Clear();
                    response = $"Succesfully cleared the ammo of {Ply.Nickname}";
                    return true;
                }
                else
                {
                    response = $"Clears the ammo of a player. Usage: {Command} c <user>";
                    return false;
                }
            }
            else if (arguments.At(0) == "drop" || arguments.At(0) == "d")
            {
                if (arguments.Count > 1)
                {
                    P Ply = P.Get(arguments.At(1));
                    if (Ply == null)
                    {
                        response = $"Player not found: {arguments.At(1)}";
                        return false;
                    }
                    if (Ply.Role == RoleType.Spectator || Ply.Role == RoleType.None || Ply.Team == Team.SCP)
                    {
                        response = "You cannot drop the ammo of this player.";
                        return false;
                    }
                    Ply.Ammo.DropAll();
                    response = $"Succesfully dropped the ammo of {Ply.Nickname}";
                    return true;
                }
                else
                {
                    response = $"Drops the ammo of a player. Usage: {Command} d <user>";
                    return false;
                }
            }
            else if (arguments.At(0) == "reset" || arguments.At(0) == "r")
            {
                if (arguments.Count > 1)
                {
                    P Ply = P.Get(arguments.At(1));
                    if (Ply == null)
                    {
                        response = $"Player not found: {arguments.At(1)}";
                        return false;
                    }
                    if (Ply.Role == RoleType.Spectator || Ply.Role == RoleType.None || Ply.Team == Team.SCP)
                    {
                        response = "You cannot reset the ammo of this player.";
                        return false;
                    }
                    Ply.Ammo.ResetAmmo();
                    response = $"Succesfully reset the ammo of {Ply.Nickname}";
                    return true;
                }
                else
                {
                    response = $"Resets the ammo of a player to the class' default. Usage: {Command} c <user>";
                    return false;
                }
            }
            else
            {
                response = "Invalid subcommand. Subcommands: clear (c), drop (d), reset (r). Will soon add the option to drop specific ammo types";
                return false;
            }
        }
    }
}