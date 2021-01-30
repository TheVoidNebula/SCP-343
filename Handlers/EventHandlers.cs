using MEC;
using SCP_343;
using Synapse;
using Synapse.Api;
using System.Linq;
using UnityEngine;
using static RoomInformation;

namespace SCP_343
{
    public class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Player.PlayerDeathEvent += OnDeath;
            Server.Get.Events.Round.SpawnPlayersEvent += OnSpawn;
            Server.Get.Events.Player.PlayerSetClassEvent += OnSetClass;
            Server.Get.Events.Round.RoundEndEvent += onRoundEnd;
            Server.Get.Events.Round.RoundRestartEvent += onRoundRestart;
            Server.Get.Events.Player.PlayerPickUpItemEvent += onPickup;
            Server.Get.Events.Player.PlayerLeaveEvent += onLeave;
            Server.Get.Events.Player.PlayerEscapesEvent += onEscape;
            Server.Get.Events.Map.DoorInteractEvent += onDoorInteract;
        }
        private CoroutineHandle _start;

        public void onRoundEnd() => Timing.KillCoroutines(_start);

        public void onRoundRestart() => Timing.KillCoroutines(_start);


        private void onLeave(Synapse.Api.Events.SynapseEventArguments.PlayerLeaveEventArgs ev)
        {
            if (Plugin.Config.noClassDDeath == true)
            {
                foreach (Player players in Server.Get.Players)
                {
                    if (RoundSummary.singleton.CountTeam(Team.CDP) <= 1 && players.RealTeam == Team.CDP && players.RoleID == 343)
                    {
                        players.Kill(DamageTypes.Poison);
                        players.SendBroadcast(5, Plugin.Config.noClassDMessage, true);
                    }
                }
            }
        }

        private void OnSetClass(Synapse.Api.Events.SynapseEventArguments.PlayerSetClassEventArgs ev)
        {
            if (ev.Player.RoleID == 343 && (ev.Player.CustomRole is SCP343PlayerScript script) && !script.Spawned)
            {
                script.Spawned = true;
                ev.Player.Position = Plugin.Config.spawnPoint.Parse().Position;
                _start = Timing.RunCoroutine(Energy.energyRegeneration(ev.Player));
            }
        }

        private void OnSpawn(Synapse.Api.Events.SynapseEventArguments.SpawnPlayersEventArgs ev)
        {
            if (Server.Get.GetPlayers(x => !x.OverWatch).Count >= Plugin.Config.minPlayers)
            {
                if (Random.Range(1f, 100f) < Plugin.Config.spawnChance)
                {
                    var playerspair = ev.SpawnPlayers.Where(x => IsClassDID(x.Value));

                    if (playerspair.Count() == 0)
                        return;

                    System.Collections.Generic.KeyValuePair<Player, int> pair;


                        pair = playerspair.ElementAt(Random.Range(0, playerspair.Count()));

                    ev.SpawnPlayers[pair.Key] = 343;
                }
            }
        }

        private bool IsClassDID(int id) => id == (int)RoleType.ClassD || id == (int)RoleType.ChaosInsurgency;

        private void OnDeath(Synapse.Api.Events.SynapseEventArguments.PlayerDeathEventArgs ev)
        {
            if (Plugin.Config.noClassDDeath == true)
            {
                foreach (Player players in Server.Get.Players)
                {
                    if (RoundSummary.singleton.CountTeam(Team.CDP) <= 1 && players.RealTeam == Team.CDP && players.RoleID == 343)
                    {
                        players.Kill(DamageTypes.Poison);
                        players.ActiveBroadcasts.Clear();
                        players.SendBroadcast(5, Plugin.Config.noClassDMessage);
                    }
                }
            }


            if (ev.Killer == null || ev.Killer == ev.Victim) return;

            if (ev.Victim.RoleID == 343)
            {
                ev.Killer.SendBroadcast(7, Plugin.Config.killMessage);
            }
        }

        private void onEscape(Synapse.Api.Events.SynapseEventArguments.PlayerEscapeEventArgs ev)
        {
            if (Plugin.Config.noClassDDeath == true)
            {
                foreach (Player players in Server.Get.Players)
                {
                    if (RoundSummary.singleton.CountTeam(Team.CDP) <= 1 && players.RealTeam == Team.CDP && players.RoleID == 343)
                    {
                        players.Kill(DamageTypes.Poison);
                        players.ActiveBroadcasts.Clear();
                        players.SendBroadcast(5, Plugin.Config.noClassDMessage);
                    }
                }
            }

            if(ev.Player.RoleID == 343)
            {
                if (Plugin.Config.canEscape == false)
                    ev.Allow = false;
                else
                    ev.SpawnRole = Plugin.Config.escapeRole;

                if (Plugin.Config.isGodmode == true)
                    ev.Player.GodMode = false;
            }
        }

        private void onPickup(Synapse.Api.Events.SynapseEventArguments.PlayerPickUpItemEventArgs ev)
        {
            if(ev.Player.RoleID == 343 && Energy.energy.ContainsKey(ev.Player.DisplayName))
            {
                if (Plugin.Config.canPickup)
                {
                    if (Energy.energy[ev.Player.DisplayName] >= Plugin.Config.convertItemEnergy && ev.Item.ItemType != ItemType.Medkit)
                    {
                        ev.Item.Destroy();
                        ev.Player.Inventory.AddItem(new Synapse.Api.Items.SynapseItem((int)ItemType.Medkit, 0, 0, 0, 0));
                        ev.Player.GiveTextHint(Plugin.Config.convertMessage.Replace("%currentEnergy%", Energy.getEnergy(ev.Player).ToString()).Replace("%maxEnergy%", Plugin.Config.maxEnergy.ToString()));
                        Energy.removeEnergy(ev.Player, Plugin.Config.convertItemEnergy);
                    }
                    else
                        ev.Allow = false;
                    
                }
                else
                    ev.Allow = false;
            }
            return;
        }



        private void onDoorInteract(Synapse.Api.Events.SynapseEventArguments.DoorInteractEventArgs ev)
        {
            if (ev.Player.RoleID == 343 && Energy.energy.ContainsKey(ev.Player.DisplayName))
            {
                if (ev.Door.DoorPermissions.RequiredPermissions != Interactables.Interobjects.DoorUtils.KeycardPermissions.None)
                {
                    if (Energy.energy[ev.Player.DisplayName] >= Plugin.Config.doorEnergy)
                    {
                        if (ev.Door.VDoor.IsConsideredOpen())
                        {
                            ev.Door.VDoor.NetworkTargetState = false;
                        }
                        else
                        {
                            ev.Door.VDoor.NetworkTargetState = true;
                        }
                        ev.Player.GiveTextHint(Plugin.Config.doorMessage.Replace("%currentEnergy%", Energy.getEnergy(ev.Player).ToString()).Replace("%maxEnergy%", Plugin.Config.maxEnergy.ToString()));
                        Energy.removeEnergy(ev.Player, Plugin.Config.doorEnergy);
                    } 
                    else
                        ev.Allow = false;
                }
            }
        }
    }
}