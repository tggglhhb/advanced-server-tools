using Exiled.API.Interfaces;
using System.ComponentModel;

namespace AdvSrvTools
{
    public sealed class Config : IConfig
    {
        [Description("Enables or disables the plugin")]
        public bool IsEnabled { get; set; } = true;
    }
}