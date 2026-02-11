using Colossal.Mods;
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
        public static float objectScale;

        public void Update()
        {
            if (PluginConfig.boxesp && PhotonNetwork.InRoom)
            {
                foreach (VRRig rig in GorillaParent.instance.vrrigs)
                {
                    if (rig != null && !rig.isOfflineVRRig)
                    {
                        Camera mainCamera = Camera.main;
                        Matrix4x4 projectionMatrix = mainCamera.projectionMatrix;
                        Vector3 objectWorldPosition = rig.transform.position;
                        float objectDistanceFromCamera = Vector3.Distance(objectWorldPosition, mainCamera.transform.position);

                        Matrix4x4 worldToCameraMatrix = mainCamera.worldToCameraMatrix;
                        Vector3 objectViewportPosition = mainCamera.WorldToViewportPoint(objectWorldPosition);

                        Vector4 objectClipPosition = projectionMatrix * worldToCameraMatrix * new Vector4(objectWorldPosition.x, objectWorldPosition.y, objectWorldPosition.z, 1);
                        objectClipPosition /= objectClipPosition.w;

                        objectScale = (objectDistanceFromCamera / objectClipPosition.w);

                        float minScale = 2f;
                        float maxScale = 8.5f;

                        objectScale = Mathf.Clamp(objectScale, minScale, maxScale);

                        GameObject go = new GameObject("box");
                        go.transform.position = rig.transform.position;

                        GameObject face = GameObject.CreatePrimitive(PrimitiveType.Plane);
                        Destroy(face.GetComponent<Collider>());
                        face.transform.SetParent(go.transform);
                        face.transform.localPosition = new Vector3(0f, 0f, 0f);
                        face.transform.localRotation = Quaternion.Euler(90, 0, 0);
                        face.transform.localScale = new Vector3(BoxEsp.objectScale / 40, BoxEsp.objectScale / 40, BoxEsp.objectScale / 40);

                        face.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                        Color Espcolor;

                        if (GorillaGameManager.instance.gameObject.GetComponent<GorillaTagManager>().currentInfectedArray.Length <= 0)
                        {
                            Espcolor = new Color(1f, 0f, 1f, 0.4f);
                        }
                        else
                        {
                            if (rig.mainSkin.material.name.Contains("fected"))
                            {
                                Espcolor = new Color(1f, 0f, 0f, 0.4f);
                            }
                            else
                            {
                                Espcolor = new Color(1f, 0f, 1f, 0.4f);
                            }
                        }

                        face.GetComponent<Renderer>().material.color = Espcolor;
                        Quaternion rotation = Quaternion.LookRotation(Camera.main.transform.forward, Camera.main.transform.up);
                        go.transform.rotation = rotation;

                        GameObject.Destroy(go, Time.deltaTime);
                    }
                }
            }
        }
    }
}
