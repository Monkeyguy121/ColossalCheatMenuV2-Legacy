using GorillaNetworking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR;
using static Colossal.Plugin;

namespace Colossal.Mods {
    public class WateryAir : MonoBehaviour {
        private GameObject waterbox;
        public void Update() {
            if (PluginConfig.wateryair) {
                if (!PluginConfig.wateryair)
                {
                    UnityEngine.Object.Destroy(GorillaTagger.Instance.GetComponent<WateryAir>());
                    if (this.waterbox != null)
                    {
                        UnityEngine.Object.Destroy(this.waterbox);
                        this.waterbox = null;
                    }
                    return;
                }
                if (this.waterbox == null)
                {
                    foreach (GameObject gameObject in Resources.FindObjectsOfTypeAll<GameObject>())
                    {
                        if (gameObject.name == "CaveWaterVolume")
                        {
                            gameObject.SetActive(true);
                            this.waterbox = UnityEngine.Object.Instantiate<GameObject>(gameObject);
                        }
                    }
                    return;
                }
                bool leftGrab = ControllerInputPoller.instance.leftGrab;
                bool rightGrab = ControllerInputPoller.instance.rightGrab;
                if (leftGrab && rightGrab)
                {
                    this.waterbox.transform.position = GorillaTagger.Instance.headCollider.transform.position + new Vector3(0f, 1f, 0f);
                    return;
                }
                this.waterbox.transform.position = new Vector3(0f, -6969f, 0f);
            }
            else {
                Destroy(Plugin.holder.GetComponent<WateryAir>());
                if (waterbox != null) {
                    GameObject.Destroy(waterbox);
                    waterbox = null;
                }
            }
        }
    }
}
