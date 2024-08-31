#if UNITY_EDITOR
using System;
using UnityEngine;

namespace YG.Insides
{
    [Serializable]
    public partial class SimulationInEditor
    {
        [HeaderYG2(Langs.advertisement)]

        [Tooltip(Langs.t_advIntervalSimulation), Min(0)]
        public int advIntervalSimulation = 60;

        [Tooltip(Langs.t_advDurationAdv), Min(0)]
        public float durationAdv = 0.5f;

        [Tooltip(Langs.t_loadAdv), Min(0)]
        public float loadAdv = 0.0f;
    }
}
#endif