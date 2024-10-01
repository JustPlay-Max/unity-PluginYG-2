using System.Collections.Generic;
using YG.Utils.Metrica;

namespace YG
{
    public static partial class YG2
    {
        public static void MetricaSend(string eventName)
        {
            if (!infoYG.Metrica.enable)
                return;

            MetricaMessage(eventName, string.Empty);
#if !UNITY_EDITOR
            iPlatform.MetricaSend(eventName, string.Empty);
#endif
        }

        public static void MetricaSend(string eventName, IDictionary<string, string> eventParams)
        {
            if (!infoYG.Metrica.enable)
                return;

            if (eventParams == null || eventParams.Count == 0)
            {
                MetricaSend(eventName);
                return;
            }

            var eventParamsJson = JsonUtils.ToJson(eventParams);

            if (string.IsNullOrEmpty(eventParamsJson))
            {
                MetricaSend(eventName);
                return;
            }

            MetricaMessage(eventName, eventParamsJson);
#if !UNITY_EDITOR
            iPlatform.MetricaSend(eventName, eventParamsJson);
#endif
        }

        private static void MetricaMessage(string eventName, string eventParams)
        {
            if (!infoYG.Metrica.log)
                return;

            if (string.IsNullOrEmpty(eventParams))
            {
                Message($"Metrica send: {eventName}");
            }
            else
            {
                Message($"Metrica send: {eventName}; {eventParams}");
            }
        }
    }
}
