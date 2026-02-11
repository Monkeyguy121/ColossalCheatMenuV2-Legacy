using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR;
using static Colossal.Plugin;

namespace Colossal.Mods {
    public class WhyIsEveryoneLookingAtMe : MonoBehaviour {
        public void Update() {
            if (PluginConfig.whyiseveryonelookingatme) {
                foreach (VRRig vrrig in GorillaParent.instance.vrrigs) {
                    if (!vrrig.isOfflineVRRig) {
                        vrrig.headConstraint.transform.LookAt(GorillaLocomotion.GTPlayer.Instance.transform.position);
                    }
                }
            } else {
                Destroy(Plugin.holder.GetComponent<WhyIsEveryoneLookingAtMe>());
            }
        }
    }
}
