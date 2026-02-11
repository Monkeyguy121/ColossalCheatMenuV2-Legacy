using Colossal;
using Colossal.Mods;
using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Colossal.Menu.ClientHub.Notifacation {
    internal class MasterChangeNotifacation : MonoBehaviourPunCallbacks {

        private static string mastername;

        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            base.OnMasterClientSwitched(newMasterClient);

            if (PluginConfig.noti)
            {
                if (mastername != newMasterClient.NickName)
                {
                    Notifacations.SendNotification($"<color=greem>[MASTER]</color> Changed, Name: {newMasterClient.NickName}");
                    mastername = newMasterClient.NickName;
                }
            }
        }
    }
}
