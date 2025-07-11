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
        [SerializeField] private bool enableClickOnImage;
        [SerializeField] private Image firstSprite;
        [SerializeField] private Image secondSprite;
        [Header("Buttons")]
        [SerializeField] private Button firstButton;
        [SerializeField] private Button secondButton;
        public override void InitializePopup(PopupInfo pi)
        {
            firstSprite.sprite = pi.FirstSprite;
            secondSprite.sprite = pi.SecondSprite;

            if (firstButton != null)
            {
                TextMeshProUGUI tmp = firstButton.GetComponentInChildren<TextMeshProUGUI>();
                if (tmp != null)
                    tmp.text = pi.GetString(EPopupStringType.FirstButtonLabel);
                firstButton.onClick.AddListener(() => OnPopupButton1Pressed());
            }

            if (secondButton != null)
            {
                TextMeshProUGUI tmp = secondButton.GetComponentInChildren<TextMeshProUGUI>();
                if (tmp != null)
                    tmp.text = pi.GetString(EPopupStringType.SecondButtonLabel);
                secondButton.onClick.AddListener(() => OnPopupButton2Pressed());
            }

            if(enableClickOnImage)
            {
                if (firstSprite != null)
                {
                    Button b = firstSprite.gameObject.GetComponent<Button>();
                    if (b == null)
                        b = firstSprite.gameObject.AddComponent<Button>();
                    b.onClick.AddListener(() => OnPopupButton1Pressed());
                }

                if (secondSprite != null)
                {
                    Button b = secondSprite.gameObject.GetComponent<Button>();
                    if(b == null)
                        b = secondSprite.gameObject.AddComponent<Button>();
                    b.onClick.AddListener(() => OnPopupButton2Pressed());
                }
            }
        }
    }
}
