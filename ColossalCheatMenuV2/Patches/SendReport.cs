using Colossal;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Colossal.Patches
{
    [HarmonyPatch(typeof(MonkeAgent), "SendReport")]
    internal class SendReport
    {
        private static void Prefix(string susReason, string susId, string susNick)
        {
            Debug.Log(string.Concat(new string[]
            {
                "Reported, Reason: ",
                susReason,
                ", ID: ",
                susId,
                ", NickName: ",
                susNick
            }));
            susReason = null;
            susId = null;
            susNick = null;
        }
    }

    // this was in new sky lmao
    [HarmonyPatch(typeof(GorillaNetworkPublicTestsJoin), "PostTick")]
    public class GorillaNetworkPublicTestsJoinPatch
    {
        public static bool Prefix()
        {
            return false;
        }
    }

    [HarmonyPatch(typeof(GorillaNetworkPublicTestJoin2), "LateUpdate")]
    public class GorillaNetworkPublicTestsJoinPatch2
    {
        public static bool Prefix()
        {
            return false;
        }
    }
}
