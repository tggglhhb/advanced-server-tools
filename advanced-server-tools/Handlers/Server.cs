namespace AdvSrvTools.Handlers
{
    internal sealed class Server
    {
        /*
		public void OnWaitingForPlayers()
		{
		//	Log.Info("Waiting for players...");
		}
		*/

        public void OnRoundStarted()
        {
            string message = AdvSrvTools.Instance.Config.RoundStartMessage;
            int duration = AdvSrvTools.Instance.Config.RoundStartMessageDuration;
            if (duration == 0) return;
            Exiled.API.Features.Map.Broadcast((ushort)duration, message);
        }
    }
}