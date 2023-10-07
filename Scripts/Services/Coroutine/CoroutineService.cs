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
    /// <summary>
    /// This service allows usage of coroutines outside MonoBehaviours
    /// </summary>
    public class CoroutineService : ICoroutineService
    {
        #region FIELDS
        
        private readonly List<CoroutineData> _coroutines = new();
        private readonly MonoBehaviour _coroutineManager;

        #endregion

        #region CONSTRUCTORS
        
        /// <summary>
        /// Default Coroutine Service constructor.
        /// </summary>
        public CoroutineService()
        {
            if (UtilityController.Instance.IsNull())
                return;

            _coroutineManager = UtilityController.Instance
                .RegisterManager(this, UtilitiesGroup.Managers, ConstantMessages.MANAGER_COROUTINE);
        }

        #endregion

        #region PUBLIC METHODS
        
        /// <summary>
        /// This method starts non-MonoBehaviour coroutine.
        /// </summary>
        /// <param name="enumerator"><see cref="IEnumerator"/> to perform as <see cref="Coroutine"/></param>
        /// <returns><see cref="Guid">GUID</see> to use when trying to <see cref="GetCoroutine"/></returns>
        public Guid StartCoroutine(IEnumerator enumerator)
        {
            var newCoroutineData = new CoroutineData(enumerator);
            var newCoroutine = _coroutineManager.StartCoroutine(newCoroutineData.Method);
            newCoroutineData.SetCoroutine(newCoroutine);
            
            _coroutines.Add(newCoroutineData);
            
            return newCoroutineData.Guid;
        }

        /// <summary>
        /// Get reference to non-MonoBehaviour coroutine.
        /// </summary>
        /// <param name="guid"><see cref="Guid">GUID</see> of requested <see cref="Coroutine"/>.</param>
        /// <returns><see cref="Coroutine"/> with given <see cref="Guid">GUID</see>.</returns>
        public UnityEngine.Coroutine GetCoroutine(Guid guid)
        {
            var coroutine = _coroutines.FirstOrDefault(c => c.Guid == guid)?.Coroutine;
            return coroutine;
        }

        
        /// <summary>
        /// Stop non-MonoBehaviour coroutine.
        /// </summary>
        /// <param name="guid"><see cref="Guid">GUID</see> of requested <see cref="Coroutine"/>.</param>
        public void StopCoroutine(Guid guid)
        {
            var coroutine = _coroutines.FirstOrDefault(c => c.Guid == guid)?.Coroutine;
            
            if (coroutine.NotNull())
            {
                _coroutineManager.StopCoroutine(coroutine);
            }
        }

        #endregion
    }
}


