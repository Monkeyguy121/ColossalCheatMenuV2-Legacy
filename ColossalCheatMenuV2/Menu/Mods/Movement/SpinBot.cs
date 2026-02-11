using Colossal.Patches;
using ColossalCheatMenuV2.Menu.Mods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using static Colossal.Plugin;

namespace Colossal.Mods
{
    public class SpinBot : MonoBehaviour
    {
        private GameObject ghost;
        public void Update()
        {
            /*if (PluginConfig.SpinBot)
            {
                if (ghost == null)
                    ghost = GhostManager.SpawnGhost(1);

                if (ghost != null)
                {
                    if (GorillaTagger.Instance.offlineVRRig.enabled)
                        GorillaTagger.Instance.offlineVRRig.enabled = false;

                    GorillaTagger.Instance.offlineVRRig.transform.position = ghost.transform.position;

                    Quaternion targetRotation = Quaternion.LookRotation(ghost.transform.position - GorillaTagger.Instance.offlineVRRig.transform.position);

                    GorillaTagger.Instance.offlineVRRig.transform.rotation = Quaternion.RotateTowards(
                        ghost.transform.rotation,
                        targetRotation,
                        100 * Time.deltaTime
                    );
                }
            }
            else
            {
                if(!GorillaTagger.Instance.offlineVRRig.enabled && !PluginConfig.desync)
                    GorillaTagger.Instance.offlineVRRig.enabled = true;

               if (ghost != null)
                    GhostManager.DestroyGhost(ghost);

                Destroy(GorillaTagger.Instance.GetComponent<SpinBot>());
            }*/
        }
    }
}
