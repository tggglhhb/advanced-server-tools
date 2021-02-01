using CommandSystem;
using System;
using P = Exiled.API.Features.Player;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.Permissions.Extensions;

namespace AdvSrvTools.Commands.Player
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    internal class PlayerInventory : ICommand
    {
        public string Command { get; } = "playerinventory";

        public string[] Aliases { get; } = { "pinv", "pi" };

        public string Description { get; } = "A tool to manage players inventory";

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
                        response = "You cannot clear the inventory of this player.";
                        return false;
                    }
                    Ply.Inventory.Clear();
                    response = $"Succesfully cleared the inventory of {Ply.Nickname}";
                    return true;
                }
                else
                {
                    response = $"Clears the inventory of a player. Usage: {Command} c <user>";
                    return false;
                }
            }
            else if (arguments.At(0) == "cooldown" || arguments.At(0) == "cd")
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
                        response = "You cannot set the inventory cooldown of this player.";
                        return false;
                    }
                    //Ply.Inventory.InventoryCooldown = 1f;
                    string possessive = "s";
                    if (Ply.Nickname.EndsWith("s") || Ply.Nickname.EndsWith("S"))
                     {
                        possessive = "";
                     }
                    response = $"{Ply.Nickname}'{possessive} inventory cooldown is now ";
                    return true;
                }
                else
                {
                    response = $"Clears the inventory of a player. Usage: {Command} c <user>";
                    return false;
                }
            }

            //P Ply = P.Get(arguments.At(1));
            //Ply.Inventory.Clear();
            throw new NotImplementedException();
        }
    }
}
