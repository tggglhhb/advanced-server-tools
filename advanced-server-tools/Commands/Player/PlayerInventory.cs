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
        public string Command { get; } = "manageplayerinventory";

        public string[] Aliases { get; } = { "mpinv", "mpi" };

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
                response = "Subcommands: clear (c), isenabled (ie), display(d)";
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
            else if (arguments.At(0) == "isenabled" || arguments.At(0) == "ie")
            {
                if (arguments.Count > 1)
                {
                    P Ply = P.Get(arguments.At(1));
                    //Ply.Inventory.Invoke(methodName: );
                    if (Ply == null)
                    {
                        response = $"Player not found: {arguments.At(1)}";
                        return false;
                    }
                    if (arguments.Count > 2)
                    {
                        if (bool.TryParse(arguments.At(1), out bool e))
                        {
                            Ply.Inventory.enabled = e;
                            string possessive = "s";
                            if (Ply.Nickname.EndsWith("s") || Ply.Nickname.EndsWith("S"))
                            {
                                possessive = "";
                            }
                            string ed;
                            if (Ply.Inventory.enabled) ed = "enabled";
                            else if (!Ply.Inventory.enabled) ed = "disabled";
                            else ed = "error";
                            response = $"{Ply.Nickname}'{possessive} inventory is now {ed}";
                            return true;
                        }
                        else
                        {
                            string ed;
                            if (Ply.Inventory.enabled) ed = "enabled";
                            else if (!Ply.Inventory.enabled) ed = "disabled";
                            else ed = "error";
                            string possessive = "s";
                            if (Ply.Nickname.EndsWith("s") || Ply.Nickname.EndsWith("S")) possessive = "";

                            response = $"{Ply.Nickname}'{possessive} inventory is {ed}. Invalid bool (true/false): {Aliases[0]} {arguments.At(0)} >>{arguments.At(1)}<<";
                            return false;
                        }
                    }
                    else 
                    {
                        string ed;
                        if (Ply.Inventory.enabled) ed = "enabled";
                        else if (!Ply.Inventory.enabled) ed = "disabled";
                        else ed = "error";
                        string possessive = "s";
                        if (Ply.Nickname.EndsWith("s") || Ply.Nickname.EndsWith("S")) possessive = "";

                        response = $"{Ply.Nickname}'{possessive} inventory is {ed}.To edit this: {Aliases[0]} {arguments.At(0)} <true/false>";

                        return true;
                    }
                }
                else
                {
                    response = $"Enables or disables the inventory of a player. Usage: {Command} ie <user> <true/false>";
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
