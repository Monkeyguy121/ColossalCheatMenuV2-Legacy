using Colossal.Mods;
using Colossal;
using HarmonyLib;
using Photon.Pun;
using System.Net;
using Photon.Realtime;
using UnityEngine;
using Colossal.Menu;
using System.Collections.Generic;

namespace Colossal.Menu.ClientHub.Notifacation {
    internal class JoinNotifacation : MonoBehaviourPunCallbacks
    {
        private static List<Player> notifiedPlayers = new List<Player>();

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);

            if (!notifiedPlayers.Contains(newPlayer) && PluginConfig.noti)
            {
                notifiedPlayers.Add(newPlayer);
                Notifacations.SendNotification($"<color=cyan>[JOIN]</color> Name: {newPlayer.NickName}");
            }
        }
    }
}