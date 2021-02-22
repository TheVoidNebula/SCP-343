using MEC;
using SCP_343;
using Synapse;
using Synapse.Api;
using System.Linq;
using UnityEngine;

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
            if (Plugin.Config.NoClassDDeath && RoundSummary.singleton.CountTeam(Team.CDP) == 1 && ev.Player.RealTeam == Team.CDP)
            {
                foreach (Player players in Server.Get.Players)
                {
                    if (players.RoleID == 343)
                    {
                        players.Kill(DamageTypes.Poison);
                        players.SendBroadcast(5, Plugin.Config.NoClassDMessage, true);
                    }
                }
            }
        }

        private void OnSetClass(Synapse.Api.Events.SynapseEventArguments.PlayerSetClassEventArgs ev)
        {
            if (ev.Player.RoleID == 343 && (ev.Player.CustomRole is SCP343PlayerScript script) && !script.Spawned)
            {
                script.Spawned = true;
                ev.Player.Position = Plugin.Config.SpawnPoint.Parse().Position;
                _start = Timing.RunCoroutine(SCP343PlayerScript.energyRegeneration(ev.Player));
            }
        }

        private void OnSpawn(Synapse.Api.Events.SynapseEventArguments.SpawnPlayersEventArgs ev)
        {
            if (Server.Get.GetPlayers(x => !x.OverWatch).Count() >= ev.SpawnPlayers.Keys.Count())
            {
                if (Random.Range(1f, 100f) < Plugin.Config.SpawnChance)
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
            if (Plugin.Config.NoClassDDeath == true)
            {
                foreach (Player players in Server.Get.Players)
                {
                    if (RoundSummary.singleton.CountTeam(Team.CDP) <= 1 && players.RealTeam == Team.CDP && players.RoleID == 343)
                    {
                        players.Kill(DamageTypes.Poison);
                        players.ActiveBroadcasts.Clear();
                        players.SendBroadcast(5, Plugin.Config.NoClassDMessage);
                    }
                }
            }


            if (ev.Killer == null || ev.Killer == ev.Victim) return;

            if (ev.Victim.RoleID == 343)
            {
                ev.Killer.SendBroadcast(7, Plugin.Config.KillMessage);
            }
        }

        private void onEscape(Synapse.Api.Events.SynapseEventArguments.PlayerEscapeEventArgs ev)
        {
            if (Plugin.Config.NoClassDDeath == true)
            {
                foreach (Player players in Server.Get.Players)
                {
                    if (RoundSummary.singleton.CountTeam(Team.CDP) <= 1 && players.RealTeam == Team.CDP && players.RoleID == 343)
                    {
                        players.Kill(DamageTypes.Poison);
                        players.ActiveBroadcasts.Clear();
                        players.SendBroadcast(5, Plugin.Config.NoClassDMessage);
                    }
                }
            }

            if(ev.Player.RoleID == 343)
            {
                if (Plugin.Config.CanEscape)
                    ev.Allow = false;
                else
                    ev.SpawnRole = Plugin.Config.EscapeRole;

                if (Plugin.Config.IsGodmode)
                    ev.Player.GodMode = false;
            }
        }

        private void onPickup(Synapse.Api.Events.SynapseEventArguments.PlayerPickUpItemEventArgs ev)
        {
            if(ev.Player.RoleID == 343 && Plugin.Config.CanPickup)
            {
                if (Plugin.Config.CanPickup)
                {
                    if ((ev.Player.CustomRole as SCP343PlayerScript).Energy >= Plugin.Config.ConvertItemEnergy && ev.Item.ItemType != ItemType.Medkit)
                    {
                        ev.Item.Destroy();
                        ev.Player.Inventory.AddItem(new Synapse.Api.Items.SynapseItem((int)ItemType.Medkit, 0, 0, 0, 0));
                        ev.Player.GiveTextHint(Plugin.Config.ConvertMessage.Replace("%currentEnergy%", (ev.Player.CustomRole as SCP343PlayerScript).Energy.ToString()).Replace("%maxEnergy%", Plugin.Config.MaxEnergy.ToString()));
                        (ev.Player.CustomRole as SCP343PlayerScript).Energy -= Plugin.Config.ConvertItemEnergy;
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
            if (ev.Player.RoleID == 343)
            {
                if (ev.Door.DoorPermissions.RequiredPermissions != Interactables.Interobjects.DoorUtils.KeycardPermissions.None)
                {
                    if ((ev.Player.CustomRole as SCP343PlayerScript).Energy >= Plugin.Config.DoorEnergy)
                    {
                        if (ev.Door.VDoor.IsConsideredOpen())
                        {
                            ev.Door.VDoor.NetworkTargetState = false;
                        }
                        else
                        {
                            ev.Door.VDoor.NetworkTargetState = true;
                        }
                        ev.Player.GiveTextHint(Plugin.Config.DoorMessage.Replace("%currentEnergy%", (ev.Player.CustomRole as SCP343PlayerScript).Energy.ToString()).Replace("%maxEnergy%", Plugin.Config.MaxEnergy.ToString()));
                        (ev.Player.CustomRole as SCP343PlayerScript).Energy -= Plugin.Config.DoorEnergy;
                    } 
                    else
                        ev.Allow = false;
                }
            }
        }
    }
}