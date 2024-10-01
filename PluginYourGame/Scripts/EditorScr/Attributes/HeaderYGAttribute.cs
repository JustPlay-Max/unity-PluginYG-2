using UnityEditor;
using UnityEngine;

namespace YG
{
    public class HeaderYGAttribute : PropertyAttribute
    {
        public string header { get; private set; }
        public Color color { get; private set; }

        public HeaderYGAttribute(string header)
        {
            this.header = header;
            color = new Color(1.0f, 0.5f, 0.0f);
        }

        public HeaderYGAttribute(string header, float r, float g, float b)
        {
            this.header = header;
            color = new Color(r, g, b);
        }

        public HeaderYGAttribute(string header, float r, float g, float b, float a)
        {
            this.header = header;
            color = new Color(r, g, b, a);
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(HeaderYGAttribute))]
    public class YGHeaderDrawer : DecoratorDrawer
    {
        public override void OnGUI(Rect position)
        {
            HeaderYGAttribute coloredHeader = (HeaderYGAttribute)attribute;

            position.y += 10;
            EditorGUI.LabelField(position, coloredHeader.header, EditorScr.TextStyles.Header());
        }

        public override float GetHeight()
        {
            return base.GetHeight() + 20;
        }
    }
#endif
}
