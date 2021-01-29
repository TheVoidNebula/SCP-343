using Synapse;
using Synapse.Command;
using System.Linq;

namespace SCP_343
{
    [CommandInformation(
        Name = "checkenergy",
        Aliases = new[] { "energy", "343", "scp343" },
        Description = "Check how much energy you have",
        Permission = "",
        Platforms = new[] { Platform.ClientConsole },
        Usage = ".checkenergy"
        )]
    public class Scp056Command : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            if (context.Player.RoleID != 343)
            {
                result.Message = "You are Not Scp343";
                result.State = CommandResultState.Error;
                return result;
            }

            if (Energy.energy.ContainsKey(context.Player.DisplayName))
            {
                result.Message = $"You have {Energy.getEnergy(context.Player)}/{Plugin.Config.maxEnergy} energy";
                result.State = CommandResultState.Ok;
                context.Player.GiveTextHint($"You have <color=#2AF61A>{Energy.getEnergy(context.Player)}/{Plugin.Config.maxEnergy}</color> energy");
                return result;
            }
            else
            {
                result.Message = "You are Not Scp343";
                result.State = CommandResultState.Error;
                return result;
            }
            


        }
    }
}