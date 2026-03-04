using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Colossal.Patches
{
    internal class RPCProtection
    {
        public static void SkiddedRPCProtection()
        {
            try
            {
                MonkeAgent.instance.rpcErrorMax = int.MaxValue;
                MonkeAgent.instance.rpcCallLimit = int.MaxValue;
                MonkeAgent.instance.logErrorMax = int.MaxValue;

                PhotonNetwork.MaxResendsBeforeDisconnect = int.MaxValue;
                PhotonNetwork.QuickResends = int.MaxValue;

                PhotonNetwork.RemoveRPCs(PhotonNetwork.LocalPlayer);
                PhotonNetwork.OpCleanRpcBuffer(GorillaTagger.Instance.myVRRig.GetView);
                PhotonNetwork.RemoveBufferedRPCs(GorillaTagger.Instance.myVRRig.ViewID, null, null);
                PhotonNetwork.RemoveRPCsInGroup(int.MaxValue);
                PhotonNetwork.SendAllOutgoingCommands();
                MonkeAgent.instance.OnPlayerLeftRoom(PhotonNetwork.LocalPlayer);
            }
            catch (Exception e)
            {
                {
                    Debug.LogException(e);
                }
            }
        }
    }
}
