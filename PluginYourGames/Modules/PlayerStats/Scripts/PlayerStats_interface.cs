using System.Collections.Generic;
using YG.Utils;

namespace YG
{
    public partial interface IPlatformsYG2
    {
        private const string KEY_STATES = "PlayerStatsYG";

        void InitStats() => YG2.ReceiveStatsJson(LocalStorage.GetKey(KEY_STATES));

        void LoadStats() => YG2.ReceiveStatsJson(LocalStorage.GetKey(KEY_STATES));

        void SetState(string key, int value) => LocalStorage.SetKey(KEY_STATES, JsonYG.SerializeDictionary(YG2.GetAllStats()));

        void SetAllStats(Dictionary<string, int> stats) => LocalStorage.SetKey(KEY_STATES, JsonYG.SerializeDictionary(YG2.GetAllStats()));
    }
}