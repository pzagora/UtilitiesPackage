using System.ComponentModel;
using UnityEngine;
using Utilities.Enums;

namespace Utilities.Models.Manager
{
    public class Manager : MonoBehaviour
    {
        [SerializeField] private AppUtilsGroup group;
        
        private object _origin;

        public Manager Initialize(object origin, AppUtilsGroup group, Transform parent)
        {
            SetupBasicData(origin, group);
            SetupHierarchy(parent);

            return this;
        }
        
        private void SetupBasicData(object origin, AppUtilsGroup utilsGroup)
        {
            _origin = origin;
            group = utilsGroup;
        }

        private void SetupHierarchy(Transform parent)
        {
            transform.SetParent(parent);
            transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        }
    }
}
