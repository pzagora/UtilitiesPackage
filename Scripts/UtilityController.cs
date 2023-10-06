using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Constants;
using Utilities.Containers;
using Utilities.Enums;
using Utilities.Models.Manager;
using Utilities.Mono;

namespace Utilities
{
    public partial class UtilityController : SingletonMonoBehaviour<UtilityController>, IPersistentContainerIgnore
    {
        #region FIELDS

        [SerializeField] private bool enablePersistentDataContainer;
        
        private readonly Dictionary<UtilitiesGroup, Transform> _appUtilsGroups = new();
        private readonly Dictionary<object, Manager> _managers = new();
        
        #endregion

        #region PUBLIC METHODS
        
        public MonoBehaviour RegisterManager<T>(T managerOrigin, UtilitiesGroup managerGroup, string managerName) where T : class
        {
            if (_managers.TryGetValue(managerOrigin, out var manager))
            {
                return manager;
            }
            
            var newManager = new GameObject(managerName)
                .AddComponent<Manager>()
                .Initialize(managerOrigin, managerGroup, _appUtilsGroups[managerGroup]);
            
            _managers.Add(managerOrigin, newManager);
            
            return newManager;
        }
        
        #endregion

        #region PROTECTED METHODS

        protected override void OnRegister()
        {
            base.OnRegister();
            gameObject.name = string.Format(ConstantMessages.BETWEEN_BRACKETS, nameof(UtilityController));
            SpawnGroups();

            if (enablePersistentDataContainer)
                CreatePersistentDataContainer();
        }

        #endregion

        #region PRIVATE METHODS
        
        private void SpawnGroups()
        {
            var values = (UtilitiesGroup[]) Enum.GetValues(typeof(UtilitiesGroup));
            foreach (var group in values)
            {
                var groupName = Enum.GetName(typeof(UtilitiesGroup), group);
                var groupTransform = new GameObject(groupName).transform;
                groupTransform.SetParent(transform);
                groupTransform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
                
                _appUtilsGroups.Add(group, groupTransform);
            }
        }

        private static void CreatePersistentDataContainer()
        {
            var persistentDataContainer = new GameObject(nameof(PersistentDataContainer));
            persistentDataContainer.AddComponent<PersistentDataContainer>();
            
            persistentDataContainer.transform.SetSiblingIndex(0);
        }

        #endregion
    }
}
