using System;
using System.Reflection;
using System.IO;
using System.Text;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using UnityEditor;
using UnityEngine;
using YG.Insides;

namespace YG.EditorScr.BuildModify
{
    public partial class ModifyBuild
    {
        private static string BUILD_PATCH;
        private static InfoYG infoYG;
        private static string indexFile;
        private static string styleFile;
        private static string methodName;
        private enum CodeType { HeadNative, BodyNative, JS, Head, Body, Init0, Init1, Init2, Init, Start };

        public static void ModifyIndex(string buildPatch)
        {
            infoYG = YG2.infoYG;
            BUILD_PATCH = buildPatch;
#if PLATFORM_WEBGL
            string indexFilePath = Path.Combine(buildPatch, "index.html");
            indexFile = File.ReadAllText(indexFilePath);

            string styleFilePath = Path.Combine(buildPatch, "style.css");
            if (File.Exists(styleFilePath))
                styleFile = File.ReadAllText(styleFilePath);

            Type type = typeof(ModifyBuild);
            MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            foreach (MethodInfo method in methods)
            {
                if (method.Name != nameof(ModifyIndex) && method.GetParameters().Length == 0)
                {
                    methodName = method.Name;
                    ModifyBuild scrCopy = new ModifyBuild();
                    method.Invoke(scrCopy, BindingFlags.Static | BindingFlags.Public, null, null, null);
                }
            }
#endif
            int.TryParse(BuildLog.ReadProperty("Build number"), out int buildNumInt);
            buildNumInt += 1;
            string buildNum = buildNumInt.ToString();

#if PLATFORM_WEBGL
            string logText = $"{InfoYG.NAME_PLUGIN} v{InfoYG.VERSION_YG2}  build: {buildNum}";
#if YandexGamesPlatform_yg
            string initFunction = $"LogStyledMessage('{logText}');";
            AddIndexCode(initFunction, CodeType.JS);
#else
            string initFunction = $"<script>console.log('%c' + '{logText}', 'color: #FFDF73; background-color: #454545');</script>";
            AddIndexCode(initFunction, CodeType.BodyNative);
#endif
            File.WriteAllText(indexFilePath, indexFile);

            if (File.Exists(styleFilePath))
                File.WriteAllText(styleFilePath, styleFile);
#endif
            EditorApplication.delayCall += () =>
            {
                Debug.Log($"<color=#00FF00>{InfoYG.NAME_PLUGIN} - Build complete!  Platform - {PlatformSettings.currentPlatformBaseName}.  Build number: {buildNum}</color>");
            };
        }

        public static void ModifyIndex()
        {
            string buildPatch = BuildLog.ReadProperty("Build path");

            if (buildPatch != null)
            {
                ModifyIndex(buildPatch);
                Process.Start("explorer.exe", buildPatch.Replace("/", "\\"));
            }
            else
            {
                Debug.LogError("Path not found:\n" + buildPatch);
            }
        }

        private static void AddIndexCode(string code, CodeType addCodeType)
        {
            string commentHelper;

            if (addCodeType == CodeType.HeadNative)
                commentHelper = "</head>";
            else if (addCodeType == CodeType.BodyNative)
                commentHelper = "</body>";
            else if (addCodeType == CodeType.Head)
                commentHelper = "<!-- Additional head modules -->";
            else if (addCodeType == CodeType.Body)
                commentHelper = "<!-- Additional body modules -->";
            else if (addCodeType == CodeType.Init0)
                commentHelper = "// Additional init0 modules";
            else if (addCodeType == CodeType.Init1)
                commentHelper = "// Additional init1 modules";
            else if (addCodeType == CodeType.Init2)
                commentHelper = "// Additional init2 modules";
            else if (addCodeType == CodeType.Init)
                commentHelper = "// Additional init modules";
            else if (addCodeType == CodeType.Start)
                commentHelper = "// Additional start modules";
            else
                commentHelper = "// Additional script modules";

            StringBuilder sb = new StringBuilder(indexFile);
            int insertIndex = sb.ToString().IndexOf(commentHelper);
            if (insertIndex >= 0)
            {
                if (addCodeType != CodeType.HeadNative && addCodeType != CodeType.BodyNative)
                    insertIndex += commentHelper.Length;

                sb.Insert(insertIndex, "\n" + code + "\n");
                indexFile = sb.ToString();
            }
        }

        public static string FileTextCopy(string fileName)
        {
            string file = $"{InfoYG.PATCH_PC_MODULES}/{methodName}/Scripts/Editor/CopyCode/{fileName}";
            return File.ReadAllText(file);
        }

        public static string ManualFileTextCopy(string filePath)
        {
            string file = $"{Application.dataPath}/{filePath}";
            return File.ReadAllText(file);
        }

        private static void InitFunction(string methodName, CodeType codeType = CodeType.Init)
        {
            string initFunction = $"await {methodName}();\nLogStyledMessage('Init {ModifyBuild.methodName} ysdk');";
            AddIndexCode(initFunction, codeType);
        }

        private static string ConvertToRGBA(Color color)
        {
            int red = (int)(color.r * 255f);
            int green = (int)(color.g * 255f);
            int blue = (int)(color.b * 255f);
            float alpha = color.a;

            return $"rgba({red}, {green}, {blue}, {alpha.ToString().Replace(",", ".")})";
        }
    }
}