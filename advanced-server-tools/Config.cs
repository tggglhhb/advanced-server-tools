using Exiled.API.Interfaces;
using System.ComponentModel;

namespace AdvSrvTools
{
    public sealed class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        [Description("Restart the round if the round is still going while the server is empty (I always wanted this feature lmao)")]
        public bool RestartRoundOnEmpty { get; set; } = false;


        [Description("Duration (in seconds) of the message when a player joins. Set to 0 to disable")]
        public int JoinedMessageDuration { get; set; } = 0;

        [Description("Message sent when a player joins. (if enabled) {player} = player name")]
        public string JoinedMessage { get; set; } = "{player} has joined the server!";


        [Description("Duration (in seconds) of the message when a player leaves. Set to 0 to disable")]
        public int LeftMessageDuration { get; set; } = 0;

        [Description("Message sent when a player leaves. (if enabled) {player} = player name")]
        public string LeftMessage { get; set; } = "{player} has left the server.";


        [Description("Duration (in seconds) of the message when the round starts. Set to 0 to disable")]
        public int RoundStartMessageDuration { get; set; } = 0;

        [Description("Message sent when a player leaves. (if enabled)")]
        public string RoundStartMessage { get; set; } = "Round has started, good luck!";
    }
}