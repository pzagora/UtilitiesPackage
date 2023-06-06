using System;
using System.Collections.Generic;
using System.Linq;
using strange.extensions.injector.api;
using UnityEngine;
using Utilities.Constants;
using Utilities.Services;

namespace Utilities
{
    public partial class UtilityController : ISerializationCallbackReceiver
    {
        [HideInInspector][SerializeField] private List<bool> enabledServicesHelper = new();
        private static List<bool> _enabledServices = new();
        
        public static void Bind(ICrossContextInjectionBinder injectionBinder)
        {
            var services = GetSortedServices();

            if (services.Count != _enabledServices.Count)
                throw new Exception(ConstantMessages.SERVICE_SPAWN_ERROR);

            for (var i = 0; i < services.Count; i++)
            {
                if (!_enabledServices[i]) 
                    continue;
                
                var instance = Activator.CreateInstance(services[i].classType);
                injectionBinder.Bind(services[i].interfaceType).ToValue(instance).ToSingleton();
            }
        }

        public static List<(Type interfaceType, Type classType)> GetSortedServices()
        {
            var sortedServices = new List<(Type interfaceType, Type classType)>();
            var serviceTypes = GetAllServiceTypes();

            var classes = serviceTypes
                .Where(type => type.IsClass)
                .ToList();
            
            var interfaces = serviceTypes
                .Where(type => type.IsInterface)
                .ToList();
            
            if (classes.Count != interfaces.Count)
                throw new Exception(ConstantMessages.SERVICE_SPAWN_ERROR);

            for (var i = 0; i < interfaces.Count; i++)
            {
                if (sortedServices.Any(s => s.interfaceType == interfaces[i]))
                    continue;
                    
                sortedServices.Add(new ValueTuple<Type, Type>(interfaces[i], classes[i]));
            }
            
            return sortedServices.OrderBy(i => i.interfaceType.Name).ToList();;
        }

        private static List<Type> GetAllServiceTypes()
        {
            var interfaceType = typeof(IService);
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => interfaceType.IsAssignableFrom(type) && type != interfaceType)
                .ToList();
        }
        
        public void OnBeforeSerialize()
        {
            _enabledServices = enabledServicesHelper;
        }

        public void OnAfterDeserialize()
        {
            _enabledServices = enabledServicesHelper;
        }
    }
}
