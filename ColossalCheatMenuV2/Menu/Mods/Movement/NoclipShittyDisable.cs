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
    public class NoclipShittyDisable : MonoBehaviour
    {
        private GameObject Forest;
        private GameObject City_Pretty;
        private bool Checked = false;
        void Update()
        {
            if (PluginConfig.noclip)
            { 
                if (!Checked)
                {
                    Forest = GameObject.Find("Environment Objects/LocalObjects_Prefab");
                    City_Pretty = GameObject.Find("City_Pretty");
                }
                if (ControllerInputPoller.instance.rightControllerTriggerButton)
                {
                    Forest.SetActive(false);
                    if (City_Pretty != null)
                    {
                        City_Pretty.SetActive(false);
                    }
                }
                else if (!ControllerInputPoller.instance.rightControllerTriggerButton)
                {
                    Forest.SetActive(true);
                    if (City_Pretty != null)
                    {
                        City_Pretty.SetActive(true);
                    }
                }
            }
            else
            {
                Destroy(Plugin.holder.GetComponent<NoclipShittyDisable>());
            }
        }
    }
}
