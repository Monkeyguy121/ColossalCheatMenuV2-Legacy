using Colossal.Menu.ClientHub;
using Colossal.Menu.ClientHub.Notifacation;
using Colossal.Mods;
using Colossal.Patches;
using ColossalCheatMenuV2.Menu;
using ColossalOnevent;
using BepInEx;
using GorillaNetworking;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace Colossal {
    [BepInPlugin("org.ColossusYTTV.ColossalCheatMenuV2", "ColossalCheatMenuV2", "1.0.0")]
    public class Plugin : MonoBehaviour {
        //public static int called = 0;
        //public static float instantate = 0;

        public static float reporttimer = 0;

        static AssetBundle assetBundle;
        private static void LoadAssetBundle()
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"ColossalCheatMenuV2.Font.utopium");
            if (stream != null)
                assetBundle = AssetBundle.LoadFromStream(stream);
            else
                Debug.LogError("Failed to load assetbundle");
        }

        public static T LoadAsset<T>(string assetName) where T : UnityEngine.Object
        {
            if (assetBundle == null)
                LoadAssetBundle();

            T gameObject = assetBundle.LoadAsset(assetName) as T;
            return gameObject;
        }

        public static GameObject hud;
        public static GameObject holder;

        public static bool oculus = false;

        public static float version = 1.0f;
        public static bool sussy = false;
        public static Font gtagfont;

        public void Start()
        {
            GameObject go = LoadAsset<GameObject>("utopium");
            Text t = go.GetComponent<Text>();
            Debug.Log("[COLOSSAL] LOADED " + t.name);
            gtagfont = t.font;

            Debug.Log("[COLOSSAL] Plugin Start Call");

            Debug.Log("[COLOSSAL] Spawned Holder");
            holder = new GameObject();
            holder.name = "Mod Handler";
            holder.AddComponent<Boards>();
            holder.AddComponent<EventNotifacation>();
            holder.AddComponent<JoinNotifacation>();
            holder.AddComponent<LeaveNotifacation>();
            holder.AddComponent<MasterChangeNotifacation>();
            holder.AddComponent<Configs>();

            string[] oculusDlls = Directory.GetFiles(Environment.CurrentDirectory, "OculusXRPlugin.dll", SearchOption.AllDirectories);
            if (oculusDlls.Length > 0)
                oculus = true;

            Menu.Menu.LoadOnce();
            CustomConsole.LogToConsole("[COLOSSAL] Loaded menu start functions");

            CustomConsole.LogToConsole("[COLOSSAL] Getting configs");
            Configs.GetConfigFileNames();

            hud = GameObject.Find("CLIENT_HUB");

            if (PhotonNetworkController.Instance.disableAFKKick == false)
            {
                PhotonNetworkController.Instance.disableAFKKick = true;
            }

            //quit box disable
            if (GameObject.Find("Environment Objects/TriggerZones_Prefab/ZoneTransitions_Prefab/QuitBox").activeSelf)
            {
                GameObject.Find("Environment Objects/TriggerZones_Prefab/ZoneTransitions_Prefab/QuitBox").SetActive(false);
            }
        }
        public void Update() {
            Menu.Menu.Load();
            Dictionary<Type, bool> componentConditions = new Dictionary<Type, bool>
            {
                { typeof(CustomConsole), true },
                { typeof(ThisGuyIsUsingColossal), true },
                { typeof(LongArm), PluginConfig.longarms },
                { typeof(WhyIsEveryoneLookingAtMe), PluginConfig.whyiseveryonelookingatme },
                { typeof(ExcelFly), PluginConfig.excelfly },
                { typeof(NoclipShittyDisable), PluginConfig.noclip },
                { typeof(WateryAir), PluginConfig.wateryair },
                { typeof(FreezeMonkey), PluginConfig.freezemonkey },
                { typeof(Platforms), PluginConfig.platforms },
                { typeof(TFly), PluginConfig.tfly },
                { typeof(QuickDisc), PluginConfig.QuickDisc },
                { typeof(UpsideDownMonkey), PluginConfig.upsidedownmonkey },
                { typeof(FirstPerson), PluginConfig.FirstPerson },
                { typeof(Chams), PluginConfig.chams },
                { typeof(HollowBoxEsp), PluginConfig.hollowboxesp },
                { typeof(MuteAll), PluginConfig.MuteAll },
                { typeof(BoxEsp), PluginConfig.boxesp },
                { typeof(CreeperMonkey), PluginConfig.creepermonkey },
                { typeof(GhostMonkey), PluginConfig.ghostmonkey },
                { typeof(InvisMonkey), PluginConfig.invismonkey },
                { typeof(LegMod), PluginConfig.legmod },
                { typeof(TagGun), PluginConfig.taggun },
                { typeof(TagAll), PluginConfig.tagall },
                { typeof(BreakModChecker), PluginConfig.breakmodcheckers },
                { typeof(BreakNameTags), PluginConfig.breaknametags },
                { typeof(SpinBot), PluginConfig.SpinBot },
                { typeof(Desync), PluginConfig.desync }
            };
            foreach (var kvp in componentConditions)
            {
                if(holder != null)
                {
                    if (kvp.Value && holder.GetComponent(kvp.Key) == null)
                    {
                        holder.AddComponent(kvp.Key);
                    }
                } else
                {
                    Debug.Log("Holder is null");
                }
            }
        }
        /*public void FixedUpdate() {
            if (PhotonNetwork.InRoom) {
                instantate += Time.deltaTime;
            } else {
                instantate = 0;
                called = 0;
            }

            if (instantate >= 120) {
                called = 0;
            }
        }*/
    }
}