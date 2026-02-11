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
    public class BreakPunchMod : MonoBehaviour
    {
        private float colorTimer = 0f;
        public static Color colour = Color.red;
        public void Update()
        {
            float r = Mathf.Lerp(0f, 1f, Mathf.Abs(Mathf.Sin(colorTimer * 0.4f)));
            float g = Mathf.Lerp(0f, 1f, Mathf.Abs(Mathf.Sin(colorTimer * 0.5f)));
            float b = Mathf.Lerp(0f, 1f, Mathf.Abs(Mathf.Sin(colorTimer * 0.6f)));
            colour = new Color(r, g, b);
            colorTimer += Time.deltaTime * 2;

            if (Plugin.breakpunchmod)
            {
                MeshRenderer[] particleSystems = GameObject.FindObjectsOfType<MeshRenderer>();
                foreach (MeshRenderer system in particleSystems) {
                    system.material.shader = Shader.Find("Standard");
                    system.material.color = colour;
                    system.material.SetFloat("_Glossiness", 0.5f);
                    system.material.SetFloat("_Metallic", 6f);
                }
            }
            else
            {
                
                Destroy(GorillaTagger.Instance.GetComponent<BreakPunchMod>());
            }
        }
    }
}
