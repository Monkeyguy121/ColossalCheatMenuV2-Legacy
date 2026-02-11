using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR;
using static Colossal.Plugin;

namespace Colossal.Mods {
    public class PcCheckBypass : MonoBehaviour {
        private GameObject igloo;
        public void Start()
        {
            igloo = GameObject.Find("goodigloo");
        }
        public void Update() {
            if (PluginConfig.pccheckbypass) {
                if(GameObject.Find("Environment Objects/LocalObjects_Prefab").transform.Find("Mountain").gameObject.activeSelf) {
                    if(igloo.activeSelf) {
                        igloo.SetActive(false);
                    }
                }
            } else {
                Destroy(Plugin.holder.GetComponent<PcCheckBypass>());
                if (!igloo.activeSelf) {
                    igloo.SetActive(true);
                }
            }
        }
    }
}
