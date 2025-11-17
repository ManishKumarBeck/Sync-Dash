using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    [SerializeField] private float duration = 0.3f;
    [SerializeField] private float magnitude = 0.2f;

    private Vector3 originalPos;

    private void Awake()
    {
        Instance = this;
        originalPos = transform.localPosition;
    }

    public void Shake() => StartCoroutine(ShakeRoutine());

    private IEnumerator ShakeRoutine()
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            Vector3 offset = Random.insideUnitSphere * magnitude;
            offset.z = 0;
            transform.localPosition = originalPos + offset;
            yield return null;
        }

        transform.localPosition = originalPos;
    }
}