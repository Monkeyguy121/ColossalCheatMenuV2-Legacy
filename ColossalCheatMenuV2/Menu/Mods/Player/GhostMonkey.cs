using Colossal.Patches;
using ColossalCheatMenuV2.Menu.Mods;
using GorillaNetworking;
using Photon.Pun;
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
    public class GhostMonkey : MonoBehaviour
    {
        private GameObject ghost;
        public void Update()
        {
            if (PluginConfig.ghostmonkey && PhotonNetwork.InRoom)
            {
                if (!PluginConfig.ghostmonkey || !PhotonNetwork.InRoom)
                {
                    UnityEngine.Object.Destroy(GorillaTagger.Instance.GetComponent<GhostMonkey>());
                    return;
                }
                if (ControllerInputPoller.instance.rightControllerSecondaryButton)
                {
                    if (ghost == null)
                        ghost = GhostManager.SpawnGhost(1);

                    if(GorillaTagger.Instance.offlineVRRig.enabled)
                        GorillaTagger.Instance.offlineVRRig.enabled = false;

                    ghost.GetComponent<VRRig>().mainSkin.material.color = new Color32(204, 51, 255, 60);
                    ghost.GetComponent<VRRig>().mainSkin.material.shader = Shader.Find("GUI/Text Shader");
                    return;
                } 
                else
                {
                    if(!GorillaTagger.Instance.offlineVRRig.enabled)
                        GorillaTagger.Instance.offlineVRRig.enabled = true;
                    if (ghost != null)
                      GhostManager.DestroyGhost(ghost);
                }
            }
            else
            {
                if (ghost != null)
                    GhostManager.DestroyGhost(ghost);
                if(!GorillaTagger.Instance.offlineVRRig.enabled)
                    GorillaTagger.Instance.offlineVRRig.enabled = true;

                Destroy(Plugin.holder.GetComponent<GhostMonkey>());
            }
        }
    }
}
