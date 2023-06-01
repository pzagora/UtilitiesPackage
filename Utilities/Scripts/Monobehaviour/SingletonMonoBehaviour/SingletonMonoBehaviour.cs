using System;
using UnityEngine;
using Utilities.Extensions;

namespace Utilities.SingletonMonoBehaviour
{
    public abstract class SingletonMonoBehaviour<T> : ValidatedMonoBehaviour.ValidatedMonoBehaviour where T : ValidatedMonoBehaviour.ValidatedMonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new Exception("Instance is NULL. Maybe you didn't add it to the scene?");
                }

                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;

                OnRegister();
                
                TargetedDontDestroyOnLoad();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void TargetedDontDestroyOnLoad()
        {
            transform.parent = null;
            DontDestroyOnLoad(this);

            if (this is IUtilities) 
                return;
            
            var persistentDataContainer = PersistentDataContainer._instance;
            if (persistentDataContainer.NotNull())
            {
                transform.parent = persistentDataContainer.transform;
            }

        }

        protected virtual void OnRegister()
        {
            // Dummy
        }
    }
}
