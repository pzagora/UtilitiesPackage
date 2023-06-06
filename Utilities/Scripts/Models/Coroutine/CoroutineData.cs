using System;
using System.Collections;

namespace Utilities.Models.Coroutine
{
    public class CoroutineData
    {
        public readonly Guid Guid;
        public readonly IEnumerator Method;
        public UnityEngine.Coroutine Coroutine;
    
        public CoroutineData(IEnumerator enumerator)
        {
            Guid = Guid.NewGuid();
            Method = enumerator;
        }

        public void SetCoroutine(UnityEngine.Coroutine coroutine)
        {
            Coroutine = coroutine;
        }
    }
}
