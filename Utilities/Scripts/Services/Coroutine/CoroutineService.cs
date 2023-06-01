using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities.Constants;
using Utilities.Enums;
using Utilities.Extensions;
using Utilities.Models.Coroutine;

namespace Utilities.Services.Coroutine
{
    public class CoroutineService : ICoroutineService
    {
        private readonly List<CoroutineData> _coroutines = new();
        private readonly MonoBehaviour _coroutineManager;

        public CoroutineService()
        {
            if (UtilityController.Instance.IsNull())
            {
                return;
            }

            _coroutineManager = UtilityController.Instance
                .RegisterManager(this, AppUtilsGroup.Managers, ConstantMessages.MANAGER_COROUTINE);
        }

        public Guid StartCoroutine(IEnumerator enumerator)
        {
            var newCoroutineData = new CoroutineData(enumerator);
            var newCoroutine = _coroutineManager.StartCoroutine(newCoroutineData.Method);
            newCoroutineData.SetCoroutine(newCoroutine);
            
            _coroutines.Add(newCoroutineData);
            
            return newCoroutineData.Guid;
        }

        public UnityEngine.Coroutine GetCoroutine(Guid guid)
        {
            var coroutine = _coroutines.FirstOrDefault(c => c.Guid == guid)?.Coroutine;
            return coroutine;
        }

        public void StopCoroutine(Guid guid)
        {
            var coroutine = _coroutines.FirstOrDefault(c => c.Guid == guid)?.Coroutine;
            
            if (coroutine.NotNull())
            {
                _coroutineManager.StopCoroutine(coroutine);
            }
        }
    }
}


