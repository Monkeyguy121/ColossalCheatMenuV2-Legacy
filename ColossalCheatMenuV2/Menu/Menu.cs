using BepInEx;
using Colossal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;
using UnityEngine;
using Colossal.Mods;
using UnityEngine.XR;
using GorillaNetworking;
using Photon.Pun;
using PlayFab;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using ExitGames.Client.Photon;
using Colossal.Menu.ClientHub;
using HarmonyLib;
using System.Reflection;
using Photon.Realtime;
using Valve.VR;
using ColossalCheatMenuV2.Patches.MakeItFuckingWork;
using System.Runtime.Remoting.Messaging;
using static Valve.VR.SteamVR_ExternalCamera;

namespace Colossal.Menu {
    public class MenuOption {
        public string DisplayName;
        public string _type;
        public bool AssociatedBool;
        public string AssociatedString;
        public float AssociatedFloat;
        public int AssociatedInt;
        public string[] StringArray;
        public int stringsliderind;
    }
    public class Menu {
        public static bool GUIToggled = true;

        public static GameObject HUDObj;
        public static GameObject HUDObj2;
        static GameObject MainCamera;
        static Text Testtext;
        private static TextAnchor textAnchor = TextAnchor.UpperRight;
        static Material AlertText = new Material(Shader.Find("GUI/Text Shader"));
        static Text NotifiText;

        public static string MenuState = "Main";
        public static string MenuColour = "magenta";
        public static float menurgb = 0;
        public static int SelectedOptionIndex = 0;
        public static MenuOption[] CurrentViewingMenu = null;
        public static MenuOption[] MainMenu;
        public static MenuOption[] Movement;
        public static MenuOption[] Visual;
        public static MenuOption[] Player;
        public static MenuOption[] Computer;
        public static MenuOption[] Modders;
        public static MenuOption[] Account;
        public static MenuOption[] Settings;

        public static MenuOption[] Speed;
        public static MenuOption[] TagAura;
        public static MenuOption[] Presets;
        public static MenuOption[] Sky;

        public static bool inputcooldown = false;
        public static bool menutogglecooldown = false;

        public static bool agreement = false;
        public static void LoadOnce() {
            Debug.Log("Load Once Ran");

            try {
                if (!agreement) {
                    Debug.Log("Aggreement Is False");

                    MainCamera = GameObject.Find("Main Camera");
                    HUDObj = new GameObject();
                    HUDObj2 = new GameObject();
                    HUDObj2.name = "CLIENT_HUB_AGREEMENT";
                    HUDObj.name = "CLIENT_HUB_AGREEMENT";
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
                    Testtext.text = "<color=magenta><CONTROLS (DRIFT MODE)></color>\nLeft Joystick (Hold): Control\nRight Grip: Select\nRight Trigger: Move\nBoth Joysticks: Toggle\n\n<color=magenta><CONTROLS></color>\nRight Joystick (Right): Select\nRight Joystick (Down): Move\nBoth Joysticks: Toggle\n\n<color=magenta><CONTROLS (PC)></color>\nEnterKey: Select\nArrowKey (Up): Move Up\nArrowKey (Down): Move Down\n\n<color=cyan>Press Both Joysticks Or Enter...</color>";
                    Testtext.fontSize = 10;
                    Testtext.font = Plugin.gtagfont;
                    Testtext.rectTransform.sizeDelta = new Vector2(260, 300);
                    Testtext.rectTransform.localScale = new Vector3(0.01f, 0.01f, 1f);
                    Testtext.rectTransform.localPosition = new Vector3(-2.4f, -0.4f, 1f);
                    Testtext.material = AlertText;
                    NotifiText = Testtext;
                    Testtext.alignment = TextAnchor.UpperLeft;

                    //HUDObj2.transform.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z);
                    //HUDObj2.transform.rotation = MainCamera.transform.rotation;
                    HUDObj2.transform.SetParent(MainCamera.transform);
                } else {
                    Debug.Log("Aggreement Is True");


                    Plugin.holder.AddComponent<SpeedMod>();
                    Plugin.holder.AddComponent<WallWalk>();
                    Plugin.holder.AddComponent<TagAura>();


                    if (GorillaTagger.Instance.gameObject.GetComponent<Overlay>() == null)
                        GorillaTagger.Instance.gameObject.AddComponent<Overlay>();

                    if (GorillaTagger.Instance.gameObject.GetComponent<Notifacations>() == null)
                        GorillaTagger.Instance.gameObject.AddComponent<Notifacations>();

                    MainCamera = GameObject.Find("Main Camera");
                    HUDObj = new GameObject();
                    HUDObj2 = new GameObject();
                    HUDObj2.name = "CLIENT_HUB";
                    HUDObj.name = "CLIENT_HUB";
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
                    Testtext.rectTransform.sizeDelta = new Vector2(260, 160);
                    Testtext.rectTransform.localScale = new Vector3(0.01f, 0.01f, 1f);
                    Testtext.rectTransform.localPosition = new Vector3(-2.4f, 1f, 2f);
                    Testtext.material = AlertText;
                    NotifiText = Testtext;
                    Testtext.alignment = TextAnchor.UpperLeft;

                    //HUDObj2.transform.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z);
                    //HUDObj2.transform.rotation = MainCamera.transform.rotation;
                    HUDObj2.transform.SetParent(MainCamera.transform);

                    MainMenu = new MenuOption[11];
                    MainMenu[0] = new MenuOption { DisplayName = "Movement", _type = "submenu", AssociatedString = "Movement" };
                    MainMenu[1] = new MenuOption { DisplayName = "Visual", _type = "submenu", AssociatedString = "Visual" };
                    MainMenu[2] = new MenuOption { DisplayName = "Player", _type = "submenu", AssociatedString = "Player" };
                    MainMenu[3] = new MenuOption { DisplayName = "Computer", _type = "submenu", AssociatedString = "Computer" };
                    MainMenu[4] = new MenuOption { DisplayName = "Modders", _type = "submenu", AssociatedString = "Modders" };
                    MainMenu[5] = new MenuOption { DisplayName = "Account", _type = "submenu", AssociatedString = "Account" };
                    MainMenu[6] = new MenuOption { DisplayName = "Settings", _type = "submenu", AssociatedString = "Settings" };
                    MainMenu[7] = new MenuOption { DisplayName = "DriftMode", _type = "toggle", AssociatedBool = true };
                    //MainMenu[8] = new MenuOption { DisplayName = "AntiCrash", _type = "toggle", AssociatedBool = true };
                    MainMenu[8] = new MenuOption { DisplayName = "Notifacations", _type = "toggle", AssociatedBool = true };
                    MainMenu[9] = new MenuOption { DisplayName = "Overlay", _type = "toggle", AssociatedBool = true };
                    MainMenu[10] = new MenuOption { DisplayName = "CS Visuals", _type = "toggle", AssociatedBool = true };

                    Movement = new MenuOption[10];
                    Movement[0] = new MenuOption { DisplayName = "ExcelFly", _type = "toggle", AssociatedBool = false };
                    Movement[1] = new MenuOption { DisplayName = "TFly", _type = "toggle", AssociatedBool = false };
                    Movement[2] = new MenuOption { DisplayName = "WallWalk", _type = "STRINGslider", StringArray = new string[] {"Off", "7.5", "7.9", "8.3", "8.7", "9.1", "9.5" } };
                    Movement[3] = new MenuOption { DisplayName = "Speed", _type = "submenu", AssociatedString = "Speed" };
                    Movement[4] = new MenuOption { DisplayName = "Platforms", _type = "toggle", AssociatedBool = false };
                    Movement[5] = new MenuOption { DisplayName = "UpsideDown Monkey", _type = "toggle", AssociatedBool = false };
                    Movement[6] = new MenuOption { DisplayName = "WateryAir", _type = "toggle", AssociatedBool = false };
                    Movement[7] = new MenuOption { DisplayName = "LongArms", _type = "toggle", AssociatedBool = false };
                    Movement[8] = new MenuOption { DisplayName = "[BROKEN] SpinBot", _type = "toggle", AssociatedBool = false };
                    Movement[9] = new MenuOption { DisplayName = "Back", _type = "submenu", AssociatedString = "Back" };
                    Speed = new MenuOption[4];
                    Speed[0] = new MenuOption { DisplayName = "Speed", _type = "STRINGslider", StringArray = new string[] { "Off", "7", "7.2", "7.4", "7.6", "7.8", "8", "8.2", "8.4", "8.6", "8.8", "9", "No Limit" } };
                    Speed[1] = new MenuOption { DisplayName = "Speed (LG)", _type = "STRINGslider", StringArray = new string[] { "Off", "7", "7.2", "7.4", "7.6", "7.8", "8", "8.2", "8.4", "8.6", "8.8", "9", "No Limit" } };
                    Speed[2] = new MenuOption { DisplayName = "Speed (RG)", _type = "STRINGslider", StringArray = new string[] { "Off", "7", "7.2", "7.4", "7.6", "7.8", "8", "8.2", "8.4", "8.6", "8.8", "9", "No Limit" } };
                    Speed[3] = new MenuOption { DisplayName = "Back", _type = "submenu", AssociatedString = "Back" };

                    Visual = new MenuOption[7];
                    Visual[0] = new MenuOption { DisplayName = "Chams", _type = "toggle", AssociatedBool = false };
                    Visual[1] = new MenuOption { DisplayName = "BoxESP", _type = "toggle", AssociatedBool = false };
                    Visual[2] = new MenuOption { DisplayName = "HollowBoxESP", _type = "toggle", AssociatedBool = false };
                    Visual[3] = new MenuOption { DisplayName = "Sky Colour", _type = "submenu", AssociatedString = "Sky" };
                    Visual[4] = new MenuOption { DisplayName = "WhyIsEveryoneLookingAtMe", _type = "toggle", AssociatedBool = false };
                    Visual[5] = new MenuOption { DisplayName = "No Expressions", _type = "toggle", AssociatedBool = false };
                    Visual[6] = new MenuOption { DisplayName = "Back", _type = "submenu", AssociatedString = "Back" };
                    Sky = new MenuOption[6];
                    Sky[0] = new MenuOption { DisplayName = "MonkeyColour", _type = "button", AssociatedString = "monkeycoloursky" };
                    Sky[1] = new MenuOption { DisplayName = "Purple", _type = "button", AssociatedString = "purplesky" };
                    Sky[2] = new MenuOption { DisplayName = "Red", _type = "button", AssociatedString = "redsky" };
                    Sky[3] = new MenuOption { DisplayName = "Cyan", _type = "button", AssociatedString = "cyansky" };
                    Sky[4] = new MenuOption { DisplayName = "Green", _type = "button", AssociatedString = "greensky" };
                    Sky[5] = new MenuOption { DisplayName = "Back", _type = "submenu", AssociatedString = "Back" };

                    // Change Player array size from 9 to 10
                    Player = new MenuOption[10];
                    Player[0] = new MenuOption { DisplayName = "NoFinger", _type = "toggle", AssociatedBool = false };
                    Player[1] = new MenuOption { DisplayName = "TagGun", _type = "toggle", AssociatedBool = false };
                    Player[2] = new MenuOption { DisplayName = "[BROKEN] LegMod", _type = "toggle", AssociatedBool = false };
                    Player[3] = new MenuOption { DisplayName = "CreeperMonkey", _type = "toggle", AssociatedBool = false };
                    Player[4] = new MenuOption { DisplayName = "GhostMonkey", _type = "toggle", AssociatedBool = false };
                    Player[5] = new MenuOption { DisplayName = "InvisMonkey", _type = "toggle", AssociatedBool = false };
                    Player[6] = new MenuOption { DisplayName = "TagAura", _type = "STRINGslider", StringArray = new string[] { "Off", "Colossal", "Ghost", "Blatant" } };
                    Player[7] = new MenuOption { DisplayName = "Desync", _type = "toggle", AssociatedBool = false };
                    Player[8] = new MenuOption { DisplayName = "TagAll", _type = "toggle", AssociatedBool = false };
                    Player[9] = new MenuOption { DisplayName = "Back", _type = "submenu", AssociatedString = "Back" };

                    Modders = new MenuOption[5];
                    Modders[0] = new MenuOption { DisplayName = "Break NameTags", _type = "toggle", AssociatedBool = false };
                    Modders[1] = new MenuOption { DisplayName = "Break ModCheckers", _type = "toggle", AssociatedBool = false };
                    Modders[2] = new MenuOption { DisplayName = "No Snitch", _type = "button", AssociatedString = "nosnitch" };
                    Modders[3] = new MenuOption { DisplayName = "Pc Check Bypass", _type = "toggle", AssociatedBool = false };
                    Modders[4] = new MenuOption { DisplayName = "Back", _type = "submenu", AssociatedString = "Back" };

                    Computer = new MenuOption[8];
                    Computer[0] = new MenuOption { DisplayName = "Disconnect", _type = "button", AssociatedString = "disconnect" };
                    Computer[1] = new MenuOption { DisplayName = "RandomIdentity", _type = "button", AssociatedString = "randomidentity" };
                    Computer[2] = new MenuOption { DisplayName = "Join GTC", _type = "button", AssociatedString = "joincgt" };
                    Computer[3] = new MenuOption { DisplayName = "Join TTT", _type = "button", AssociatedString = "jointtt" };
                    Computer[4] = new MenuOption { DisplayName = "Join CBOT", _type = "button", AssociatedString = "joincbot" };
                    Computer[5] = new MenuOption { DisplayName = "Modded Casual", _type = "button", AssociatedString = "moddedcasual" };
                    Computer[6] = new MenuOption { DisplayName = "Modded Infection", _type = "button", AssociatedString = "moddedinfection" };
                    Computer[7] = new MenuOption { DisplayName = "Back", _type = "submenu", AssociatedString = "Back" };

                    Account = new MenuOption[5];
                    Account[0] = new MenuOption { DisplayName = "Disconnect", _type = "button", AssociatedString = "disconnectplayfab" };
                    Account[1] = new MenuOption { DisplayName = "Server: USW", _type = "button", AssociatedString = "serverusw" };
                    Account[2] = new MenuOption { DisplayName = "Server: US", _type = "button", AssociatedString = "serverus" };
                    Account[3] = new MenuOption { DisplayName = "Server: EU", _type = "button", AssociatedString = "servereu" };
                    Account[4] = new MenuOption { DisplayName = "Back", _type = "submenu", AssociatedString = "Back" };

                    Settings = new MenuOption[6];
                    Settings[0] = new MenuOption { DisplayName = "MenuColour", _type = "STRINGslider", StringArray = new string[] { "Purple", "Red", "Yellow", "Green", "Blue", "RGB" } };
                    Settings[1] = new MenuOption { DisplayName = "MenuPosition", _type = "STRINGslider", StringArray = new string[] { "Top Right", "Middle" } };
                    Settings[2] = new MenuOption { DisplayName = "Config", _type = "STRINGslider", StringArray = Configs.GetConfigFileNames() };
                    Settings[3] = new MenuOption { DisplayName = "Load Config", _type = "button", AssociatedString = "loadconfig" };
                    Settings[4] = new MenuOption { DisplayName = "Save Config", _type = "button", AssociatedString = "saveconfig" };
                    Settings[5] = new MenuOption { DisplayName = "Back", _type = "submenu", AssociatedString = "Back" };

                    MenuState = "Main";
                    CurrentViewingMenu = MainMenu;
                }

                UpdateMenuState(new MenuOption(), null, null);

                Debug.Log("Updated Menu State");
            }
            catch (Exception ex) {
                Debug.Log(ex.ToString());
            }
            
        }
        public static void Load() {
            if (!agreement)
            {
                Menu.HUDObj2.transform.transform.position = new Vector3(Menu.MainCamera.transform.position.x, Menu.MainCamera.transform.position.y, Menu.MainCamera.transform.position.z);
                Menu.HUDObj2.transform.rotation = Menu.MainCamera.transform.rotation;
                bool state = SteamVR_Actions.gorillaTag_LeftJoystickClick.GetState(SteamVR_Input_Sources.LeftHand);
                bool state2 = SteamVR_Actions.gorillaTag_RightJoystickClick.GetState(SteamVR_Input_Sources.RightHand);
                if (state && state2 && !Menu.menutogglecooldown)
                {
                    Menu.menutogglecooldown = true;
                    Menu.agreement = true;
                    UnityEngine.Object.Destroy(GameObject.Find("CLIENT_HUB_AGREEMENT"));
                    Menu.LoadOnce();
                }
                else
                {
                    Menu.menutogglecooldown = false;
                }
                if (Keyboard.current.enterKey.wasPressedThisFrame)
                {
                    Menu.menutogglecooldown = true;
                    Menu.agreement = true;
                    UnityEngine.Object.Destroy(GameObject.Find("CLIENT_HUB_AGREEMENT"));
                    Menu.LoadOnce();
                    return;
                }
            }
            else
            {
                bool state3 = SteamVR_Actions.gorillaTag_LeftJoystickClick.GetState(SteamVR_Input_Sources.LeftHand);
                bool state4 = SteamVR_Actions.gorillaTag_RightJoystickClick.GetState(SteamVR_Input_Sources.RightHand);
                if (state3 && state4 && !Menu.menutogglecooldown)
                {
                    Menu.menutogglecooldown = true;
                    Menu.HUDObj2.active = !Menu.HUDObj2.active;
                    Menu.GUIToggled = !Menu.GUIToggled;
                }
                if (!state3 && !state4 && Menu.menutogglecooldown)
                {
                    Menu.menutogglecooldown = false;
                }
                if (Menu.GUIToggled)
                {
                    Menu.HUDObj2.transform.position = new Vector3(Menu.MainCamera.transform.position.x, Menu.MainCamera.transform.position.y, Menu.MainCamera.transform.position.z);
                    Menu.HUDObj2.transform.rotation = Menu.MainCamera.transform.rotation;


                    Keyboard current = Keyboard.current;
                    if (current.upArrowKey.wasPressedThisFrame)
                    {
                        Menu.inputcooldown = true;
                        if (Menu.SelectedOptionIndex == 0)
                        {
                            Menu.SelectedOptionIndex = Menu.CurrentViewingMenu.Count<MenuOption>() - 1;
                        }
                        else
                        {
                            Menu.SelectedOptionIndex--;
                        }
                        Menu.UpdateMenuState(new MenuOption(), null, null);
                    }
                    if (current.downArrowKey.wasPressedThisFrame)
                    {
                        Menu.inputcooldown = true;
                        if (Menu.SelectedOptionIndex + 1 == Menu.CurrentViewingMenu.Count<MenuOption>())
                        {
                            Menu.SelectedOptionIndex = 0;
                        }
                        else
                        {
                            Menu.SelectedOptionIndex++;
                        }
                        Menu.UpdateMenuState(new MenuOption(), null, null);
                    }
                    if (current.enterKey.wasPressedThisFrame)
                    {
                        Menu.inputcooldown = true;
                        Menu.UpdateMenuState(Menu.CurrentViewingMenu[Menu.SelectedOptionIndex], null, "optionhit");
                    }
                    if (CurrentViewingMenu[SelectedOptionIndex]._type == "STRINGslider")
                    {
                        if (current.leftArrowKey.wasPressedThisFrame)
                        {
                            if (CurrentViewingMenu[SelectedOptionIndex].stringsliderind == 0)
                                CurrentViewingMenu[SelectedOptionIndex].stringsliderind = CurrentViewingMenu[SelectedOptionIndex].StringArray.Count() - 1;
                            else
                            {
                                CurrentViewingMenu[SelectedOptionIndex].stringsliderind = CurrentViewingMenu[SelectedOptionIndex].stringsliderind - 1;
                            }
                            Menu.inputcooldown = true;
                        }
                        if (current.rightArrowKey.wasPressedThisFrame)
                        {
                            if ((CurrentViewingMenu[SelectedOptionIndex].stringsliderind + 1) == CurrentViewingMenu[SelectedOptionIndex].StringArray.Count())
                                CurrentViewingMenu[SelectedOptionIndex].stringsliderind = 0;
                            else
                            {
                                CurrentViewingMenu[SelectedOptionIndex].stringsliderind = CurrentViewingMenu[SelectedOptionIndex].stringsliderind + 1;
                            }
                            Menu.inputcooldown = true;
                        }
                        UpdateMenuState(new MenuOption(), null, null);
                    }



                    bool rightGrab = ControllerInputPoller.instance.rightGrab;

                    if (!PluginConfig.driftmode)
                    {
                        Vector2 rightJoystickAxis = SteamVR_Actions.gorillaTag_RightJoystick2DAxis.GetAxis(SteamVR_Input_Sources.RightHand);
                        if (rightJoystickAxis.y >= 0.7f && !Menu.inputcooldown)
                        {
                            Menu.inputcooldown = true;
                            if (Menu.SelectedOptionIndex == 0)
                            {
                                Menu.SelectedOptionIndex = Menu.CurrentViewingMenu.Count<MenuOption>() - 1;
                            }
                            else
                            {
                                Menu.SelectedOptionIndex--;
                            }
                            Menu.UpdateMenuState(new MenuOption(), null, null);
                        }
                        if (rightJoystickAxis.y >= -0.7f && !Menu.inputcooldown)
                        {
                            Menu.inputcooldown = true;
                            if (Menu.SelectedOptionIndex + 1 == Menu.CurrentViewingMenu.Count<MenuOption>())
                            {
                                Menu.SelectedOptionIndex = 0;
                            }
                            else
                            {
                                Menu.SelectedOptionIndex++;
                            }
                            Menu.UpdateMenuState(new MenuOption(), null, null);
                        }
                        if (rightJoystickAxis.x >= 0.7f && !Menu.inputcooldown)
                        {
                            Menu.inputcooldown = true;
                            Menu.UpdateMenuState(Menu.CurrentViewingMenu[Menu.SelectedOptionIndex], null, "optionhit");
                        }
                        if (CurrentViewingMenu[SelectedOptionIndex]._type == "STRINGslider")
                        {
                            if ((rightJoystickAxis.x < -0.7f && !Menu.inputcooldown))
                            {
                                if ((CurrentViewingMenu[SelectedOptionIndex].stringsliderind + 1) == CurrentViewingMenu[SelectedOptionIndex].StringArray.Count())
                                    CurrentViewingMenu[SelectedOptionIndex].stringsliderind = 0;
                                else
                                {
                                    CurrentViewingMenu[SelectedOptionIndex].stringsliderind = CurrentViewingMenu[SelectedOptionIndex].stringsliderind + 1;
                                }
                                Menu.inputcooldown = true;
                            }
                            if ((rightJoystickAxis.x > 0.7f && !Menu.inputcooldown))
                            {
                                if (CurrentViewingMenu[SelectedOptionIndex].stringsliderind == 0)
                                    CurrentViewingMenu[SelectedOptionIndex].stringsliderind = CurrentViewingMenu[SelectedOptionIndex].StringArray.Count() - 1;
                                else
                                {
                                    CurrentViewingMenu[SelectedOptionIndex].stringsliderind = CurrentViewingMenu[SelectedOptionIndex].stringsliderind - 1;
                                }
                                Menu.inputcooldown = true;
                            }
                            UpdateMenuState(new MenuOption(), null, null);
                        }
                        if (rightJoystickAxis.y <= -0.7f && rightJoystickAxis.y <= 0.7f && rightJoystickAxis.x <= 0.7f && Menu.inputcooldown)
                        {
                            Menu.inputcooldown = false;
                        }
                    }
                    else if (SteamVR_Actions.gorillaTag_LeftJoystickClick.GetState(SteamVR_Input_Sources.LeftHand))
                    {
                        bool trigger = SteamVR_Actions.gorillaTag_RightTriggerClick.GetState(SteamVR_Input_Sources.RightHand);
                        if (trigger && !Menu.inputcooldown)
                        {
                            Menu.inputcooldown = true;
                            if (Menu.SelectedOptionIndex + 1 == Menu.CurrentViewingMenu.Count<MenuOption>())
                            {
                                Menu.SelectedOptionIndex = 0;
                            }
                            else
                            {
                                Menu.SelectedOptionIndex++;
                            }
                            Menu.UpdateMenuState(new MenuOption(), null, null);
                        }
                        if (!trigger && !rightGrab && Menu.inputcooldown)
                        {
                            Menu.inputcooldown = false;
                        }

                        if (CurrentViewingMenu[SelectedOptionIndex]._type == "STRINGslider")
                        {
                            if ((rightGrab && !Menu.inputcooldown))
                            {
                                if (CurrentViewingMenu[SelectedOptionIndex].stringsliderind == 0)
                                    CurrentViewingMenu[SelectedOptionIndex].stringsliderind = CurrentViewingMenu[SelectedOptionIndex].StringArray.Count() - 1;
                                else
                                {
                                    CurrentViewingMenu[SelectedOptionIndex].stringsliderind = CurrentViewingMenu[SelectedOptionIndex].stringsliderind - 1;
                                }
                                Menu.inputcooldown = true;
                            }
                            UpdateMenuState(new MenuOption(), null, null);
                        }
                        if (rightGrab && !Menu.inputcooldown)
                        {
                            Menu.inputcooldown = true;
                            Menu.UpdateMenuState(Menu.CurrentViewingMenu[Menu.SelectedOptionIndex], null, "optionhit");
                        }
                    }
                }
                //DriftMode
                PluginConfig.driftmode = MainMenu[7].AssociatedBool;
                //PluginConfig.anticrash = MainMenu[8].AssociatedBool;
                PluginConfig.noti = MainMenu[8].AssociatedBool;
                PluginConfig.overlay = MainMenu[9].AssociatedBool;
                PluginConfig.csghostclient = MainMenu[10].AssociatedBool;

                //Movement
                PluginConfig.excelfly = Movement[0].AssociatedBool;
                PluginConfig.tfly = Movement[1].AssociatedBool;
                PluginConfig.wallwalk = Movement[2].stringsliderind;
                PluginConfig.speed = Speed[0].stringsliderind;
                PluginConfig.speedlg = Speed[1].stringsliderind;
                PluginConfig.speedrg = Speed[2].stringsliderind;
                PluginConfig.platforms = Movement[4].AssociatedBool;
                PluginConfig.upsidedownmonkey = Movement[5].AssociatedBool;
                PluginConfig.wateryair = Movement[6].AssociatedBool;
                PluginConfig.longarms = Movement[7].AssociatedBool;
                PluginConfig.SpinBot = Movement[8].AssociatedBool;

                //Visual
                PluginConfig.chams = Visual[0].AssociatedBool;
                PluginConfig.boxesp = Visual[1].AssociatedBool;
                PluginConfig.hollowboxesp = Visual[2].AssociatedBool;
                PluginConfig.whyiseveryonelookingatme = Visual[4].AssociatedBool;
                PluginConfig.noexpressions = Visual[5].AssociatedBool;

                //Player
                PluginConfig.nofinger = Player[0].AssociatedBool;
                PluginConfig.taggun = Player[1].AssociatedBool;
                PluginConfig.legmod = Player[2].AssociatedBool;
                PluginConfig.creepermonkey = Player[3].AssociatedBool;
                PluginConfig.ghostmonkey = Player[4].AssociatedBool;
                PluginConfig.invismonkey = Player[5].AssociatedBool;
                PluginConfig.tagaura = Player[6].stringsliderind;
                PluginConfig.desync = Player[7].AssociatedBool;
                PluginConfig.tagall = Player[8].AssociatedBool;

                //Modders
                PluginConfig.breaknametags = Modders[0].AssociatedBool;
                PluginConfig.breakmodcheckers = Modders[1].AssociatedBool;
                PluginConfig.pccheckbypass = Modders[3].AssociatedBool;

                //Settings
                PluginConfig.MenuColour = Settings[0].stringsliderind;
                PluginConfig.MenuPos = Settings[1].stringsliderind;

                string ToDraw = Plugin.sussy ? $"<color={MenuColour}>SUSSY : {MenuState}</color>\n" : $"<color={MenuColour}>COLOSSAL: LEGACY : {MenuState}</color>\n";
                int i = 0;
                if (CurrentViewingMenu != null)
                {
                    foreach (MenuOption opt in CurrentViewingMenu)
                    {
                        if (SelectedOptionIndex == i)
                        {
                            ToDraw = ToDraw + "> ";
                        }
                        ToDraw = ToDraw + opt.DisplayName;

                        if (opt._type == "toggle")
                        {
                            if (opt.AssociatedBool == true)
                            {
                                ToDraw = ToDraw + $" <color={MenuColour}>[ON]</color>";
                            }
                            else
                            {
                                ToDraw = ToDraw + " <color=red>[OFF]</color>";
                            }
                        }
                        if (opt._type == "STRINGslider")
                        {
                            ToDraw = ToDraw + ": " + opt.StringArray[opt.stringsliderind] + " [" + (opt.stringsliderind + 1).ToString() + "/" + opt.StringArray.Length.ToString() + "]";
                        }
                        ToDraw = ToDraw + "\n";
                        i++;
                    }
                    Testtext.text = ToDraw;
                }
                else
                {
                    Debug.Log("Null for some reason");
                }


                if (PluginConfig.MenuRGB)
                {
                    menurgb += Time.deltaTime;
                }
                else
                {
                    if (menurgb != 0)
                    {
                        menurgb = 0;
                    }
                }
                if (PluginConfig.MenuRGB)
                {
                    if (menurgb >= 0.1f)
                    {
                        MenuColour = "magenta";
                    }
                    if (menurgb >= 0.2f)
                    {
                        MenuColour = "red";
                    }
                    if (menurgb >= 0.3f)
                    {
                        MenuColour = "green";
                    }
                    if (menurgb >= 0.4f)
                    {
                        MenuColour = "blue";
                    }
                    if (menurgb >= 0.5f)
                    {
                        MenuColour = "cyan";
                    }
                    if (menurgb >= 0.6f)
                    {
                        MenuColour = "yellow";
                    }
                    if (menurgb >= 0.6f)
                    {
                        menurgb = 0;
                    }
                }
            }
        }
        static void UpdateMenuState(MenuOption option, string _MenuState, string OperationType) {
            try {
                if (OperationType == "optionhit") {
                    if (option._type == "submenu") {
                        if (option.AssociatedString == "Movement") {
                            CurrentViewingMenu = Movement;
                            Debug.Log("<color=magenta>Movement...</color>");
                        }
                        if (option.AssociatedString == "Visual") {
                            CurrentViewingMenu = Visual;
                            Debug.Log("<color=magenta>Visual...</color>");
                        }
                        if (option.AssociatedString == "Player") {
                            CurrentViewingMenu = Player;
                            Debug.Log("<color=magenta>Player...</color>");
                        }
                        if (option.AssociatedString == "Computer") {
                            CurrentViewingMenu = Computer;
                            Debug.Log("<color=magenta>Computer...</color>");
                        }
                        if (option.AssociatedString == "Modders") {
                            CurrentViewingMenu = Modders;
                            Debug.Log("<color=magenta>Modders...</color>");
                        }
                        if (option.AssociatedString == "Account") {
                            CurrentViewingMenu = Account;
                            Debug.Log("<color=magenta>Account...</color>");
                        }
                        if (option.AssociatedString == "Settings") {
                            CurrentViewingMenu = Settings;
                            Debug.Log("<color=magenta>Settings...</color>");
                        }
                        if (option.AssociatedString == "DriftMode") {
                            CurrentViewingMenu = Account;
                            Debug.Log("<color=magenta>Account...</color>");
                        }
                        if (option.AssociatedString == "Back") {
                            CurrentViewingMenu = MainMenu;
                            Debug.Log("<color=magenta>Back...</color>");
                        }

                        if (option.AssociatedString == "Speed") {
                            CurrentViewingMenu = Speed;
                            Debug.Log("<color=magenta>Speed...</color>");
                        }
                        if (option.AssociatedString == "TagAura") {
                            CurrentViewingMenu = TagAura;
                            Debug.Log("<color=magenta>TagAura...</color>");
                        }
                        if (option.AssociatedString == "Sky") {
                            CurrentViewingMenu = Sky;
                        }
                        MenuState = option.AssociatedString;
                        SelectedOptionIndex = 0;
                    }
                    if (option._type == "toggle") {
                        if (option.AssociatedBool == false) {
                            option.AssociatedBool = true;
                            CustomConsole.LogToConsole($"\nToggled {option.DisplayName} : {option.AssociatedBool}");
                            Notifacations.SendNotification($"<color={MenuColour}>[TOGGLED]</color> {option.DisplayName} : {option.AssociatedBool}");
                        } else {
                            option.AssociatedBool = false;
                            CustomConsole.LogToConsole($"\nToggled {option.DisplayName} : {option.AssociatedBool}");
                            Notifacations.SendNotification($"<color={MenuColour}>[TOGGLED]</color> {option.DisplayName} : {option.AssociatedBool}");
                        }
                    }
                    if (option._type == "button")
                    {
                        //Computer
                        if (option.AssociatedString == "disconnect")
                        {
                            PhotonNetwork.Disconnect();
                        }
                        if (option.AssociatedString == "randomidentity")
                        {
                            string[] names =
                            {
                                "COLOSSUS",
                                "123",
                                "PP",
                                "PBBV",
                                "SKILLISSUE",
                                "IMAGINE",
                                "SREN17",
                                "YOURMOM",
                                "GUMMIES",
                                "WATCH",
                                "MOUSE",
                                "BOZO",
                                "KEYS",
                                "PINE",
                                "LEMMING",
                                "ELECTRONIC",
                                "BODA",
                                "TTTPIG",
                                "TTTPIGFAN",
                                "555999",
                                "83459230",
                                "923059439",
                                "IJ48FNSF",
                                "MF4J8T9J",
                                "J3VU",
                                "3993NF39",
                                "FEMBOY",
                                "RAWR",
                                "MEOW",
                            };
                            System.Random rand = new System.Random();
                            int index = rand.Next(names.Length);
                            PhotonNetwork.LocalPlayer.NickName = names[index];
                            GorillaComputer.instance.currentName = names[index];
                            GorillaComputer.instance.savedName = names[index];
                            PlayerPrefs.SetString("GorillaLocomotion.GTPlayerName", names[index]);
                        }
                        if (option.AssociatedString == "join GTC")
                        {
                            PhotonNetworkController.Instance.AttemptToJoinSpecificRoom("GTC", JoinType.Solo);
                        }
                        if (option.AssociatedString == "join TTT")
                        {
                            PhotonNetworkController.Instance.AttemptToJoinSpecificRoom("TTT", JoinType.Solo);
                        }
                        if (option.AssociatedString == "join YTTV")
                        {
                            PhotonNetworkController.Instance.AttemptToJoinSpecificRoom("YTTV", JoinType.Solo);
                        }
                        if (option.AssociatedString == "moddedcasual")
                        {
                            GorillaComputer.instance.currentGameMode.Value = "MODDED_MODDED_CASUALCASUAL";
                        }
                        if (option.AssociatedString == "moddedinfection")
                        {
                            GorillaComputer.instance.currentGameMode.Value = "MODDED_MODDED_DEFAULTINFECTION";
                        }

                        //Account
                        if (option.AssociatedString == "disconnectplayfab")
                        {
                            PhotonNetwork.Disconnect();
                            PlayFabClientAPI.ForgetAllCredentials();
                        }
                        if (option.AssociatedString == "serverusw")
                        {
                            PhotonNetwork.Disconnect();
                            PhotonNetwork.ConnectToRegion("USW");
                        }
                        if (option.AssociatedString == "serverus")
                        {
                            PhotonNetwork.Disconnect();
                            PhotonNetwork.ConnectToRegion("US");
                        }
                        if (option.AssociatedString == "servereu")
                        {
                            PhotonNetwork.Disconnect();
                            PhotonNetwork.ConnectToRegion("EU");
                        }

                        //Sky
                        if (option.AssociatedString == "monkeycoloursky")
                        {
                            GameObject gameObject = GameObject.Find("Environment Objects/LocalObjects_Prefab/Standard Sky/newsky (1)");
                            gameObject.GetComponent<MeshRenderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                            gameObject.GetComponent<MeshRenderer>().material.color = new Color(GorillaTagger.Instance.offlineVRRig.GetComponent<SkinnedMeshRenderer>().material.color.r, GorillaTagger.Instance.offlineVRRig.GetComponent<SkinnedMeshRenderer>().material.color.g, GorillaTagger.Instance.offlineVRRig.GetComponent<SkinnedMeshRenderer>().material.color.b);
                        }
                        if (option.AssociatedString == "purplesky")
                        {
                            GameObject gameObject = GameObject.Find("Environment Objects/LocalObjects_Prefab/Standard Sky/newsky (1)");
                            gameObject.GetComponent<MeshRenderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                            gameObject.GetComponent<MeshRenderer>().material.color = Color.magenta;
                        }
                        if (option.AssociatedString == "redsky")
                        {
                            GameObject gameObject = GameObject.Find("Environment Objects/LocalObjects_Prefab/Standard Sky/newsky (1)");
                            gameObject.GetComponent<MeshRenderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                            gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                        }
                        if (option.AssociatedString == "cyansky")
                        {
                            GameObject gameObject = GameObject.Find("Environment Objects/LocalObjects_Prefab/Standard Sky/newsky (1)");
                            gameObject.GetComponent<MeshRenderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                            gameObject.GetComponent<MeshRenderer>().material.color = Color.cyan;
                        }
                        if (option.AssociatedString == "greensky")
                        {
                            GameObject gameObject = GameObject.Find("Environment Objects/LocalObjects_Prefab/Standard Sky/newsky (1)");
                            gameObject.GetComponent<MeshRenderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                            gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
                        }


                        if (option.AssociatedString == "loadconfig")
                            Configs.LoadConfig($"{Configs.folderPath}\\{Settings[2].StringArray[Settings[2].stringsliderind]}.json");
                        if (option.AssociatedString == "saveconfig")
                            Configs.SaveConfig();

                        if (option.AssociatedString == "nosnitch")
                        {
                            if (PhotonNetwork.InRoom)
                            {
                                if (!GorillaTagger.Instance.offlineVRRig.gameObject.GetPhotonView().Controller.CustomProperties.ContainsValue("You know mod checkers are a illegal mod right :p"))
                                {
                                    Hashtable hash = new Hashtable();
                                    hash.Add("mods", "You know mod checkers are a illegal mod right :p");
                                    GorillaTagger.Instance.offlineVRRig.gameObject.GetPhotonView().Controller.SetCustomProperties(hash);
                                }
                            }
                        }
                    }

                    switch (Settings[0].stringsliderind)
                    {
                        case 0:
                            if (PluginConfig.MenuRGB)
                                PluginConfig.MenuRGB = false;
                            MenuColour = "magenta";
                            break;
                        case 1:
                            if (PluginConfig.MenuRGB)
                                PluginConfig.MenuRGB = false;
                            MenuColour = "red";
                            break;
                        case 2:
                            if (PluginConfig.MenuRGB)
                                PluginConfig.MenuRGB = false;
                            MenuColour = "yellow";
                            break;
                        case 3:
                            if (PluginConfig.MenuRGB)
                                PluginConfig.MenuRGB = false;
                            MenuColour = "green";
                            break;
                        case 4:
                            if (PluginConfig.MenuRGB)
                                PluginConfig.MenuRGB = false;
                            MenuColour = "blue";
                            break;
                        case 5:
                            PluginConfig.MenuRGB = true;
                            break;
                    }
                    switch (Settings[1].stringsliderind)
                    {
                        case 0:
                            Testtext.rectTransform.localPosition = new Vector3(-2.4f, 1f, 2f);
                            break;
                        case 1:
                            Testtext.rectTransform.localPosition = new Vector3(-0.8f, 0f, 1f);
                            break;
                    }
                }
            } catch {
            }
        }
    }
}