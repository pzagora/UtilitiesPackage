using strange.extensions.mediation.impl;

namespace Utilities.Services.ViewManagement.Views
{
	public class ViewMediator<T> : EventMediator where T : ViewBehaviour
	{
		[Inject] public T View { get; set; }

		public virtual void Close()
		{
			View.Close();
		}
	}
}