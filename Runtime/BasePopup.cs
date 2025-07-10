using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization;

using Tweens;

namespace SayItLabs.PopupSystem
{
    /// <summary>
    /// Base Popup class which all popups should inherit from. Provides basic functionality such as animation in and out. 
    /// The lifetime of a popup is only as long as it is displayed.
    /// </summary>
    public abstract class BasePopup : MonoBehaviour
    {
        [Header("Animation Variables")]
        [SerializeField] private Vector2 finalActivePosition = Vector2.zero;
        [Header("Transition In")]
        [SerializeField] private float inDuration = 1f;
        [SerializeField] private Vector2 inFrom = Vector2.zero;
        [SerializeField] private EaseType inEase = EaseType.Linear;

        [Header("Transition Out")]
        [SerializeField] private float outDuration = 1f;
        [SerializeField] private Vector2 outTo = Vector2.zero;
        [SerializeField] private EaseType outEase = EaseType.Linear;

        public RectTransform RectTransform { get; private set; }

        public static Action OnPopupButton1Pressed;
        public static Action OnPopupButton2Pressed;

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();

#if UNITY_EDITOR
            OnPopupButton1Pressed += LogButton1Pressed;
            OnPopupButton2Pressed += LogButton2Pressed;
#endif
        }

        private void OnDestroy()
        {
#if UNITY_EDITOR
            OnPopupButton1Pressed -= LogButton1Pressed;
            OnPopupButton2Pressed -= LogButton2Pressed;
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

            gameObject.SetActive(true);

            gameObject.AddTween(new AnchoredPositionTween
            {
                from = inFrom,
                to = finalActivePosition,
                duration = inDuration,
                easeType = inEase
            });
        }

        public void Hide()
        {
            gameObject.AddTween(new AnchoredPositionTween
            {
                from = finalActivePosition,
                to = outTo,
                duration = outDuration,
                easeType = outEase,
                onEnd = (instance) => Destroy(gameObject)
            });
        }

        #region DEBUG
#if UNITY_EDITOR
        private void LogButton1Pressed()
        {
            Debug.Log($"DEBUG: Popup Button 1 pressed.");
        }

        private void LogButton2Pressed()
        {
            Debug.Log($"DEBUG: Popup Popup Button 2 pressed.");
        }
#endif
        #endregion
    }
}