#if YandexGamesPlatform_yg
using System.Runtime.InteropServices;

namespace YG
{
    public partial class PlatformYG2 : IPlatformsYG2
    {
        [DllImport("__Internal")]
        private static extern bool YandexMetricaSend_js(string eventName);

        public void MetricaSend(string eventName)
        {
            YandexMetricaSend_js(eventName);
        }

        [DllImport("__Internal")]
        private static extern bool YandexMetricaSend2_js(string eventName, string eventData);

        public void MetricaSend(string eventName, string eventData)
        {
            YandexMetricaSend2_js(eventName, eventData);
        }
    }
}
#endif
