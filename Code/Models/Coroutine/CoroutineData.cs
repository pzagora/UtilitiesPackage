using System;
using System.Collections;

namespace Utilities.Models.Coroutine
{
    public class CoroutineData
    {
        public readonly IEnumerator Method;
        public UnityEngine.Coroutine Coroutine;
    
        public CoroutineData(IEnumerator enumerator, UnityEngine.Coroutine coroutine)
        {
            Method = enumerator;
            Coroutine = coroutine;
        }
    }
}
