namespace Utilities.Extensions
{
    public static class GenericExtensions
    {
        /// <summary>
        /// Gets whether or not a specific input is null.
        /// </summary>
        /// <param name="input">The input to parse.</param>
        /// <typeparam name="T">The data type.</typeparam>
        /// <returns>Returns whether or not the input is null.</returns>
        public static bool IsNull<T>(this T input) => input == null;
        
        /// <summary>
        /// Gets whether or not a specific input is NOT null.
        /// </summary>
        /// <param name="input">The input to parse.</param>
        /// <typeparam name="T">The data type.</typeparam>
        /// <returns>Returns whether or not the input is NOT null.</returns>
        public static bool NotNull<T>(this T input) => input != null;
    }
}
