using System;
using System.Text.RegularExpressions;
using Utilities.Constants;

namespace Utilities.Extensions
{
    public static class StringExtensions
    {
        #region FIELDS
        
        private const string CamelCasePattern = @"(?<!^)(?=[A-Z])";

        #endregion
        
        /// <param name="input">The input to parse.</param>
        /// <returns>Returns an input string with first letter transformed to uppercase.</returns>
        public static string FirstToUpper(this string input) =>
            input switch
            {
                null => string.Empty,
                "" => string.Empty,
                _ => input[0].ToString().ToUpper() + input[1..]
            };
        
        /// <summary>
        /// Splits text before every capital letter.
        /// </summary>
        /// <param name="input">The input to parse.</param>
        /// <returns>Returns an input string with space character before each uppercase character.</returns>
        public static string SplitCamelCase(this string input) {
            var splitText = Regex.Split(input, CamelCasePattern);
            return string.Join(ConstantMessages.SPACE, splitText);
        }
    }
}
