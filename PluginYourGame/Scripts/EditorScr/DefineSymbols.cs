#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

namespace YG.EditorScr
{
    [InitializeOnLoad]
    public static class DefineSymbols
    {
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
            AddDefine("PLUGIN_YG_2");
            PlatformDefineSymbols();
            ModulesDefineSymbols();
            DefinePackage(TMP_PACKAGE, TMP_DEFINE);
            DefinePackage(NJSON_PACKAGE, NJSON_DEFINE);
        }

        public static void PlatformDefineSymbols()
        {
            string currentPlatform = string.Empty;
            InfoYG infoRes = Resources.Load<InfoYG>(InfoYG.NAME_INFOYG_FILE);

            if (infoRes != null && InfoYG.Inst().basicSettings.platform != null)
                currentPlatform = InfoYG.Inst().basicSettings.platform.nameDefining;

            BuildTargetGroup buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            string definesText = PlayerSettings.GetScriptingDefineSymbols(UnityEditor.Build.NamedBuildTarget.FromBuildTargetGroup(buildTargetGroup));

            if (currentPlatform != string.Empty && !definesText.Contains(currentPlatform))
                definesText += ";" + currentPlatform;

            List<string> defines = definesText.Split(";").ToList();
            string[] platforms = Directory.GetDirectories(InfoYG.PATCH_PC_PLATFORMS);

            for (int d = 0; d < defines.Count; d++)
            {
                if (defines[d] == currentPlatform)
                    continue;

                for (int p = 0; p < platforms.Length; p++)
                {
                    string platform = Path.GetFileName(platforms[p]);
                    platform += "Platform";

                    if (defines[d] == platform)
                        defines.RemoveAt(d);
                }
            }

            definesText = string.Empty;
            foreach (string d in defines)
                definesText += d + ";";

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
                    RemoveDefine(modules[i]);
                }
            }

            string text = string.Empty;

            for (int i = 0; i < folderNames.Length; i++)
            {
                string dataFilePatch = $"{InfoYG.PATCH_PC_MODULES}/{folderNames[i]}/Version.txt";
                string version = "v1.0";

                if (File.Exists(dataFilePatch))
                    version = File.ReadAllLines(dataFilePatch)[0];

                text += $"{folderNames[i]} {version}\n";
            }

            File.WriteAllText(InfoYG.FILE_MODULES_PC, text);

            if (modules.Length == folderNames.Length && !mismatch)
                return;

            foreach (var folderName in folderNames)
                AddDefine(folderName);
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
            if (define == string.Empty || define == " ")
                return;

            BuildTargetGroup buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            string defines = PlayerSettings.GetScriptingDefineSymbols(UnityEditor.Build.NamedBuildTarget.FromBuildTargetGroup(buildTargetGroup));

            if (defines.Contains(define))
                return;

            PlayerSettings.SetScriptingDefineSymbols(UnityEditor.Build.NamedBuildTarget.FromBuildTargetGroup(buildTargetGroup), defines + ";" + define);
        }

        public static void RemoveDefine(string define)
        {
            if (define == string.Empty || define == " ")
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