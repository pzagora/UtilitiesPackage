using UnityEngine;

namespace Utilities.Extensions
{
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Gets component if present on the GameObject or adds it, and checks if its NULL.
        /// </summary>
        /// <param name="gameObject">Object to perform operation on.</param>
        /// <param name="component">Output component.</param>
        /// <typeparam name="T">Component Type to add or get</typeparam>
        /// <returns>True if component is successfully retrieved, false otherwise.</returns>
        public static bool AddOrGetComponent<T>(this GameObject gameObject, out T component) where T : Component
        {
            component = gameObject.TryGetComponent<T>(out var attachedComponent)
                ? attachedComponent
                : gameObject.AddComponent<T>();

            return component.NotNull();
        }
    }
}
