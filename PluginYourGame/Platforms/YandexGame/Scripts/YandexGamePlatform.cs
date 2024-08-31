#if YandexGamePlatform
using System.Runtime.InteropServices;

namespace YG
{
    public partial class PlatformYG2 : IPlatformsYG2
    {
        public void InitAwake() { }
        public void InitStart() { }

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

        public void HappyTime() { }
    }
}
#endif