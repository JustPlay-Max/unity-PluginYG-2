using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace YG.EditorScr
{
    [CustomEditor(typeof(EventsYG2), true)]
    public class EventsYG2Editor : Editor
    {
        SerializedProperty m_DelegatesProperty;

        GUIContent m_IconToolbarMinus;
        GUIContent m_EventIDName;
        GUIContent[] m_EventTypes;
        GUIContent m_AddButonContent;

        protected virtual void OnEnable()
        {
#if RU_YG2
            m_AddButonContent = EditorGUIUtility.TrTextContent("Добавить новый тип события");
#else
            m_AddButonContent = EditorGUIUtility.TrTextContent("Add New Event Type");
#endif
            m_DelegatesProperty = serializedObject.FindProperty("m_Delegates");
            m_EventIDName = new GUIContent("");
            m_IconToolbarMinus = new GUIContent(EditorGUIUtility.IconContent("Toolbar Minus"));
            m_IconToolbarMinus.tooltip = "Remove all events in this list.";

            m_EventTypes = new GUIContent[]
            {
                new GUIContent("GetSDKData"),
                new GUIContent("PauseStopGame"),
                new GUIContent("PauseResumeGame"),
    #if InterstitialAdv
                new GUIContent("OpenInterAdv"),
                new GUIContent("CloseInterAdv"),
                new GUIContent("ErrorInterAdv"),
    #endif
    #if RewardedAdv
                new GUIContent("OpenRewardedAdv"),
                new GUIContent("CloseRewaededAdv"),
                new GUIContent("RewardAdv"),
                new GUIContent("ErrorRewardedAdv"),
    #endif
    #if Authorization
                new GUIContent("AuthTrue"),
                new GUIContent("AuthFalse"),
    #endif
    #if Payments
                new GUIContent("PurchaseSuccess"),
                new GUIContent("PurchaseFailed"),
    #endif
    #if Review
                new GUIContent("ReviewSuccess"),
                new GUIContent("ReviewFailed"),
    #endif
    #if GameLabel
                new GUIContent("GameLabelSuccess"),
                new GUIContent("GameLabelFail")
#endif
            };
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            int toBeRemovedEntry = -1;

            EditorGUILayout.Space();

            Vector2 removeButtonSize = GUIStyle.none.CalcSize(m_IconToolbarMinus);

            for (int i = 0; i < m_DelegatesProperty.arraySize; ++i)
            {
                SerializedProperty delegateProperty = m_DelegatesProperty.GetArrayElementAtIndex(i);
                SerializedProperty eventProperty = delegateProperty.FindPropertyRelative("eventID");
                SerializedProperty callbacksProperty = delegateProperty.FindPropertyRelative("callback");
                m_EventIDName.text = eventProperty.enumDisplayNames[eventProperty.enumValueIndex];

                EditorGUILayout.PropertyField(callbacksProperty, m_EventIDName);
                Rect callbackRect = GUILayoutUtility.GetLastRect();

                Rect removeButtonPos = new Rect(callbackRect.xMax - removeButtonSize.x - 8, callbackRect.y + 1, removeButtonSize.x, removeButtonSize.y);
                if (GUI.Button(removeButtonPos, m_IconToolbarMinus, GUIStyle.none))
                {
                    toBeRemovedEntry = i;
                }

                EditorGUILayout.Space();
            }

            if (toBeRemovedEntry > -1)
            {
                RemoveEntry(toBeRemovedEntry);
            }

            Rect btPosition = GUILayoutUtility.GetRect(m_AddButonContent, GUI.skin.button);
            const float addButonWidth = 200f;
            btPosition.x = btPosition.x + (btPosition.width - addButonWidth) / 2;
            btPosition.width = addButonWidth;
            if (GUI.Button(btPosition, m_AddButonContent, YGEditorStyles.button))
            {
                ShowAddTriggermenu();
            }

            serializedObject.ApplyModifiedProperties();
            Repaint();
        }

        private void RemoveEntry(int toBeRemovedEntry)
        {
            m_DelegatesProperty.DeleteArrayElementAtIndex(toBeRemovedEntry);
        }

        void ShowAddTriggermenu()
        {
            GenericMenu menu = new GenericMenu();
            for (int i = 0; i < m_EventTypes.Length; ++i)
            {
                bool active = true;

                for (int p = 0; p < m_DelegatesProperty.arraySize; ++p)
                {
                    SerializedProperty delegateEntry = m_DelegatesProperty.GetArrayElementAtIndex(p);
                    SerializedProperty eventProperty = delegateEntry.FindPropertyRelative("eventID");
                    if (eventProperty.enumValueIndex == i)
                    {
                        active = false;
                    }
                }
                if (active)
                    menu.AddItem(m_EventTypes[i], false, OnAddNewSelected, i);
                else
                    menu.AddDisabledItem(m_EventTypes[i]);
            }
            menu.ShowAsContext();
            Event.current.Use();
        }

        private void OnAddNewSelected(object index)
        {
            int selected = (int)index;

            m_DelegatesProperty.arraySize += 1;
            SerializedProperty delegateEntry = m_DelegatesProperty.GetArrayElementAtIndex(m_DelegatesProperty.arraySize - 1);
            SerializedProperty eventProperty = delegateEntry.FindPropertyRelative("eventID");
            eventProperty.enumValueIndex = selected;
            serializedObject.ApplyModifiedProperties();
        }
    }
}
