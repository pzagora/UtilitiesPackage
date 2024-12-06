using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Enums;
using Utilities.Extensions;
using Utilities.Models.Manager;

namespace Utilities
{
    public partial class UtilityController : MonoBehaviour
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

        #endregion
    }
}
