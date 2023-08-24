using UnityEngine;

namespace Artilleries.Shaders
{
    public sealed class ShaderSettings : MonoBehaviour
    {
        [SerializeField] private Material _targetMaterial;

        [SerializeField] private Vector3 _globalLightDirection;
        [SerializeField] private Color _lightColor; 
        [SerializeField] private float _lightScale; 
        [SerializeField] private float _saturation; 

        void Awake()
        {
            if (_targetMaterial != null && _targetMaterial.HasProperty("_g_dir") && _targetMaterial.HasProperty("_g_cl") &&
                _targetMaterial.HasProperty("_g_scl") && _targetMaterial.HasProperty("_g_sat"))
            {
                Vector4 dir = new Vector4(_globalLightDirection.x, _globalLightDirection.y, _globalLightDirection.z, 0);
                _targetMaterial.SetVector("_g_dir", dir);
                _targetMaterial.SetColor("_g_cl", _lightColor);
                _targetMaterial.SetFloat("_g_scl", _lightScale);
                _targetMaterial.SetFloat("_g_sat", _saturation);
            }
            else
            {
                Debug.LogError("Material or shader properties not found.");
            }
        }

    }
}