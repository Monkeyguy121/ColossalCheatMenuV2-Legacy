using ExitGames.Client.Photon;
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
    public class QuickDisc : MonoBehaviour
    {
        
        public void Update()
        {
            if (PluginConfig.QuickDisc && PhotonNetwork.InRoom)
            {
                if (ControllerInputPoller.instance.leftControllerSecondaryButton && ControllerInputPoller.instance.rightControllerSecondaryButton)
                {
                    CustomConsole.LogToConsole($"\n Panic Kick Triggered!");
                    PhotonNetwork.Disconnect();
                }
            }
            else
            {
                Destroy(Plugin.holder.GetComponent<QuickDisc>());
            }
        }
    }
}
