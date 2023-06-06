using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities.Materials
{
    public static class AssetBundleMaterialsRefresher
    {
        public static void FixShadersForEditor(GameObject gameObject)
        {
#if UNITY_EDITOR
            var renderers = gameObject.GetComponentsInChildren<Renderer>(true);
            foreach (var renderer in renderers)
            {
                ReplaceShaderForEditor(renderer.sharedMaterials);
            }

            var tmps = gameObject.GetComponentsInChildren<TextMeshProUGUI>(true);
            foreach (var tmp in tmps)
            {
                ReplaceShaderForEditor(tmp.material);
                ReplaceShaderForEditor(tmp.materialForRendering);
            }
            
            var spritesRenderers = gameObject.GetComponentsInChildren<SpriteRenderer>(true);
            foreach (var spriteRenderer in spritesRenderers)
            {
                ReplaceShaderForEditor(spriteRenderer.sharedMaterials);
            }

            var images = gameObject.GetComponentsInChildren<Image>(true);
            foreach (var image in images)
            {
                ReplaceShaderForEditor(image.material);
            }
            
            var particleSystemRenderers = gameObject.GetComponentsInChildren<ParticleSystemRenderer>(true);
            foreach (var particleSystemRenderer in particleSystemRenderers)
            {
                ReplaceShaderForEditor(particleSystemRenderer.sharedMaterials);
            }

            var particles = gameObject.GetComponentsInChildren<ParticleSystem>(true);
            foreach (var particle in particles)
            {
                var renderer = particle.GetComponent<Renderer>();
                if (renderer != null) ReplaceShaderForEditor(renderer.sharedMaterials);
            }
#endif
        }

#if UNITY_EDITOR
        private static void ReplaceShaderForEditor(Material[] materials)
        {
            foreach (var t in materials)
            {
                ReplaceShaderForEditor(t);
            }
        }

        private static void ReplaceShaderForEditor(Material material)
        {
            if (material == null) return;

            var shaderName = material.shader.name;
            var shader = Shader.Find(shaderName);

            if (shader != null) material.shader = shader;
        }
#endif
    }
}