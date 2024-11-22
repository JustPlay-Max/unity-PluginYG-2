using System;
using UnityEditor;
using UnityEngine;

namespace YG.Insides
{
    [Serializable]
    public partial class ProjectSettings
    {
#if UNITY_EDITOR
        public bool selectWebGLTemplate = true;
        public bool runInBackground = false;
        public WebGLExceptionSupport enableExceptions = WebGLExceptionSupport.FullWithoutStacktrace;
        public WebGLCompressionFormat compressionFormat = WebGLCompressionFormat.Brotli;
        public bool decompressionFallback;
        public bool autoGraphicsAPI = true;
        public bool dataCaching = true;
        public ColorSpace colorSpace = ColorSpace.Gamma;
        public bool archivingBuild = true;
        public bool syncInitSDK;

        public void ApplySettings()
        {
            PlatformToggles toggles = YG2.infoYG.platformToggles;

            if (toggles.runInBackground)
                PlayerSettings.runInBackground = runInBackground;

            if (toggles.enableExceptions)
                PlayerSettings.WebGL.exceptionSupport = enableExceptions;

            if (toggles.compressionFormat)
                PlayerSettings.WebGL.compressionFormat = compressionFormat;

            if (toggles.decompressionFallback)
                PlayerSettings.WebGL.decompressionFallback = decompressionFallback;

            if (toggles.autoGraphicsAPI)
            {
                bool currentAutoGraphicsAPIState = PlayerSettings.GetUseDefaultGraphicsAPIs(EditorUserBuildSettings.activeBuildTarget);
                if (currentAutoGraphicsAPIState != autoGraphicsAPI)
                    PlayerSettings.SetUseDefaultGraphicsAPIs(EditorUserBuildSettings.activeBuildTarget, autoGraphicsAPI);
            }

            if (toggles.dataCaching)
                PlayerSettings.WebGL.dataCaching = dataCaching;

            if (toggles.colorSpace)
                PlayerSettings.colorSpace = colorSpace;

            if (toggles.archivingBuild)
                YG2.infoYG.Basic.archivingBuild = archivingBuild;

            if (toggles.syncInitSDK)
                YG2.infoYG.Basic.syncInitSDK = syncInitSDK;

            CallAction.CallIByAttribute(typeof(ApplySettingsAttribute), GetType(), this);
            AssetDatabase.SaveAssets();
        }
#endif
    }
}