using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Orb"))
        {
            Orb orb = other.GetComponent<Orb>();
            if (orb != null)
            {
                orb.PlayPickupEffect();
                GameEventRecorder.Instance.RecordEvent(GameEventRecorder.EventType.OrbCollected, other.transform.position);

                if (ScoreManager.Instance != null)
                    ScoreManager.Instance.AddCoin();

                ObjectPool pool = other.GetComponentInParent<ObjectPool>();
                if (pool != null)
                    pool.ReturnToPool(other.gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            var dissolve = collision.collider.GetComponent<ObstacleDissolve>();
            if (dissolve != null)
            {
                dissolve.TriggerDissolve();
                GameEventRecorder.Instance.RecordEvent(GameEventRecorder.EventType.ObstacleHit, collision.collider.transform.position);
            }

            playerController.Die();
            GameManager.Instance.GameOver();
        }
    }
}