using MEC;
using Synapse.Api;
using System.Collections.Generic;

namespace SCP_343
{
    public class SCP343PlayerScript : Synapse.Api.Roles.Role
    {
        public override List<Team> GetEnemys() => new List<Team> { Team.MTF, Team.SCP, Team.RSC };

        public override List<Team> GetFriends() => new List<Team> { Team.CDP, Team.CHI };

        public override int GetRoleID() => 343;

        public override string GetRoleName() => "Scp343";

        public override Team GetTeam() => Team.CDP;

        public override void Spawn()
        {
            Spawned = false;
            Player.RoleType = RoleType.ClassD;
            Player.Inventory.Clear();

            Player.RemoveDisplayInfo(PlayerInfoArea.Role);
            Player.DisplayInfo = Plugin.Config.badge;

            foreach (var item in Plugin.Config.items)
                Player.Inventory.AddItem(item.Parse());

            Player.Health = Plugin.Config.health;
            Player.MaxHealth = Plugin.Config.maxHealth;



            if (Plugin.Config.isGodmode)
                Player.GodMode = true;


            Player.OpenReportWindow(Plugin.Config.spawnMessage);
        }

        internal bool Spawned = false;

        public override void DeSpawn()
        {
            Player.DisplayInfo = string.Empty;
            Player.AddDisplayInfo(PlayerInfoArea.Role);
            Map.Get.AnnounceScpDeath("3 4 3");
            Player.Ammo5 = 0;
            Player.Ammo7 = 0;
            Player.Ammo9 = 0;
        }

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
