using UnityEngine;

namespace Shaders
{
    public sealed class ShaderSettings : MonoBehaviour
    {
        [SerializeField] private Vector3 _globalLightDirection;
        [SerializeField] private Color _lightColor;
        [Range(0f, 100f)]
        [SerializeField] private float _lightScale;
        [Range(0f, 100f)]
        [SerializeField] private float _saturation;

        public void SetParameters()
        {
            Shader.SetGlobalVector("_g_dir", _globalLightDirection);
            Shader.SetGlobalColor("_g_cl", _lightColor);
            Shader.SetGlobalFloat("_g_scl", _lightScale);
            Shader.SetGlobalFloat("_g_sat", _saturation);
        }
        
    }
}