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

            var missingReferenceExceptions = (from fieldInfo in fieldInfos
                let attribute = fieldInfo.GetCustomAttributes(typeof(Required), false).FirstOrDefault()
                where attribute != null
                where fieldInfo.GetValue(this) == null
                select string.Format(ConstantMessages.VMB_FIELD_NULL, this, fieldInfo.Name)).ToList();

            if (!missingReferenceExceptions.Any()) 
                return;
            
            missingReferenceExceptions.ForEach(mre => Debug.LogError(mre, this));
        }
#endif
    }
}
