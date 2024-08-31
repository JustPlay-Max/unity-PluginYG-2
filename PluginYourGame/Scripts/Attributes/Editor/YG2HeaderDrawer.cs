using UnityEditor;
using UnityEngine;
using YG.Utils;

namespace YG.Insides
{
    [CustomPropertyDrawer(typeof(HeaderYG2Attribute))]
    public class YG2HeaderDrawer : DecoratorDrawer
    {
        public override void OnGUI(Rect position)
        {
            HeaderYG2Attribute coloredHeader = (HeaderYG2Attribute)attribute;

            position.y += 10;
            EditorGUI.LabelField(position, coloredHeader.header, TextStyles.Header());
        }

        public override float GetHeight()
        {
            return base.GetHeight() + 20;
        }
    }
}