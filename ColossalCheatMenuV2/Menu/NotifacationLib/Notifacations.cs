using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using static Colossal.Plugin;

namespace Colossal.Menu.ClientHub {
    public class Notifacations : MonoBehaviour {
        private bool loaded = false;

        int NotificationDecayTime = 150;
        int NotificationDecayTimeCounter = 0;
        public static int NoticationThreshold = 5;
        string[] Notifilines;
        string newtext;
        public static string PreviousNotifi;

        public static GameObject HUDObj;
        public static GameObject HUDObj2;
        static GameObject MainCamera;
        static Text Testtext;
        private static TextAnchor textAnchor = TextAnchor.UpperRight;
        static Material AlertText = new Material(Shader.Find("GUI/Text Shader"));
        static Text NotifiText;

        private static bool once = false;
        public void Update() {
            if (PluginConfig.noti && Menu.agreement && PluginConfig.csghostclient) {
                if(!loaded) {
                    MainCamera = GameObject.Find("Main Camera");
                    HUDObj = new GameObject();
                    HUDObj2 = new GameObject();
                    HUDObj2.name = "CLIENT_HUB_NOTIFACATIONS";
                    HUDObj.name = "CLIENT_HUB_NOTIFACATIONS";
                    HUDObj.AddComponent<Canvas>();
                    HUDObj.AddComponent<CanvasScaler>();
                    HUDObj.AddComponent<GraphicRaycaster>();
                    HUDObj.GetComponent<Canvas>().enabled = true;
                    HUDObj.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
                    HUDObj.GetComponent<Canvas>().worldCamera = MainCamera.GetComponent<Camera>();
                    HUDObj.GetComponent<RectTransform>().sizeDelta = new Vector2(5, 5);
                    HUDObj.GetComponent<RectTransform>().position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z);
                    HUDObj2.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z - 4.6f);
                    HUDObj.transform.parent = HUDObj2.transform;
                    HUDObj.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 1.6f);
                    var Temp = HUDObj.GetComponent<RectTransform>().rotation.eulerAngles;
                    Temp.y = -270f;
                    HUDObj.transform.localScale = new Vector3(1f, 1f, 1f);
                    HUDObj.GetComponent<RectTransform>().rotation = Quaternion.Euler(Temp);
                    GameObject TestText = new GameObject();
                    TestText.transform.parent = HUDObj.transform;
                    Testtext = TestText.AddComponent<Text>();
                    Testtext.text = "";
                    Testtext.fontSize = 10;
                    Testtext.font = Plugin.gtagfont;
                    Testtext.rectTransform.sizeDelta = new Vector2(260, 300);
                    Testtext.rectTransform.localScale = new Vector3(0.01f, 0.01f, 0.7f);
                    Testtext.rectTransform.localPosition = new Vector3(-2.4f, 0.3f, 0f);
                    Testtext.material = AlertText;
                    NotifiText = Testtext;
                    Testtext.alignment = TextAnchor.UpperLeft;

                    HUDObj2.transform.SetParent(MainCamera.transform);

                    loaded = true;
                }
                HUDObj2.transform.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z);
                HUDObj2.transform.rotation = MainCamera.transform.rotation;
            } else {
                if(Testtext.text != null) {
                    Testtext.text = "";
                }
            }
        }
        private void FixedUpdate() {
            if(PluginConfig.noti && Menu.agreement) {

                //Debug.Log("Noti enabled thingy thing");

                if (Testtext.text != null) {
                    //Debug.Log("Noti test text isnt null");

                    NotificationDecayTimeCounter++;
                    if (NotificationDecayTimeCounter > NotificationDecayTime) {
                        Notifilines = null;
                        newtext = "";
                        NotificationDecayTimeCounter = 0;
                        Notifilines = Testtext.text.Split(Environment.NewLine.ToCharArray()).Skip(1).ToArray();
                        foreach (string Line in Notifilines) {
                            if (Line != "") {
                                newtext = newtext + Line + "\n";
                            }
                        }

                        Testtext.text = newtext;

                        //Debug.Log("Did noti shit");
                    }
                } else {
                    if(NotificationDecayTimeCounter != null)
                        NotificationDecayTimeCounter = 0;
                }
            }
        }

        public static void SendNotification(string NotificationText) {
            if (PluginConfig.noti) {
                if (!NotificationText.Contains(Environment.NewLine)) { NotificationText = NotificationText + Environment.NewLine; }
                NotifiText.text = NotifiText.text + NotificationText;
                PreviousNotifi = NotificationText;
            }
        }
        public static void ClearAllNotifications() {
            NotifiText.text = "";
        }
        public static void ClearPastNotifications(int amount) {
            string[] Notifilines = null;
            string newtext = "";
            Notifilines = NotifiText.text.Split(Environment.NewLine.ToCharArray()).Skip(amount).ToArray();
            foreach (string Line in Notifilines) {
                if (Line != "") {
                    newtext = newtext + Line + "\n";
                }
            }

            NotifiText.text = newtext;
        }
    }
}
