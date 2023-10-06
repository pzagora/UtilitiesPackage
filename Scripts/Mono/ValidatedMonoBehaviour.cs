using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Utilities.Attributes;
using Utilities.Constants;

namespace Utilities.Mono
{
    public abstract class ValidatedMonoBehaviour : MonoBehaviour
    {
#if UNITY_EDITOR
        private const BindingFlags Flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static;
        
        private void Start()
        {
            ValidateRequiredReferences();
        }

        private void ValidateRequiredReferences()
        {
            var type = GetType();
            var fieldInfos = type.GetFields(Flags).ToList();
            if (!fieldInfos.Any()) 
                return;

            var missingReferenceExceptions = new List<string>();
            foreach (var fieldInfo in fieldInfos)
            {
                var attribute = fieldInfo
                    .GetCustomAttributes(typeof(Required), false)
                    .FirstOrDefault();

                if (attribute == null)
                    continue;

                if (fieldInfo.GetValue(this) != null) 
                    continue;
                    
                missingReferenceExceptions.Add(string.Format(ConstantMessages.VMB_FIELD_NULL, this, fieldInfo.Name));
            }

            if (!missingReferenceExceptions.Any()) 
                return;
            
            missingReferenceExceptions.ForEach(mre => Debug.LogError(mre, this));
        }
#endif
    }
}
