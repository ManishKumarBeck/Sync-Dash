using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDissolve : MonoBehaviour
{
    private List<Material> materials = new List<Material>();
    private Material dissolveMaterial;

    [Range(0f, 1f)]
    public float dissolveValue = 0f; 

    void Start()
    {
        
        var renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            materials.AddRange(renderer.materials);
        }

        
        SetValue(dissolveValue);
            
        
    }

    public void SetValue(float value)
    {
        dissolveValue = Mathf.Clamp01(value);
        for (int i = 0; i < materials.Count; i++)
        {
            materials[i].SetFloat("_Dissolve", dissolveValue);
        }
    }

    
    public void TriggerDissolve(float duration = 3f)
    {
        StopAllCoroutines();
        StartCoroutine(DissolveCoroutine(duration));
    }

    private System.Collections.IEnumerator DissolveCoroutine(float duration)
    {
        float elapsed = 0f;
        float start = dissolveValue;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            SetValue(Mathf.Lerp(start, 1f, t)); // Dissolve to 1
            yield return null;
        }
        SetValue(1f); 
    
        
    }
}