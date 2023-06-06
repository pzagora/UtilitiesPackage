using Utilities.Services.ViewManagement.Views;

namespace Utilities.Services.ViewManagement
{
    public interface IViewManagerService : IService
    {
        public void Close(ViewBehaviour view);
        public T Show<T>() where T : ViewBehaviour;
        public T Get<T>() where T : ViewBehaviour;
    }
}