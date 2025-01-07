using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization;

using DG.Tweening;
using UI.Utils;

namespace SayItLabs.PopupSystem
{
    /// <summary>
    /// Base Popup class which all popups should inherit from. Provides basic functionality such as animation in and out. 
    /// The lifetime of a popup is only as long as it is displayed.
    /// </summary>
    public abstract class BasePopup : MonoBehaviour
    {
        [Header("Animation Variables")]
        [SerializeField] private float animDuration = 1f;
        [SerializeField] private Ease animEase = Ease.Linear;

        public RectTransform RectTransform { get; private set; }

        public static Action OnPopupConfirmation;
        public static Action OnPopupRefuse;

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();

#if UNITY_EDITOR
            OnPopupConfirmation += LogConfirmation;
            OnPopupRefuse += logRefuse;
#endif
        }

        private void OnDestroy()
        {
#if UNITY_EDITOR
            OnPopupConfirmation -= LogConfirmation;
            OnPopupRefuse -= logRefuse;
#endif
        }

        /// <summary>
        /// Function to take care of assigning any specific information during setup. Should be implemented by derived class.
        /// </summary>
        /// <param name="popupInfo"></param>
        public abstract void InitializePopup(PopupInfo popupInfo);

        public void Show()
        {
            if (RectTransform == null)
                return;

            RectTransform.anchoredPosition = new Vector2(0f, CanvasWidthHeight.CanvasHeight);
            RectTransform.DOAnchorPos(Vector2.zero, animDuration)
                .SetEase(animEase);
        }

        public void Hide()
        {
            RectTransform.DOAnchorPos(new Vector2(0f, -CanvasWidthHeight.CanvasHeight), animDuration)
                .SetEase(animEase)
                .OnComplete(() => Destroy(gameObject));
        }

        #region DEBUG
#if UNITY_EDITOR
        private void LogConfirmation()
        {
            Debug.Log($"DEBUG: Popup Confirmed");
        }

        private void logRefuse()
        {
            Debug.Log($"DEBUG: Popup Refused");
        }
#endif
        #endregion
    }
}