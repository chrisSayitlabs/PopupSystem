using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SayItLabs.PopupSystem
{
    public class ParentGatePopup : BasePopup
    {
        [Header("Components")]
        [SerializeField] private TextMeshProUGUI mainBodyTxt;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private TMP_InputField inputFieldPlaceholder;

        [Header("Buttons")]
        [SerializeField] private Button confirmButton;
        [SerializeField] private Button cancelButton;

        public override void InitializePopup(PopupInfo popupInfo)
        {
            mainBodyTxt.text = popupInfo.MainBodyText.GetLocalizedString();

            inputField.text = "";
            inputFieldPlaceholder.text = popupInfo.GetLocalizedStringFor(EPopupStringType.InputFieldPlaceholder)?.GetLocalizedString();

            if (confirmButton != null)
            {
                TextMeshProUGUI tmp = confirmButton.GetComponentInChildren<TextMeshProUGUI>();
                if (tmp != null)
                    tmp.text = popupInfo.LeftButtonLabel.GetLocalizedString();
                confirmButton.onClick.AddListener(() => OnPopupConfirmation());
            }

            if (cancelButton != null)
            {
                TextMeshProUGUI tmp = cancelButton.GetComponentInChildren<TextMeshProUGUI>();
                if (tmp != null)
                    tmp.text = popupInfo.RightButtonLabel.GetLocalizedString();
                cancelButton.onClick.AddListener(() => OnPopupRefuse());
            }
        }
    }
}