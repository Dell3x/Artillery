using UnityEngine;
using UnityEditor;

namespace Shaders
{
    [CustomEditor(typeof(ShaderSettings))]
    public class ShaderParameterEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            ShaderSettings shaderScript = (ShaderSettings)target;
            if (GUILayout.Button("Apply Changes"))
            {
                shaderScript.SetParameters();
            }
        }
    }
}