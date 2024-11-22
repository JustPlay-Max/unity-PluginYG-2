using System.Globalization;
using UnityEditor;
using UnityEngine;
using YG.Insides;

namespace YG.EditorScr
{
    [InitializeOnLoad]
    public class WelcomeWindow : EditorWindow
    {
        static WelcomeWindow() => InitializeOnLoad();
        private static void InitializeOnLoad()
        {
            EditorApplication.delayCall += () =>
            {
                if (PlayerPrefs.GetInt(InfoYG.FIRST_STARTUP_KEY, 0) == 1)
                {
                    PlayerPrefs.SetInt(InfoYG.FIRST_STARTUP_KEY, 2);
                    PlayerPrefs.Save();

                    ShowWindow();
                }
            };
        }

        //[MenuItem("YG2/Welcome")]
        public static void ShowWindow()
        {
            WelcomeWindow window = GetWindow<WelcomeWindow>($"Welcome to {InfoYG.NAME_PLUGIN}!");
            window.position = new Rect(250, 150, 700, 540);
            window.minSize = new Vector2(700, 540);
        }

        private void OnGUI()
        {
            GUIStyle styleWelcome = TextStyles.LabelStyleColor(Color.white);
            styleWelcome.fontSize = 35;
            styleWelcome.alignment = TextAnchor.MiddleCenter;
            styleWelcome.fontStyle = FontStyle.Bold;
#if RU_YG2
            GUILayout.Label("Добро пожаловать!", styleWelcome);
#else
            GUILayout.Label("Welcome!", styleWelcome);
#endif
            styleWelcome = TextStyles.Orange();
            styleWelcome.fontSize = 70;
            styleWelcome.alignment = TextAnchor.MiddleCenter;
            styleWelcome.fontStyle = FontStyle.Bold;

            GUILayout.Label(InfoYG.NAME_PLUGIN, styleWelcome);

            styleWelcome = TextStyles.LabelStyleColor(Color.gray);
            styleWelcome.fontSize = 30;
            styleWelcome.alignment = TextAnchor.MiddleCenter;

            GUILayout.Label(Langs.fullNamePlugin.ToLower(), styleWelcome);
            GUILayout.Space(50);

            styleWelcome.fontSize = 16;
            styleWelcome.alignment = TextAnchor.MiddleLeft;

            GUIStyle buttonStyle = new GUIStyle(YGEditorStyles.button);
            buttonStyle.fontSize = 15;

            GUILayout.BeginHorizontal();
            GUILayout.Space(50);

            if (GUILayout.Button(Langs.switchLang, buttonStyle, GUILayout.Height(27), GUILayout.Width(130)))
            {
#if RU_YG2
                DefineSymbols.RemoveDefine("RU_YG2");
#else
                DefineSymbols.AddDefine("RU_YG2");
#endif
            }
#if RU_YG2
            GUILayout.Label("поддерживается Английский и Русский языки", styleWelcome);
#else
            GUILayout.Label("English and Russian languages are supported", styleWelcome);
#endif
            GUILayout.EndHorizontal();
            GUILayout.Space(20);

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            buttonStyle.fontSize = 20;
#if RU_YG2
            string settingsButtonText = "Настройки плагина";
#else
            string settingsButtonText = "Plugin settings";
#endif
            GUILayout.BeginVertical();
            if (GUILayout.Button(settingsButtonText, buttonStyle, GUILayout.Height(40), GUILayout.Width(525)))
                InfoYGEditorWindow.ShowWindow();

            if (GUILayout.Button(Langs.versionControl, buttonStyle, GUILayout.Height(40), GUILayout.Width(525)))
                VersionControlWindow.ShowWindow();
            GUILayout.EndVertical();

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            styleWelcome.alignment = TextAnchor.MiddleCenter;
#if RU_YG2
            GUILayout.Label("Контролируйте версии плагина и его модулей не выходя из Unity", styleWelcome);
#else
            GUILayout.Label("Control the versions of the plugin and its modules without leaving Unity", styleWelcome);
#endif
            if (Server.loadComplete)
            {
                GUILayout.Space(30);
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();

                buttonStyle.fontSize = 20;
                if (GUILayout.Button(Langs.documentation, buttonStyle, GUILayout.Height(35), GUILayout.Width(175)))
                    DocumentationEditor.DocMenuItem();
                if (GUILayout.Button(Langs.helpChat, buttonStyle, GUILayout.Height(35), GUILayout.Width(175)))
                    DocumentationEditor.ChatMenuItem();
                if (GUILayout.Button(Langs.video, buttonStyle, GUILayout.Height(35), GUILayout.Width(175)))
                    DocumentationEditor.VideoMenuItem();

                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
#if RU_YG2
                GUILayout.Label("Вся информация в документации и нашем уютном чате, заходите :)", styleWelcome);
#else
                GUILayout.Label("All information is in the documentation and our cozy chat, come in :)", styleWelcome);
#endif
                bool newerVersion = false;
                float thisVersion;
                string cloudVersionStr = "";

                float.TryParse(InfoYG.VERSION_YG2, NumberStyles.Float, CultureInfo.InvariantCulture, out thisVersion);
                if (thisVersion == 0)
                    return;

                for (int i = 0; i < ServerInfo.saveInfo.modules.Length; i++)
                {
                    if (ServerInfo.saveInfo.modules[i].name == InfoYG.NAME_PLUGIN)
                    {
                        cloudVersionStr = ServerInfo.saveInfo.modules[i].version;
                        float.TryParse(cloudVersionStr, NumberStyles.Float, CultureInfo.InvariantCulture, out float cloudVersion);

                        if (cloudVersion == 0)
                            return;

                        if (cloudVersion > thisVersion)
                            newerVersion = true;

                        if (ServerInfo.saveInfo.modules[i].critical == true)
                            cloudVersionStr += " critical";

                        break;
                    }
                }

                if (!newerVersion)
                {
                    GUILayout.Space(40);
                    styleWelcome = TextStyles.Green();
                    styleWelcome.fontSize = 16;
                    styleWelcome.alignment = TextAnchor.MiddleRight;
#if RU_YG2
                    GUILayout.Label($"У Вас самая свежая версия {InfoYG.NAME_PLUGIN}!  v{InfoYG.VERSION_YG2}", styleWelcome);
#else
                    GUILayout.Label($"You have the latest version of {InfoYG.NAME_PLUGIN}!  v{InfoYG.VERSION_YG2}", styleWelcome);
#endif
                }
                else
                {
                    GUILayout.Space(40);
                    styleWelcome = TextStyles.Red();
                    styleWelcome.fontSize = 16;
                    styleWelcome.alignment = TextAnchor.MiddleRight;
#if RU_YG2
                    GUILayout.Label($"Есть более свежая версия {InfoYG.NAME_PLUGIN}!  v{cloudVersionStr}", styleWelcome);
#else
                    GUILayout.Label($"There is a more recent version of {InfoYG.NAME_PLUGIN}!  v{cloudVersionStr}", styleWelcome);
#endif
                }
            }

            Repaint();
        }
    }
}