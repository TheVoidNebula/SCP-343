using Synapse;
using Synapse.Api.Plugin;

namespace SCP_343
{
    [PluginInformation(
        Author = "TheVoidNebula",
        Description = "Adds a new powerful SCP to the game to help the Class-D.",
        LoadPriority = 0,
        Name = "SCP-343",
        SynapseMajor = 2,
        SynapseMinor = 4,
        SynapsePatch = 2,
        Version = "1.0"
        )]
    public class Plugin : AbstractPlugin
    {
        [Synapse.Api.Plugin.Config(section = "SCP-343")]
        public static Config Config;
        public override void Load()
        {
            Server.Get.RoleManager.RegisterCustomRole<SCP343PlayerScript>();
            SynapseController.Server.Logger.Info("SCP-343 loaded!");

            new EventHandlers();
        }

    }
}