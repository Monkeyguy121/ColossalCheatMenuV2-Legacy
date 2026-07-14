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
    public class MuteAll : MonoBehaviour
    {
        private bool IsNull = true;
        public void Update()
        {
            if (PluginConfig.MuteAll)
            {
                var buttons = UnityEngine.Object.FindObjectsOfType<GorillaPlayerLineButton>();
                foreach (var btn in buttons)
                {
                    if (btn.buttonType != GorillaPlayerLineButton.ButtonType.Mute) continue;
                    if (!btn.isOn)
                    {
                        btn.isOn = true;
                        btn.parentLine.muteButton.isOn = true;
                        btn.UpdateColor();
                    }
                }
            }
            else
            {
                var buttons = UnityEngine.Object.FindObjectsOfType<GorillaPlayerLineButton>();
                foreach (var btn in buttons)
                {
                    if (btn.buttonType != GorillaPlayerLineButton.ButtonType.Mute) continue;
                    if (btn.isOn)
                    {
                        btn.isOn = false;
                        btn.parentLine.muteButton.isOn = false;
                        btn.UpdateColor();
                    }
                }
                Destroy(Plugin.holder.GetComponent<MuteAll>());
            }
        }
    }
}