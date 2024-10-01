using System;
using UnityEngine;
using YG.Utils.LB;
using System.Linq;
using YG.Insides;
#if UNITY_EDITOR
using YG.EditorScr;
#endif

namespace YG
{
    public partial class YG2
    {
        public static Action<LBData> onGetLeaderboard;

#if UNITY_EDITOR
        [InitYG]
        private static void ResetStaticLB()
        {
            onGetLeaderboard = null;
        }
#endif

        public static void SetLeaderboard(string nameLB, long score)
        {
            const long maxNum = 9007199254740991;
            if (score > maxNum)
                score = 9007199254740991;

            if (infoYG.Leaderboards.enable && player.auth)
            {
                if (infoYG.Leaderboards.saveScoreAnonymousPlayers == false && player.name == InfoYG.ANONYMOUS)
                    return;

#if !UNITY_EDITOR
                Message("Set Leaderboard: " + score);
                iPlatform.SetLeaderboard(nameLB, score);
#else
                Message($"Set Leaderboard «{nameLB}»: {score}");
#endif
            }
        }

        public static void SetLBTimeConvert(string nameLB, float secondsScore)
        {
            if (infoYG.Leaderboards.enable && player.auth)
            {
                if (infoYG.Leaderboards.saveScoreAnonymousPlayers == false && player.name == InfoYG.ANONYMOUS)
                    return;

                int result;
                int indexComma = secondsScore.ToString().IndexOf(",");

                if (secondsScore < 1)
                {
                    Debug.LogError("You can't record a record below zero!");
                    return;
                }
                else if (indexComma <= 0)
                {
                    result = (int)(secondsScore);
                }
                else
                {
                    string rec = secondsScore.ToString();
                    string sec = rec.Remove(indexComma);
                    string milSec = rec.Remove(0, indexComma + 1);

                    if (milSec.Length > 3)
                        milSec = milSec.Remove(3);
                    else if (milSec.Length == 2)
                        milSec += "0";
                    else if (milSec.Length == 1)
                        milSec += "00";

                    rec = sec + milSec;
                    result = int.Parse(rec);
                }

                SetLeaderboard(nameLB, result);
            }
        }

        public static void GetLeaderboard(string nameLB, int quantityTop, int quantityAround, string photoSizeLB)
        {
#if !UNITY_EDITOR
            iPlatform.GetLeaderboard(nameLB, quantityTop, quantityAround, photoSizeLB);
#else
            if (infoYG.Leaderboards.enable)
            {
                Message($"Get Leaderboard «{nameLB}»");

                LBData lb = null;
                LBData[] LBs = new LBData[infoYG.simulationInEditor.leaderboards.Length];

                for (int i = 0; i < infoYG.simulationInEditor.leaderboards.Length; i++)
                    LBMethods.CopyLBData(out LBs[i], infoYG.simulationInEditor.leaderboards[i]);

                for (int i = 0; i < LBs.Length; i++)
                {
                    if (nameLB == LBs[i].technoName)
                    {
                        lb = LBs[i];

                        foreach (LBPlayerData playerData in lb.players)
                        {
                            if (playerData.name == InfoYG.ANONYMOUS)
                            {
                                playerData.photo = InfoYG.ANONYMOUS;
                                continue;
                            }

                            if (playerData.photo == InfoYG.DEMO_IMAGE)
                                playerData.photo = ServerInfo.saveInfo.playerImage;
                        }
                        break;
                    }
                }

                if (lb != null)
                {
                    lb.players = lb.players.OrderBy(item => item.score).ToArray();

                    if (!lb.isInvertSortOrder)
                        Array.Reverse(lb.players);

                    for (int i = 0; i < lb.players.Length; i++)
                        lb.players[i].rank = i + 1;

                    onGetLeaderboard?.Invoke(lb);
                }
                else
                {
                    onGetLeaderboard?.Invoke(YGInsides.NoLBData(nameLB));
                }
            }
            else
            {
                onGetLeaderboard?.Invoke(YGInsides.NoLBData(nameLB));
            }
#endif
        }

        public static void GetLeaderboard(string nameLB, int quantityTop, int quantityAround)
        {
            GetLeaderboard(nameLB, quantityTop, quantityAround, "small");
        }

        public static void GetLeaderboard(string nameLB)
        {
            GetLeaderboard(nameLB, 3, 3, "small");
        }
    }
}

namespace YG.Insides
{
    public static partial class YGInsides
    {
        public static LBData NoLBData(string nameLB)
        {
            LBData lb = new LBData()
            {
                technoName = nameLB,
                entries = InfoYG.NO_DATA,
                players = new LBPlayerData[1]
                {
                    new LBPlayerData()
                    {
                        name = InfoYG.NO_DATA,
                        photo = null
                    }
                }
            };
            return lb;
        }
    }
}
