using Colossal;
using Colossal.Mods;
using HarmonyLib;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Colossal.Menu.ClientHub.Notifacation {
    [HarmonyPatch(typeof(GorillaNot), "SendReport")]
    internal class ReportNotifacation {
        private static List<string> notifiedPlayers = new List<string>();

        [HarmonyPrefix]
        private static void Postfix(string susReason, string susId, string susNick) {
            if (!notifiedPlayers.Contains(susId) && PluginConfig.noti && !susReason.Contains("PlayHandTap")) {
                notifiedPlayers.Add(susId);
                Notifacations.SendNotification($"<color=yellow>[ANTICHEAT]</color> Name: {susNick}");
                if(Plugin.reporttimer <= 20) {
                    notifiedPlayers.Remove(susId);
                }
            }
        }
    }
}
