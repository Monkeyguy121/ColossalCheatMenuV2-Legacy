using Colossal.Patches;
using ColossalCheatMenuV2.Patches.MakeItFuckingWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR;
using static Colossal.Plugin;

namespace Colossal.Mods
{
    public class CreeperMonkey : MonoBehaviour
    {
        public void Update()
        {
            if (PluginConfig.creepermonkey)
            {
                bool rightTrigger = Controls.RightTrigger();
                if (Controls.LeftTrigger())
                {
                    float num = float.PositiveInfinity;
                    foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                    {
                        if (vrrig != GorillaTagger.Instance.offlineVRRig)
                        {
                            float sqrMagnitude = (vrrig.transform.position - GorillaLocomotion.GTPlayer.Instance.transform.position).sqrMagnitude;
                            if (sqrMagnitude < num)
                            {
                                num = sqrMagnitude;
                                foreach (VRRig vrrig2 in GorillaParent.instance.vrrigs)
                                {
                                    vrrig2.headConstraint.transform.LookAt(vrrig.headMesh.transform.position);
                                }
                            }
                        }
                    }
                }
                if (!rightTrigger)
                {
                    return;
                }
                float num2 = float.PositiveInfinity;
                using (List<VRRig>.Enumerator enumerator = GorillaParent.instance.vrrigs.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        VRRig vrrig3 = enumerator.Current;
                        if (vrrig3 != GorillaTagger.Instance.offlineVRRig)
                        {
                            float sqrMagnitude2 = (vrrig3.transform.position - GorillaLocomotion.GTPlayer.Instance.transform.position).sqrMagnitude;
                            if (sqrMagnitude2 < num2)
                            {
                                num2 = sqrMagnitude2;
                                foreach (VRRig vrrig4 in GorillaParent.instance.vrrigs)
                                {
                                    vrrig4.rightHandTransform.position = vrrig3.headMesh.transform.position;
                                }
                            }
                        }
                    }
                    return;
                }
            }
            else
            {
                Destroy(Plugin.holder.GetComponent<CreeperMonkey>());
            }
        }
    }
}
