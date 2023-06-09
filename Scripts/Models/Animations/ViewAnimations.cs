using System;
using UnityEngine;

namespace Utilities.Models.Animations
{
    [Serializable]
    public class ViewAnimations
    {
        [SerializeField] private AnimationClip openAnimationClip;
        [SerializeField] private AnimationClip closeAnimationClip;

        public AnimationClip OpenAnimationClip => openAnimationClip;
        public AnimationClip CloseAnimationClip => closeAnimationClip;
    }
}