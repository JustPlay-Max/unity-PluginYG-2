using System;
using UnityEngine;

namespace YG
{
    public partial class InfoYG
    {
        public LeaderboardsSettings Leaderboards = new LeaderboardsSettings();

        [Serializable]
        public class LeaderboardsSettings
        {
#if RU_YG2
            [Tooltip("Вкл/Выкл лидерборды")]
#else
            [Tooltip("On/Off Leaderboards")]
#endif
            public bool enable = true;
#if UNITY_EDITOR
            [NestedYG(nameof(enable))]
#endif
            public bool saveScoreAnonymousPlayers = true;
        }
    }
}