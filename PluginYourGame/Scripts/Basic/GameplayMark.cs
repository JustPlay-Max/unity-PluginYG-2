
namespace YG
{
    public partial interface IPlatformsYG2
    {
        void GameplayStart() { }
        void GameplayStop() { }
        void HappyTime() { }
    }

    public static partial class YG2
    {
        private static bool gamePlaying;
        public static bool isGamePlaying { get { return gamePlaying; } }
        private static bool saveGameplayState;

        public static void GameplayStart(bool useSaveGameplayState = false)
        {
            if (useSaveGameplayState && (!saveGameplayState || nowAdsShow || !isFocusWindowGame))
                return;

            if (!gamePlaying)
            {
                gamePlaying = true;
                Message("Gameplay Start");
                iPlatform.GameplayStart();
            }
        }

        public static void GameplayStop(bool useSaveGameplayState = false)
        {
            if (useSaveGameplayState && !nowAdsShow && isFocusWindowGame)
                saveGameplayState = gamePlaying;

            if (gamePlaying)
            {
                gamePlaying = false;
                Message("Gameplay Stop");
                iPlatform.GameplayStop();
            }
        }

        public static void HappyTime()
        {
            iPlatform.HappyTime();
        }
    }

    public partial class YG2Instance
    {
        public void GameplayStart() => YG2.GameplayStart();
        public void GameplayStop() => YG2.GameplayStop();
    }
}