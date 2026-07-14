using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.XR;
using static Colossal.Plugin;

namespace Colossal.Mods
{
    public class Platforms : MonoBehaviour
    {
        //Fixed shader
        private Shader guiTextShader;
        private Shader uberShader;
        private bool CheckedShaders = false;

        public static GameObject PlatL;
        private bool PlatLonce = false;

        public static GameObject PlatR;
        private bool PlatRonce = false;
        public void Update()
        {
            if (PluginConfig.platforms)
            {
                if (!CheckedShaders)
                {
                    if (guiTextShader == null) guiTextShader = Shader.Find("GUI/Text Shader");
                    if (uberShader == null) uberShader = Shader.Find("GorillaTag/UberShader");
                    CheckedShaders = true;
                }

                bool leftGrab = ControllerInputPoller.instance.leftGrab;
                bool rightGrab = ControllerInputPoller.instance.rightGrab;
                if (leftGrab)
                {
                    if (!this.PlatLonce)
                    {
                        Platforms.PlatL = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        Platforms.PlatL.AddComponent<MeshCollider>();
                        var rend = Platforms.PlatL.GetComponent<Renderer>();
                        if (uberShader != null)
                            rend.material = new Material(uberShader);
                        rend.material.color = Color.magenta;
                        Platforms.PlatL.transform.localScale = new Vector3(-0.2f, -0.02f, -0.2f);
                        Platforms.PlatL.transform.position = GorillaLocomotion.GTPlayer.Instance.LeftHand.controllerTransform.position + new Vector3(0f, -0.04f, 0f);
                        //Platforms.PlatL.transform.rotation = GorillaLocomotion.GTPlayer.Instance.LeftHand.controllerTransform.rotation * quaternion.Euler(0f, 90f, 0f);
                        this.PlatLonce = true;
                    }
                }
                else if (this.PlatLonce)
                {
                    UnityEngine.Object.Destroy(Platforms.PlatL);
                    this.PlatLonce = false;
                }
                if (rightGrab)
                {
                    if (!this.PlatRonce)
                    {
                        Platforms.PlatR = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        var rend = Platforms.PlatR.GetComponent<Renderer>();
                        Platforms.PlatR.AddComponent<MeshCollider>();
                        if (uberShader != null)
                            rend.material = new Material(uberShader);
                        rend.material.color = Color.magenta;
                        Platforms.PlatR.transform.localScale = new Vector3(-0.2f, -0.02f, -0.2f);
                        Platforms.PlatR.transform.position = GorillaLocomotion.GTPlayer.Instance.RightHand.controllerTransform.position + new Vector3(0f, -0.04f, 0f);
                        //Platforms.PlatR.transform.rotation = GorillaLocomotion.GTPlayer.Instance.RightHand.controllerTransform.rotation * quaternion.Euler(0f, 90f, 0f);
                        this.PlatRonce = true;
                    }
                }
                else if (this.PlatRonce)
                {
                    UnityEngine.Object.Destroy(Platforms.PlatR);
                    this.PlatRonce = false;
                }
            }
            else
            {
                UnityEngine.Object.Destroy(Platforms.PlatL);
                UnityEngine.Object.Destroy(Platforms.PlatR);
                Destroy(Plugin.holder.GetComponent<Platforms>());
            }
        }
    }
}
