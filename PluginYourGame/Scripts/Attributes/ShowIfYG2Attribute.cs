using UnityEngine;
using UnityEditor;

namespace YG.Insides
{
    public sealed class ShowIfYG2Attribute : PropertyAttribute
    {
        public string propertyName { get; }

        public ShowIfYG2Attribute(string propName)
        {
            propertyName = propName;
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ShowIfYG2Attribute))]
    public class ShowIfYG2Drawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (ShouldDisplay(property))
            {
                using (new EditorGUI.IndentLevelScope())
                {
                    EditorGUI.PropertyField(position, property, label, includeChildren: true);
                }
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return ShouldDisplay(property)
                ? EditorGUI.GetPropertyHeight(property, label, includeChildren: true)
                : 0;
        }

        private bool ShouldDisplay(SerializedProperty property)
        {
            var attr = (ShowIfYG2Attribute)attribute;
            var dependentProp = FindPropertyRelative(property, attr.propertyName);
            return dependentProp != null && dependentProp.boolValue;
        }

        private SerializedProperty FindPropertyRelative(SerializedProperty property, string propertyPath)
        {
            if (property == null)
                return null;

            var path = property.propertyPath.Replace(property.name, propertyPath);
            return property.serializedObject.FindProperty(path);
        }
    }
#endif
}
