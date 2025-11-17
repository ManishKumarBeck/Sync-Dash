using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private ObjectPool pool;

    public void Init(ObjectPool pool)
    {
        this.pool = pool;
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