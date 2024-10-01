using System;
using UnityEditor;
using UnityEngine;
using YG.Insides;

namespace YG
{
    //[CreateAssetMenu(fileName = "SettingsYG2", menuName = "YG2/Create SettingsYG2")]
    public partial class InfoYG : ScriptableObject
    {
        public Basic basicSettings = new Basic();

        [Serializable]
        public class Basic
        {
#if UNITY_EDITOR
            [HeaderYG(Langs.platform)]
#endif
            public PlatformSettings platform;
#if UNITY_EDITOR
            [Tooltip(Langs.applySettingsBySwitchPlatform)]
#endif
            public bool autoApplySettings = true;

#if UNITY_EDITOR
            [HeaderYG(Langs.other)]
            [Tooltip(Langs.t_autoPauseGame)]
#endif
            public bool autoPauseGame = true;
#if UNITY_EDITOR
            [Tooltip(Langs.t_autoGRA)]
#endif
            public bool autoGRA = true;

#if UNITY_EDITOR
            [Tooltip(Langs.t_debugInEditor)]
#endif
            public bool logInEditor = true;
#if UNITY_EDITOR
            [Tooltip(Langs.t_archivingBuild)]
#endif
            public bool archivingBuild = true;
#if UNITY_EDITOR
            [Tooltip(Langs.t_syncInitSDK)]
#endif
            public bool syncInitSDK;
#if UNITY_EDITOR
            [NestedYG(nameof(syncInitSDK)), Tooltip(Langs.t_loadSceneIfSDKLate)]
#endif
            public bool loadSceneIfSDKLate;
#if UNITY_EDITOR
            [NestedYG(nameof(loadSceneIfSDKLate), nameof(syncInitSDK)), Min(0)]
#endif
            public int loadSceneIndex;

#if YandexGamePlatform
#if UNITY_EDITOR
            [HeaderYG("YandexGame")]
            [Tooltip(Langs.t_pixelRatio)]
            public bool pixelRatioEnable;
            [NestedYG(nameof(pixelRatioEnable)), Min(0)]
            public float pixelRatioValue = 1.3f;

            public enum LogoImgFormat
            {
                [InspectorName("No Logo")] no,
                [InspectorName("PNG")] png,
                [InspectorName("JPG")] jpg,
                [InspectorName("GIF")] gif
            }
            [Tooltip(Langs.logoImageFormat)]
            public LogoImgFormat logoImageFormat;

            public enum BackgroundImageFormat
            {
                [InspectorName("Player Settings")] unity,
                [InspectorName("No Background")] no,
                [InspectorName("PNG")] png,
                [InspectorName("JPG")] jpg,
                [InspectorName("GIF")] gif
            }
            [Tooltip(Langs.backgroundImgFormat)]
            public BackgroundImageFormat backgroundImgFormat;
#endif
#endif
        }

#if UNITY_EDITOR
        [Tooltip(Langs.t_simulationInEditor)]
        public SimulationInEditor simulationInEditor = new SimulationInEditor();

        public static void SetDefaultPlatform()
        {
            string standartPlatformSettingsPath = $"{PATCH_ASSETS_PLATFORMS}/YandexGame/YandexGame.asset";
            PlatformSettings standartPlatformSettings = AssetDatabase.LoadAssetAtPath<PlatformSettings>(standartPlatformSettingsPath);

            if (standartPlatformSettings != null)
            {
                instance.basicSettings.platform = standartPlatformSettings;

                if (YG2.infoYG.basicSettings.autoApplySettings)
                    instance.basicSettings.platform.ApplyProjectSettings();

                EditorUtility.SetDirty(instance);
                AssetDatabase.SaveAssets();
            }
        }
#endif
    }
}
