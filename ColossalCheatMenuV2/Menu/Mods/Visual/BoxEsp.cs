using Colossal.Mods;
using Colossal.Patches;
using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.XR;
using Viveport;
using static Colossal.Plugin;

namespace Colossal.Mods
{
    public class BoxEsp : MonoBehaviour
    {
        private Dictionary<VRRig, GameObject> boxPool = new Dictionary<VRRig, GameObject>();
        private Shader guiTextShader;
        private int lastPlayerCount = -1;

        private void RefreshPool()
        {
            // ensure shader cached
            if (guiTextShader == null) guiTextShader = Shader.Find("GUI/Text Shader");

            // Add new rigs using VRRigProvider (avoid relying on GorillaParent.vrrigs)
            foreach (VRRig rig in VRRigProvider.GetRigs())
            {
                if (rig == null || rig.isOfflineVRRig) continue;
                if (!boxPool.ContainsKey(rig))
                    boxPool[rig] = CreateBoxForRig(rig);
            }

            // Remove rigs that are gone
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

        private GameObject CreateBoxForRig(VRRig rig)
        {
            GameObject go = new GameObject($"boxesp_{rig.GetInstanceID()}");
            go.transform.SetParent(transform, false);

            GameObject face = GameObject.CreatePrimitive(PrimitiveType.Plane);
            UnityEngine.Object.Destroy(face.GetComponent<Collider>());
            face.transform.SetParent(go.transform, false);
            face.transform.localPosition = Vector3.zero;
            face.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
            face.name = "box_face";

            var rend = face.GetComponent<Renderer>();
            if (guiTextShader != null)
                rend.sharedMaterial.shader = guiTextShader;

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
                if (boxPool.Count > 0)
                {
                    foreach (var kv in boxPool) if (kv.Value != null) kv.Value.SetActive(false);
                }
                lastPlayerCount = 0;
                return;
            }

            int currentCount = PhotonNetwork.PlayerList?.Length ?? 0;
            if (currentCount != lastPlayerCount)
            {
                RefreshPool();
                lastPlayerCount = currentCount;
            }

            if (!PluginConfig.boxesp)
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
                Vector3 objectWorldPosition = rig.transform.position;
                float objectDistanceFromCamera = Vector3.Distance(objectWorldPosition, mainCamera.transform.position);

                Matrix4x4 projectionMatrix = mainCamera.projectionMatrix;
                Matrix4x4 worldToCameraMatrix = mainCamera.worldToCameraMatrix;
                Vector4 objectClipPosition = projectionMatrix * worldToCameraMatrix * new Vector4(objectWorldPosition.x, objectWorldPosition.y, objectWorldPosition.z, 1);
                objectClipPosition /= objectClipPosition.w;

                float objScale = Mathf.Clamp(objectDistanceFromCamera / objectClipPosition.w, 2f, 8.5f);

                var face = go.transform.Find("box_face");
                if (face != null)
                {
                    face.localScale = new Vector3(objScale / 40f, objScale / 40f, objScale / 40f);
                    face.position = rig.transform.position;
                    Color espColor = new Color(1f, 0f, 1f, 0.4f);

                    var tagManager = GorillaGameManager.instance != null ? GorillaGameManager.instance.gameObject.GetComponent<GorillaTagManager>() : null;
                    if (tagManager != null && tagManager.currentInfectedArray.Length > 0 && rig.mainSkin != null && rig.mainSkin.material != null && rig.mainSkin.material.name.Contains("fected"))
                        espColor = new Color(1f, 0f, 0f, 0.4f);

                    var rend = face.GetComponent<Renderer>();
                    if (rend != null)
                    {
                        if (guiTextShader != null && rend.sharedMaterial.shader != guiTextShader) rend.sharedMaterial.shader = guiTextShader;
                        rend.sharedMaterial.color = espColor;
                    }
                }

                Quaternion rotation = Quaternion.LookRotation(mainCamera.transform.forward, mainCamera.transform.up);
                go.transform.rotation = rotation;
            }
        }
    }
}