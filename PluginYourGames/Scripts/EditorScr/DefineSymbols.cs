#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;
using YG.Insides;

namespace YG.EditorScr
{
    [InitializeOnLoad]
    public static class DefineSymbols
    {
        public const string YG2_DEFINE = "PLUGIN_YG_2";
        public const string LANG_DEFINE = "RU_YG2";
        public const string TMP_DEFINE = "TMP_YG2", TMP_PACKAGE = "com.unity.textmeshpro";
        public const string NJSON_DEFINE = "NJSON_YG2", NJSON_PACKAGE = "com.unity.nuget.newtonsoft-json";
        public const string NJSON_STORAGE_DEFINE = "NJSON_STORAGE_YG2";

        static DefineSymbols()
        {
            if (PlayerPrefs.GetInt(InfoYG.FIRST_STARTUP_KEY) == 0)
            {
                FirstStartup();
            }
            else
            {
                EditorApplication.projectChanged += UpdateDefineSymbols;
                UpdateDefineSymbols();
            }
        }

        private static async void FirstStartup()
        {
            for (int i = 0; i <= 10; i++)
            {
                EditorUtility.DisplayProgressBar($"{InfoYG.NAME_PLUGIN} first startup", "first startup operations", 0.1f + (i / 20f));
                await Task.Delay(100);
            }
            EditorUtility.ClearProgressBar();

            PlayerPrefs.SetInt(InfoYG.FIRST_STARTUP_KEY, 1);
            PlayerPrefs.Save();

            InfoYG.Inst();

            UpdateDefineSymbols();
            CompilationPipeline.RequestScriptCompilation();
        }

        public static void UpdateDefineSymbols()
        {
            AddDefine(YG2_DEFINE);
            PlatformDefineSymbols();
            ModulesDefineSymbols();
            DefinePackage(TMP_PACKAGE, TMP_DEFINE);
            DefinePackage(NJSON_PACKAGE, NJSON_DEFINE);
        }

        public static void PlatformDefineSymbols()
        {
            string currentPlatform = string.Empty;
            InfoYG infoRes = Resources.Load<InfoYG>(InfoYG.NAME_INFOYG_FILE);

            if (infoRes != null && InfoYG.Inst().Basic.platform != null)
                currentPlatform = PlatformSettings.currentPlatformFullName + "_yg";

            if (currentPlatform == "Error_yg" || currentPlatform == "ErrorPlatform_yg" || currentPlatform == "_yg" || currentPlatform == "NewPlatform")
            {
                string path = AssetDatabase.GetAssetPath(InfoYG.instance.Basic.platform);
                if (string.IsNullOrEmpty(path))
                    return;

                string folderPath = Path.GetDirectoryName(path);
                string modulName = Path.GetFileName(folderPath);

                InfoYG.instance.Basic.platform.nameFull = modulName + "Platform";

                EditorUtility.SetDirty(InfoYG.instance.Basic.platform);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

            BuildTargetGroup buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            string definesText = PlayerSettings.GetScriptingDefineSymbols(UnityEditor.Build.NamedBuildTarget.FromBuildTargetGroup(buildTargetGroup));

            if (currentPlatform != string.Empty && !definesText.Contains(currentPlatform))
                definesText += ";" + currentPlatform;

            List<string> defines = definesText.Split(";").ToList();
            string[] platforms = Directory.GetDirectories(InfoYG.PATCH_PC_PLATFORMS);

            if (defines == null || defines.Count == 0)
            {
                Debug.LogWarning("Defines list is empty. Nothing to process.");
                return;
            }

            if (platforms == null || platforms.Length == 0)
            {
                Debug.LogWarning("Platforms directory is empty. Nothing to process.");
                return;
            }

            for (int d = defines.Count - 1; d >= 0; d--)
            {
                if (defines[d] == currentPlatform)
                    continue;

                for (int p = 0; p < platforms.Length; p++)
                {
                    string platform = Path.GetFileName(platforms[p]);
                    platform += "Platform_yg";

                    if (defines[d] == platform)
                    {
                        defines.RemoveAt(d);
                        break;
                    }
                }
            }

            definesText = string.Join(";", defines);
            PlayerSettings.SetScriptingDefineSymbols(UnityEditor.Build.NamedBuildTarget.FromBuildTargetGroup(buildTargetGroup), definesText);
        }


        public static void ModulesDefineSymbols()
        {
            string directory = Path.GetDirectoryName(InfoYG.FILE_MODULES_PC);

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            if (!File.Exists(InfoYG.FILE_MODULES_PC))
                File.WriteAllText(InfoYG.FILE_MODULES_PC, string.Empty);

            string[] modules = File.ReadAllLines(InfoYG.FILE_MODULES_PC);
            List<string> modulesList = new List<string>();

            for (int i = 0; i < modules.Length; i++)
            {
                if (modules[i] == string.Empty)
                    continue;

                int spaceIndex = modules[i].IndexOf(' ');
                if (spaceIndex > -1)
                    modules[i] = modules[i].Remove(spaceIndex);

                modulesList.Add(modules[i]);
            }
            modules = modulesList.ToArray();

            string[] folders = Directory.GetDirectories(InfoYG.PATCH_PC_MODULES);
            string[] folderNames = new string[folders.Length];

            for (int i = 0; i < folders.Length; i++)
                folderNames[i] = Path.GetFileName(folders[i]);

            bool mismatch = false;

            for (int i = 0; i < modules.Length; i++)
            {
                bool foundLastModule = false;

                for (int j = 0; j < folderNames.Length; j++)
                {
                    if (modules[i] == folderNames[j])
                    {
                        foundLastModule = true;
                        break;
                    }
                }

                if (!foundLastModule)
                {
                    mismatch = true;
                    RemoveDefine(modules[i] + "_yg");
                }

                if (!mismatch)
                {
                    BuildTargetGroup buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
                    string definesText = PlayerSettings.GetScriptingDefineSymbols(UnityEditor.Build.NamedBuildTarget.FromBuildTargetGroup(buildTargetGroup));
                    List<string> defines = definesText.Split(";").ToList();

                    if (!defines.Contains(modules[i] + "_yg"))
                    {
                        mismatch = true;
                    }
                }
            }

            string text = string.Empty;

            for (int i = 0; i < folderNames.Length; i++)
            {
                string dataFilePatch = $"{InfoYG.PATCH_PC_MODULES}/{folderNames[i]}/Version.txt";
                string version = "v0.0";

                if (File.Exists(dataFilePatch))
                    version = File.ReadAllLines(dataFilePatch)[0];

                text += $"{folderNames[i]} {version}\n";
            }

            File.WriteAllText(InfoYG.FILE_MODULES_PC, text);

            if (modules.Length == folderNames.Length && !mismatch)
                return;

            foreach (var folderName in folderNames)
                AddDefine(folderName + "_yg");
        }

        private static void DefinePackage(string packageName, string defineSymbols)
        {
            if (UnityPackagesManager.IsPackageImported(packageName))
                AddDefine(defineSymbols);
            else
                RemoveDefine(defineSymbols);
        }

        public static bool CheckDefine(string define)
        {
            BuildTargetGroup buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            string defines = PlayerSettings.GetScriptingDefineSymbols(UnityEditor.Build.NamedBuildTarget.FromBuildTargetGroup(buildTargetGroup));

            if (define != string.Empty && defines.Contains(define))
            {
                return true;
            }
            else return false;
        }

        public static void AddDefine(string define)
        {
            if (string.IsNullOrEmpty(define) || define == " " || define.Contains(" "))
                return;

            BuildTargetGroup buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            string defines = PlayerSettings.GetScriptingDefineSymbols(UnityEditor.Build.NamedBuildTarget.FromBuildTargetGroup(buildTargetGroup));

            if (defines.Contains(define))
                return;

            PlayerSettings.SetScriptingDefineSymbols(UnityEditor.Build.NamedBuildTarget.FromBuildTargetGroup(buildTargetGroup), defines + ";" + define);
        }

        public static void RemoveDefine(string define)
        {
            if (string.IsNullOrEmpty(define) || define == " " || define.Contains(" "))
                return;

            BuildTargetGroup buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            string defines = PlayerSettings.GetScriptingDefineSymbols(UnityEditor.Build.NamedBuildTarget.FromBuildTargetGroup(buildTargetGroup));

            if (defines.Contains(define))
            {
                string[] defineArray = defines.Split(';');

                List<string> updatedDefines = new List<string>();
                foreach (string d in defineArray)
                {
                    if (d != define)
                    {
                        updatedDefines.Add(d);
                    }
                }

                string newDefines = string.Join(";", updatedDefines);
                PlayerSettings.SetScriptingDefineSymbols(UnityEditor.Build.NamedBuildTarget.FromBuildTargetGroup(buildTargetGroup), newDefines);
            }
        }
    }
}
#endif