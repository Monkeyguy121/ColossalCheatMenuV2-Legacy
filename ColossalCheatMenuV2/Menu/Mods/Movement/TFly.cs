using Colossal.Patches;
using ColossalCheatMenuV2.Patches.MakeItFuckingWork;
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
    public class TFly : MonoBehaviour
    {
        public void Update()
        {
            if (PluginConfig.tfly)
            {
                bool leftControllerSecondaryButton = ControllerInputPoller.instance.leftControllerSecondaryButton;
                bool rightTrigger = Controls.RightTrigger();
                if (leftControllerSecondaryButton)
                {
                    GorillaLocomotion.GTPlayer.Instance.bodyCollider.attachedRigidbody.linearVelocity = new Vector3(0f, 0.01f, 0f);
                }
                if (rightTrigger)
                {
                    GorillaLocomotion.GTPlayer.Instance.transform.position += GorillaLocomotion.GTPlayer.Instance.RightHand.controllerTransform.forward * 0.45f;
                    GorillaLocomotion.GTPlayer.Instance.bodyCollider.attachedRigidbody.linearVelocity = Vector3.zero;
                    return;
                }
            }
            else
            {
                Destroy(Plugin.holder.GetComponent<TFly>());
            }
        }
    }
}
