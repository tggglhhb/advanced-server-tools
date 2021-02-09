using Exiled.API.Enums;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using MEC;
using Player = Exiled.Events.Handlers.Player;
using Server = Exiled.Events.Handlers.Server;

namespace AdvSrvTools
{
    public class AdvSrvTools : Plugin<Config>
    {
        public override string Author { get; } = "Created by Hippolyte (hippo)";
        public override string Name { get; } = "Advanced Server Tools";
        public override Version Version { get; } = new Version(0, 0, 1);

        private static readonly Lazy<AdvSrvTools> LazyInstance = new Lazy<AdvSrvTools>(valueFactory: () => new AdvSrvTools());
        public static AdvSrvTools Instance => LazyInstance.Value;

        public override PluginPriority Priority { get; } = PluginPriority.Medium;

        internal static AdvSrvTools Singleton;

        private CoroutineHandle update;

        private Handlers.Player player;
        private Handlers.Server server;
        private Handlers.Warhead warhead;
        private Handlers.Map map;

        private AdvSrvTools()
        {
        }
        public override void OnEnabled()
        {
            Singleton = this;
            RegisterEvents();
            if (Instance.Config.RestartRoundOnEmpty)
            {
                Log.Info("Restart Round On Empty enabled!");
            }
            if (Config.AutoUpdates >= 60)
            {
                Updater.RunUpdater(false);
                update = Timing.RunCoroutine(AutoUpdates());
            }
        }

        public override void OnDisabled()
        {
            UnregisterEvents();
        }

        public IEnumerator<float> RegisterEvents()
        {
            if (Singleton == null) yield break;
            player = new Handlers.Player();
            server = new Handlers.Server();
            warhead = new Handlers.Warhead();
            map = new Handlers.Map();

            //Server.WaitingForPlayers += server.OnWaitingForPlayers;
            Server.RoundStarted += server.OnRoundStarted;

            Player.Left += player.OnLeft;
            Player.Joined += player.OnVerified;
            //    Player.InteractingDoor += player.OnInteractingDoor;
        }

        public void UnregisterEvents()
        {
            //Server.WaitingForPlayers -= server.OnWaitingForPlayers;
            Server.RoundStarted -= server.OnRoundStarted;

            Player.Left -= player.OnLeft;
            Player.Joined -= player.OnVerified;

            server = null;
            player = null;
            warhead = null;
            map = null;

            Singleton = null;
        }
        private IEnumerator<float> AutoUpdates()
        {
            while (true)
            {
                for (int i = 0; i < Config.AutoUpdates; i++)
                {
                    yield return Timing.WaitForSeconds(1);
                    if (Updater.run) 
                    {
                        if (Updater.running) Log.Warn("Updater is already running.");
                        if (!Updater.running) Updater.RunUpdater(true);
                        Updater.run = false;
                    }
                }

                if (Updater.running && Config.VerboseMode) Log.Warn("Updater is already running.");
                if (!Updater.running) Updater.RunUpdater(false);
            }
        }
    }
}