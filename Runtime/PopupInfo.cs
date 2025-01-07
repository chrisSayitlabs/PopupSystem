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
        YesNoPopup
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

        [SerializeField] private LocalizedString confirmationButtonLabel;
        public LocalizedString ConfirmationButtonLabel { get { return confirmationButtonLabel; } }
        [SerializeField] private LocalizedString yesButtonLabel;
        public LocalizedString YesButtonLabel { get { return yesButtonLabel; } }

        [SerializeField] private LocalizedString noButtonLabel;
        public LocalizedString NoButtonLabel { get { return noButtonLabel; } }
    }
}