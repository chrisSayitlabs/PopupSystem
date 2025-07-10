#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine.Localization;
using UnityEngine;
using UnityEditor.Localization.Editor;
using Codice.Client.BaseCommands.BranchExplorer;

namespace SayItLabs.PopupSystem.Editor
{
    [CustomPropertyDrawer(typeof(PopupInfo))]
    public class PopupInfoDrawer : PropertyDrawer
    {
        private float headerHeight = 20f;
        private float widthOffset = -8f;
        GUIStyle Window = null;
    
        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            if(Window == null)
            {
                Window = GUI.skin.window;
            }
    
            SerializedProperty popupTypeProp = property.FindPropertyRelative("popupType");
    
            EditorGUI.BeginChangeCheck();
            {
                int indent = EditorGUI.indentLevel;
                EditorGUI.indentLevel = 0;
                
                //Rect headerRect = EditorGUI.IndentedRect(rect);
                //headerRect.width = headerRect.width + widthOffset;
                //headerRect.height = headerHeight;
                GUILayout.BeginVertical(label, Window);
                EditorGUILayout.PropertyField(popupTypeProp);
                EPopupType[] enums = (EPopupType[])Enum.GetValues(typeof(EPopupType));
                EPopupType typeSelected = enums[popupTypeProp.enumValueIndex];
                switch(typeSelected)
                {
                    case EPopupType.None:
                        break;
                    case EPopupType.SimplePopup:
                        EditorGUILayout.LabelField("Main Body");
                        DrawLocalizedStringEntry(EPopupStringType.MainBodyText, property);
                        EditorGUILayout.LabelField("Buttons");
                        DrawLocalizedStringEntry(EPopupStringType.SingleButtonLabel, property);
                        break;
                    case EPopupType.YesNoPopup:
                        EditorGUILayout.LabelField("Main Body");
                        DrawLocalizedStringEntry(EPopupStringType.MainBodyText, property);
                        EditorGUILayout.LabelField("Buttons");
                        DrawLocalizedStringEntry(EPopupStringType.FirstButtonLabel, property);
                        DrawLocalizedStringEntry(EPopupStringType.SecondButtonLabel, property);
                        break;
                    case EPopupType.ParentalGatePopup:
                        EditorGUILayout.LabelField("Main Body");
                        DrawLocalizedStringEntry(EPopupStringType.MainBodyText, property);
                        EditorGUILayout.LabelField("Buttons");
                        DrawLocalizedStringEntry(EPopupStringType.FirstButtonLabel, property);
                        DrawLocalizedStringEntry(EPopupStringType.SecondButtonLabel, property);
                        break;
                    case EPopupType.DualVisualPopup:
                        EditorGUILayout.LabelField("Images");
                        EditorGUILayout.PropertyField(property.FindPropertyRelative("firstSprite"));
                        EditorGUILayout.PropertyField(property.FindPropertyRelative("secondSprite"));
                        EditorGUILayout.LabelField("Buttons");
                        DrawLocalizedStringEntry(EPopupStringType.FirstButtonLabel, property);
                        DrawLocalizedStringEntry(EPopupStringType.SecondButtonLabel, property);
                        break;
                }
    
                GUILayout.EndVertical();
            }
        }

        private void DrawLocalizedStringEntry(EPopupStringType popupStringType, SerializedProperty p)
        {
            SerializedProperty strArr = p.FindPropertyRelative("localizedStrings");
            EPopupStringType[] enums = (EPopupStringType[])Enum.GetValues(typeof(EPopupStringType));
            if (strArr.arraySize < enums.Length)
                strArr.arraySize = enums.Length;

            EditorGUILayout.PropertyField(strArr.GetArrayElementAtIndex((int)popupStringType), new GUIContent(PopupStringTypeToString(popupStringType)));
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 0f;
        }

        private string PopupStringTypeToString(EPopupStringType pst)
        {
            return System.Text.RegularExpressions.Regex.Replace(pst.ToString(), "([A-Z])", " $1", System.Text.RegularExpressions.RegexOptions.Compiled).Trim();
        }
    }
}
#endif