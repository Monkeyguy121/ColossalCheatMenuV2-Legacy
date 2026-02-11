using GorillaGameModes;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR;
using static Colossal.Plugin;

namespace Colossal.Mods {
    public class TagAura : MonoBehaviour {
        private LineRenderer radiusLine;
        private Material lineMaterial;

        private int ammount;
        public void Update() {
            if(Menu.Menu.Player[6].stringsliderind != null)
            {
                switch (Menu.Menu.Player[6].stringsliderind)
                {
                    case 0:
                        ammount = 0;
                        break;
                    case 1:
                        ammount = 3;
                        break;
                    case 2:
                        ammount = 2;
                        break;
                    case 3:
                        ammount = 1;
                        break;
                }

                if(ammount > 0)
                {
                    foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                    {
                        if (!vrrig.isOfflineVRRig)
                        {
                            float distance = Vector3.Distance(GorillaTagger.Instance.offlineVRRig.transform.position, vrrig.transform.position);
                            if (distance < GorillaGameManager.instance.tagDistanceThreshold / ammount && !vrrig.mainSkin.material.name.Contains("fected"))
                            {
                                if (GorillaGameManager.instance.gameObject.GetComponent<GorillaTagManager>().currentInfectedArray.Contains(GorillaTagger.Instance.myVRRig.GetView.Owner.ActorNumber))
                                {
                                    if(PluginConfig.csghostclient)
                                    {
                                        if (radiusLine == null)
                                        {
                                            lineMaterial = new Material(Shader.Find("Sprites/Default"));
                                            lineMaterial.color = new Color(0.6f, 0f, 0.8f, 0.5f);

                                            GameObject lineObject = new GameObject("RadiusLine");
                                            lineObject.transform.parent = vrrig.transform;
                                            radiusLine = lineObject.AddComponent<LineRenderer>();
                                            radiusLine.positionCount = 2;
                                            radiusLine.startWidth = 0.05f;
                                            radiusLine.endWidth = 0.05f;
                                            radiusLine.material = lineMaterial;
                                        }
                                        radiusLine.SetPosition(0, vrrig.transform.position);
                                        radiusLine.SetPosition(1, GorillaTagger.Instance.transform.position);
                                        if (radiusLine.GetPosition(0) == null)
                                        {
                                            if (radiusLine != null)
                                            {
                                                Destroy(radiusLine);
                                                radiusLine = null;
                                            }
                                        }
                                    }
                                    GorillaLocomotion.GTPlayer.Instance.RightHand.controllerTransform.position = vrrig.transform.position;
                                    GameMode.ReportTag(vrrig.OwningNetPlayer);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
