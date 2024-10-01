#if YandexGamePlatform
using System.Runtime.InteropServices;
using UnityEngine;
using YG.Insides;
#if NJSON_STORAGE_YG2
using Newtonsoft.Json;
#endif

namespace YG
{
    public partial class PlatformYG2 : IPlatformsYG2
    {
        [DllImport("__Internal")]
        private static extern string InitCloudStorage_js();
        public void InitStorage()
        {
            YGInsides.SetLoadSaves(InitCloudStorage_js());
        }

        [DllImport("__Internal")]
        private static extern void SaveYG(string jsonData, bool flush);
        public void SaveCloud()
        {
#if NJSON_STORAGE_YG2
            SaveYG(JsonConvert.SerializeObject(YG2.saves), YG2.infoYG.Storage.flush);
#else
            SaveYG(JsonUtility.ToJson(YG2.saves), YG2.infoYG.Storage.flush);
#endif
        }

        [DllImport("__Internal")]
        private static extern string LoadYG();
        public void LoadCloud()
        {
            LoadYG();
        }
    }
}

namespace YG.Insides
{
    public partial class YGSendMessage
    {
        public void SetLoadSaves(string data) => YGInsides.SetLoadSaves(data);
    }
}
#endif