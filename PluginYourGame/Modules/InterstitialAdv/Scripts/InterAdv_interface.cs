namespace YG
{
    public partial interface IPlatformsYG2
    {
        void InterstitialAdvShow() { }
        void FirstInterAdvShow() => InterstitialAdvShow();
        void OtherInterAdvShow() => InterstitialAdvShow();
        void LoadInterAdv() { }
    }
}