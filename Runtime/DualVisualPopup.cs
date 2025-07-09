using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SayItLabs.PopupSystem
{
    public class DualVisualPopup : BasePopup
    {
        [Header("Images")]
        [SerializeField] private Image leftSprite;
        [SerializeField] private Image rightSprite;
        [Header("Buttons")]
        [SerializeField] private Button leftButton;
        [SerializeField] private Button rightButton;
        public override void InitializePopup(PopupInfo pi)
        {
            leftSprite.sprite = pi.LeftSprite;
            rightSprite.sprite = pi.RightSprite;

            if (leftButton != null)
            {
                TextMeshProUGUI tmp = leftButton.GetComponentInChildren<TextMeshProUGUI>();
                if (tmp != null)
                    tmp.text = pi.GetString(EPopupStringType.LeftButtonLabel);
                leftButton.onClick.AddListener(() => OnPopupConfirmation());
            }

            if (rightButton != null)
            {
                TextMeshProUGUI tmp = rightButton.GetComponentInChildren<TextMeshProUGUI>();
                if (tmp != null)
                    tmp.text = pi.GetString(EPopupStringType.RightButtonLabel);
                rightButton.onClick.AddListener(() => OnPopupRefuse());
            }
        }
    }
}
