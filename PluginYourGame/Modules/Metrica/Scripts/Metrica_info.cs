using System;
using UnityEngine;
using YG.Insides;

namespace YG
{
    public partial class InfoYG
    {
        public MetricaSettings Metrica = new MetricaSettings();

        [Serializable]
        public class MetricaSettings
        {
            public bool enable;

            [NestedYG(nameof(enable)), Min(0)]
            public int metricaCounterID;

            [NestedYG(nameof(enable))]
            public bool log = true;
        }
    }
}