using System;
using System.Collections;

namespace Utilities.Services.Coroutine
{
    public interface ICoroutineService
    {
        public Guid StartCoroutine(IEnumerator enumerator);
        public UnityEngine.Coroutine GetCoroutine(Guid guid);
        public void StopCoroutine(Guid guid);
    }
}