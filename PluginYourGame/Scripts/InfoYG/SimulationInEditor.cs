#if UNITY_EDITOR
using System;
using UnityEngine;

namespace YG.Insides
{
    [Serializable]
    public partial class SimulationInEditor
    {
        [HeaderYG(Langs.advertisement)]

        [Tooltip(Langs.t_advIntervalSimulation), Min(0)]
        public int advIntervalSimulation = 60;

        [Tooltip(Langs.t_advDurationAdv), Min(0)]
        public float durationAdv = 0.5f;

        [Tooltip(Langs.t_loadAdv), Min(0)]
        public float loadAdv = 0.0f;

#if UNITY_EDITOR
#if RU_YG2
        [Tooltip("Симулирование вызова ошибки при просмотре рекламы.")]
#else
        [Tooltip("Click the check mark to simulate an error call when viewing ads.")]
#endif
        public bool testFailAds;
#endif
    }
}
#endif