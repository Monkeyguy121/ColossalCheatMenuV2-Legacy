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
    public class Chams : MonoBehaviour
    {
        private List<VRRig> cachedRigs = new List<VRRig>();
        private List<Renderer> cachedBugRenderers = new List<Renderer>();
        private int lastPlayerCount = -1;

        private Shader guiTextShader;
        private Shader uberShader;
        private bool lastChamsEnabled = false;

        private void RefreshRigs()
        {
            cachedRigs.Clear();
            try
            {
                var allRigs = UnityEngine.Object.FindObjectsOfType<VRRig>();
                foreach (var rig in allRigs)
                {
                    if (rig != null)
                        cachedRigs.Add(rig);
                }
            }
            catch { }
        }

        private void RefreshBugs()
        {
            cachedBugRenderers.Clear();
            try
            {
                var bugObjects = Resources.FindObjectsOfTypeAll<ThrowableBug>();
                foreach (var bug in bugObjects)
                {
                    if (bug == null) continue;
                    var parentObject = bug.transform.root != null ? bug.transform.root.gameObject : bug.gameObject;
                    var renderer = parentObject.GetComponentInChildren<Renderer>();
                    if (renderer != null)
                        cachedBugRenderers.Add(renderer);
                }
            }
            catch { }
        }

        private void ApplyChams()
        {
            if (guiTextShader == null) guiTextShader = Shader.Find("GUI/Text Shader");
            if (uberShader == null) uberShader = Shader.Find("GorillaTag/UberShader");

            foreach (VRRig vrrig in cachedRigs)
            {
                if (vrrig == null) continue;
                if (vrrig.isOfflineVRRig) continue;

                var smr = vrrig.mainSkin;
                if (smr == null) continue;

                var mat = smr.sharedMaterial;
                if (mat == null) continue;

                if (mat.shader != guiTextShader)
                    mat.shader = guiTextShader;

                if (GorillaGameManager.instance != null && GorillaGameManager.instance.gameObject != null)
                {
                    var tagManager = GorillaGameManager.instance.gameObject.GetComponent<GorillaTagManager>();
                    if (tagManager != null && tagManager.currentInfectedArray.Length <= 0)
                    {
                        mat.color = new Color(1f, 0f, 1f, 0.4f);
                    }
                    else
                    {
                        if (mat.name.Contains("fected"))
                            mat.color = new Color(1f, 0f, 0f, 0.4f);
                        else
                            mat.color = new Color(1f, 0f, 1f, 0.4f);
                    }
                }
            }

            foreach (var renderer in cachedBugRenderers)
            {
                if (renderer == null) continue;
                var mat = renderer.sharedMaterial;
                if (mat == null) continue;
                if (mat.shader != guiTextShader) mat.shader = guiTextShader;
                mat.color = new Color(1, 1, 0, 0.4f);
            }
        }

        private void RevertChams()
        {
            if (uberShader == null) uberShader = Shader.Find("GorillaTag/UberShader");
            foreach (VRRig vrrigs in cachedRigs)
            {
                if (vrrigs == null) continue;
                if (vrrigs.isOfflineVRRig) continue;

                var mat = vrrigs.mainSkin?.sharedMaterial;
                if (mat == null) continue;

                if (mat.shader == guiTextShader)
                    mat.shader = uberShader;

                vrrigs.mainSkin.sharedMaterial.color = vrrigs.playerColor;
            }

            foreach (var renderer in cachedBugRenderers)
            {
                if (renderer == null) continue;
                var mat = renderer.sharedMaterial;
                if (mat == null) continue;
                if (mat.shader == guiTextShader)
                    mat.shader = uberShader;
                mat.color = new Color(1, 1, 1, 1f);
            }
        }

        public void OnEnable()
        {
            lastPlayerCount = -1;
            guiTextShader = Shader.Find("GUI/Text Shader");
            uberShader = Shader.Find("GorillaTag/UberShader");

            if (PhotonNetwork.InRoom)
            {
                lastPlayerCount = PhotonNetwork.PlayerList?.Length ?? 0;
                RefreshRigs();
                RefreshBugs();
            }
            else
            {
                cachedRigs.Clear();
                cachedBugRenderers.Clear();
            }

            lastChamsEnabled = PluginConfig.chams && PhotonNetwork.InRoom;
            if (lastChamsEnabled)
                ApplyChams();
        }

        public void OnDisable()
        {
            cachedRigs.Clear();
            cachedBugRenderers.Clear();
            lastPlayerCount = -1;
            lastChamsEnabled = false;
        }

        public void Update()
        {
            // Update player list / caches only when room membership changes
            if (PhotonNetwork.InRoom)
            {
                int currentCount = PhotonNetwork.PlayerList?.Length ?? 0;
                if (currentCount != lastPlayerCount)
                {
                    RefreshRigs();
                    RefreshBugs();
                    lastPlayerCount = currentCount;

                    // If chams were on, reapply to newly found objects
                    if (PluginConfig.chams)
                        ApplyChams();
                }
            }
            else
            {
                if (lastPlayerCount != 0)
                {
                    cachedRigs.Clear();
                    cachedBugRenderers.Clear();
                    lastPlayerCount = 0;
                }
            }

            bool nowChamsEnabled = PluginConfig.chams && PhotonNetwork.InRoom;
            if (nowChamsEnabled != lastChamsEnabled)
            {
                lastChamsEnabled = nowChamsEnabled;
                if (nowChamsEnabled)
                    ApplyChams();
                else
                    RevertChams();
            }
        }
    }
}