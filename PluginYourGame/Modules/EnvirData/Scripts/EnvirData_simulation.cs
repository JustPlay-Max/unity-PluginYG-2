#if UNITY_EDITOR
namespace YG.Insides
{
    public partial class SimulationInEditor
    {
        [HeaderYG(Langs.environmentData)]
        public YG2.Device device;
        public string language = "ru";
    }
}
#endif