namespace Utilities.Extensions
{
    public static class StringExtensions
    {
        /// <param name="input">The input to parse.</param>
        /// <returns>Returns an input string with first letter transformed to uppercase.</returns>
        public static string FirstToUpper(this string input) =>
            input switch
            {
                null => string.Empty,
                "" => string.Empty,
                _ => input[0].ToString().ToUpper() + input[1..]
            };
    }
}
