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
    public class ExcelFly : MonoBehaviour
    {
        public void Update()
        {
            if (PluginConfig.excelfly)
            {
                if (ControllerInputPoller.instance.leftControllerPrimaryButton)
                {
                    GorillaLocomotion.GTPlayer.Instance.bodyCollider.attachedRigidbody.linearVelocity += -GorillaLocomotion.GTPlayer.Instance.LeftHand.controllerTransform.right / 2f;
                }
                if (ControllerInputPoller.instance.rightControllerPrimaryButton)
                {
                    GorillaLocomotion.GTPlayer.Instance.bodyCollider.attachedRigidbody.linearVelocity += GorillaLocomotion.GTPlayer.Instance.RightHand.controllerTransform.right / 2f;
                }
            }
            else
            {
                Destroy(Plugin.holder.GetComponent<ExcelFly>());
            }
        }
    }
}
