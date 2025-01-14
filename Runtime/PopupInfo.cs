using System;
using System.Collections;
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

        [SerializeField] private LocalizedString mainBodyText;
        public LocalizedString MainBodyText { get { return mainBodyText; } }

        [SerializeField] private LocalizedString singleButtonLabel;
        public LocalizedString SingleButtonLabel { get { return singleButtonLabel; } }
        [SerializeField] private LocalizedString leftButtonLabel;
        public LocalizedString LeftButtonLabel { get { return leftButtonLabel; } }

        [SerializeField] private LocalizedString rightButtonLabel;
        public LocalizedString RightButtonLabel { get { return rightButtonLabel; } }
    }
}