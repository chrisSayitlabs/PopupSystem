using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SayItLabs.PopupSystem;
using UnityEngine.UI;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.Localization;

public class ShowPopuptest : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Button showSimplePopup;
    [SerializeField] private Button showYesNoPopup;
    [SerializeField] private TextMeshProUGUI outputText;

    [Header("Popup Info")]
    [SerializeField] private PopupInfo simplePopup; 
    [SerializeField] private PopupInfo yesNoPopup;

    // Start is called before the first frame update
    void Awake()
    {
        if(showSimplePopup != null)
            showSimplePopup.onClick.AddListener(ShowSimplePopup);

        if (showYesNoPopup != null)
            showYesNoPopup.onClick.AddListener(ShowYesNoPopup);
    }

    private void ShowSimplePopup()
    {
        PopupManager.Instance?.CreatePopupWindow(simplePopup);
        _ = AwaitSimplePopupResponse();
    }

    private async Task AwaitSimplePopupResponse()
    {
        PopupResult result = await PopupManager.Instance.AwaitInput();
        outputText.text = "Simple popup closed!";
        await Task.Delay(1000);
        outputText.text = "";
    }

    private void ShowYesNoPopup()
    {
        PopupManager.Instance?.CreatePopupWindow(yesNoPopup);
        _ = AwaitYesNoPopupResponse();
    }

    private async Task AwaitYesNoPopupResponse()
    {
        PopupResult result = await PopupManager.Instance.AwaitInput();
        outputText.text = result.WasSuccessful ? "Yes was pressed!" : "No was pressed!";
        await Task.Delay(1000);
        outputText.text = "";
    }
}
