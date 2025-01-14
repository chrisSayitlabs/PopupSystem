using System;
using UnityEditor;
using UnityEngine;

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
                
                Rect headerRect = EditorGUI.IndentedRect(rect);
                headerRect.width = headerRect.width + widthOffset;
                headerRect.height = headerHeight;
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
                        EditorGUILayout.PropertyField(property.FindPropertyRelative("mainBodyText"));
                        EditorGUILayout.LabelField("Buttons");
                        EditorGUILayout.PropertyField(property.FindPropertyRelative("singleButtonLabel"));
                        break;
                    case EPopupType.YesNoPopup:
                        EditorGUILayout.LabelField("Main Body");
                        EditorGUILayout.PropertyField(property.FindPropertyRelative("mainBodyText"));
                        EditorGUILayout.LabelField("Buttons");
                        EditorGUILayout.PropertyField(property.FindPropertyRelative("leftButtonLabel"));
                        EditorGUILayout.PropertyField(property.FindPropertyRelative("rightButtonLabel"));
                        break;
                }
    
                GUILayout.EndVertical();
            }
        }
    
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 2f + EditorGUIUtility.standardVerticalSpacing;
        }
    }
}