using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Localization;

namespace SayItLabs.PopupSystem
{
    public enum EPopupType
    {
        None,
        SimplePopup,
        YesNoPopup,
        ParentalGatePopup,
        DualVisualPopup
    }

    public enum EPopupStringType : uint
    {
        MainBodyText = 0,
        SingleButtonLabel = 1,
        FirstButtonLabel = 2,
        SecondButtonLabel = 3,
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

        [SerializeField] private Sprite firstSprite;
        public Sprite FirstSprite { get { return firstSprite; } }
        [SerializeField] private Sprite secondSprite;
        public Sprite SecondSprite { get { return secondSprite; } }

        public LocalizedString GetLocalizedString(EPopupStringType popupStringType)
        {
            if (localizedStrings == null)
            {
                Debug.LogError($"DEBUG: Localized Strings array is null!");
                return null;
            }

            if (localizedStrings.Count == 0)
            {
                Debug.LogError($"DEBUG: Localizes Strings array length is 0!");
                return null;
            }

            if (localizedStrings.Count < (int)popupStringType)
            {
                Debug.LogError($"DEBUG: Localized Strings array length is less than provided EPopupStringType number!");
                return null;
            }

            return localizedStrings[(int)popupStringType];
        }

        public string GetString(EPopupStringType popupStringType)
        {
            return GetLocalizedString(popupStringType).GetLocalizedString();
        }

        public static PopupInfo CreateSimplePopup(LocalizedString mainBody, LocalizedString buttonLabel)
        {
            PopupInfo popupInfo = new PopupInfo();
            popupInfo.popupType = EPopupType.YesNoPopup;
            popupInfo.localizedStrings = new LocalizedString[(int)EPopupStringType.SingleButtonLabel + 1].ToList();
            popupInfo.localizedStrings[(int)EPopupStringType.MainBodyText] = mainBody;
            popupInfo.localizedStrings[(int)EPopupStringType.SingleButtonLabel] = buttonLabel;
            return popupInfo;
        }
        
        public static PopupInfo CreateYesNoPopup(LocalizedString mainBody, LocalizedString leftButtonLabel, LocalizedString rightButtonLabel)
        {
            PopupInfo popupInfo = new PopupInfo();
            popupInfo.popupType = EPopupType.YesNoPopup;
            popupInfo.localizedStrings = new LocalizedString[(int)EPopupStringType.SecondButtonLabel + 1].ToList();
            popupInfo.localizedStrings[(int)EPopupStringType.MainBodyText] = mainBody;
            popupInfo.localizedStrings[(int)EPopupStringType.FirstButtonLabel] = leftButtonLabel;
            popupInfo.localizedStrings[(int)EPopupStringType.SecondButtonLabel] = rightButtonLabel;
            return popupInfo;
        }

        public static PopupInfo CreateDualVisualPopup(Sprite firstSprite, Sprite secondSprite, LocalizedString firstButtonLabel, LocalizedString secondButtonLabel)
        {
            PopupInfo popupInfo = new PopupInfo();
            popupInfo.popupType = EPopupType.DualVisualPopup;
            popupInfo.firstSprite = firstSprite;
            popupInfo.secondSprite = secondSprite;
            popupInfo.localizedStrings = new LocalizedString[(int)EPopupStringType.SecondButtonLabel + 1].ToList();
            popupInfo.localizedStrings[(int)EPopupStringType.FirstButtonLabel] = firstButtonLabel;
            popupInfo.localizedStrings[(int)EPopupStringType.SecondButtonLabel] = secondButtonLabel;
            return popupInfo;
        }
    }
}