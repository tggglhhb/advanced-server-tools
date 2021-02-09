using System.IO;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Exiled.API.Features;

namespace AdvSrvTools
{
    internal static class Updater
    {
        private static readonly string Version = "0.0.0";
        public static bool running = false;
        public static bool run = false;
        internal static async Task RunUpdater(bool manual/*int waitTime = 0*/)
        {
            //if (waitTime != 0) await Task.Delay(waitTime);

            using (var client = new WebClient())
            {
                var nv = await client.DownloadStringTaskAsync("https://raw.githubusercontent.com/hippo9799/advanced-server-tools/master/version.txt");
                if (nv == null)
                {
                    Log.Error("Failed to get new version");
                    return;
                }
                if (nv == Version)
                {
                    if (manual || AdvSrvTools.Instance.Config.VerboseMode) Log.Warn("Plugin already to the lastest version!");
                    return;
                }

                string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                if (assemblyFolder == null)
                {
                    Log.Error("The updater could not determine the plugin path. Make sure the plugin is named \"AdvSrvTools.dll\".");
                    return;
                }

                try
                {
                    await client.DownloadFileTaskAsync("https://github.com/hippo9799/advanced-server-tools/releases/download/v" + nv + "/AdvSrvTools.dll", assemblyFolder);
                } catch
                {
                    Log.Error("Failed to download the new version.");
                    return;
                }
                Log.Info("Updated Advanced Server Tools. Please restart your server to apply the update.");
                return;
            }
        }
    }
}
