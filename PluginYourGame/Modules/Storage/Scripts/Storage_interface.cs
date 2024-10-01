using YG.Insides;

namespace YG
{
    public partial interface IPlatformsYG2
    {
        void InitStorage() => YGInsides.LoadProgress();
        void SaveCloud() { }
        void LoadCloud() => YGInsides.LoadLocal();
    }
}
