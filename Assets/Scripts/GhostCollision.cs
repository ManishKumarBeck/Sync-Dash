using UnityEngine;

public class GhostCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Orb"))
        {
            Orb orb = other.GetComponent<Orb>();
            if (orb != null)
                orb.PlayPickupEffect();

            ObjectPool pool = other.GetComponentInParent<ObjectPool>();
            if (pool != null)
                pool.ReturnToPool(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            var dissolve = collision.collider.GetComponent<ObstacleDissolve>();
            if (dissolve != null)
                dissolve.TriggerDissolve();
        }
    }
}