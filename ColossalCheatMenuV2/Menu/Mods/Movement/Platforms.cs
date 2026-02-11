using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
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
    public class Platforms : MonoBehaviour
    {
        public static GameObject PlatL;
        private bool PlatLonce = false;

        public static GameObject PlatR;
        private bool PlatRonce = false;
        public void Update()
        {
            if (PluginConfig.platforms)
            {
                bool leftGrab = ControllerInputPoller.instance.leftGrab;
                bool rightGrab = ControllerInputPoller.instance.rightGrab;
                if (leftGrab)
                {
                    if (!this.PlatLonce)
                    {
                        Platforms.PlatL = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        Platforms.PlatL.GetComponent<Renderer>().material.color = Color.magenta;
                        Platforms.PlatL.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                        Platforms.PlatL.transform.position = GorillaLocomotion.GTPlayer.Instance.LeftHand.controllerTransform.position;
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
                        Platforms.PlatR = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        Platforms.PlatR.GetComponent<Renderer>().material.color = Color.magenta;
                        Platforms.PlatR.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                        Platforms.PlatR.transform.position = GorillaLocomotion.GTPlayer.Instance.RightHand.controllerTransform.position;
                        this.PlatRonce = true;
                        return;
                    }
                }
                else if (this.PlatRonce)
                {
                    UnityEngine.Object.Destroy(Platforms.PlatR);
                    this.PlatRonce = false;
                    return;
                }
            }
            else
            {
                Destroy(Plugin.holder.GetComponent<Platforms>());
            }
        }
    }
}
