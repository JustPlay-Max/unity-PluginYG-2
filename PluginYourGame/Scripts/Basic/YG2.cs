using System;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
using YG.Insides;

namespace YG
{
    [DefaultExecutionOrder(-5000)]
    public static partial class YG2
    {
        public static InfoYG infoYG { get { return InfoYG.Inst(); } }

        public static IPlatformsYG2 iPlatform;
        public static YGSendMessage sendMessage;
        public static OptionalPlatform optionalPlatform = new OptionalPlatform();
        public static bool SDKEnabled { get => _SDKEnabled; }
        private static bool _SDKEnabled;

        private static bool syncInitSDKComplete, awakePassed;

        public static Action onGetSDKData;
        public static Action<bool> onPauseGame;

        public static bool nowAdsShow
        {
            get
            {
                if (nowInterAdv || nowRewardAdv)
                    return true;
                else
                    return false;
            }
        }
        public static bool nowInterAdv;
        public static bool nowRewardAdv;
        public static Action onAdvNotification, onOpenAnyAdv, onCloseAnyAdv, onErrorAnyAdv;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
#if UNITY_EDITOR
            // Reset static for ESC
            _SDKEnabled = false;
            sendMessage = null;
            nowInterAdv = false;
            nowRewardAdv = false;
            onGetSDKData = null;
            onAdvNotification = null;
            onOpenAnyAdv = null;
            onCloseAnyAdv = null;
            onErrorAnyAdv = null;
#endif
            iPlatform = new PlatformYG2();

            GameObject YGObj = new GameObject() { name = "YG2Instance" };
            MonoBehaviour.DontDestroyOnLoad(YGObj);
            sendMessage = YGObj.AddComponent<YGSendMessage>();

            iPlatform.InitAwake();
            awakePassed = true;

            if (!infoYG.basicSettings.syncInitSDK || syncInitSDKComplete)
            {
                AwakeInit();
            }
        }

        private static void AwakeInit()
        {
            CallAction.CallIByAttribute(typeof(InitYG_0Attribute), typeof(YG2));
            CallAction.CallIByAttribute(typeof(InitYG_1Attribute), typeof(YG2));
            CallAction.CallIByAttribute(typeof(InitYG_2Attribute), typeof(YG2));
            CallAction.CallIByAttribute(typeof(InitYGAttribute), typeof(YG2));
        }

        public static void StartInit()
        {
            if (!_SDKEnabled && (!infoYG.basicSettings.syncInitSDK || syncInitSDKComplete))
            {
                iPlatform.InitStart();
                CallAction.CallIByAttribute(typeof(StartYGAttribute), typeof(YG2));
                _SDKEnabled = true;
                GetDataInvoke();
                iPlatform.InitComplete();
#if !UNITY_EDITOR
                Message("Init Game Success");
#endif
            }
        }

        public static void SyncInitialization()
        {
            if (infoYG.basicSettings.syncInitSDK)
            {
                syncInitSDKComplete = true;

                if (!_SDKEnabled)
                {
                    if (awakePassed)
                    {
                        AwakeInit();
                    }
                    else
                    {
                        LoadNextScene();
                        return;
                    }

                    if (infoYG.basicSettings.loadSceneIfSDKLate)
                    {
                        SceneManager.sceneLoaded += LoadLastScene;
                        SceneManager.LoadScene(infoYG.basicSettings.loadSceneIndex);

                        void LoadLastScene(Scene scene, LoadSceneMode mode)
                        {
                            StartInit();
                            SceneManager.sceneLoaded -= LoadLastScene;
                        }
                    }
                    else StartInit();
                }
                else LoadNextScene();
            }
            else LoadNextScene();

            void LoadNextScene()
            {
                if (infoYG.basicSettings.loadSceneIfSDKLate)
                {
                    if (infoYG.basicSettings.loadSceneIndex != 0)
                        SceneManager.LoadScene(infoYG.basicSettings.loadSceneIndex);
                }
            }
        }

        public static void GetDataInvoke()
        {
            if (_SDKEnabled)
                onGetSDKData?.Invoke();
        }

        public static void PauseGame(bool pause)
        {
            if (infoYG.basicSettings.autoPauseGame)
            {
                if (pause)
                {
                    GameObject pauseObj = new GameObject() { name = "PauseGameYG" };
                    MonoBehaviour.DontDestroyOnLoad(pauseObj);
                    PauseGameYG pauseScr = pauseObj.AddComponent<PauseGameYG>();
                    pauseScr.Setup();
                }
                else
                {
                    if (PauseGameYG.inst != null)
                        PauseGameYG.inst.PauseDisabled();
                }
            }

            onPauseGame?.Invoke(pause);
        }

        public static void Message(string message)
        {
#if UNITY_EDITOR
            if (infoYG.basicSettings.logInEditor)
            {
                System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(1, true);
                System.Diagnostics.StackFrame frame = stackTrace.GetFrame(0);

                string fileName = frame.GetFileName();
                int lineNumber = frame.GetFileLineNumber();

                string dataPath = Application.dataPath.Replace("/", "\\");
                fileName = fileName.Replace(dataPath, string.Empty);
                fileName = "Assets" + fileName;

                Debug.Log($"<color=#ffffff>{message}</color>\n<color=#6b6b6b>{fileName}: {lineNumber}</color>");
            }
#else

#if PLATFORM_WEBGL
            LogStyledMessage(message);
#else
            Debug.Log(message);
#endif
#endif
        }

        [DllImport("__Internal")]
        private static extern void LogStyledMessage(string message);

        [DllImport("__Internal")]
        private static extern void LogStyledMessage(string message, string style);

        [AttributeUsage(AttributeTargets.Method)]
        private class InitYG_0Attribute : Attribute { }

        [AttributeUsage(AttributeTargets.Method)]
        private class InitYG_1Attribute : Attribute { }

        [AttributeUsage(AttributeTargets.Method)]
        private class InitYG_2Attribute : Attribute { }

        [AttributeUsage(AttributeTargets.Method)]
        private class InitYGAttribute : Attribute { }

        [AttributeUsage(AttributeTargets.Method)]
        private class StartYGAttribute : Attribute { }
    }
}