#if YandexGamePlatform
using System.Runtime.InteropServices;

namespace YG
{
    public partial class PlatformYG2 : IPlatformsYG2
    {
        [DllImport("__Internal")]
        private static extern bool YandexMetricaSend_js(string eventName, string eventData);

        public void MetricaSend(string eventName, string eventData)
        {
            YandexMetricaSend_js(eventName, eventData);
        }
    }
}
#endif
