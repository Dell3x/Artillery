using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderSettings : MonoBehaviour
{
    public Material targetMaterial; // Посилання на ваш матеріал

    public Vector3 globalLightDirection = new Vector3(1, 1, 1); // Напрямок глобального світла
    public Color lightColor = Color.white; // Колір світла
    public float lightScale = 1.0f; // Масштаб світла
    public float saturation = 1.0f; // Насиченість

    void Awake()
    {
        // Перевірка, чи є матеріал і чи є потрібні властивості у шейдері
        if (targetMaterial != null && targetMaterial.HasProperty("_g_dir") && targetMaterial.HasProperty("_g_cl") &&
            targetMaterial.HasProperty("_g_scl") && targetMaterial.HasProperty("_g_sat"))
        {
            // Встановлення глобальних параметрів
            Vector4 dir = new Vector4(globalLightDirection.x, globalLightDirection.y, globalLightDirection.z, 0);
            targetMaterial.SetVector("_g_dir", dir);
            targetMaterial.SetColor("_g_cl", lightColor);
            targetMaterial.SetFloat("_g_scl", lightScale);
            targetMaterial.SetFloat("_g_sat", saturation);
        }
        else
        {
            Debug.LogWarning("Material or shader properties not found.");
        }
    }

}
