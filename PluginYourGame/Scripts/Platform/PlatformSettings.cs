using UnityEditor;
using UnityEngine;

namespace YG.Insides
{
    [CreateAssetMenu(fileName = "NewPlatformYG", menuName = "YG2/New Platform")]
    public partial class PlatformSettings : ScriptableObject
    {
        public string nameDefining = "NewPlatform";

#if UNITY_EDITOR
        public bool applySettingsBySwitchPlatform = true;
        public ProjectSettings projectSettings = new ProjectSettings();

        public void ApplyProjectSettings()
        {
            projectSettings.ApplySettings();

            CallAction.CallIByAttribute(typeof(ApplySettingsAttribute), typeof(AddOptions), YG2.infoYG.addOptions);

            if (YG2.infoYG.platformToggles.selectWebGLTemplate && projectSettings.selectWebGLTemplate)
            {
                string templateName = nameDefining.Replace("Platform", string.Empty);
                string templatePath = $"Assets/WebGLTemplates/{templateName}";

                if (AssetDatabase.IsValidFolder(templatePath))
                    PlayerSettings.WebGL.template = "PROJECT:" + templateName;
            }
        }

        public static void SelectPlatform()
        {
            CallAction.CallIByAttribute(typeof(SelectPlatformAttribute), typeof(AddOptions), YG2.infoYG.addOptions);
        }
        public static void DeletePlatform()
        {
            CallAction.CallIByAttribute(typeof(DeletePlatformAttribute), typeof(AddOptions), YG2.infoYG.addOptions);
        }
#endif
    }
}