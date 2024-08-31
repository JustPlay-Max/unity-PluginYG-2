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

        public AddPlatformSettings addPlatformSettings;

        public void ApplyProjectSettings()
        {
            projectSettings.ApplySettings();

            if (addPlatformSettings)
                addPlatformSettings.ApplyProjectSettings();

            if (projectSettings.toggle_selectWebGLTemplate && projectSettings.selectWebGLTemplate)
            {
                string templateName = nameDefining.Replace("Platform", string.Empty);
                string templatePath = $"Assets/WebGLTemplates/{templateName}";

                if (AssetDatabase.IsValidFolder(templatePath))
                    PlayerSettings.WebGL.template = "PROJECT:" + templateName;
            }
        }

        public void SelectPlatform()
        {
            if (addPlatformSettings)
                addPlatformSettings.SelectPlatform();
        }
        public void DeletePlatform()
        {
            if (addPlatformSettings)
                addPlatformSettings.SelectPlatform();
        }
#endif
    }
}