namespace Utilities.Constants
{
    public readonly struct ConstantMessages
    {
        #region MANAGERS

        public const string MANAGER_COROUTINE = "Coroutine Manager";
        public const string MANAGER_VIEWS = "View Manager";
        public const string MANAGER_HYPERLINK = "Hyperlink Manager";

        #endregion
        
        #region COROUTINES
        
        public const string COROUTINE_DISPATCHED = "Coroutine Started";
        public const string COROUTINE_TERMINATED = "Coroutine Stopped";

        #endregion
        
        #region OBJECT POOL
        
        public const string OBJECT_POOL_PARENT_NAME = "[ POOL <{0}> ]";

        #endregion
        
        #region ERRORS

        // Default
        public const string NOT_INITIALIZED = "[{0}] {1} not initialized!";
        
        // ValidatedMonoBehaviour Errors
        public const string VMB_FIELD_NULL = "[{0}] Required variable \"{1}\" is NULL!";
        
        // Services Errors
        public const string SERVICE_BIND_EMPTY = "No selected services in the Utilities controller prefab. Binding context is pointless if no service will be used!";
        public const string SERVICE_SPAWN_ERROR = "Spawning services failed. Utilities package will not work!";
        
        // Hyperlink Errors
        public const string HYPERLINK_UNKNOWN = "Unknown hyperlink action clicked!";
        
        // Object Pool Errors
        public const string OBJECT_POOL_TYPE_NOT_SUPPORTED = "[{0}] {1} is not supported! Please create child class for advanced usages.";

        #endregion
        
        #region EDITOR

        public const string GENERAL_SETTINGS = "General Settings";
        public const string SPECIFIC_SETTINGS = "Specific Settings";

        #endregion
        
        #region OTHER

        public const string SPACE = " ";
        public const string BETWEEN_BRACKETS = "[{0}]";

        #endregion
    }
}
