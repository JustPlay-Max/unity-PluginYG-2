#if YandexGamesPlatform_yg
using System.Runtime.InteropServices;
using System.Collections.Generic;
using YG.Utils;

namespace YG
{
    public partial class PlatformYG2 : IPlatformsYG2
    {
        [DllImport("__Internal")]
        private static extern string InitStats_js();

        public void InitStats()
        {
            YG2.ReceiveStatsJson(InitStats_js());
        }

        [DllImport("__Internal")]
        private static extern void GetStats_js();
        public void LoadStats()
        {
            GetStats_js();
        }

        [DllImport("__Internal")]
        private static extern void SetState_js(string key, int value);
        public void SetState(string key, int value)
        {
            SetState_js(key, value);
        }

        [DllImport("__Internal")]
        private static extern void SetAllStats_js(string jsonStats);
        public void SetAllStats(Dictionary<string, int> stats)
        {
            SetAllStats_js(JsonYG.SerializeDictionary(stats));
        }
    }
}
namespace YG.Insides
{
    public partial class YGSendMessage
    {
        public void ReceiveStats(string json) => YG2.ReceiveStatsJson(json);
    }
}
#endif