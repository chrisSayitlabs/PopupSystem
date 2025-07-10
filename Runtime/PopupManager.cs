using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace SayItLabs.PopupSystem
{
    /// <summary>
    /// Simple class to hold reference to all the different types of popup and spawn them when requested.
    /// </summary>
    public class PopupManager : MonoBehaviour
    {
        private static PopupManager instance;
        public static PopupManager Instance { get { return instance; } }

        [Header("Popups")]
        [SerializeField] private SimplePopup simplePopup;
        [SerializeField] private YesNoPopup yesNoPopup;
        [SerializeField] private ParentGatePopup parentalGatePopup;
        [SerializeField] private ParentGatePopup dualVisualPopup;

        [Header("Debug")]
        [SerializeField] private PopupInfo debugPopupInfo;

        //Events
        public event Action OnPopupChanged;
        public event Action OnPopupDisappear;
        //Private vars
        private BasePopup currentActivePopup = default;
        private PopupResult? popupResult;

        private Dictionary<EPopupType, BasePopup> popupDatabase = new Dictionary<EPopupType, BasePopup>();
        #region UNITY CALLBACKS
        // Start is called before the first frame update
        void Awake()
        {
            if(instance != null)
            {
                Debug.LogWarning($"DEBUG: There is already an instance of PopupManager active! Destroying {gameObject.name}");
                return;
            }

            popupDatabase.Add(EPopupType.SimplePopup, simplePopup);
            popupDatabase.Add(EPopupType.YesNoPopup, yesNoPopup);
            popupDatabase.Add(EPopupType.ParentalGatePopup, parentalGatePopup);
            popupDatabase.Add(EPopupType.DualVisualPopup, dualVisualPopup);

            BasePopup.OnPopupButton1Pressed += ClosePopup;
            BasePopup.OnPopupButton1Pressed += RecordButton1Pressed;
            BasePopup.OnPopupButton2Pressed += ClosePopup;
            BasePopup.OnPopupButton2Pressed += RecordButton2Pressed;

            currentActivePopup = null;

            instance = this;
        }
        #endregion

        /// <summary>
        /// Creates a popup window using the supplied PopupInfo parameter. By default, if there is already a popup active
        /// then it will simple return early. 
        /// </summary>
        /// <param name="popupInfo">Info object to describe the type of the popup and contents.</param>
        public void CreatePopupWindow(PopupInfo popupInfo)
        {
            if(!popupDatabase.ContainsKey(popupInfo.PopupType))
            {
                Debug.LogWarning($"DEBUG: Cannot find popup of type {popupInfo.PopupType}");
                return;
            }

            if(currentActivePopup != null)
            {
                Debug.LogWarning($"DEBUG: Popup already active!");
                return;
            }

            currentActivePopup = Instantiate(popupDatabase[popupInfo.PopupType], transform.parent);
            currentActivePopup.InitializePopup(popupInfo);
            OnPopupChanged?.Invoke();
            popupResult = null;
            currentActivePopup.Show();
        }

        private void ClosePopup()
        {
            if(currentActivePopup == null)
            {
                Debug.LogWarning($"DEBUG: Tried to close popup when there is no active popup!");
                return;
            }

            currentActivePopup.Hide();
            currentActivePopup = null;
            OnPopupDisappear?.Invoke();
        }

        private void RecordButton1Pressed()
        {
            popupResult = new PopupResult(PopupStatics.BUTTON_1);
        }

        private void RecordButton2Pressed()
        {
            popupResult = new PopupResult(PopupStatics.BUTTON_2);
        }

        public async Task<PopupResult> AwaitInput()
        {
            while(popupResult == null)
                await Task.Yield();

            return popupResult.Value;
        }

        #region DEBUG
#if UNITY_EDITOR
        [ContextMenu("Create Debug Popup")]
        public void CreateDebugPopup()
        {
            CreatePopupWindow(debugPopupInfo);
        }
#endif
#endregion
    }
}
