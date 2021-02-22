# SCP-343
 Adds a new powerful SCP on the side of the Class-D
 
## Features
* Adds a new powerful ally for the Class-D
* Play a completly new play style!
* Use the completly unique energy system to convert items and open special doors
* Die when all other Class-D are either dead or have escape
* Escape! SCP-343 will not be able to take weapons and fight back!
* SCP-343 is still pretty easy to kill for most SCPs so be careful!

## Commands
Command  | Usage | Aliases | Description 
------------ | ------------ | ------------ | ------------ 
`checkEnergy` | `checkEnergy` | `energy`, `343`, `scp343` | Check how much energy you have (only usable by SCP-343)

## Installation
1. [Install Synapse](https://github.com/SynapseSL/Synapse/wiki#hosting-guides)
2. Place the SCP-343.dll file that you can download [here](https://github.com/TheVoidNebula/SCP-343/releases) in your plugin directory
3. Restart/Start your server.

## Config
Name  | Type | Default | Description
------------ | ------------ | ------------- | ------------ 
`IsEnabled` | Boolean | true | Is this plugin enabled?
`NoClassDDeath` | Boolean | true | Should SCP-343 die when no other Class-D is alive?
`NoClassDMessage` | String | All other Class-D are either dead or have escaped! Your job is done. | The text SCP-343 gets when no other Class-D is alive
`SpawnChance` | Int | 5 | What is the chance that SCP-343 spawns (1-100)?
`MinPlayers` | Int | 12 | How many players need to be on the server to have SCP-343 spawn in?
`IsGodmode` | Boolean | false | Should SCP-343 be in godmode?
`MaxHealth` | Int | 1000 | What is the max health for SCP-343?
`Health` | Int | 1000 | With how much health does SCP-343 spawn in?
`EnableEnergy` | Boolean | true | Should the energy system for SCP-343 be enabled?
`StartingEnergy` | Int | 0 | With how much energy does SCP-343 spawn in?
`MaxEnergy` | Int | 100 | What is the max energy SCP-343 can have?
`EnergyPerSek` | Float | 1.5f | How much energy should SCP-343 get every second?
`ConvertItemEnergy` | Int | 10 | How much energy should SCP-343 use for one item to convert to a medkit?
`DoorEnergy` | Int | 50 | How much energy should SCP-343 use for opening any keycard door?
`Items` | List | 14, 0, 0, 0, 0, 1, 1, 1 | With what items should SCP-343 spawn? [14: ItemID (Medkit), 0: Durability (Ammo, useful for weapons), 0: Barrel (useful for weapons), 0: Sight (Scope, usefol for weapons), 0: other (usefol for weapons), 1, 1, 1: The size of the Item just leave it 1, 1 ,1 thx]
`SpawnPoint` | SerializedMapPoint | LCZ_ClassDSpawn (1), -27f, 2f, 0f | Where should SCP-343 spawn? (Use the `mappoint` command to find a new position and copy it in here)
`CanPickup` | Boolean | true | Should SCP-343 be able to pickup items?
`ConvertMessage` | String | %currentEnergy%/%maxEnergy% You have just converted a item to a medkit! | What should the item convert broadcast be?
`DoorMessage` | String | %currentEnergy%/%maxEnergy% You have just interacted with a special door! | What should the item convert broadcast be?
`SpawnMessage` | String | You have spawned as SCP-343 Help your team to escape! | What should the spawn broadcast be?
`KillMessage` | String | You have killed SCP-343! | What should the broadcast for the killer of SCP-343 be?
`Badge` | String | SCP-343 | What should the tag of 343 be (only visible if you directly look at SCP-343)?
`CanEscape` | Boolean | true | Can SCP-343 escape?
`EscapeRole` | RoleType | Spectator | What role should SCP-343 become after escaping?
