using Colossal;
using Colossal.Menu.ClientHub;
using HarmonyLib;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ColossalCheatMenuV2.Menu
{
    internal class Boards : MonoBehaviour
    {
        public static GorillaLevelScreen[] joinScreens;
        public static Material boardmat;
        public static string joinscreentext;
        public static string coctext;

        private static string ogcocktext;
        private static string ogcodeofcocktext;

        private float rainbowtext = 0f;
        private static string textcolour = "#ff00ff";
        public void Start()
        {
            boardmat = new Material(Shader.Find("GorillaTag/UberShader"));
            boardmat.color = new Color(0.6f, 0, 0.80f);

            GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/CodeOfConductHeadingText").GetComponent<TextMeshPro>().richText = true;
            GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/COCBodyText_TitleData").GetComponent<TextMeshPro>().richText = true;

            ogcodeofcocktext = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/CodeOfConductHeadingText").GetComponent<TextMeshPro>().text;
            ogcocktext = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/COCBodyText_TitleData").GetComponent<TextMeshPro>().text;
        }
        public void Update()
        {
            coctext = $"<color=#00ffff>Thank you for using CCMV2 LEGACY, the successor to the</color><color={textcolour}> first cheat menu!</color><color=#00ffff> CCMV2 Legacy WONT be getting frequently updated with new features/FUD. \n\nContributors:\n</color><color={textcolour}>ColossusYTTV: Menu Maker/Mod Creator\nLars/LHAX: Menu Base</color><color=#00ffff>\nWM: No Fingers\nAntic/ChatGPT: Tester\nCunzaki/Plinko: Tester\novaissilly: SourceCode\nCurrent Menu Version: {Plugin.version}</color>";

            if (GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/") != null)
            {
                if (PluginConfig.csghostclient)
                {
                    rainbowtext += Time.deltaTime;
                    if (rainbowtext >= 0.1f)
                    {
                        textcolour = "#ff00ff";
                        GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/CodeOfConductHeadingText").GetComponent<TextMeshPro>().text = $"<color={textcolour}>COLOSSAL CHEAT MENU V2</color>";
                        GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/COCBodyText_TitleData").GetComponent<TextMeshPro>().text = coctext;
                    }
                    if (rainbowtext >= 0.2f)
                    {
                        textcolour = "red";
                        GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/CodeOfConductHeadingText").GetComponent<TextMeshPro>().text = $"<color={textcolour}>COLOSSAL CHEAT MENU V2</color>";
                        GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/COCBodyText_TitleData").GetComponent<TextMeshPro>().text = coctext;
                    }
                    if (rainbowtext >= 0.3f)
                    {
                        textcolour = "green";
                        GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/CodeOfConductHeadingText").GetComponent<TextMeshPro>().text = $"<color={textcolour}>COLOSSAL CHEAT MENU V2</color>";
                        GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/COCBodyText_TitleData").GetComponent<TextMeshPro>().text = coctext;
                    }
                    if (rainbowtext >= 0.4f)
                    {
                        textcolour = "blue";
                        GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/CodeOfConductHeadingText").GetComponent<TextMeshPro>().text = $"<color={textcolour}>COLOSSAL CHEAT MENU V2</color>";
                        GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/COCBodyText_TitleData").GetComponent<TextMeshPro>().text = coctext;
                    }
                    if (rainbowtext >= 0.5f)
                    {
                        textcolour = "#00ffff";
                        GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/CodeOfConductHeadingText").GetComponent<TextMeshPro>().text = $"<color={textcolour}>COLOSSAL CHEAT MENU V2</color>";
                        GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/COCBodyText_TitleData").GetComponent<TextMeshPro>().text = coctext;
                    }
                    if (rainbowtext >= 0.6f)
                    {
                        textcolour = "yellow";
                        GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/CodeOfConductHeadingText").GetComponent<TextMeshPro>().text = $"<color={textcolour}>COLOSSAL CHEAT MENU V2</color>";
                        GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/COCBodyText_TitleData").GetComponent<TextMeshPro>().text = coctext;
                    }
                    if (rainbowtext >= 0.6f)
                    {
                        rainbowtext = 0;
                    }
                }
                else
                {
                    GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/CodeOfConductHeadingText").GetComponent<TextMeshPro>().text = ogcodeofcocktext;
                    GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/COCBodyText_TitleData").GetComponent<TextMeshPro>().text = ogcocktext;
                }
            }
        }
    }
}