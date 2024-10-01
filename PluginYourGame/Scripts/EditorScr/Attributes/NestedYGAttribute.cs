using UnityEngine;
using UnityEditor;

namespace YG
{
    public sealed class NestedYGAttribute : PropertyAttribute
    {
        public string[] propertyNames { get; }

        public NestedYGAttribute(params string[] propNames)
        {
            propertyNames = propNames;
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(NestedYGAttribute))]
    public class NestedYGDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (ShouldDisplay(property))
            {
                float offset = 20f;
                float angleSize = 10f;

                NestedYGAttribute attr = (NestedYGAttribute)attribute;

                if (attr.propertyNames != null && attr.propertyNames.Length > 1)
                {
                    offset *= attr.propertyNames.Length;
                }

                Rect newPosition = new Rect(position.x + offset, position.y, position.width - offset, position.height);

                Handles.color = Color.grey;

                Handles.DrawLine(new Vector3(position.x + offset, position.y), new Vector3(position.x + offset, position.y + angleSize));
                Handles.DrawLine(new Vector3(position.x + offset, position.y + angleSize), new Vector3(position.x + offset + angleSize, position.y + angleSize));

                EditorGUI.PropertyField(newPosition, property, label, includeChildren: true);
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
            NestedYGAttribute attr = (NestedYGAttribute)attribute;

            if (attr.propertyNames == null || attr.propertyNames.Length == 0)
                return true;

            for (int i = 0; i < attr.propertyNames.Length; i++)
            {
                SerializedProperty dependentProp = FindPropertyRelative(property, attr.propertyNames[i]);

                if (dependentProp == null)
                {
                    Debug.LogError($"[{nameof(NestedYGAttribute)}] {property.name};  Field '{attr.propertyNames[i]}' not foud!");
                    return true;
                }

                if (!dependentProp.boolValue)
                    return false;
            }

            return true;
        }

        private SerializedProperty FindPropertyRelative(SerializedProperty property, string propertyPath)
        {
            if (property == null)
                return null;

            string path = property.propertyPath.Replace(property.name, propertyPath);
            return property.serializedObject.FindProperty(path);
        }
    }
#endif
}
