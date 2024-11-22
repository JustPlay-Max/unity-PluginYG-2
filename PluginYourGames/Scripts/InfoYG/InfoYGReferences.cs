﻿using System.IO;
using UnityEngine;

namespace YG
{
    public partial class InfoYG
    {
        public const string NAME_PLUGIN = "PluginYG2";
        public const string FULL_NAME_PLUGIN = "Plugin Your Games";
        public const string NAME_INFOYG_FILE = "SettingsYG2";
        public const string NO_DATA = "no data";
        public const string ANONYMOUS = "anonymous";

#if UNITY_EDITOR
        public const string PATCH_ASSETS_YG2 = "Assets/PluginYourGames";
        public const string CORE_FOLDER_YG2 = "PluginYourGames";
        public const string FIRST_STARTUP_KEY = "FirstStartup_YG2";
        public const string DEMO_IMAGE = "demo image";

        public static string PATCH_PC_YG2
        {
            get { return $"{Application.dataPath}/{CORE_FOLDER_YG2}"; }
        }
        public static string PATCH_PC_EDITOR
        {
            get
            {
                string directory = $"{PATCH_PC_YG2}/Editor";
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);
                return directory;
            }
        }
        public static string PATCH_ASSETS_MODULES
        {
            get
            {
                string path = $"{PATCH_ASSETS_YG2}/Modules";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                return path;
            }
        }
        public static string PATCH_PC_MODULES
        {
            get
            {
                string path = $"{PATCH_PC_YG2}/Modules";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                return path;
            }
        }
        public static string FILE_MODULES_PC
        {
            get => PATCH_PC_EDITOR + "/ModulesListYG2.txt";
        }
        public static string FILE_SERVER_INFO
        {
            get { return PATCH_PC_EDITOR + "/ServerInfoYG2.json"; }
        }

        public static string PATCH_PC_PLATFORMS
        {
            get
            {
                string path = $"{PATCH_PC_YG2}/Platforms";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                return path;
            }
        }
        public static string PATCH_ASSETS_PLATFORMS
        {
            get
            {
                string createDirectory = $"{PATCH_PC_YG2}/Platforms";
                if (!Directory.Exists(createDirectory))
                    Directory.CreateDirectory(createDirectory);

                string path = $"Assets/{CORE_FOLDER_YG2}/Platforms";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                return path;
            }
        }

        public static string PATCH_PC_EXAMPLE
        {
            get { return $"{PATCH_PC_YG2}/Example"; }
        }
        public static string PATCH_PC_WEBGLTEMPLATES
        {
            get { return $"{Application.dataPath}/WebGLTemplates"; }
        }
        public static string VERSION_YG2
        {
            get
            {
                string file = $"{PATCH_PC_YG2}/Version.txt";
                if (File.Exists(file))
                {
                    return File.ReadAllText(file).Replace("v", string.Empty);
                }
                else
                {
                    return "0";
                }
            }
        }

        public static string ICONS
        {
            get => $"{PATCH_ASSETS_YG2}/Scripts/EditorScr/Editor/Icons";
        }

        public static string ICON_YG2
        {
            get => $"{ICONS}/IconPluginYG2.png";
        }
#endif
    }
}