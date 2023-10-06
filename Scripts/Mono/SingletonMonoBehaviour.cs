using System;
using Utilities.Constants;
using Utilities.Containers;
using Utilities.Extensions;

namespace Utilities.Mono
{
    public abstract class SingletonMonoBehaviour<T> : ValidatedMonoBehaviour where T : ValidatedMonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                    throw new NullReferenceException(ConstantMessages.SMB_INSTANCE_NULL);

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

            if (this is IPersistentContainerIgnore) 
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
