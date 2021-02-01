using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;

namespace AdvSrvTools.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    internal class WarheadSet : ICommand
    {
        public string Command { get; } = "warheadssettings";

        public string[] Aliases { get; } = { "wset", "ws" };

        public string Description { get; } = "View the status or modify the warhead behaviour. Run the command without any arguments to check the subcommands.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("ast.warhead"))
            {
                response = "You can't use this command, you don't have \"ast.warhead\" permission.";
                return false;
            }
            if (arguments.Count < 1)
            {
                response = "Subcommands (* = editable): detonationtimer (dt)*, realdetonationtimer (rdt), leverstatus (ls)*, buttonstatus (bs)*, detonated (d), isdetonating (isd), islocked (il)*, canbestarted (cbs)*, start, stop, detonate (det), shake";
                return false;
            }
            else if (arguments.At(0) == "detonationtimer" || arguments.At(0) == "dt")
            {
                if (arguments.Count >= 2)
                {
                    if (Single.TryParse(arguments.At(1), out float newdt))
                    {
                        Warhead.DetonationTimer = newdt;
                        float dt = Warhead.DetonationTimer;
                        string sp;
                        if (dt != 1) sp = "s";
                        else sp = "";
                        response = $"The warhead will now detonate in T minus {dt} second{sp}.";
                        return true;
                    }
                    else
                    {
                        float dt = Warhead.DetonationTimer;
                        string sp;
                        if (dt != 1) sp = "s";
                        else sp = "";
                        response = $"Invalid number: \"{Command} dt >{arguments.At(1)}<\" The warhead will detonate in T minus {dt} second{sp}.";
                        return false;
                    }
                }
                else
                {
                    float dt = Warhead.DetonationTimer;
                    string sp;
                    if (dt != 1) sp = "s";
                    else sp = "";
                    response = $"The warhead will detonate in T minus {dt} second{sp}. To change this, send the command again with the new timer: \"{Command} dt 90\"";
                    return true;
                }
            }
            else if (arguments.At(0) == "leverstatus" || arguments.At(0) == "ls")
            {
                if (arguments.Count >= 2)
                {
                    if (Boolean.TryParse(arguments.At(1), out bool tf))
                    {
                        Warhead.LeverStatus = tf;
                        bool ls = Warhead.LeverStatus;
                        if (ls)
                        {
                            response = "The warhead lever is now on \"enabled\"!";
                        }
                        else
                        {
                            response = "The warhead lever is now on \"disabled\"!";
                        }
                        return true;
                    }
                    else
                    {
                        bool ls = Warhead.LeverStatus;
                        string ed;
                        if (ls) ed = "enabled";
                        else ed = "disabled";

                        response = $"Invalid boolean (true/false): \"{Command} dt >{arguments.At(1)}<\" The warhead lever is on \"{ed}\".";
                        return false;
                    }
                }
                else
                {
                    bool ls = Warhead.LeverStatus;
                    string ed;
                    if (ls) ed = "enabled";
                    else ed = "disabled";
                    response = $"The warhead lever is on \"{ed}\". To edit this, send \"{Command} ls <true/false>\"";
                    return true;
                }
            }
            else if (arguments.At(0) == "buttonstatus" || arguments.At(0) == "bs")
            {
                if (arguments.Count >= 2)
                {
                    if (bool.TryParse(arguments.At(1), out bool tf))
                    {
                        Warhead.IsKeycardActivated = tf;
                        bool bs = Warhead.IsKeycardActivated;
                        if (bs)
                        {
                            response = "The warhead button cover is open";
                        }
                        else
                        {
                            response = "The warhead button cover is closed";
                        }
                        return true;
                    }
                    else
                    {
                        bool bs = Warhead.IsKeycardActivated;
                        string ed;
                        if (bs) ed = "open";
                        else ed = "closed";

                        response = $"Invalid boolean (true/false): \"{Command} bs >{arguments.At(1)}<\" The warhead button cover is {ed}.";
                        return false;
                    }
                }
                else
                {
                    bool bs = Warhead.IsKeycardActivated;
                    string ed;
                    if (bs) ed = "open";
                    else ed = "closed";
                    response = $"The warhead button cover is {ed}. To edit this, send \"{Command} bs <true/false>\"";
                    return true;
                }
            }
            else if (arguments.At(0) == "detonated" || arguments.At(0) == "d")
            {
                bool d = Warhead.IsDetonated;
                string ed;
                if (d) ed = "";
                else ed = "n\'t";
                response = $"The warhead was{ed} detonated.";
                return true;
            }
            else if (arguments.At(0) == "isdetonating" || arguments.At(0) == "isd")
            {
                bool d = Warhead.IsInProgress;
                string ed;
                if (d) ed = "";
                else ed = "n\'t";
                response = $"The warhead detonation is{ed} in progress.";
                return true;
            }
            else if (arguments.At(0) == "realdetonationtimer" || arguments.At(0) == "rdt")
            {
                string sp;
                if (Warhead.RealDetonationTimer != 1) sp = "s";
                else sp = "";
                response = $"The warhead will detonate in T minus {Warhead.RealDetonationTimer} second{sp}.";
                return true;
            }
            else if (arguments.At(0) == "islocked" || arguments.At(0) == "il")
            {
                if (arguments.Count >= 2)
                {
                    if (bool.TryParse(arguments.At(1), out bool tf))
                    {
                        Warhead.IsLocked = tf;
                        bool bs = Warhead.IsLocked;
                        if (bs)
                        {
                            response = "The warhead is locked";
                        }
                        else
                        {
                            response = "The warhead isn't locked";
                        }
                        return true;
                    }
                    else
                    {
                        bool il = Warhead.IsLocked;
                        string ed;
                        if (il) ed = " locked";
                        else ed = "n't locked";
                        response = $"Invalid boolean (true/false): \"{Command} bs >{arguments.At(1)}<\" The warhead is{ed}.";
                        return false;
                    }
                }
                else
                {
                    bool il = Warhead.IsLocked;
                    string ed;
                    if (il) ed = " locked";
                    else ed = "n't locked";
                    response = $"The warhead is{ed}. To edit this, send \"{Command} il <true/false>\"";
                    return true;
                }
            }
            else if (arguments.At(0) == "canbestarted" || arguments.At(0) == "cbs")
            {
                bool d = Warhead.CanBeStarted;
                string ed;
                if (d) ed = "be started";
                else ed = "be resumed";
                response = $"The warhead detonation can {ed}";
                return true;
            }
            else if (arguments.At(0) == "start")
            {
                Warhead.Start();
                if (Warhead.CanBeStarted) response = $"Detonation sequence started, T minus {Warhead.DetonationTimer} seconds";
                else response = $"Detonation sequence resumed, T minus {Warhead.DetonationTimer} seconds";
                return true;
            }
            else if (arguments.At(0) == "stop")
            {
                Warhead.Stop();
                response = $"Detonation cancelled, restarting systems";
                return true;
            }
            else if (arguments.At(0) == "detonate" || arguments.At(0) == "det")
            {
                Warhead.Detonate();
                response = $"Warhead detonated";
                return true;
            }
            else if (arguments.At(0) == "shake")
            {
                Warhead.Shake();
                response = $"Shake effect played";
                return true;
            }
            else
            {
                response = "Invalid subcommand. Subcommands (* = editable): detonationtimer (dt)*, realdetonationtimer (rdt), leverstatus (ls)*, buttonstatus (bs)*, detonated (d), isdetonating (isd), islocked (il)*, canbestarted (cbs)*, start, stop, detonate (det), shake";
                return false;
            }
        }
    }
}