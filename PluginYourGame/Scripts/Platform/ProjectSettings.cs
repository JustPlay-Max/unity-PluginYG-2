#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace YG.Insides
{
    [Serializable]
    public partial class ProjectSettings
    {
        public bool archivingBuild = true;
        public bool toggle_archivingBuild;

        public bool selectWebGLTemplate = true;
        public bool toggle_selectWebGLTemplate;

        public bool runInBackground = false;
        public bool toggle_runInBackground;

        public WebGLExceptionSupport enableExceptions = WebGLExceptionSupport.FullWithoutStacktrace;
        public bool toggle_enableExceptions;

        public WebGLCompressionFormat compressionFormat = WebGLCompressionFormat.Brotli;
        public bool toggle_compressionFormat;

        public bool dataCaching = true;
        public bool toggle_dataCaching;

        public bool decompressionFallback;
        public bool toggle_decompressionFallback;

        public ColorSpace colorSpace = ColorSpace.Gamma;
        public bool toggle_colorSpace;

        public bool autoGraphicsAPI = true;
        public bool toggle_autoGraphicsAPI;

        public void ApplySettings()
        {
            if (toggle_archivingBuild)
                YG2.infoYG.basicSettings.archivingBuild = archivingBuild;

            if (toggle_runInBackground)
                PlayerSettings.runInBackground = runInBackground;

            if (toggle_enableExceptions)
                PlayerSettings.WebGL.exceptionSupport = enableExceptions;

            if (toggle_enableExceptions)
                PlayerSettings.WebGL.compressionFormat = compressionFormat;

            if (toggle_dataCaching)
                PlayerSettings.WebGL.dataCaching = dataCaching;

            if (toggle_decompressionFallback)
                PlayerSettings.WebGL.decompressionFallback = decompressionFallback;

            if (toggle_colorSpace)
                PlayerSettings.colorSpace = colorSpace;

            if (toggle_autoGraphicsAPI)
            {
                bool currentAutoGraphicsAPIState = PlayerSettings.GetUseDefaultGraphicsAPIs(EditorUserBuildSettings.activeBuildTarget);
                if (currentAutoGraphicsAPIState != autoGraphicsAPI)
                    PlayerSettings.SetUseDefaultGraphicsAPIs(EditorUserBuildSettings.activeBuildTarget, autoGraphicsAPI);
            }

            ApplayOtherSettings();
        }

        [AttributeUsage(AttributeTargets.Method)]
        private class ApplySettingsAttribute : Attribute { }

        private void ApplayOtherSettings()
        {
            List<Action> methodsToCall = new List<Action>();

            Type type = GetType();
            MethodInfo[] methods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);

            foreach (MethodInfo method in methods)
            {
                var attributes = method.GetCustomAttributes(typeof(ApplySettingsAttribute), true);
                if (attributes.Length > 0)
                {
                    MethodInfo currentMethod = method;
                    methodsToCall.Add(() => currentMethod.Invoke(this, null));
                }
            }

            foreach (var action in methodsToCall)
            {
                action.Invoke();
            }
        }
    }
}
#endif