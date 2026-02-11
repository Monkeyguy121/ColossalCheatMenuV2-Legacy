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
    public class Overlay : MonoBehaviour {
        private bool loaded1 = false;

        private float deltaTime;

        static GameObject HUDObjROOM;
        static GameObject HUDObj2ROOM;
        static GameObject MainCamera;
        static Text TesttextROOM;
        private static TextAnchor textAnchor = TextAnchor.UpperRight;
        static Material AlertText = new Material(Shader.Find("GUI/Text Shader"));
        static Text NotifiTextROOM;

        static GameObject HUDObj;
        static GameObject HUDObj2;
        static Text Testtext;
        static Text NotifiText;

        public void Update() {
            if (PluginConfig.overlay && Menu.agreement && PluginConfig.csghostclient) {
                if (!loaded1) {
                    MainCamera = GameObject.Find("Main Camera");
                    HUDObjROOM = new GameObject();
                    HUDObj2ROOM = new GameObject();
                    HUDObj2ROOM.name = "CLIENT_HUB_OVERLAYROOM";
                    HUDObjROOM.name = "CLIENT_HUB_OVERLAYROOM";
                    HUDObjROOM.AddComponent<Canvas>();
                    HUDObjROOM.AddComponent<CanvasScaler>();
                    HUDObjROOM.AddComponent<GraphicRaycaster>();
                    HUDObjROOM.GetComponent<Canvas>().enabled = true;
                    HUDObjROOM.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
                    HUDObjROOM.GetComponent<Canvas>().worldCamera = MainCamera.GetComponent<Camera>();
                    HUDObjROOM.GetComponent<RectTransform>().sizeDelta = new Vector2(5, 5);
                    HUDObjROOM.GetComponent<RectTransform>().position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z);
                    HUDObj2ROOM.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z - 4.6f);
                    HUDObjROOM.transform.parent = HUDObj2ROOM.transform;
                    HUDObjROOM.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 1.6f);
                    var Temp = HUDObjROOM.GetComponent<RectTransform>().rotation.eulerAngles;
                    Temp.y = -270f;
                    HUDObjROOM.transform.localScale = new Vector3(1f, 1f, 1f);
                    HUDObjROOM.GetComponent<RectTransform>().rotation = Quaternion.Euler(Temp);
                    GameObject TestText = new GameObject();
                    TestText.transform.parent = HUDObjROOM.transform;
                    TesttextROOM = TestText.AddComponent<Text>();
                    TesttextROOM.text = "";
                    TesttextROOM.fontSize = 10;
                    TesttextROOM.font = Plugin.gtagfont;
                    TesttextROOM.rectTransform.sizeDelta = new Vector2(260, 300);
                    TesttextROOM.rectTransform.localScale = new Vector3(0.01f, 0.01f, 0.7f);
                    TesttextROOM.rectTransform.localPosition = new Vector3(-2.4f, -2f, 2f);
                    TesttextROOM.material = AlertText;
                    NotifiTextROOM = TesttextROOM;
                    TesttextROOM.alignment = TextAnchor.UpperLeft;

                    HUDObj = new GameObject();
                    HUDObj2 = new GameObject();
                    HUDObj2.name = "CLIENT_HUB_OVERLAY";
                    HUDObj.name = "CLIENT_HUB_OVERLAY";
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
                    var Temp1 = HUDObj.GetComponent<RectTransform>().rotation.eulerAngles;
                    Temp.y = -270f;
                    HUDObj.transform.localScale = new Vector3(1f, 1f, 1f);
                    HUDObj.GetComponent<RectTransform>().rotation = Quaternion.Euler(Temp);
                    GameObject TestText1 = new GameObject();
                    TestText1.transform.parent = HUDObj.transform;
                    Testtext = TestText1.AddComponent<Text>();
                    Testtext.text = "";
                    Testtext.fontSize = 10;
                    Testtext.font = Plugin.gtagfont;
                    Testtext.rectTransform.sizeDelta = new Vector2(260, 300);
                    Testtext.rectTransform.localScale = new Vector3(0.01f, 0.01f, 0.7f);
                    Testtext.rectTransform.localPosition = new Vector3(-2.4f, -2f, 0f);
                    Testtext.material = AlertText;
                    NotifiText = Testtext;
                    Testtext.alignment = TextAnchor.UpperLeft;

                    HUDObj2.transform.SetParent(MainCamera.transform);
                    HUDObj2ROOM.transform.SetParent(MainCamera.transform);

                    loaded1 = true;
                }
                HUDObj2.transform.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z);
                HUDObj2.transform.rotation = MainCamera.transform.rotation;
                HUDObj2ROOM.transform.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z);
                HUDObj2ROOM.transform.rotation = MainCamera.transform.rotation;

                deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
                float fps = 1.0f / deltaTime;
                if(PhotonNetwork.InRoom) {
                    TesttextROOM.text = $"<color={Menu.MenuColour}>RoomName: </color>{PhotonNetwork.CurrentRoom.Name}\n<color={Menu.MenuColour}>Players: </color>{PhotonNetwork.CurrentRoom.PlayerCount}";
                } else {
                    if (TesttextROOM.text != null) {
                        TesttextROOM.text = "";
                    }
                }
                Testtext.text = $"<color={Menu.MenuColour}>Ping: </color>{PhotonNetwork.GetPing()}\n<color={Menu.MenuColour}>FPS: </color>{fps.ToString("F2")}";
            } else {
                if(Testtext.text != null) {
                    Testtext.text = "";
                }
                if (TesttextROOM.text != null) {
                    TesttextROOM.text = "";
                }
            }
        }
    }
}
