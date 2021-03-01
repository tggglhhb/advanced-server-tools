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
        public override string Name { get; } = "Advanced Server Tools respawning only";
        public override Version Version { get; } = new Version(0, 0, 1);

        private static readonly Lazy<AdvSrvTools> LazyInstance = new Lazy<AdvSrvTools>(valueFactory: () => new AdvSrvTools());
        public static AdvSrvTools Instance => LazyInstance.Value;

        public override PluginPriority Priority { get; } = PluginPriority.Medium;

        private Handlers.Player player;
        private Handlers.Server server;

        private AdvSrvTools()
        {
        }
        public override void OnEnabled()
        {
            player = new Handlers.Player();
            server = new Handlers.Server();
        }

        public override void OnDisabled()
        {
            UnregisterEvents();
        }


        public void UnregisterEvents()
        {

            server = null;
            player = null;

        }
    }
}