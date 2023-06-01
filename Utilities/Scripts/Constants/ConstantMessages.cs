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
        
        #region ERRORS

        public const string SERVICE_SPAWN_ERROR = "Spawning services failed. Utilities package will not work!";
        public const string FIELD_NULL = "Required variable \"{0}\" is NULL!";
        public const string HYPERLINK_UNKNOWN = "Unknown hyperlink action clicked!";

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
