#if UNITY_EDITOR
namespace YG.Insides
{
    public partial class SimulationInEditor
    {
#if RU_YG2
        [HeaderYG("Серверное время")]
#else
        [HeaderYG("Server time")]
#endif
        public long serverTime = 1721201231000;
    }
}
#endif