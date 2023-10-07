using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;
using Utilities.Constants;
using Utilities.Enums;
using Utilities.Extensions;
using Object = UnityEngine.Object;

namespace Utilities.ObjectPool
{
    /// <summary>
    /// A simple generic class to simplify object pooling in Unity.
    /// One can derive from this class for more advanced usages.
    /// </summary>
    /// <typeparam name="T">A <see cref="MonoBehaviour"/> to perform pooling on.</typeparam>
    public class GenericObjectPool<T> where T : Object
    {
        #region FIELDS

        private readonly T _prefab;
        private readonly Transform _parentTransform;
        private readonly Dictionary<T, ObjectPoolStatus> _createdPoolObjects = new();

        private ObjectPool<T> _pool;

        #endregion

        #region PROPERTIES

        private ObjectPool<T> Pool
        {
            get
            {
                if (_pool.IsNull())
                    throw new NullReferenceException(
                        string.Format(ConstantMessages.NOT_INITIALIZED, this, nameof(Pool)));

                return _pool;
            }
            set => _pool = value;
        }

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Default constructor for generic pool class.
        /// Creates custom parent object to reduce hierarchy clutter.
        /// </summary>
        /// <param name="prefab">Pool of this object and it's type will be created.</param>
        /// <param name="initial">Initial pool size.</param>
        /// <param name="max">Max pool size.</param>
        /// <param name="collectionChecks">Should throw exception when releasing already released object from pool.</param>
        public GenericObjectPool(T prefab, int initial = 10, int max = 50, bool collectionChecks = false)
        {
            _prefab = prefab;
            var parentObjectName = string.Format(ConstantMessages.OBJECT_POOL_PARENT_NAME, prefab.name);
            _parentTransform = new GameObject(parentObjectName).transform;

            CreatePool(initial, max, collectionChecks);
        }

        /// <summary>
        /// Constructor with parent transform for generic pool class.
        /// </summary>
        /// <param name="prefab">Pool of this object and it's type will be created.</param>
        /// <param name="parent">Transform parent for pool object spawn.</param>
        /// <param name="initial">Initial pool size.</param>
        /// <param name="max">Max pool size.</param>
        /// <param name="collectionChecks">Should throw exception when releasing already released object from pool.</param>
        public GenericObjectPool(T prefab, Transform parent, int initial = 10, int max = 50,
            bool collectionChecks = false)
        {
            _prefab = prefab;
            _parentTransform = parent;

            CreatePool(initial, max, collectionChecks);
        }

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Method that gets next available object from object pool.
        /// One can change <see cref="Get"/> method by overriding <see cref="GetSetup"/> method.
        /// </summary>
        /// <returns>Next available object of pool type.</returns>
        public T Get() => Pool.Get();

        /// <summary>
        /// Method that releases given object back to the pool.
        /// One can change <see cref="Release"/> method by overriding <see cref="ReleaseSetup"/> method.
        /// </summary>
        /// <param name="poolItem">Item to return to the pool.</param>
        public void Release(T poolItem) => Pool.Release(poolItem);

        /// <summary>
        /// Method that releases all active pool objects back to the pool using <see cref="Release"/> method.
        /// </summary>
        public void ReleaseAll()
        {
            var activePoolObjects = _createdPoolObjects
                .Where(poolItem => poolItem.Value == ObjectPoolStatus.Active)
                .Select(poolItem => poolItem.Key)
                .ToList();

            foreach (var activePoolObject in activePoolObjects)
            {
                Release(activePoolObject);
            }
        }

        /// <summary>
        /// Returns amount of pool objects with status Active
        /// </summary>
        public int ActiveCount => _createdPoolObjects
            .Count(cpo => cpo.Value == ObjectPoolStatus.Active);
        
        /// <summary>
        /// Returns amount of pool objects with status Inactive
        /// </summary>
        public int InactiveCount => _createdPoolObjects
            .Count(cpo => cpo.Value == ObjectPoolStatus.Inactive);

        /// <summary>
        /// Returns amount of pool objects, both Active and Inactive
        /// </summary>
        public int Count => _createdPoolObjects.Count;

        #endregion

        #region PROTECTED VIRTUAL METHODS

        protected virtual T CreateSetup() => Object.Instantiate(_prefab, _parentTransform);
        protected virtual void GetSetup(T poolItem) => HandleGenericSetup(poolItem, true);
        protected virtual void ReleaseSetup(T poolItem) => HandleGenericSetup(poolItem, false);
        protected virtual void DestroySetup(T poolItem) => Object.Destroy(poolItem);

        #endregion

        #region PRIVATE METHODS

        private void CreatePool(int initial = 10, int max = 50, bool collectionChecks = false)
        {
            Pool = new ObjectPool<T>(
                CreateSetupWithTracking,
                GetSetupWithTracking,
                ReleaseSetupWithTracking,
                DestroySetupWithTracking,
                collectionChecks,
                initial,
                max);
        }

        private T CreateSetupWithTracking()
        {
            var poolItem = CreateSetup();
            _createdPoolObjects.Add(poolItem, ObjectPoolStatus.Inactive);
            return poolItem;
        }

        private void GetSetupWithTracking(T poolItem)
        {
            if (poolItem.IsNull())
                return;

            if (_createdPoolObjects.ContainsKey(poolItem))
                _createdPoolObjects[poolItem] = ObjectPoolStatus.Active;

            GetSetup(poolItem);
        }

        private void ReleaseSetupWithTracking(T poolItem)
        {
            if (poolItem.IsNull())
                return;

            if (_createdPoolObjects.ContainsKey(poolItem))
                _createdPoolObjects[poolItem] = ObjectPoolStatus.Inactive;

            ReleaseSetup(poolItem);
        }

        private void DestroySetupWithTracking(T poolItem)
        {
            if (poolItem.IsNull())
                return;

            if (_createdPoolObjects.ContainsKey(poolItem))
                _createdPoolObjects.Remove(poolItem);

            DestroySetup(poolItem);
        }

        private void HandleGenericSetup(T poolItem, bool isEnabled)
        {
            switch (poolItem)
            {
                case GameObject gameObject:
                    gameObject.SetActive(isEnabled);
                    break;

                // Component includes MonoBehaviour
                case Component component:
                    component.gameObject.SetActive(isEnabled);
                    break;

                default:
                    var message = string.Format(ConstantMessages.OBJECT_POOL_TYPE_NOT_SUPPORTED, this, typeof(T));
                    throw new NotSupportedException(message);
            }
        }

        #endregion
    }
}