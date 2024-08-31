using UnityEngine;
using System.Runtime.InteropServices;
using System;

namespace YG
{
    [DefaultExecutionOrder(-5000)]
    public static partial class YG2
    {
        public static InfoYG infoYG { get { return InfoYG.Inst(); } }

        public static IPlatformsYG2 iPlatform;
        public static YG2Instance instance;
        public static bool SDKEnabled { get => _SDKEnabled; }
        private static bool _SDKEnabled;

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
            instance = null;
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
            instance = YGObj.AddComponent<YG2Instance>();

            iPlatform.InitAwake();

            CallInitYG_0();
            CallInitYG_1();
            CallInitYG_2();
            CallInitYG();
        }

        public static void Start()
        {
            if (!_SDKEnabled)
            {
                iPlatform.InitStart();
                CallStartYG();
                _SDKEnabled = true;
                GetDataInvoke();
                iPlatform.InitComplete();
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
    }
}