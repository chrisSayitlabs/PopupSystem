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
        [SerializeField] private TextMeshProUGUI inputFieldLabel;
        [SerializeField] private TextMeshProUGUI inputFieldPlaceholderText;

        [Header("Buttons")]
        [SerializeField] private Button confirmButton;
        [SerializeField] private Button cancelButton;

        private int correctAnswer;

        public override void InitializePopup(PopupInfo popupInfo)
        {
            mainBodyTxt.text = popupInfo.MainBodyText.GetLocalizedString();
            inputField.contentType = TMP_InputField.ContentType.IntegerNumber;
            inputFieldPlaceholderText.text = "";
            inputField.text = "";

            if (confirmButton != null)
            {
                TextMeshProUGUI tmp = confirmButton.GetComponentInChildren<TextMeshProUGUI>();
                if (tmp != null)
                    tmp.text = popupInfo.LeftButtonLabel.GetLocalizedString();
                confirmButton.onClick.AddListener(() => {
                    if (CheckAnswer())
                        OnPopupConfirmation();
                });
            }

            if (cancelButton != null)
            {
                TextMeshProUGUI tmp = cancelButton.GetComponentInChildren<TextMeshProUGUI>();
                if (tmp != null)
                    tmp.text = popupInfo.RightButtonLabel.GetLocalizedString();
                cancelButton.onClick.AddListener(() => OnPopupRefuse());
            }

            GenerateQuery();
        }

        #region Gate Query Functions
        private void GenerateQuery()
        {
            int num1, num2;
            int operation;

            // Initialize correctAnswer and operator symbol
            correctAnswer = 0;
            string operatorSymbol = "";
            // Generate a valid question
            do
            {
                num1 = Random.Range(1, 9);
                num2 = Random.Range(1, 9);
                operation = Random.Range(0, 3); // 0 = +, 1 = -, 2 = *, 3 = /

                switch (operation)
                {
                    case 0: // Addition
                        correctAnswer = num1 + num2;
                        operatorSymbol = "+";
                        break;

                    case 1: // Subtraction
                            // Ensure num1 is greater than num2 to get a positive result
                        if (num1 == num2)
                        {
                            num1 = num2 + Random.Range(1, 9); // Ensure num1 is greater than num2
                        }
                        correctAnswer = num1 - num2;
                        operatorSymbol = "-";
                        break;

                    case 2: // Multiplication
                            // Avoid multiplication with 0 and ensure positive result
                        while (num2 == 0 || num1 == 0)
                        {
                            num2 = Random.Range(1, 9);
                            num1 = Random.Range(1, 9);
                        }
                        correctAnswer = num1 * num2;
                        operatorSymbol = "X";
                        break;

                    case 3: // Division
                            // Ensure num2 is not zero, num1 is divisible by num2, and result is positive
                            // num1 should not be 1
                        if (num1 == 1)
                            num1 = Random.Range(2, 9);
                        while (num2 == 0 || num2 == 1 || num1 % num2 != 0 || num1 < num2) // if we don't allow num1 == num2 than prime numbers will lead to an infinite loop
                        {
                            num2 = Random.Range(1, 9);
                        }
                        correctAnswer = num1 / num2;
                        operatorSymbol = "/";
                        break;
                }
            } while (num1 == num2 || correctAnswer <= 0); // Avoid having the same number twice and non-positive results

            // Display the question text
            inputFieldLabel.text = $"{num1} {operatorSymbol} {num2} = ";
        }

        private bool CheckAnswer()
        {
            int userAnswer;
            if (int.TryParse(inputField.text, out userAnswer))
            {
                if (userAnswer == correctAnswer)
                    return true;
            }

            StartCoroutine(WrongWait());
            return false;
        }
        #endregion

        private IEnumerator WrongWait()
        {
            inputField.contentType = TMPro.TMP_InputField.ContentType.Standard;
            inputField.text = "Wrong!";
            yield return new WaitForSecondsRealtime(1);
            GenerateQuery();
            inputField.contentType = TMPro.TMP_InputField.ContentType.IntegerNumber;
            inputField.text = ""; // Clear the input field
        }
    }
}