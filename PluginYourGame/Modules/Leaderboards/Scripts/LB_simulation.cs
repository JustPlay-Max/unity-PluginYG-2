#if UNITY_EDITOR
using YG.Utils.LB;

namespace YG.Insides
{
    public partial class SimulationInEditor
    {
#if RU_YG2
        [HeaderYG("Лидерборды")]
#else
        [HeaderYG("Leaderboards")]
#endif
        public LBData[] leaderboards = new LBData[]
        {
            new LBData
            {
                technoName = "test",
                entries = "1. Max: 10\n2. Masha: 15\n3. anonymous: 23\n4. Player 4: 30\n5. Player current: 40\n6. Player 6: 50\n7. Player 7: 60\n8. Player 8: 70\n9. Player 9: 80\n10. Player 10: 90",
                players = new LBPlayerData[10]
                {
                    new LBPlayerData { name = "Max", rank = 1, score = 100, uniqueID = "123", photo = InfoYG.DEMO_IMAGE },
                    new LBPlayerData { name = "Masha", rank = 2, score = 90, uniqueID = "321", photo = InfoYG.DEMO_IMAGE },
                    new LBPlayerData { name = "anonymous", rank = 3, score = 80, uniqueID = "456", photo = InfoYG.ANONYMOUS },
                    new LBPlayerData { name = "Player 4", rank = 4, score = 70, uniqueID = "321", photo = InfoYG.DEMO_IMAGE },
                    new LBPlayerData { name = "Player current", rank = 5, score = 60, uniqueID = "000", photo = InfoYG.DEMO_IMAGE },
                    new LBPlayerData { name = "Player 6", rank = 6, score = 50, uniqueID = "326", photo = InfoYG.DEMO_IMAGE },
                    new LBPlayerData { name = "Player 7", rank = 7, score = 40, uniqueID = "327", photo = InfoYG.DEMO_IMAGE },
                    new LBPlayerData { name = "Player 8", rank = 8, score = 30, uniqueID = "328", photo = InfoYG.DEMO_IMAGE },
                    new LBPlayerData { name = "Player 9", rank = 9, score = 20, uniqueID = "329", photo = InfoYG.DEMO_IMAGE },
                    new LBPlayerData { name = "Player 10", rank = 10, score = 10, uniqueID = "330", photo = InfoYG.DEMO_IMAGE }
                },
                type = "numeric",
                currentPlayer = new LBCurrentPlayerData
                {
                    rank = 2,
                    score = 10,
                }
            },
            new LBData
            {
                technoName = "time",
                isInvertSortOrder = true,
                entries = "1. Max: 7123\n2. Maria: 15321\n3. anonymous: 62000\n4. Player 4: 122000\n5. Player current: 127000\n6. Player 6: 340000",
                players = new LBPlayerData[6]
                {
                    new LBPlayerData { name = "Max", rank = 1, score = 7123, uniqueID = "789", photo = InfoYG.DEMO_IMAGE },
                    new LBPlayerData { name = "Maria", rank = 2, score = 15321, uniqueID = "987", photo = InfoYG.DEMO_IMAGE },
                    new LBPlayerData { name = "anonymous", rank = 3, score = 62000, uniqueID = "891", photo = InfoYG.ANONYMOUS },
                    new LBPlayerData { name = "Player 4", rank = 4, score = 122000, uniqueID = "321", photo = InfoYG.DEMO_IMAGE },
                    new LBPlayerData { name = "Player current", rank = 5, score = 127000, uniqueID = "000", photo = InfoYG.DEMO_IMAGE },
                    new LBPlayerData { name = "Player 6", rank = 6, score = 340000, uniqueID = "321", photo = InfoYG.DEMO_IMAGE }
                },
                type = "numeric",
                currentPlayer = new LBCurrentPlayerData
                {
                    rank = 2,
                    score = 10,
                }
            }
        };
    }
}
#endif