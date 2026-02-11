using System;
using System.Reflection;
using HarmonyLib;

namespace Colossal
{
    public class HarmonyLoader
    {
        public static bool IsPatched { get; private set; }
        private static Harmony instance;
        public const string InstanceId = "org.Colossal";

        internal static void ApplyHarmonyPatches()
        {
            if (!HarmonyLoader.IsPatched)
            {
                if (HarmonyLoader.instance == null)
                {
                    HarmonyLoader.instance = new Harmony("org.Colossal");
                }
                HarmonyLoader.instance.PatchAll(Assembly.GetExecutingAssembly());
                HarmonyLoader.IsPatched = true;
            }
        }
        internal static void ApplyHarmonyPatchesOnEvent() {
            if (!HarmonyLoader.IsPatched) {
                if (HarmonyLoader.instance == null) {
                    HarmonyLoader.instance = new Harmony("org.ColossalOnevent");
                }
                HarmonyLoader.instance.PatchAll(Assembly.GetExecutingAssembly());
                HarmonyLoader.IsPatched = true;
            }
        }

        internal static void RemoveHarmonyPatches()
        {
            if (HarmonyLoader.instance != null && HarmonyLoader.IsPatched)
            {
                HarmonyLoader.instance.UnpatchSelf();
                HarmonyLoader.IsPatched = false;
            }
        }
    }
}
