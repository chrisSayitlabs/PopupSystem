using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

namespace SayItLabs.PopupSystem
{
    public enum EPopupType
    {
        None,
        SimplePopup,
        YesNoPopup,
        ParentalGatePopup
    }

    public enum EPopupStringType : uint
    {
        MainBodyText = 0,
        SingleButtonLabel = 1,
        LeftButtonLabel = 2,
        RightButtonLabel = 3,
        InputFieldPlaceholder = 4
    }

    /// <summary>
    /// Struct for providing information on customising a Popup. 
    /// </summary>
    [Serializable]
    public struct PopupInfo
    {
        [SerializeField]
        private EPopupType popupType;
        public EPopupType PopupType { get { return popupType; } }

        [SerializeField] private List<LocalizedString> localizedStrings;

        public string GetString(EPopupStringType popupStringType)
        {
            if (localizedStrings == null)
            {
                Debug.LogError($"DEBUG: Localized Strings array is null!");
                return null;
            }

            if(localizedStrings.Count == 0)
            {
                Debug.LogError($"DEBUG: Localizes Strings array length is 0!");
                return null;
            }

            if(localizedStrings.Count < (int)popupStringType)
            {
                Debug.LogError($"DEBUG: Localized Strings array length is less than provided EPopupStringType number!");
                return null;
            }

            return localizedStrings[(int)popupStringType].GetLocalizedString();
        }

        public static PopupInfo CreateSimplePopup(LocalizedString mainBody, LocalizedString buttonLabel)
        {
            PopupInfo popupInfo = new PopupInfo();
            popupInfo.popupType = EPopupType.YesNoPopup;
            popupInfo.localizedStrings = new List<LocalizedString>((int)EPopupStringType.SingleButtonLabel);
            popupInfo.localizedStrings[(int)EPopupStringType.MainBodyText] = mainBody;
            popupInfo.localizedStrings[(int)EPopupStringType.SingleButtonLabel] = buttonLabel;
            return popupInfo;
        }
        
        public static PopupInfo CreateYesNoPopup(LocalizedString mainBody, LocalizedString leftButtonLabel, LocalizedString rightButtonLabel)
        {
            PopupInfo popupInfo = new PopupInfo();
            popupInfo.popupType = EPopupType.YesNoPopup;
            popupInfo.localizedStrings = new List<LocalizedString>((int)EPopupStringType.RightButtonLabel);
            popupInfo.localizedStrings[(int)EPopupStringType.MainBodyText] = mainBody;
            popupInfo.localizedStrings[(int)EPopupStringType.LeftButtonLabel] = leftButtonLabel;
            popupInfo.localizedStrings[(int)EPopupStringType.RightButtonLabel] = rightButtonLabel;
            return popupInfo;
        }
    }
}