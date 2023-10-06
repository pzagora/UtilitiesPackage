using System;
using UnityEngine;

namespace Utilities.Extensions
{
    public static class CanvasGroupExtensions
    {
        /// <summary>
        /// Enables given CanvasGroup.
        /// </summary>
        /// <param name="input">The input to parse.</param>
        public static void Show(this CanvasGroup input)
        {
            input.Toggle(true);
        }
        
        /// <summary>
        /// Disables given CanvasGroup.
        /// </summary>
        /// <param name="input">The input to parse.</param>
        public static void Hide(this CanvasGroup input)
        {
            input.Toggle(false);
        }
        
        /// <summary>
        /// Toggles given CanvasGroup.
        /// </summary>
        /// <param name="input">The input to parse.</param>
        /// <param name="isEnabled">State to set for input CanvasGroup</param>
        public static void Toggle(this CanvasGroup input, bool isEnabled)
        {
            input.alpha = Convert.ToInt32(isEnabled);
            input.interactable = isEnabled;
            input.blocksRaycasts = isEnabled;
        }
    }
}
