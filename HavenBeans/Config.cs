using Dalamud.Configuration;
using Dalamud.Plugin;
using System.Numerics;

namespace HavenBeans
{
    public class Config : IPluginConfiguration
    {
        public int Version { get; set; } = 0;

        public Vector3 Floor;
        public Vector3 Target;
        public float MinJump = 10;
        public float LandOffset = 0.1f;

        [NonSerialized]
        private IDalamudPluginInterface pluginInterface;

        public void Initialize(IDalamudPluginInterface pluginInterface)
        {
            this.pluginInterface = pluginInterface;
        }

        public void Save()
        {
            pluginInterface.SavePluginConfig(this);
        }
    }
}
