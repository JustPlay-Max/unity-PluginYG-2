using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using YG.Utils;
using YG.Insides;
using UnityEditor.Compilation;

namespace YG.EditorScr
{
    public class VersionControlWindow : EditorWindow
    {
        private float rowHeight = 20f;
        private static VersionControlWindow window;
        private static List<Module> modules = new List<Module>();
        private static string fileContent;
        private static bool downloadProcessPackage;
        private const string NULL = "null";
        private const string REMOVE_BEFORE_IMPORT_TOGGLE_KEY = "RemoveBeforeImport_YG2";
        private static bool removeBeforeImport;

        private Vector2 scrollPosition;
        private bool cloudComplete = true;

        [MenuItem("YG2/" + Langs.versionControl)]
        public static void ShowWindow()
        {
            window = GetWindow<VersionControlWindow>(Langs.versionControl + " YG2");
            window.position = new Rect(300, 200, 900, 800);
            window.minSize = new Vector2(700, 500);
            Server.LoadServerInfo(true);
        }

        private void OnEnable()
        {
            if (PlayerPrefs.GetInt(InfoYG.FIRST_STARTUP_KEY) == 0)
                return;

            removeBeforeImport = EditorPrefs.GetBool(REMOVE_BEFORE_IMPORT_TOGGLE_KEY, true);

            ServerInfo.onLoadServerInfo += OnLoadServerInfo;

            if (!Server.loadComplete)
                InitData(null);
            else
                InitData(ServerInfo.saveInfo);

        }

        private void OnDisable()
        {
            if (PlayerPrefs.GetInt(InfoYG.FIRST_STARTUP_KEY) == 0)
                return;

            ServerInfo.onLoadServerInfo -= OnLoadServerInfo;
        }

        private void OnLoadServerInfo() => InitData(ServerInfo.saveInfo);

        private void InitData(ServerJson cloud)
        {
            cloudComplete = cloud != null;

            modules = new List<Module>();

            string pluginVersionPatch = InfoYG.PATCH_PC_YG2 + "/Version.txt";
            string pluginVersion = string.Empty;

            if (File.Exists(pluginVersionPatch))
            {
                pluginVersion = File.ReadAllText(pluginVersionPatch);
                pluginVersion = pluginVersion.Replace("v", string.Empty);
            }

            Module pluginModule = new Module
            {
                nameModule = InfoYG.NAME_PLUGIN,
                projectVersion = pluginVersion
            };
            modules.Add(pluginModule);

            // Modules
            string[] modulesTextLines = null;

            if (File.Exists(InfoYG.FILE_MODULES_PC))
                modulesTextLines = File.ReadAllLines(InfoYG.FILE_MODULES_PC);

            for (int i = 0; i < modulesTextLines.Length; i++)
            {
                if (modulesTextLines[i] == string.Empty)
                    continue;

                string name = modulesTextLines[i];
                string version = "imported";

                int spaceIndex = modulesTextLines[i].IndexOf(' ');
                if (spaceIndex > -1)
                {
                    name = modulesTextLines[i].Remove(spaceIndex);
                    version = modulesTextLines[i].Remove(0, spaceIndex + 2);
                }

                Module module = new Module
                {
                    nameModule = name,
                    projectVersion = version
                };

                modules.Add(module);
            }

            // Platforms
            string[] platfomFolders = Directory.GetDirectories(InfoYG.PATCH_PC_PLATFORMS);
            string[] platfomNames = new string[platfomFolders.Length];

            for (int i = 0; i < platfomFolders.Length; i++)
                platfomNames[i] = Path.GetFileName(platfomFolders[i]);

            for (int i = 0; i < platfomNames.Length; i++)
            {
                string version = "imported";
                string platfomVersionPathc = $"{InfoYG.PATCH_PC_PLATFORMS}/{platfomNames[i]}/Version.txt";

                if (File.Exists(platfomVersionPathc))
                {
                    version = File.ReadAllText(platfomVersionPathc);
                    version = version.Replace("v", string.Empty);
                }

                Module module = new Module
                {
                    nameModule = platfomNames[i],
                    projectVersion = version
                };
                modules.Add(module);
            }

            // Cloud
            if (cloud != null && cloud.modules.Length > 0)
            {
                for (int i = 0; i < cloud.modules.Length; i++)
                {
                    bool found = false;

                    for (int j = 0; j < modules.Count; j++)
                    {
                        if (cloud.modules[i].name == modules[j].nameModule)
                        {
                            Module module = new Module
                            {
                                nameModule = modules[j].nameModule,
                                projectVersion = modules[j].projectVersion,
                                lastVersion = cloud.modules[i].version,
                                download = cloud.modules[i].download,
                                doc = cloud.modules[i].doc,
                                critical = cloud.modules[i].critical,
                                dependencies = cloud.modules[i].dependencies
                            };
                            modules[j] = module;

                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        Module module = new Module
                        {
                            nameModule = cloud.modules[i].name,
                            projectVersion = string.Empty,
                            lastVersion = cloud.modules[i].version,
                            download = cloud.modules[i].download,
                            doc = cloud.modules[i].doc,
                            critical = cloud.modules[i].critical,
                            dependencies = cloud.modules[i].dependencies
                        };
                        modules.Add(module);
                    }
                }
            }
        }

        private void OnGUI()
        {
            GUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();
            removeBeforeImport = EditorGUILayout.ToggleLeft(Langs.removeBeforeImport, removeBeforeImport);
            if (EditorGUI.EndChangeCheck())
                EditorPrefs.SetBool(REMOVE_BEFORE_IMPORT_TOGGLE_KEY, removeBeforeImport);

            if (FastButton.Stringy(Langs.updateInfo))
            {
                InitData(null);
                Server.LoadServerInfo();
            }

            GUILayout.EndHorizontal();

            if (downloadProcessPackage || modules == null || modules.Count == 0)
            {
                GUILayout.Space(20);
                GUIStyle centeredStyle = TextStyles.Header();
                centeredStyle.alignment = TextAnchor.MiddleCenter;
                centeredStyle.fontSize = 14;
                EditorGUILayout.LabelField(Langs.loading, centeredStyle);
                return;
            }

            GUIStyle labelStyleGreen = TextStyles.Green();
            GUIStyle labelStyleGray = TextStyles.Gray();
            labelStyleGray.fontSize = 11;
            GUIStyle labelStyleOrange = TextStyles.Header();

            bool imported = true;
            float columnWidth = position.width / 4;
            float columnWidth_Name = columnWidth + 20;
            float columnWidth_ProjectVersion = columnWidth - 20;
            float columnWidth_LatestVersion = columnWidth - 50;
            float columnWidth_Control = columnWidth + 10;

            GUILayout.Space(10);
            GUILayout.BeginHorizontal(YGEditorStyles.box);
            GUILayout.Label(Langs.name, labelStyleOrange, GUILayout.Width(columnWidth_Name), GUILayout.Height(rowHeight));
            GUILayout.Label(Langs.projectVersion, labelStyleOrange, GUILayout.Width(columnWidth_ProjectVersion), GUILayout.Height(rowHeight));
            GUILayout.Label(Langs.latestVersion, labelStyleOrange, GUILayout.Width(columnWidth_LatestVersion), GUILayout.Height(rowHeight));
            GUIStyle headerCenter = TextStyles.Header();
            headerCenter.alignment = TextAnchor.MiddleCenter;
            GUILayout.Label(Langs.control, headerCenter, GUILayout.Width(columnWidth_Control), GUILayout.Height(rowHeight));
            GUILayout.EndHorizontal();

            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

            for (int i = 0; i < modules.Count; i++)
            {
                GUILayout.BeginHorizontal(YGEditorStyles.selectable);

                string projectVersionStr = modules[i].projectVersion;
                if (projectVersionStr == string.Empty || projectVersionStr == null)
                {
                    imported = false;
                    projectVersionStr = "not imported";
                }

                string lastVersionStr = modules[i].lastVersion;
                float.TryParse(modules[i].projectVersion, NumberStyles.Float, CultureInfo.InvariantCulture, out float projectVersion);
                float.TryParse(modules[i].lastVersion, NumberStyles.Float, CultureInfo.InvariantCulture, out float lastVersion);

                // Name
                Rect rect = GUILayoutUtility.GetRect(new GUIContent(modules[i].nameModule), GUIStyle.none, GUILayout.Width(columnWidth_Name), GUILayout.Height(rowHeight));
                GUIStyle labelStyleName;
                if (imported)
                {
                    if (lastVersion > projectVersion)
                        labelStyleName = TextStyles.Green();
                    else
                        labelStyleName = TextStyles.While();
                }
                else
                {
                    if (EditorGUIUtility.isProSkin)
                        labelStyleName = TextStyles.LabelStyleColor(new Color(0.65f, 0.65f, 0.65f));
                    else
                        labelStyleName = TextStyles.LabelStyleColor(new Color(0.3f, 0.3f, 0.3f));
                }
                GUI.Label(rect, modules[i].nameModule, labelStyleName);

                // Project version
                rect = GUILayoutUtility.GetRect(new GUIContent(projectVersionStr), GUIStyle.none, GUILayout.Width(columnWidth_ProjectVersion), GUILayout.Height(rowHeight));

                if (imported)
                {
                    if (lastVersion > projectVersion && modules[i].critical)
                        GUI.Label(rect, projectVersionStr, TextStyles.Red());
                    else
                        GUI.Label(rect, projectVersionStr);
                }
                else
                {
                    GUI.Label(rect, projectVersionStr, labelStyleGray);
                }

                // Latest version
                rect = GUILayoutUtility.GetRect(new GUIContent(lastVersionStr), GUIStyle.none, GUILayout.Width(columnWidth_LatestVersion), GUILayout.Height(rowHeight));

                if (cloudComplete)
                {
                    if (lastVersionStr == string.Empty || lastVersionStr == null)
                    {
                        lastVersionStr = "not found";
                        GUI.Label(rect, lastVersionStr, labelStyleGray);
                    }
                    else if (imported && lastVersion > projectVersion)
                    {
                        if (modules[i].critical)
                            lastVersionStr += "  critical!";

                        GUI.Label(rect, lastVersionStr, labelStyleGreen);
                    }
                    else
                    {
                        GUI.Label(rect, lastVersionStr);
                    }
                }
                else
                {
                    GUI.Label(rect, Langs.loading, labelStyleGray);
                }

                // Control
                rect = GUILayoutUtility.GetRect(new GUIContent("Control"), GUIStyle.none, GUILayout.Width(columnWidth_Control / 1.5f), GUILayout.Height(rowHeight));

                if (!cloudComplete)
                {
                    GUI.Label(rect, Langs.loading, labelStyleGray);
                }
                else
                {
                    GUILayout.BeginHorizontal();

                    if (imported)
                    {
                        if (lastVersion > projectVersion)
                        {
                            GUILayout.BeginHorizontal();

                            rect.width = columnWidth_Control / 3;
                            ButtonUpdate();
                            rect.x += columnWidth_Control / 3;
                            ButtonDelete();

                            GUILayout.EndHorizontal();
                        }
                        else
                        {
                            ButtonDelete();
                        }

                        void ButtonUpdate()
                        {
                            if (GUI.Button(rect, "Update", YGEditorStyles.button))
                            {
                                if (removeBeforeImport)
                                {
                                    if (modules[i].nameModule == InfoYG.NAME_PLUGIN)
                                    {
                                        UpdatePlyginYG(modules[i]);
                                    }
                                    else
                                    {
                                        ImportPackage(modules[i]);

                                        string patchModules = $"{InfoYG.PATCH_PC_MODULES}/{modules[i].nameModule}";
                                        string patchPlatforms = $"{InfoYG.PATCH_PC_PLATFORMS}/{modules[i].nameModule}";

                                        if (Directory.Exists(patchModules))
                                        {
                                            FileYG.DeleteDirectory(patchModules);
                                        }
                                        else if (Directory.Exists(patchPlatforms))
                                        {
                                            DeletePlatformWebGLTemplate(modules[i].nameModule);
                                            PlatformSettings.DeletePlatform();

                                            FileYG.DeleteDirectory(patchPlatforms);
                                        }
                                    }
                                }
                                else
                                {
                                    ImportPackage(modules[i]);
                                }
                            }
                        }

                        void ButtonDelete()
                        {
                            string folderDelete = string.Empty;
                            bool isModulPlatform = false;

                            if (modules[i].nameModule == InfoYG.NAME_PLUGIN)
                            {
                                folderDelete = InfoYG.PATCH_PC_YG2;
                            }
                            else
                            {
                                string patchModules = $"{InfoYG.PATCH_PC_MODULES}/{modules[i].nameModule}";
                                string patchPlatforms = $"{InfoYG.PATCH_PC_PLATFORMS}/{modules[i].nameModule}";

                                if (Directory.Exists(patchModules))
                                    folderDelete = patchModules;

                                if (Directory.Exists(patchPlatforms))
                                {
                                    folderDelete = patchPlatforms;
                                    isModulPlatform = true;
                                }
                            }

                            if (folderDelete != string.Empty)
                            {
                                if (modules[i].nameModule == InfoYG.NAME_PLUGIN)
                                {
                                    if (GUI.Button(rect, "Del all", YGEditorStyles.button))
                                    {
                                        if (EditorUtility.DisplayDialog($"{Langs.correctDelete} {InfoYG.NAME_PLUGIN}", Langs.fullDeletePluginYG, Langs.deleteAll, Langs.cancel))
                                        {
                                            DeletePluginYG();
                                        }
                                    }
                                }
                                else
                                {
                                    if (GUI.Button(rect, "Delete", YGEditorStyles.button))
                                    {
                                        if (EditorUtility.DisplayDialog(Langs.deletePackage, $"{Langs.deletePackage} {modules[i].nameModule}?", "Ok", Langs.cancel))
                                        {
                                            string folderName = folderDelete.Substring(folderDelete.LastIndexOf('/') + 1);
                                            folderName = folderDelete.Substring(folderDelete.LastIndexOf('\\') + 1);

                                            if (isModulPlatform)
                                            {
                                                PlatformSettings.DeletePlatform();
                                                folderName += "Platform";

                                                DeletePlatformWebGLTemplate(modules[i].nameModule);
                                            }

                                            DefineSymbols.RemoveDefine(modules[i].nameModule);
                                            FileYG.DeleteDirectory(folderDelete);
                                            DefineSymbols.ModulesDefineSymbols();
                                            InitData(ServerInfo.saveInfo);
                                            AssetDatabase.Refresh();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                GUI.Label(rect, "manual deletion", labelStyleGray);
                            }
                        }
                    }
                    else
                    {
                        if (modules[i].download != string.Empty && modules[i].download != null)
                        {
                            if (GUI.Button(rect, "Import", YGEditorStyles.button))
                                ImportPackage(modules[i]);
                        }
                        else
                        {
                            GUI.Label(rect, "no download", labelStyleGray);
                        }
                    }

                    rect = GUILayoutUtility.GetRect(new GUIContent("Doc"), GUIStyle.none, GUILayout.Width(columnWidth_Control / 3), GUILayout.Height(rowHeight));

                    if (modules[i].doc != string.Empty && modules[i].doc != null)
                    {
                        if (GUI.Button(rect, "Doc", YGEditorStyles.button))
                            Application.OpenURL(modules[i].doc);
                    }
                    else
                    {
                        GUIStyle notDocStyle = TextStyles.Gray();
                        notDocStyle.alignment = TextAnchor.MiddleRight;
                        GUI.Label(rect, "not doc", notDocStyle);
                    }

                    GUILayout.EndHorizontal();
                }

                GUILayout.EndHorizontal();
            }

            EditorGUILayout.EndScrollView();
            Repaint();
        }

        private void DeletePluginYG()
        {
            EditorUtility.DisplayDialog($"{Langs.correctDelete} {InfoYG.NAME_PLUGIN}", Langs.fullDeletePluginYGComplete, "Ok");

            EditorPrefs.DeleteKey(REMOVE_BEFORE_IMPORT_TOGGLE_KEY);
            Server.DeletePrefs();

            for (int i = 0; i < modules.Count; i++)
                DefineSymbols.RemoveDefine(modules[i].nameModule);

            if (InfoYG.Inst().basicSettings.platform)
                DefineSymbols.RemoveDefine(InfoYG.Inst().basicSettings.platform.nameDefining);
            DefineSymbols.RemoveDefine("RU_YG2");
            DefineSymbols.RemoveDefine("TMP_YG2");
            DefineSymbols.RemoveDefine("PLUGIN_YG_2");

            Close();

            string[] templateFolders = Directory.GetDirectories(InfoYG.PATCH_PC_PLATFORMS);
            for (int i = 0; i < templateFolders.Length; i++)
                DeletePlatformWebGLTemplate(Path.GetFileName(templateFolders[i]));

            FileYG.DeleteDirectory(InfoYG.PATCH_PC_YG2);
        }

        private void UpdatePlyginYG(Module module)
        {
            if (module.download == string.Empty)
                return;

            string tempScrName = "UpdatePluginYGTemp";
            string tempScrText = File.ReadAllText($"{InfoYG.PATCH_PC_YG2}/Scripts/Server/Editor/{tempScrName}.txt");
            tempScrText = tempScrText.Replace("DOWNLOAD_URL_KEY", module.download);
            tempScrText = tempScrText.Replace("PATH_YG2", InfoYG.CORE_FOLDER_YG2);

            string scenesFile = $"{InfoYG.PATCH_PC_EXAMPLE}/Resources/DemoSceneNames.txt";
            if (File.Exists(scenesFile))
            {
                string scenesText = File.ReadAllText(scenesFile);
                tempScrText = tempScrText.Replace("EXAMPLE_SCENES = string.Empty", @$"EXAMPLE_SCENES = @""{scenesText}""");
            }

            File.WriteAllText($"{Application.dataPath}/{tempScrName}.cs", tempScrText);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Close();
            CompilationPipeline.RequestScriptCompilation();
        }

        private static void DeletePlatformWebGLTemplate(string namePlatform)
        {
            FileYG.DeleteDirectory($"{InfoYG.PATCH_PC_WEBGLTEMPLATES}/{namePlatform}");

            if (FileYG.IsFolderEmpty(InfoYG.PATCH_PC_WEBGLTEMPLATES))
                Directory.Delete(InfoYG.PATCH_PC_WEBGLTEMPLATES);
        }

        private Module GetModuleByName(string name)
        {
            foreach (Module module in modules)
            {
                if (name == module.nameModule)
                    return module;
            }
            return null;
        }

        private async void ImportPackage(Module module)
        {
            await ImportPackageAsync(module);
        }

        private async Task<bool> ImportPackageAsync(Module module)
        {
            if (!EditorPrefs.HasKey("approvalDownloadPackagesYG2"))
            {
                if (EditorUtility.DisplayDialog($"{Langs.importPackage} {module.nameModule}", Langs.thirdPartyDialog, "Ok", Langs.cancel))
                {
                    EditorPrefs.SetInt("approvalDownloadPackagesYG2", 1);
                }
                else return false;
            }

            if (module.dependencies != null && module.dependencies != string.Empty)
            {
                List<string> dependencies = module.dependencies.Split(", ").ToList();
                string dependenciesStr = string.Empty;

                for (int i = dependencies.Count - 1; i > -1; i--)
                {
                    if (GetModuleByName(dependencies[i]).projectVersion != string.Empty)
                    {
                        dependencies.RemoveAt(i);
                    }
                    else
                    {
                        dependenciesStr += $"- {dependencies[i]}\n";
                    }
                }

                if (dependencies.Count > 0)
                {
                    EditorUtility.DisplayDialog(Langs.InstallDependencies, $"{Langs.importDependenciesDialog1} '{module.nameModule}' {Langs.importDependenciesDialog2}\n\n{dependenciesStr}", "Ok");
                    return false;
                }
            }

            if (string.IsNullOrEmpty(module.download))
            {
                Debug.LogError("URL is empty! (Import package)");
                return false;
            }

            try
            {
                string downloadPath = $"{InfoYG.PATCH_PC_EDITOR}/{module.nameModule}_tempYG.unitypackage";
                downloadProcessPackage = true;

                await DownloadPackageAsync(module.download, downloadPath);

                AssetDatabase.ImportPackage(downloadPath, true);
                File.Delete(downloadPath);
                downloadProcessPackage = false;

                return true;
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error downloading or importing package '{module.nameModule}': {e.Message}");
                downloadProcessPackage = false;
                return false;
            }
        }

        private async Task DownloadPackageAsync(string packageUrl, string savePath)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(packageUrl);
                response.EnsureSuccessStatusCode();

                byte[] packageBytes = await response.Content.ReadAsByteArrayAsync();
                File.WriteAllBytes(savePath, packageBytes);
            }
        }
    }
}