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
        [SerializeField] private Image firstSprite;
        [SerializeField] private Image secondSprite;
        [Header("Buttons")]
        [SerializeField] private Button firstButton;
        [SerializeField] private Button secondButton;
        public override void InitializePopup(PopupInfo pi)
        {
            firstSprite.sprite = pi.LeftSprite;
            rightSprite.sprite = pi.RightSprite;

            if (leftButton != null)
            {
                TextMeshProUGUI tmp = leftButton.GetComponentInChildren<TextMeshProUGUI>();
                if (tmp != null)
                    tmp.text = pi.GetString(EPopupStringType.LeftButtonLabel);
                leftButton.onClick.AddListener(() => OnPopupButton1Pressed());
            }

            if (rightButton != null)
            {
                TextMeshProUGUI tmp = rightButton.GetComponentInChildren<TextMeshProUGUI>();
                if (tmp != null)
                    tmp.text = pi.GetString(EPopupStringType.RightButtonLabel);
                rightButton.onClick.AddListener(() => OnPopupButton2Pressed());
            }
        }
    }
}
