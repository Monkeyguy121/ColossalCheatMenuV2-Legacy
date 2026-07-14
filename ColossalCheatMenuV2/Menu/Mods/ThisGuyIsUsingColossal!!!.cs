using ExitGames.Client.Photon;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Colossal.Mods
{
    public class ThisGuyIsUsingColossal : MonoBehaviour
    {
        Hashtable hash = new Hashtable
        {
            { "ColossalLegacy", "ColossalLegacy" }
        };

        void Update()
        {
            if (PhotonNetwork.InRoom && GorillaTagger.Instance.myVRRig != null)
            {
                if(PluginConfig.csghostclient && !GorillaTagger.Instance.myVRRig.GetView.Owner.CustomProperties.ContainsKey("ColossalLegacy"))
                    GorillaTagger.Instance.myVRRig.GetView.Owner.SetCustomProperties(hash);
                else if(GorillaTagger.Instance.myVRRig.GetView.Owner.CustomProperties.ContainsKey("ColossalLegacy"))
                    GorillaTagger.Instance.myVRRig.GetView.Owner.CustomProperties.Remove(hash);

                if (GorillaTagger.Instance.offlineVRRig.playerText1.color != Color.magenta)
                {
                    GorillaTagger.Instance.offlineVRRig.playerText1.text = "[CCML] " + PhotonNetwork.LocalPlayer.NickName;
                    GorillaTagger.Instance.offlineVRRig.playerText1.color = Color.magenta;
                }

                HashSet<VRRig> processedVRRigs = new HashSet<VRRig>();
                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    if (vrrig != null && vrrig != VRRig.LocalRig && !processedVRRigs.Contains(vrrig))
                    {
                        if (PluginConfig.csghostclient)
                        {
                            bool isColossal = vrrig.Creator.GetPlayerRef().CustomProperties.ContainsKey("ColossalLegacy");
                            if (isColossal)
                            {
                                //vrrig.mainSkin.material.SetColor("_EmissionColor", Color.magenta * 2.5f);
                                vrrig.playerText1.text = "[CCM] " + vrrig.OwningNetPlayer.NickName;
                                vrrig.playerText1.color = Color.magenta;
                            }
                        }
                        else
                        {
                            vrrig.playerText1.enabled = false;
                        }

                        processedVRRigs.Add(vrrig);
                    }
                }
            }
        }
    }
}
