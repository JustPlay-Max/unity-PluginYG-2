using UnityEditor;

namespace YG.EditorScr
{
    [CustomEditor(typeof(TimerBeforeAdsYG))]
    public class TimerBeforeAdsYGEditor : Editor
    {
        TimerBeforeAdsYG scr;

        private void OnEnable()
        {
            scr = (TimerBeforeAdsYG)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (WarningPostponeCall.Draw())
                Repaint();
        }
    }
}