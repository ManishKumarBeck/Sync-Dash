using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private ObjectPool obstaclePool;
    [SerializeField] private ObjectPool orbPool;

    [SerializeField] private float spawnZStart = 20f;
    [SerializeField] private float spawnZDistance = 10f;
    [SerializeField] private int preSpawnCount = 10;

    [SerializeField] private float playerLaneX = 2f;
    [SerializeField] private float ghostLaneX = -2f;

    private float nextSpawnZ;

    private void Start()
    {
        nextSpawnZ = spawnZStart;

        for (int i = 0; i < preSpawnCount; i++)
        {
            SpawnRow();
            nextSpawnZ += spawnZDistance;
        }
    }

    private void SpawnRow()
    {
        bool spawnObstacle = Random.value > 0.3f;
        bool spawnOrb = Random.value > 0.5f;

        if (spawnObstacle) SpawnObstacleRow(nextSpawnZ);
        if (spawnOrb) SpawnOrbRow(nextSpawnZ + 2f);
    }

    private void SpawnObstacleRow(float z)
    {
        GameObject obstacleRight = obstaclePool.Get();
        obstacleRight.transform.position = new Vector3(playerLaneX, 0.5f, z);

        GameObject obstacleLeft = obstaclePool.Get();
        obstacleLeft.transform.position = new Vector3(ghostLaneX, 0.5f, z);
    }

    private void SpawnOrbRow(float z)
    {
        GameObject orbRight = orbPool.Get();
        orbRight.transform.position = new Vector3(playerLaneX, 1f, z + 2f);

        GameObject orbLeft = orbPool.Get();
        orbLeft.transform.position = new Vector3(ghostLaneX, 1f, z + 2f);
    }

    public void SpawnMoreAhead(float playerZ)
    {
        while (nextSpawnZ < playerZ + 100f)
        {
            SpawnRow();
            nextSpawnZ += spawnZDistance;
        }
    }
}