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
    public class FirstPerson : MonoBehaviour
    {
        private GameObject ThirdPersonCam;
        private bool gavewarning = false;
        private bool IsNull = true;
        public void Update()
        {
            if (IsNull)
            {
                ThirdPersonCam = GameObject.Find("Player Objects/Third Person Camera");
                IsNull = false;
            }
            if (!gavewarning)
            {
                CustomConsole.LogToConsole($"\nWARNING: Works Best Without Vr!");
                gavewarning = true;
            }
            if (PluginConfig.FirstPerson)
            {
                ThirdPersonCam.SetActive(false);
            }
            else
            {
                ThirdPersonCam.SetActive(true);
                Destroy(Plugin.holder.GetComponent<FirstPerson>());
            }
        }
    }
}