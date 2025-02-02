using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SayItLabs.PopupSystem
{
    /// <summary>
    /// Popup that can allow a user to accept or deny certain requests. 
    /// </summary>
    public class YesNoPopup : BasePopup
    {
        [Header("Text Components")]
        [SerializeField] private TextMeshProUGUI mainBodyTxt;

        [Header("Buttons")]
        [SerializeField] private Button yesButton;
        [SerializeField] private Button noButton;

        public override void InitializePopup(PopupInfo pi)
        {
            mainBodyTxt.text = pi.GetString(EPopupStringType.MainBodyText);

            if (yesButton != null)
            {
                TextMeshProUGUI tmp = yesButton.GetComponentInChildren<TextMeshProUGUI>();
                if (tmp != null)
                    tmp.text = pi.GetString(EPopupStringType.LeftButtonLabel);
                yesButton.onClick.AddListener(() => OnPopupConfirmation());
            }

            if (noButton != null)
            {
                TextMeshProUGUI tmp = noButton.GetComponentInChildren<TextMeshProUGUI>();
                if (tmp != null)
                    tmp.text = pi.GetString(EPopupStringType.RightButtonLabel);
                noButton.onClick.AddListener(() => OnPopupRefuse());
            }
        }
    }
}