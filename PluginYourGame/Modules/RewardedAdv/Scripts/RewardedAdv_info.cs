using System;
using UnityEngine;

namespace YG
{
    public partial class InfoYG
    {
        public RewardedAdvSettings RewardedAdv = new RewardedAdvSettings();

        [Serializable]
        public class RewardedAdvSettings
        {
#if RU_YG2
            [Tooltip("Выдавать вознаграждение за просмотр рекламы только после закрытия рекламы?\n(true = после закрытия, false = сразу после того как таймер закончит свой отчёт)")]
#else
            [Tooltip("Give out rewards for viewing ads only after the ad is closed?\n(true = after closing, false = immediately after the timer finishes its report)")]
#endif
            public bool rewardedAfterClosing = true;
        }
    }
}