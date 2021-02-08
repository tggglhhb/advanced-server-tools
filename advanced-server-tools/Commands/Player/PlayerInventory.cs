using CommandSystem;
using System;
using P = Exiled.API.Features.Player;
using Exiled.Permissions.Extensions;

namespace AdvSrvTools.Commands.Player
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    internal class PlayerInventory : ICommand
    {
        public string Command { get; } = "manageplayerinventory";

        public string[] Aliases { get; } = { "mpinv", "mpi" };

        public string Description { get; } = "A tool to manage players inventory";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            throw new NotImplementedException();

            if (!sender.CheckPermission("ast.playermgm"))
            {
                response = "You can't use this command, you don't have \"ast.playermgm\" permission.";
                return false;
            }
            if (arguments.Count < 1)
            {
                response = "Subcommands: clear (c), additem (ai), display(d)";
                return true;
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
                    return true;
                }
            }
            else if (arguments.At(0) == "additem" || arguments.At(0) == "ai")
            {
                if (arguments.Count > 1)
                {
                    P Ply = P.Get(arguments.At(1));
                    if (Ply == null)
                    {
                        response = $"Player not found: {arguments.At(1)}";
                        return false;
                    }
                    if (arguments.Count > 2)
                    {

                    }
                    else
                    {
                        response = "";
                        return true;
                    }
                }
                else
                {
                    response = $"Adds an item to the inventory of a player. Usage: {Command} ai <user> <item>";
                    return true;
                }
            }
            else
            {
                response = "Invalid subcommand. Subcommands: ";
                return false;
            }
        }
    }
}
