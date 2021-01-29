using System.Collections.Generic;

namespace SCP_343
{
    public class SCP343PlayerScript : Synapse.Api.Roles.Role
    {
        public override List<Team> GetEnemys() => new List<Team> { Team.MTF, Team.SCP, Team.RSC, Team.TUT };

        public override List<Team> GetFriends() => new List<Team> { Team.CDP, Team.CHI };

        public override int GetRoleID() => 343;

        public override string GetRoleName() => "Scp343";

        public override Team GetTeam() => Team.CDP;

        public override void Spawn()
        {
            Spawned = false;
            Player.RoleType = RoleType.ClassD;
            Player.Inventory.Clear();

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
    }
}
