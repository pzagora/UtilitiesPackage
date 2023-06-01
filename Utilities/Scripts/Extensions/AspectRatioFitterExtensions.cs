using UnityEngine;
using UnityEngine.UI;

namespace Utilities.Extensions
{
    public static class AspectRatioFitterExtensions
    {
        public static void ResetWithAspectMode(this AspectRatioFitter aspectRatioFitter, AspectRatioFitter.AspectMode mode)
        {
            aspectRatioFitter.aspectMode = mode;
            aspectRatioFitter.aspectRatio = 1f;
        }
        
        public static void FitToSprite(this AspectRatioFitter aspectRatioFitter, Sprite sprite)
        {
            aspectRatioFitter.ResetWithAspectMode(AspectRatioFitter.AspectMode.FitInParent);
            aspectRatioFitter.aspectRatio = sprite.rect.width / sprite.rect.height;
        }
        
        public static void EnvelopeWithSprite(this AspectRatioFitter aspectRatioFitter, Sprite sprite)
        {
            aspectRatioFitter.ResetWithAspectMode(AspectRatioFitter.AspectMode.EnvelopeParent);
            aspectRatioFitter.aspectRatio = sprite.rect.width / sprite.rect.height;
        }
    }
}
