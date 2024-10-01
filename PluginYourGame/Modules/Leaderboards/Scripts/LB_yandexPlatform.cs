#if YandexGamePlatform
using System.Runtime.InteropServices;
using UnityEngine;
using YG.Utils.LB;
using YG.Insides;

namespace YG
{
    public partial class PlatformYG2 : IPlatformsYG2
    {
        public class JsonLB
        {
            public string technoName;
            public bool isDefault;
            public bool isInvertSortOrder;
            public int decimalOffset;
            public string type;
            public string entries;
            public int[] ranks;
            public string[] photos;
            public string[] names;
            public int[] scores;
            public string[] uniqueIDs;
        }

        [DllImport("__Internal")]
        private static extern void SetLeaderboard_js(string nameLB, long score);

        public void SetLeaderboard(string nameLB, long score)
        {
            SetLeaderboard_js(nameLB, score);
        }

        [DllImport("__Internal")]
        private static extern void GetLeaderboard_js(string nameLB, int quantityTop, int quantityAround, string photoSizeLB, bool auth);

        public void GetLeaderboard(string nameLB, int quantityTop, int quantityAround, string photoSizeLB)
        {
            if (YG2.infoYG.Leaderboards.enable)
            {
                YG2.Message("Get Leaderboard");
                GetLeaderboard_js(nameLB, quantityTop, quantityAround, photoSizeLB, YG2.player.auth);
            }
            else
            {
                YG2.onGetLeaderboard?.Invoke(YGInsides.NoLBData(nameLB));
            }
        }
    }
}

namespace YG.Insides
{
    public partial class YGSendMessage
    {
        public void LeaderboardEntries(string data)
        {
            PlatformYG2.JsonLB jsonLB = JsonUtility.FromJson<PlatformYG2.JsonLB>(data);

            LBData lbData = new LBData()
            {
                technoName = jsonLB.technoName,
                isDefault = jsonLB.isDefault,
                isInvertSortOrder = jsonLB.isInvertSortOrder,
                decimalOffset = jsonLB.decimalOffset,
                type = jsonLB.type,
                entries = jsonLB.entries,
                players = new LBPlayerData[jsonLB.names.Length],
                currentPlayer = null
            };

            for (int i = 0; i < jsonLB.names.Length; i++)
            {
                lbData.players[i] = new LBPlayerData();
                lbData.players[i].name = jsonLB.names[i];
                lbData.players[i].rank = jsonLB.ranks[i];
                lbData.players[i].score = jsonLB.scores[i];
                lbData.players[i].photo = jsonLB.photos[i];
                lbData.players[i].uniqueID = jsonLB.uniqueIDs[i];

                if (jsonLB.uniqueIDs[i] == YG2.player.id)
                {
                    lbData.currentPlayer = new LBCurrentPlayerData
                    {
                        rank = jsonLB.ranks[i],
                        score = jsonLB.scores[i]
                    };
                }
            }

            YG2.onGetLeaderboard?.Invoke(lbData);
        }
    }
}
#endif
