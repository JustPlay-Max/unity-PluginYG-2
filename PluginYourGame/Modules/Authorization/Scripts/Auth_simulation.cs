#if UNITY_EDITOR
using UnityEngine;

namespace YG.Insides
{
    public partial class SimulationInEditor
    {
        [HeaderYG(Langs.playerData)]
        public bool authorized = true;
        public string playerName = "Player current";
        public string uniqueID = "000";
        public string playerPhoto = InfoYG.DEMO_IMAGE;
        [Tooltip(Langs.t_payingStatus)]
        public YG2.PayingStatus payingStatus;
    }
}
#endif