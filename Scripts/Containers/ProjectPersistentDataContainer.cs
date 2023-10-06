using Utilities.Mono;

namespace Utilities.Containers
{
    /// <summary>
    /// Persistent container for all SingletonMonoBehaviours excluding ones with IPersistentContainerIgnore interface.
    /// </summary>
    public class PersistentDataContainer : SingletonMonoBehaviour<PersistentDataContainer>, IPersistentContainerIgnore
    {
    }
}
