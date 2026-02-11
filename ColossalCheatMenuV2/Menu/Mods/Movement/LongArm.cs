using Colossal.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;
using static Colossal.Plugin;

namespace Colossal.Mods {
    public class LongArm : MonoBehaviour {
        private float armlenght;
        public void Update() {
            if (PluginConfig.longarms) {
                bool state = SteamVR_Actions.gorillaTag_LeftTriggerClick.GetState(SteamVR_Input_Sources.LeftHand);
                bool state2 = SteamVR_Actions.gorillaTag_RightTriggerClick.GetState(SteamVR_Input_Sources.RightHand);
                bool state3 = SteamVR_Actions.gorillaTag_RightJoystickClick.GetState(SteamVR_Input_Sources.RightHand);
                if (state && state3)
                {
                    this.armlenght -= 0.01f;
                    GorillaTagger.Instance.transform.localScale = new Vector3(this.armlenght, this.armlenght, this.armlenght);
                }
                if (state2 && state3)
                {
                    this.armlenght += 0.01f;
                    GorillaTagger.Instance.transform.localScale = new Vector3(this.armlenght, this.armlenght, this.armlenght);
                }
                if (state2 && state && state3)
                {
                    GorillaTagger.Instance.transform.localScale = new Vector3(1f, 1f, 1f);
                    return;
                }
            } else {
                Destroy(Plugin.holder.GetComponent<LongArm>());
                GorillaTagger.Instance.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}
