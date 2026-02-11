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
        public void Update()
        {
            if (PluginConfig.hollowboxesp && PhotonNetwork.InRoom)
            {
                foreach (VRRig rig in GorillaParent.instance.vrrigs)
                {
                    if (rig != null && !rig.isOfflineVRRig)
                    {
                        GameObject go = new GameObject("box");
                        go.transform.position = rig.transform.position;
                        GameObject top = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        GameObject right = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        GameObject left = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        GameObject bottom = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        Destroy(top.GetComponent<BoxCollider>());
                        Destroy(bottom.GetComponent<BoxCollider>());
                        Destroy(left.GetComponent<BoxCollider>());
                        Destroy(right.GetComponent<BoxCollider>());
                        top.transform.SetParent(go.transform);
                        top.transform.localPosition = new Vector3(0f, 1f / 2f - 0.02f / 2f, 0f);
                        top.transform.localScale = new Vector3(1f, 0.02f, 0.02f);
                        bottom.transform.SetParent(go.transform);
                        bottom.transform.localPosition = new Vector3(0f, (0f - 1f) / 2f + 0.02f / 2f, 0f);
                        bottom.transform.localScale = new Vector3(1f, 0.02f, 0.02f);
                        left.transform.SetParent(go.transform);
                        left.transform.localPosition = new Vector3((0f - 1f) / 2f + 0.02f / 2f, 0f, 0f);
                        left.transform.localScale = new Vector3(0.02f, 1f, 0.02f);
                        right.transform.SetParent(go.transform);
                        right.transform.localPosition = new Vector3(1f / 2f - 0.02f / 2f, 0f, 0f);
                        right.transform.localScale = new Vector3(0.02f, 1f, 0.02f);

                        top.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                        bottom.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                        left.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                        right.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");

                        Color Espcolor;

                        if (GorillaGameManager.instance.gameObject.GetComponent<GorillaTagManager>().currentInfectedArray.Length <= 0)
                        {
                            Espcolor = Color.magenta;
                        }
                        else
                        {
                            if (rig.mainSkin.material.name.Contains("fected"))
                            {
                                Espcolor = Color.red;
                            }
                            else
                            {
                                Espcolor = Color.magenta;
                            }
                        }

                        top.GetComponent<Renderer>().material.color = Espcolor;
                        bottom.GetComponent<Renderer>().material.color = Espcolor;
                        left.GetComponent<Renderer>().material.color = Espcolor;
                        right.GetComponent<Renderer>().material.color = Espcolor;

                        go.transform.LookAt(go.transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
                        GameObject.Destroy(go, Time.deltaTime);
                    }
                }
            }
        }
    }
}