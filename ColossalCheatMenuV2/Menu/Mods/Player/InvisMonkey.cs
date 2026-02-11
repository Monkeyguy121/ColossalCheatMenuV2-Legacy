using Colossal.Patches;
using ColossalCheatMenuV2.Menu.Mods;
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
    public class InvisMonkey : MonoBehaviour
    {
        private GameObject ghost;
        public void Update()
        {
            if (PluginConfig.invismonkey && PhotonNetwork.InRoom)
            {
                if (!PluginConfig.invismonkey || !PhotonNetwork.InRoom)
                {
                    UnityEngine.Object.Destroy(GorillaTagger.Instance.GetComponent<InvisMonkey>());
                    return;
                }
                if (ControllerInputPoller.instance.leftControllerSecondaryButton)
                {
                    if(ghost == null) 
                        ghost = GhostManager.SpawnGhost(1);

                    if(GorillaTagger.Instance.offlineVRRig.enabled)
                        GorillaTagger.Instance.offlineVRRig.enabled = false;

                    ghost.GetComponent<VRRig>().mainSkin.material.color = new Color32(204, 51, 255, 60);
                    ghost.GetComponent<VRRig>().mainSkin.material.shader = Shader.Find("GUI/Text Shader");

                    GorillaTagger.Instance.offlineVRRig.transform.position = new Vector3(GorillaLocomotion.GTPlayer.Instance.headCollider.transform.position.x, -646.46466f, GorillaLocomotion.GTPlayer.Instance.headCollider.transform.position.z);
                    if (GorillaTagger.Instance.offlineVRRig.mainSkin.enabled)
                        GorillaTagger.Instance.offlineVRRig.mainSkin.enabled = false;
                    if (GorillaTagger.Instance.offlineVRRig.headMesh.active)
                        GorillaTagger.Instance.offlineVRRig.headMesh.active = false;
                    if (GorillaTagger.Instance.offlineVRRig.showName)
                        GorillaTagger.Instance.offlineVRRig.showName = false;
                    return;
                } else
                {
                    if (ghost != null)
                        GhostManager.DestroyGhost(ghost);
                    if(!GorillaTagger.Instance.offlineVRRig.enabled)
                        GorillaTagger.Instance.offlineVRRig.enabled = true;
                }
            }
            else
            {
                if(ghost != null)
                    GhostManager.DestroyGhost(ghost);
                if(!GorillaTagger.Instance.offlineVRRig.enabled)
                    GorillaTagger.Instance.offlineVRRig.enabled = true;

                if (!GorillaTagger.Instance.offlineVRRig.mainSkin.enabled)
                    GorillaTagger.Instance.offlineVRRig.mainSkin.enabled = true;
                if(!GorillaTagger.Instance.offlineVRRig.headMesh.active)
                    GorillaTagger.Instance.offlineVRRig.headMesh.active = true;
                if(!GorillaTagger.Instance.offlineVRRig.showName)
                    GorillaTagger.Instance.offlineVRRig.showName = true;

                Destroy(Plugin.holder.GetComponent<InvisMonkey>());
            }
        }
    }
}
