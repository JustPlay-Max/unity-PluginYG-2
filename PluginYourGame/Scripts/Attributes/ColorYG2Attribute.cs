using UnityEditor;
using UnityEngine;

namespace YG.Insides
{
    public class ColorYG2Attribute : PropertyAttribute
    {
        public Color color { get; private set; }

        public ColorYG2Attribute(float r, float g, float b)
        {
            color = new Color(r, g, b);
        }

        public ColorYG2Attribute(float r, float g, float b, float a)
        {
            color = new Color(r, g, b, a);
        }

        public ColorYG2Attribute()
        {
            color = new Color(1.3f, 1.3f, 1.0f);
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ColorYG2Attribute))]
    public class ColorYG2AttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ColorYG2Attribute colorAttribute = (ColorYG2Attribute)attribute;
            Color previousColor = GUI.color;
            GUI.color = colorAttribute.color;

            if (property.propertyType == SerializedPropertyType.Generic)
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
            else
            {
                EditorGUI.PropertyField(position, property, label);
            }

            GUI.color = previousColor;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }
    }
#endif
}