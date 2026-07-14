using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using HarmonyLib;
using System.Threading;
using System.Net;
using Photon.Pun;

namespace Colossal.Patches
{
    //[HarmonyPatch(typeof(VRRig), "OnDisable")]
    internal class VRRigProvider : MonoBehaviour
    {
        private static readonly List<VRRig> CachedRigs = new List<VRRig>();
        private static int lastPlayerCount = -1;

        public static IReadOnlyList<VRRig> GetRigs()
        {
            int currentCount = PhotonNetwork.InRoom ? (PhotonNetwork.PlayerList?.Length ?? 0) : 0;
            if (currentCount != lastPlayerCount)
            {
                RefreshCache();
                lastPlayerCount = currentCount;
            }

            return CachedRigs;
        }

        private static void RefreshCache()
        {
            CachedRigs.Clear();
            try
            {
                var allRigs = UnityEngine.Object.FindObjectsOfType<VRRig>();
                foreach (var rig in allRigs)
                {
                    if (rig != null)
                        CachedRigs.Add(rig);
                }
            }
            catch
            {
                // keep cache empty on failure
            }
        }

        public static void ForceRefresh() => RefreshCache();
        public static bool Prefix(VRRig __instance)
        {
            Traverse.Create(__instance).Field("initialized").SetValue(false);
            __instance.muted = false;
            Traverse.Create(__instance).Field("voiceAudio").SetValue(null);
            Traverse.Create(__instance).Field("tempRig").SetValue(null);
            Traverse.Create(__instance).Field("timeSpawned").SetValue(0f);
            Traverse.Create(__instance).Field("tempMatIndex").SetValue(0);
            __instance.setMatIndex = 0;
            __instance.ChangeMaterialLocal(__instance.setMatIndex);
            Traverse.Create(__instance).Field("creator").SetValue(null);
            return false;
        }
    }
}