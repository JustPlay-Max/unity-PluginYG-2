using UnityEditor;

namespace YG.EditorScr
{
    [CustomEditor(typeof(AdNotificationYG))]
    public class AdNotificationYGEditor : Editor
    {
        AdNotificationYG scr;

        private void OnEnable()
        {
            scr = (AdNotificationYG)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (WarningPostponeCall.Draw())
                Repaint();
        }
    }
}