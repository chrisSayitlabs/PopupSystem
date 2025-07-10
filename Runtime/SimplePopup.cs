using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

namespace SayItLabs.PopupSystem
{
    /// <summary>
    /// Displays a popup with room for a message and a single button to confirm. Suitable for displaying information to a player.
    /// </summary>
    public class SimplePopup : BasePopup
    {
        [Header("Text Components")]
        [SerializeField] private TextMeshProUGUI mainBodyTxt;

        [Header("Buttons")]
        [SerializeField] private Button confirmButton;

        public override void InitializePopup(PopupInfo pi)
        {
            mainBodyTxt.text = pi.GetString(EPopupStringType.MainBodyText);

            if(confirmButton != null )
            {
                TextMeshProUGUI tmp = confirmButton.GetComponentInChildren<TextMeshProUGUI>();
                if(tmp!=null)
                    tmp.text = pi.GetString(EPopupStringType.SingleButtonLabel);
                confirmButton.onClick.AddListener(() => OnPopupButton1Pressed());
            }
        }
    }
}