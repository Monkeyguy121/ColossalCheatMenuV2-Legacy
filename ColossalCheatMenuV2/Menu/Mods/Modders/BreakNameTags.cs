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
    public class BreakNameTags : MonoBehaviour
    {
        bool once = false;
        string name = "GET FUCKED BY COLOSSAL CHEAT MENU V2 GET FUCKED BY COLOSSAL CHEAT MENU V2\nGET FUCKED BY COLOSSAL CHEAT MENU V2 GET FUCKED BY COLOSSAL CHEAT MENU V2\nGET FUCKED BY COLOSSAL CHEAT MENU V2 GET FUCKED BY COLOSSAL CHEAT MENU V2\nGET FUCKED BY COLOSSAL CHEAT MENU V2 GET FUCKED BY COLOSSAL CHEAT MENU V2\nGET FUCKED BY COLOSSAL CHEAT MENU V2 GET FUCKED BY COLOSSAL CHEAT MENU V2\nGET FUCKED BY COLOSSAL CHEAT MENU V2 GET FUCKED BY COLOSSAL CHEAT MENU V2\n";
        public void Update()
        {
            if (PluginConfig.breaknametags && !once && PhotonNetwork.InRoom)
            {
                PhotonNetwork.LocalPlayer.NickName = name;
                GorillaComputer.instance.currentName = name;
                GorillaComputer.instance.savedName = name;
                PlayerPrefs.SetString("GorillaLocomotion.GTPlayerName", name);
                once = true;
            }
            else
            {
                once = false;
                PhotonNetwork.LocalPlayer.NickName = "MONKEYGUYONE";
                GorillaComputer.instance.currentName = "MONKEYGUYONE";
                GorillaComputer.instance.savedName = "MONKEYGUYONE";
                PlayerPrefs.SetString("GorillaLocomotion.GTPlayerName", "MONKEYGUYONE");
                Destroy(Plugin.holder.GetComponent<BreakNameTags>());
            }
        }
    }
}
