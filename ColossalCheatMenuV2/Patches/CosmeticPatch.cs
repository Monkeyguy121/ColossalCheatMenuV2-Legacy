using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace Colossal.Patches
{
    [HarmonyPatch(typeof(VRRig), "IsItemAllowed")]
    public class CosmeticPatch
    {
        public static bool enabled;
        // Credit to iidk
        public static void Postfix(VRRig __instance, ref bool __result)
        {
            if (enabled)
                __result = true;
        }
    }
}
