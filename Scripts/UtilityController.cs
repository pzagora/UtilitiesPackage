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
        [SerializeField] private bool enablePersistentDataContainer;
        
        private readonly Dictionary<AppUtilsGroup, Transform> _appUtilsGroups = new();
        private readonly Dictionary<object, Manager> _managers = new();

        protected override void OnRegister()
        {
            base.OnRegister();
            gameObject.name = string.Format(ConstantMessages.BETWEEN_BRACKETS, nameof(UtilityController));
            SpawnGroups();

            if (enablePersistentDataContainer)
                CreatePersistentDataContainer();
        }

        public MonoBehaviour RegisterManager<T>(T managerOrigin, AppUtilsGroup managerGroup, string managerName) where T : class
        {
            if (_managers.ContainsKey(managerOrigin))
            {
                return _managers[managerOrigin];
            }
            
            var newManager = new GameObject(managerName)
                .AddComponent<Manager>()
                .Initialize(managerOrigin, managerGroup, _appUtilsGroups[managerGroup]);
            
            _managers.Add(managerOrigin, newManager);
            
            return newManager;
        }

        private void SpawnGroups()
        {
            var values = (AppUtilsGroup[]) Enum.GetValues(typeof(AppUtilsGroup));
            foreach (var group in values)
            {
                var groupName = Enum.GetName(typeof(AppUtilsGroup), group);
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
    }
}
