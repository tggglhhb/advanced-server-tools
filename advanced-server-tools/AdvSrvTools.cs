using Exiled.API.Enums;
using Exiled.API.Features;
using System;
using Player = Exiled.Events.Handlers.Player;
using Server = Exiled.Events.Handlers.Server;

namespace AdvSrvTools
{
    public class AdvSrvTools : Plugin<Config>
    {
        public override string Author { get; } = "Created by Hippolyte (hippo)";
        public override string Name { get; } = "Advanced Server Tools";
        public override Version Version { get; } = new Version(2, 0, 0);

        private static readonly Lazy<AdvSrvTools> LazyInstance = new Lazy<AdvSrvTools>(valueFactory: () => new AdvSrvTools());
        public static AdvSrvTools Instance => LazyInstance.Value;

        public override PluginPriority Priority { get; } = PluginPriority.Medium;

        private Handlers.Player player;
        private Handlers.Server server;
        private Handlers.Warhead warhead;
        private Handlers.Map map;

        private AdvSrvTools()
        {
        }
        public override void OnEnabled()
        {
            RegisterEvents();
            if (Instance.Config.RestartRoundOnEmpty)
            {
                Log.Info("Restart Round On Empty enabled!");
            }
        }

        public override void OnDisabled()
        {
            UnregisterEvents();
        }

        public void RegisterEvents()
        {
            player = new Handlers.Player();
            server = new Handlers.Server();
            warhead = new Handlers.Warhead();
            map = new Handlers.Map();

            //Server.WaitingForPlayers += server.OnWaitingForPlayers;
            Server.RoundStarted += server.OnRoundStarted;

            Player.Left += player.OnLeft;
            Player.Joined += player.OnJoined;
            //    Player.InteractingDoor += player.OnInteractingDoor;
        }

        public void UnregisterEvents()
        {
            //Server.WaitingForPlayers -= server.OnWaitingForPlayers;
            Server.RoundStarted -= server.OnRoundStarted;

            Player.Left -= player.OnLeft;
            Player.Joined -= player.OnJoined;

            server = null;
            player = null;
            warhead = null;
            map = null;
        }
    }
}