using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using YG.Utils;
using YG.Insides;

namespace YG.EditorScr
{
    [CustomEditor(typeof(PlatformSettings))]
    public class PlatformSettingsEditor : Editor
    {
        private PlatformSettings scr;
        private Texture2D iconPlatform;

        private void OnEnable()
        {
            scr = (PlatformSettings)target;

            string path = AssetDatabase.GetAssetPath(scr);
            string folderPath = System.IO.Path.GetDirectoryName(path);
            string modulName = folderPath.Substring(folderPath.LastIndexOf('/') + 1);
            modulName = folderPath.Substring(folderPath.LastIndexOf('\\') + 1);
            string finalName = modulName + "Platform";

            scr.nameDefining = finalName;

            string iconPath = $"{InfoYG.PATCH_PC_WEBGLTEMPLATES}/{modulName}/thumbnail.png";

            if (File.Exists(iconPath))
            {
                byte[] fileData = File.ReadAllBytes(iconPath);
                iconPlatform = new Texture2D(2, 2);
                iconPlatform.LoadImage(fileData);
            }
        }

        public override void OnInspectorGUI()
        {
            Undo.RecordObject(scr, "Platform Settings Change");

            GUILayout.BeginHorizontal();

            if (iconPlatform)
            {
                Rect textureRect = GUILayoutUtility.GetRect(45, 45, GUILayout.ExpandWidth(false));
                GUI.DrawTexture(textureRect, iconPlatform);
            }

            GUILayout.Space(7);
            GUILayout.BeginVertical();

            GUIStyle styleNamePlatform = TextStyles.While();
            styleNamePlatform.fontSize = 18;
            styleNamePlatform.fontStyle = FontStyle.Bold;
            if (!iconPlatform)
                styleNamePlatform.alignment = TextAnchor.MiddleCenter;

            GUIContent tooltip = new GUIContent(scr.nameDefining, Langs.t_nameDefining);
            EditorGUILayout.LabelField(tooltip, styleNamePlatform);

            styleNamePlatform = TextStyles.LabelStyleColor(Color.gray);
            styleNamePlatform.fontSize = 12;
            styleNamePlatform.fontStyle = FontStyle.Bold;
            if (!iconPlatform)
                styleNamePlatform.alignment = TextAnchor.MiddleCenter;

            tooltip = new GUIContent(Langs.namePlatform.ToUpper(), Langs.t_nameDefining);
            EditorGUILayout.LabelField(tooltip, styleNamePlatform);

            GUILayout.EndVertical();
            GUILayout.EndHorizontal();

            GUILayout.Space(20);

            EditorGUILayout.LabelField(Langs.projectSettings, TextStyles.Header());
            DisplayFieldsWithToggles(scr.projectSettings);

            if (YG2.infoYG.addOptions.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance).Length > 0)
            {
                GUILayout.Space(20);
                EditorGUILayout.LabelField(Langs.addProjectSettings, TextStyles.Header());
                DisplayFieldsWithToggles(YG2.infoYG.addOptions);
            }

            GUILayout.Space(20);
            if (FastButton.Stringy(Langs.applySettingsProject))
                scr.ApplyProjectSettings();

            if (GUI.changed)
                EditorUtility.SetDirty(scr);

            Repaint();
        }

        private void DisplayFieldsWithToggles(object scrObject)
        {
            FieldInfo[] fields = scrObject.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
            object toggleScrObject = YG2.infoYG.platformToggles;
            FieldInfo[] toggleFields = toggleScrObject.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);

            foreach (FieldInfo field in fields)
            {
                bool isToggle = false;

                for (int i = 0; i < toggleFields.Length; i++)
                {
                    FieldInfo toggle = toggleFields[i];

                    if (toggle.Name == field.Name)
                    {
                        EditorGUILayout.BeginHorizontal(YGEditorStyles.selectable);

                        bool toggleValue = (bool)toggle.GetValue(toggleScrObject);
                        bool newToggleValue = EditorGUILayout.Toggle(toggleValue, GUILayout.Width(20));
                        toggle.SetValue(toggleScrObject, newToggleValue);

                        EditorGUI.BeginDisabledGroup(!newToggleValue);
                        DrawField(field, scrObject);
                        EditorGUI.EndDisabledGroup();

                        EditorGUILayout.EndHorizontal();

                        isToggle = true;
                        break;
                    }
                }

                if (!isToggle)
                {
                    EditorGUILayout.BeginHorizontal(YGEditorStyles.selectable);
                    DrawField(field, scrObject);
                    EditorGUILayout.EndHorizontal();
                }
            }
        }

        private void DrawField(FieldInfo field, object target)
        {
            object value = field.GetValue(target);

            if (field.FieldType == typeof(bool))
            {
                field.SetValue(target, EditorGUILayout.Toggle(ObjectNames.NicifyVariableName(field.Name), (bool)value));
            }
            else if (field.FieldType == typeof(string))
            {
                field.SetValue(target, EditorGUILayout.TextField(ObjectNames.NicifyVariableName(field.Name), (string)value));
            }
            else if (field.FieldType == typeof(int))
            {
                field.SetValue(target, EditorGUILayout.IntField(ObjectNames.NicifyVariableName(field.Name), (int)value));
            }
            else if (field.FieldType == typeof(float))
            {
                field.SetValue(target, EditorGUILayout.FloatField(ObjectNames.NicifyVariableName(field.Name), (float)value));
            }
            else if (field.FieldType.IsEnum)
            {
                field.SetValue(target, EditorGUILayout.EnumPopup(ObjectNames.NicifyVariableName(field.Name), (Enum)value));
            }
            else
            {
                EditorGUILayout.LabelField(field.Name, $"Unsupported field type: {field.FieldType}");
            }
        }
    }
}
