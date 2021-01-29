# SCP-343
 Adds a new powerful SCP on the side of the Class-D
 
## Features
* Adds a new powerful ally for the Class-D
* Play a completly new play style!
* Use the completly unique energy system to convert items and open special doors
* Die when all other Class-D are either dead or have escape
* Escape! SCP-343 will not be able to take weapons and fight back!
* SCP-343 is still pretty easy to kill for most SCPs so be careful!

## Installation
1. [Install Synapse](https://github.com/SynapseSL/Synapse/wiki#hosting-guides)
2. Place the SCP-343.dll file that you can download [here](https://github.com/TheVoidNebula/SCP-343/releases) in your plugin directory
3. Restart/Start your server.

## Config
Name  | Type | Default | Description
------------ | ------------ | ------------- | ------------ 
`isEnabled` | Boolean | true | Is this plugin enabled?
`noClassDDeath` | Boolean | true | Should SCP-343 die when no other Class-D is alive?
`noClassDMessage` | String | All other Class-D are either dead or have escaped! Your job is done. | The text SCP-343 gets when no other Class-D is alive
`spawnChance` | Int | 5 | What is the chance that SCP-343 spawns (1-100)?
`minPlayers` | Int | 12 | How many players need to be on the server to have SCP-343 spawn in?
`isGodmode` | Boolean | false | Should SCP-343 be in godmode?
`maxHealth` | Int | 1000 | What is the max health for SCP-343?
`health` | Int | 1000 | With how much health does SCP-343 spawn in?
`enableEnergy` | Boolean | true | Should the energy system for SCP-343 be enabled?
`startingEnergy` | Int | 0 | With how much energy does SCP-343 spawn in?
`maxEnergy` | Int | 100 | What is the max energy SCP-343 can have?
`energyPerSek` | Float | 1.5f | How much energy should SCP-343 get every second?
`convertItemEnergy` | Int | 10 | How much energy should SCP-343 use for one item to convert to a medkit?
`doorEnergy` | Int | 50 | How much energy should SCP-343 use for opening any keycard door?
`items` | List | 14, 0, 0, 0, 0, 1, 1, 1 | With what items should SCP-343 spawn? [14: ItemID (Medkit), 0: Durability (Ammo, useful for weapons), 0: Barrel (useful for weapons), 0: Sight (Scope, usefol for weapons), 0: other (usefol for weapons), 1, 1, 1: The size of the Item just leave it 1, 1 ,1 thx]
`spawnPoint` | SerializedMapPoint | LCZ_ClassDSpawn (1), -27f, 2f, 0f | Where should SCP-343 spawn? (Use the `mappoint` command to find a new position and copy it in here)
`canPickup` | Boolean | true | Should SCP-343 be able to pickup items?
`convertMessage` | String | %currentEnergy%/%maxEnergy% You have just converted a item to a medkit! | What should the item convert broadcast be?
`doorMessage` | String | %currentEnergy%/%maxEnergy% You have just interacted with a special door! | What should the item convert broadcast be?
`spawnMessage` | String | You have spawned as <color=#F69914>SCP-343</color> Help your team to escape! | What should the spawn broadcast be?
`killMessage` | String | You have killed <color=#F69914>SCP-343</color>! | What should the broadcast for the killer of SCP-343 be?
`badge` | String | <color=#F69914>SCP-343</color> | What should the tag of 343 be (only visible if you directly look at SCP-343)?
`canEscape` | Boolean | true | Can SCP-343 escape?
`escapeRole` | RoleType | Spectator | What role should SCP-343 become after escaping?
