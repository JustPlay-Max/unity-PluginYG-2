namespace YG
{
    public partial interface IPlatformsYG2
    {
        void InitAwake() { }
        void InitStart() { }
        void InitComplete() { }
    }

    public partial class PlatformYG2 : IPlatformsYG2 { }
}