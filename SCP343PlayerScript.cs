using MEC;
using Synapse.Api;
using System.Collections.Generic;

namespace SCP_343
{
    public class SCP343PlayerScript : Synapse.Api.Roles.Role
    {

        public float Energy { get; set; }
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
            Player.DisplayInfo = Plugin.Config.Badge;

            foreach (var item in Plugin.Config.Items)
                Player.Inventory.AddItem(item.Parse());

            Player.Health = Plugin.Config.Health;
            Player.MaxHealth = Plugin.Config.MaxHealth;



            if (Plugin.Config.IsGodmode)
                Player.GodMode = true;


            Player.OpenReportWindow(Plugin.Config.SpawnMessage);
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





        public static IEnumerator<float> energyRegeneration(Player p)
        {
            while (p.RoleID == 343)
            {
                    float missingEnergy = Plugin.Config.MaxEnergy - (p.CustomRole as SCP343PlayerScript).Energy;
                if (missingEnergy < Plugin.Config.EnergyPerSek)
                    (p.CustomRole as SCP343PlayerScript).Energy = Plugin.Config.MaxEnergy;
                else
                    (p.CustomRole as SCP343PlayerScript).Energy += Plugin.Config.EnergyPerSek;
                

                yield return Timing.WaitForSeconds(1f);
                yield return Timing.WaitForOneFrame;
            }
        }
    }


}
