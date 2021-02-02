using Exiled.API.Features;
using Exiled.Events.EventArgs;
using System.Linq;

namespace AdvSrvTools.Handlers
{
    internal sealed class Player
    {
        public void OnLeft(LeftEventArgs ev)
        {
            if (AdvSrvTools.Instance.Config.VerboseMode) Log.Info("left event trigerred");
            if (AdvSrvTools.Instance.Config.RestartRoundOnEmpty)
            {
                if (AdvSrvTools.Instance.Config.VerboseMode) Log.Info("entered RR on empty if, players:" + (Exiled.API.Features.Player.List.Count()-1));
                if (Round.IsStarted && (Exiled.API.Features.Player.List.Count() <= 1))
                {
                    Log.Info("No players online and round started, restarting it");
                    Round.Restart(true, false);
                }
            }
            int duration = AdvSrvTools.Instance.Config.LeftMessageDuration;
            if (duration < 0)
            {
                string message = AdvSrvTools.Instance.Config.LeftMessage.Replace(oldValue: "{player}", newValue: ev.Player.Nickname);
                Exiled.API.Features.Map.Broadcast((ushort)duration, message);
            }
        }

        public void OnJoined(JoinedEventArgs ev)
        {
            string message = AdvSrvTools.Instance.Config.JoinedMessage.Replace(oldValue: "{player}", newValue: ev.Player.Nickname);
            int duration = AdvSrvTools.Instance.Config.JoinedMessageDuration;
            if (duration == 0) return;
            Exiled.API.Features.Map.Broadcast((ushort)duration, message);
        }

        /*
		public void OnInteractingDoor(InteractingDoorEventArgs ev)
		{
			if (ev.IsAllowed == false)
			{
				ev.Player.Broadcast(duration:1,message:"You tried");
			}
			else
			{
			}
		}
		*/
    }
}