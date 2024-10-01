#if UNITY_EDITOR
namespace YG.Insides
{
    public partial class SimulationInEditor
    {
#if RU_YG2
        [HeaderYG("Флаги")]
#else
        [HeaderYG("Flags")]
#endif
        public YG2.Flag[] flags;
    }
}
#endif