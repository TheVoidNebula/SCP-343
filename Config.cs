using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace SCP_343
{
    public class Config : AbstractConfigSection
    {

        [Description("Is this plugin enabled?")]
        public bool isEnabled = true;

        [Description("Should SCP-343 die when all Class-D are dead?")]
        public bool noClassDDeath = true;

        [Description("What should the broadcast be when SCP-343 dies when all other Class-D are dead?")]
        public string noClassDMessage = "<b>All other Class-D are either dead or have escaped!</b>\n<i>Your job is done.</i>";

        [Description("What is the chance that SCP-343 spawns (1-100)?")]
        public int spawnChance = 5;

        [Description("How many players need to be on the server to have SCP-343 spawn in?")]
        public int minPlayers = 12;

        [Description("Should SCP-343 be in godmode?")]
        public bool isGodmode = false;

        [Description("What is the max health for SCP-343?")]
        public int maxHealth = 1000;

        [Description("With how much health does SCP-343 spawn in?")]
        public int health = 1000;

        [Description("Should the energy system for SCP-343 be enabled?")]
        public bool enableEnergy = true;

        [Description("With how much energy does SCP-343 spawn in?")]
        public int startingEnergy = 0;

        [Description("What is the max energy SCP-343 can have?")]
        public int maxEnergy = 100;

        [Description("How much energy should SCP-343 get every second?")]
        public float energyPerSek = 1.5f;

        [Description("How much energy should SCP-343 use for one item to convert to a medkit?")]
        public int convertItemEnergy = 10;

        [Description("How much energy should SCP-343 use for opening any keycard door?")]
        public int doorEnergy = 50;

        [Description("With what items should SCP-343 spawn?")]
        public List<SerializedItem> items = new List<SerializedItem>()
        {
            new SerializedItem((int) ItemType.Medkit, 0, 0, 0, 0, Vector3.one),
            new SerializedItem((int) ItemType.Medkit, 0, 0, 0, 0, Vector3.one),
            new SerializedItem((int) ItemType.Medkit, 0, 0, 0, 0, Vector3.one)
        };
        [Description("Where should SCP-343 spawn?")]
        public SerializedMapPoint spawnPoint = new SerializedMapPoint("LCZ_ClassDSpawn (1)", -27f, 2f, 0f);

        [Description("Should SCP-343 be able to pickup items?")]
        public bool canPickup = true;

        [Description("What should the item convert broadcast be?")]
        public string convertMessage = "<b>%currentEnergy%/%maxEnergy%</b>\nYou have just converted a item to a medkit!";

        [Description("What should the item convert broadcast be?")]
        public string doorMessage = "<b>%currentEnergy%/%maxEnergy%</b>\nYou have just interacted with a special door!";

        [Description("What should the spawn broadcast be?")]
        public string spawnMessage = "<b>You have spawned as <color=#F69914>SCP-343</color></b>\n<i>Help your team to escape!</b>";

        [Description("What should the broadcast for the killer of SCP-343 be?")]
        public string killMessage = "<b>You have killed <color=#F69914>SCP-343</color>!</b>";

        [Description("What should the tag of 343 be?")]
        public string badge = "<color=#F69914>SCP-343</color>";

        [Description("Can SCP-343 escape?")]
        public bool canEscape = true;

        [Description("What role should SCP-343 become after escaping?")]
        public RoleType escapeRole = RoleType.Spectator;
    }
}
