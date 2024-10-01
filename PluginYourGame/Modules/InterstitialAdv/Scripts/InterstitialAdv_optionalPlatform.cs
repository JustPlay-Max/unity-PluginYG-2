namespace YG.Insides
{
    public partial class OptionalPlatform
    {
        public void FirstInterAdvShow() => YG2.iPlatform.FirstInterAdvShow();
        public void OtherInterAdvShow() => YG2.iPlatform.OtherInterAdvShow();
        public void LoadInterAdv() => YG2.iPlatform.LoadInterAdv();

        private static bool firstInterAd;
        public static void FirstInterAdvShow_RealizationSkip()
        {
            if (!firstInterAd)
            {
                firstInterAd = true;
                return;
            }

            YG2.InterstitialAdvShow();
        }
    }
}