namespace YG
{
    public partial interface IPlatformsYG2
    {
        void SetLeaderboard(string nameLB, long score) { }
        void GetLeaderboard(string nameLB, int quantityTop, int quantityAround, string photoSizeLB) { }
    }
}