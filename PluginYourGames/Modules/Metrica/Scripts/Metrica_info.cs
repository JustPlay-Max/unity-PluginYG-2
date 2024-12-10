using System;
using UnityEngine;

namespace YG
{
    public partial class InfoYG
    {
        public MetricaSettings Metrica = new MetricaSettings();

        [Serializable]
        public partial class MetricaSettings
        {
            public bool enable;

            [NestedYG(nameof(enable)), Min(0)]
            public int metricaCounterID;

            [NestedYG(nameof(enable))]
            public bool log = true;
        }
    }
}