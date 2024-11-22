using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace YG.EditorScr
{
    [InitializeOnLoad]
    public static class Server
    {
        public const string LOAD_COMPLETE_KEY = "PluginYG_LoadServerComplete";
        private const string URL_KEY = "PluginYG_URLCloudInfo";
        private const string STANDART_URL = "https://max-games.ru/public/pluginYG2/data.json";
        private static string testUrl = "";
        public static bool loadComplete
        {
            get { return SessionState.GetBool(LOAD_COMPLETE_KEY, false); }
        }

        private static int loadCount;

        static Server() => InitializeOnLoad();
        private static void InitializeOnLoad()
        {
            EditorApplication.delayCall += () =>
            {
                if (PlayerPrefs.GetInt(InfoYG.FIRST_STARTUP_KEY) != 0 &&
                SessionState.GetBool(LOAD_COMPLETE_KEY, false) == false)
                {
                    LoadServerInfo();
                }
            };
        }

        public static async void LoadServerInfo(bool core = false)
        {
            if (core == false)
            {
                loadCount = 0;
                SessionState.SetBool(LOAD_COMPLETE_KEY, false);
            }

            try
            {
                loadCount++;
                if (loadCount < 4)
                {
                    string fileContent = null;

                    if (testUrl == "")
                    {
                        fileContent = await ReadFileFromURL(PlayerPrefs.GetString(URL_KEY, STANDART_URL));

                        if (fileContent == null)
                        {
                            PlayerPrefs.SetString(URL_KEY, STANDART_URL);
                            fileContent = await ReadFileFromURL(STANDART_URL);
                        }
                        else
                        {
                            ServerJson cloud = JsonUtility.FromJson<ServerJson>(fileContent);

                            if (cloud.redirection != string.Empty && cloud.redirection != PlayerPrefs.GetString(URL_KEY))
                            {
                                PlayerPrefs.SetString(URL_KEY, cloud.redirection);
                                LoadServerInfo(true);
                                return;
                            }
                        }
                    }
                    else
                    {
                        fileContent = await ReadFileFromURL(PlayerPrefs.GetString(URL_KEY, testUrl));
                        ServerJson cloud = JsonUtility.FromJson<ServerJson>(fileContent);
                    }

                    File.WriteAllText(InfoYG.FILE_SERVER_INFO, fileContent);
                    ServerInfo.Read();
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error loading server info: {ex.Message}");
            }
            finally
            {
                await Task.Delay(100);
                SessionState.SetBool(LOAD_COMPLETE_KEY, true);
                ServerInfo.DoActionLoadServerInfo();
            }
        }

        private static async Task<string> ReadFileFromURL(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }
                catch (HttpRequestException ex)
                {
                    Debug.LogError($"Server info request failed: {ex.Message}");
                    return null;
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Server info request error: {ex.Message}");
                    return null;
                }
            }
        }

        public static void DeletePrefs()
        {
            PlayerPrefs.DeleteKey(URL_KEY);
            PlayerPrefs.DeleteKey(InfoYG.FIRST_STARTUP_KEY);
            SessionState.SetBool(LOAD_COMPLETE_KEY, false);
        }
    }
}