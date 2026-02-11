using GorillaTagScripts;
using HarmonyLib;

namespace Colossal.Patches
{
    [HarmonyPatch(typeof(SubscriptionManager), "IsLocalSubscribed")]
    public class SubsciberPatch
    {
        public static bool enableinmenucs = false;
        private static bool Prefix(ref bool __result)
        {
            if (enableinmenucs)
            {
                __result = true;    
                return false;
            }
            return true;
        }
    }
}