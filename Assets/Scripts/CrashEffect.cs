using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class CrashEffect : MonoBehaviour
{
    [SerializeField] private Volume volume;

    private ChromaticAberration chroma;
    private LensDistortion lens;

    private void Start()
    {
        volume.profile.TryGet(out chroma);
        volume.profile.TryGet(out lens);
    }

    public void TriggerCrash()
    {
        StartCoroutine(RunCrash());
    }

    private IEnumerator RunCrash()
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 3f;
            float v = 1f - Mathf.Abs(0.5f - t) * 2f;

            if (chroma != null) chroma.intensity.value = v;
            if (lens != null) lens.intensity.value = -v * 0.6f;

            yield return null;
        }

        if (chroma != null) chroma.intensity.value = 0f;
        if (lens != null) lens.intensity.value = 0f;
    }
}