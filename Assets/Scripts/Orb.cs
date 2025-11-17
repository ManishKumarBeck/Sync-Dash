using UnityEngine;

public class Orb : MonoBehaviour
{
    private ObjectPool pool;

    [SerializeField] private ParticleSystem pickupEffect;

    public void Init(ObjectPool pool)
    {
        this.pool = pool;
    }

    public void PlayPickupEffect()
    {
        if (pickupEffect != null)
        {
            pickupEffect.transform.parent = null;
            pickupEffect.transform.position = transform.position + Vector3.up * 0.5f;
            pickupEffect.Play();
            Destroy(pickupEffect.gameObject, pickupEffect.main.duration);
        }
    }

    private void Update()
    {
        if (Camera.main != null && transform.position.z < Camera.main.transform.position.z - 10f)
        {
            if (pool != null)
                pool.ReturnToPool(gameObject);
        }
    }
}