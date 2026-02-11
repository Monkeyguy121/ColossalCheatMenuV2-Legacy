using Mono.Cecil.Cil;
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
    public class SpeedMod : MonoBehaviour
    {
        public void Update()
        {
            if(Menu.Menu.Speed[0].stringsliderind != null)
            {
                switch (Menu.Menu.Speed[0].stringsliderind)
                {
                    case 1:
                        GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = 7.2f;
                        break;
                    case 2:
                        GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = 7.4f;
                        break;
                    case 3:
                        GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = 7.6f;
                        break;
                    case 4:
                        GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = 7.8f;
                        break;
                    case 5:
                        GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = 8f;
                        break;
                    case 6:
                        GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = 8.2f;
                        break;
                    case 7:
                        GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = 8.4f;
                        break;
                    case 8:
                        GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = 8.6f;
                        break;
                    case 9:
                        GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = 8.8f;
                        break;
                    case 10:
                        GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = 9f;
                        break;
                    case 11:
                        GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = int.MaxValue;
                        break;
                }
                if (ControllerInputPoller.instance.leftGrab)
                {
                    switch (Menu.Menu.Speed[1].stringsliderind)
                    {
                        case 1:
                            GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = 7.2f;
                            break;
                        case 2:
                            GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = 7.4f;
                            break;
                        case 3:
                            GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = 7.6f;
                            break;
                        case 4:
                            GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = 7.8f;
                            break;
                        case 5:
                            GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = 8f;
                            break;
                        case 6:
                            GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = 8.2f;
                            break;
                        case 7:
                            GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = 8.4f;
                            break;
                        case 8:
                            GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = 8.6f;
                            break;
                        case 9:
                            GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = 8.8f;
                            break;
                        case 10:
                            GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = 9f;
                            break;
                        case 11:
                            GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = int.MaxValue;
                            break;
                    }
                }
                if (ControllerInputPoller.instance.rightGrab)
                {
                    switch (Menu.Menu.Speed[2].stringsliderind)
                    {
                        case 1:
                            GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = 7.2f;
                            break;
                        case 2:
                            GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = 7.4f;
                            break;
                        case 3:
                            GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = 7.6f;
                            break;
                        case 4:
                            GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = 7.8f;
                            break;
                        case 5:
                            GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = 8f;
                            break;
                        case 6:
                            GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = 8.2f;
                            break;
                        case 7:
                            GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = 8.4f;
                            break;
                        case 8:
                            GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = 8.6f;
                            break;
                        case 9:
                            GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = 8.8f;
                            break;
                        case 10:
                            GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = 9f;
                            break;
                        case 11:
                            GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = int.MaxValue;
                            break;
                    }
                }
            }
        }
    }
}
