using Colossal.Patches;
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
    public class FreezeMonkey : MonoBehaviour
    {
        public void Update()
        {
            /*if (PluginConfig.freezemonkey)
            {
                if (ControllerInputPoller.instance.leftGrab)
                {
                    if(GorillaTagger.Instance.offlineVRRig.enabled)
                        GorillaTagger.Instance.offlineVRRig.enabled = false;
                    GorillaTagger.Instance.offlineVRRig.transform.position = GorillaLocomotion.GTPlayer.Instance.transform.position;
                    GorillaTagger.Instance.offlineVRRig.transform.rotation = GorillaLocomotion.GTPlayer.Instance.transform.rotation;
                }
                else
                {
                    if (!GorillaTagger.Instance.offlineVRRig.enabled)
                        GorillaTagger.Instance.offlineVRRig.enabled = true;
                }
            }
            else
            {
                Destroy(GorillaTagger.Instance.GetComponent<FreezeMonkey>());
            }*/
        }
    }
}
