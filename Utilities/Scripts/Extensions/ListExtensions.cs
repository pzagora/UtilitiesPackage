using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.UI;

namespace Utilities.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Clears all TMP_Text.text values in a given input list.
        /// </summary>
        /// <param name="input">The input list to parse.</param>
        public static void ClearTexts(this List<TMP_Text> input)
        {
            foreach (var iTMPText in input)
            {
                iTMPText.text = string.Empty;
            }
        }
        
        /// <summary>
        /// Clears all Text.text values in a given input list.
        /// </summary>
        /// <param name="input">The input list to parse.</param>
        public static void ClearTexts(this List<Text> input)
        {
            foreach (var text in input)
            {
                text.text = string.Empty;
            }
        }

        /// <summary>
        /// Checks if actions can be performed on given input.
        /// </summary>
        /// <param name="input">The input list to parse.</param>
        public static bool IsValid<T>(this List<T> input)
        {
            if (input == null)
                throw new NullReferenceException();

            return !input.Any();
        }
    }
}
