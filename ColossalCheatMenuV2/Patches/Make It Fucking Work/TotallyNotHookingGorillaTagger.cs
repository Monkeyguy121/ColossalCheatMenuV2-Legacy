using System;
using HarmonyLib;
using Colossal.Patches;

namespace ColossalCheatMenuV2.Patches.MakeItFuckingWork
{
    [HarmonyPatch(typeof(GorillaTagger), "Awake")]
    internal class OnGameInit
    {
        public static void Postfix() => BepInPatcher.LoadModStuff();
    }
}
