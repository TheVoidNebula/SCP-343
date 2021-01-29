using MEC;
using Synapse.Api;
using System.Collections.Generic;

namespace SCP_343
{
    public class Energy
    {
        public static Dictionary<string, float> energy = new Dictionary<string, float>();
        

        public static void setEnergy(Player p, int value)
        {
            if (energy.ContainsKey(p.DisplayName))
                energy[p.DisplayName] = value;
            else
                energy.Add(p.DisplayName, value);
        }

        public static void addEnergy(Player p, int value)
        {
            if (energy.ContainsKey(p.DisplayName))
                energy[p.DisplayName] += value;
            else
                energy.Add(p.DisplayName, value);
        }

        public static void removeEnergy(Player p, int value)
        {
            if (energy.ContainsKey(p.DisplayName))
                energy[p.DisplayName] -= value;
        }

        public static float getEnergy(Player p)
        {
                return energy[p.DisplayName];
        }


        public static IEnumerator<float> energyRegeneration(Player p)
        {
            while (p.RoleID == 343)
            {
                if (energy.ContainsKey(p.DisplayName))
                {
                    float missingEnergy = Plugin.Config.maxEnergy - energy[p.DisplayName];
                    if (missingEnergy < Plugin.Config.energyPerSek)
                        energy[p.DisplayName] = Plugin.Config.maxEnergy;
                    else
                        energy[p.DisplayName] += Plugin.Config.energyPerSek;
                }
                else
                    energy.Add(p.DisplayName, Plugin.Config.startingEnergy);

                    yield return Timing.WaitForSeconds(1f);
                yield return Timing.WaitForOneFrame;
            }
        }
    }
}
