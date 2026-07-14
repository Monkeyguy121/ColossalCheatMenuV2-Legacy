using Colossal.Patches;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using Colossal.Mods;
using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.XR;
using Viveport;
using static Colossal.Plugin;

namespace Colossal.Mods
{
    public class HollowBoxEsp : MonoBehaviour
    {
        private Dictionary<VRRig, GameObject> boxPool = new Dictionary<VRRig, GameObject>();
        private Shader guiTextShader;
        private int lastPlayerCount = -1;

        private void RefreshPool()
        {
            if (guiTextShader == null) guiTextShader = Shader.Find("GUI/Text Shader");
            foreach (VRRig rig in VRRigProvider.GetRigs())
            {
                if (rig == null || rig.isOfflineVRRig) continue;
                if (!boxPool.ContainsKey(rig))
                    boxPool[rig] = CreateHollowForRig(rig);
            }

            var toRemove = new List<VRRig>();
            foreach (var kv in boxPool)
            {
                if (kv.Key == null)
                {
                    if (kv.Value != null) UnityEngine.Object.Destroy(kv.Value);
                    toRemove.Add(kv.Key);
                }
            }
            foreach (var r in toRemove) boxPool.Remove(r);
        }

        private GameObject CreateHollowForRig(VRRig rig)
        {
            GameObject go = new GameObject($"hollowbox_{rig.GetInstanceID()}");
            go.transform.SetParent(transform, false);

            GameObject top = GameObject.CreatePrimitive(PrimitiveType.Cube);
            GameObject right = GameObject.CreatePrimitive(PrimitiveType.Cube);
            GameObject left = GameObject.CreatePrimitive(PrimitiveType.Cube);
            GameObject bottom = GameObject.CreatePrimitive(PrimitiveType.Cube);

            UnityEngine.Object.Destroy(top.GetComponent<BoxCollider>());
            UnityEngine.Object.Destroy(bottom.GetComponent<BoxCollider>());
            UnityEngine.Object.Destroy(left.GetComponent<BoxCollider>());
            UnityEngine.Object.Destroy(right.GetComponent<BoxCollider>());

            top.name = "top"; right.name = "right"; left.name = "left"; bottom.name = "bottom";

            top.transform.SetParent(go.transform, false);
            bottom.transform.SetParent(go.transform, false);
            left.transform.SetParent(go.transform, false);
            right.transform.SetParent(go.transform, false);

            top.transform.localPosition = new Vector3(0f, 1f / 2f - 0.02f / 2f, 0f);
            top.transform.localScale = new Vector3(1f, 0.02f, 0.02f);
            bottom.transform.localPosition = new Vector3(0f, (0f - 1f) / 2f + 0.02f / 2f, 0f);
            bottom.transform.localScale = new Vector3(1f, 0.02f, 0.02f);
            left.transform.localPosition = new Vector3((0f - 1f) / 2f + 0.02f / 2f, 0f, 0f);
            left.transform.localScale = new Vector3(0.02f, 1f, 0.02f);
            right.transform.localPosition = new Vector3(1f / 2f - 0.02f / 2f, 0f, 0f);
            right.transform.localScale = new Vector3(0.02f, 1f, 0.02f);
            var rends = new Renderer[] { top.GetComponent<Renderer>(), bottom.GetComponent<Renderer>(), left.GetComponent<Renderer>(), right.GetComponent<Renderer>() };
            foreach (var r in rends)
            {
                if (r != null && guiTextShader != null)
                    r.sharedMaterial.shader = guiTextShader;
            }

            go.SetActive(false);
            return go;
        }

        private void OnEnable()
        {
            lastPlayerCount = -1;
            guiTextShader = Shader.Find("GUI/Text Shader");
            RefreshPool();
        }

        private void OnDisable()
        {
            foreach (var kv in boxPool)
            {
                if (kv.Value != null) UnityEngine.Object.Destroy(kv.Value);
            }
            boxPool.Clear();
            lastPlayerCount = -1;
        }

        public void Update()
        {
            if (!PhotonNetwork.InRoom)
            {
                foreach (var kv in boxPool) if (kv.Value != null) kv.Value.SetActive(false);
                lastPlayerCount = 0;
                return;
            }

            int currentCount = PhotonNetwork.PlayerList?.Length ?? 0;
            if (currentCount != lastPlayerCount)
            {
                RefreshPool();
                lastPlayerCount = currentCount;
            }

            if (!PluginConfig.hollowboxesp)
            {
                foreach (var kv in boxPool) if (kv.Value != null) kv.Value.SetActive(false);
                return;
            }
            
            Camera mainCamera = Camera.main;
            if (mainCamera == null) return;

            foreach (var kv in boxPool)
            {
                var rig = kv.Key;
                var go = kv.Value;
                if (rig == null || go == null) continue;

                go.SetActive(true);

                Color espColor = Color.magenta;
                var tagManager = GorillaGameManager.instance != null ? GorillaGameManager.instance.gameObject.GetComponent<GorillaTagManager>() : null;
                if (tagManager != null && tagManager.currentInfectedArray.Length > 0 && rig.mainSkin != null && rig.mainSkin.material != null && rig.mainSkin.material.name.Contains("fected"))
                    espColor = Color.red;

                var top = go.transform.Find("top")?.GetComponent<Renderer>();
                var bottom = go.transform.Find("bottom")?.GetComponent<Renderer>();
                var left = go.transform.Find("left")?.GetComponent<Renderer>();
                var right = go.transform.Find("right")?.GetComponent<Renderer>();
                foreach (var r in new Renderer[] { top, bottom, left, right })
                {
                    if (r == null) continue;
                    if (guiTextShader != null && r.sharedMaterial.shader != guiTextShader) r.sharedMaterial.shader = guiTextShader;
                    r.sharedMaterial.color = espColor;
                }

                go.transform.position = rig.transform.position;
                go.transform.rotation = Quaternion.LookRotation(go.transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
            }
        }
    }
}