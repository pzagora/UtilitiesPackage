using strange.extensions.mediation.impl;
using UnityEngine;
using Utilities.Models.Animations;

namespace Utilities.Services.ViewManagement.Views
{
	public class ViewBehaviour : EventView
	{
		[HideInInspector][SerializeField] private bool animateView;
		[HideInInspector][SerializeField] private ViewAnimations viewAnimations;

		public bool AnimateView => animateView;
		
		public virtual void Close()
		{
			Destroy(gameObject);
		}
	}
}