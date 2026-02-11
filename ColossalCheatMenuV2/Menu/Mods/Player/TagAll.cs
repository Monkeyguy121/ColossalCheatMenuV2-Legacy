using Colossal.Patches;
using GorillaGameModes;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.XR;
using static Colossal.Plugin;

namespace Colossal.Mods
{
    public class TagAll : MonoBehaviour
    {
        private LineRenderer radiusLine;
        private Material lineMaterial;

        private void Start()
        {
            lineMaterial = new Material(Shader.Find("Sprites/Default"));
            lineMaterial.color = new Color(0.6f, 0f, 0.8f, 0.5f);
        }

        public void Update()
        {
            if (!PluginConfig.tagall)
            {
                if (!GorillaTagger.Instance.offlineVRRig.enabled)
                    GorillaTagger.Instance.offlineVRRig.enabled = true;

                if (radiusLine != null)
                {
                    Destroy(radiusLine.gameObject);
                    radiusLine = null;
                }
                Destroy(this);
                return;
            }

            GorillaTagger.Instance.tagCooldown = 0f;
            GorillaLocomotion.GTPlayer.Instance.teleportThresholdNoVel = int.MaxValue;

            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (rig == null || rig == VRRig.LocalRig)
                    continue;
                if (GorillaTagger.Instance.offlineVRRig.mainSkin.material.name.Contains("fected") == rig.mainSkin.material.name.Contains("fected"))
                    continue;
                if (GorillaTagger.Instance.offlineVRRig.enabled)
                    GorillaTagger.Instance.offlineVRRig.enabled = false;

                GorillaTagger.Instance.offlineVRRig.transform.position = rig.transform.position;

                if (GorillaTagger.Instance.offlineVRRig.mainSkin.material.name.Contains("fected"))
                    GameMode.ReportTag(rig.OwningNetPlayer);

                DrawLine(rig);
                break; 
            }
        }

        private void DrawLine(VRRig target)
        {
            if (radiusLine == null)
            {
                GameObject obj = new GameObject("TagAllLine");
                radiusLine = obj.AddComponent<LineRenderer>();
                radiusLine.positionCount = 2;
                radiusLine.startWidth = 0.05f;
                radiusLine.endWidth = 0.05f;
                radiusLine.material = lineMaterial;
                radiusLine.useWorldSpace = true;
            }

            radiusLine.SetPosition(0, target.transform.position);
            radiusLine.SetPosition(1, GorillaTagger.Instance.rightHandTransform.position);
        }
    }
}
