using UnityEngine;

namespace Utilities.Extensions
{
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Gets component if present on the GameObject or adds it.
        /// </summary>
        /// <param name="gameObject">Object to perform operation on.</param>
        /// <typeparam name="T">Component Type to add or get</typeparam>
        /// <returns>Requested component</returns>
        public static T AddOrGetComponent<T>(this GameObject gameObject) where T : Component
        {
            return gameObject.TryGetComponent<T>(out var component) 
                ? component 
                : gameObject.AddComponent<T>();
        }
    }
}
