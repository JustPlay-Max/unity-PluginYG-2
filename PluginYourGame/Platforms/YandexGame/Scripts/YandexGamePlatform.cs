#if YandexGamePlatform
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace YG
{
    public partial class PlatformYG2 : IPlatformsYG2
    {
        [DllImport("__Internal")]
        private static extern bool IsInitSDK_js();
        public void InitAwake()
        {
            if (YG2.infoYG.basicSettings.syncInitSDK)
            {
#if !UNITY_EDITOR
                if (IsInitSDK_js())
                    YG2.SyncInitialization();
#else
                DelayInitSimulation();
#endif
            }
        }
#if UNITY_EDITOR
        private async void DelayInitSimulation()
        {
            await Task.Delay(1000);
            YG2.SyncInitialization();
        }
#endif

        [DllImport("__Internal")]
        private static extern void InitGame_js();
        public void InitComplete()
        {
#if !UNITY_EDITOR
            InitGame_js();
#endif
        }

        [DllImport("__Internal")]
        private static extern void GameReadyAPI_js();

        public void GameReadyAPI()
        {
#if !UNITY_EDITOR
            GameReadyAPI_js();
#endif
        }

        [DllImport("__Internal")]
        private static extern void GameplayStart_js();

        public void GameplayStart()
        {
#if !UNITY_EDITOR
            GameplayStart_js();
#endif
        }

        [DllImport("__Internal")]
        private static extern void GameplayStop_js();

        public void GameplayStop()
        {
#if !UNITY_EDITOR
            GameplayStop_js();
#endif
        }
    }
}

namespace YG.Insides
{
    public partial class YGSendMessage
    {
        public void InitSDKComplete() => YG2.SyncInitialization();
    }
}
#endif