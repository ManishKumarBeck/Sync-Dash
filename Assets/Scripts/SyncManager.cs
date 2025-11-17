using UnityEngine;
using System.Collections.Generic;

public class SyncManager : MonoBehaviour
{
    public static SyncManager Instance;
    public float networkDelay = 0.15f;

    [System.Serializable]
    public struct PlayerSnapshot
    {
        public float time;
        public Vector3 position;
        public Vector3 velocity;
        public bool isGrounded;
        public bool isAlive;
        public int coinScore;
        public int distanceScore;
    }

    private List<PlayerSnapshot> snapshots = new List<PlayerSnapshot>();

    private void Awake()
    {
        Instance = this;
    }

    public void RecordSnapshot(PlayerSnapshot snap)
    {
        snapshots.Add(snap);
        if (snapshots.Count > 120) // keep about 2 s of history
            snapshots.RemoveAt(0);
    }

    public bool TryGetInterpolatedSnapshot(float currentTime, out PlayerSnapshot interpolated)
    {
        interpolated = default;
        if (snapshots.Count < 2)
            return false;

        float targetTime = currentTime - networkDelay;

        PlayerSnapshot older = snapshots[0];
        PlayerSnapshot newer = snapshots[snapshots.Count - 1];

        for (int i = 1; i < snapshots.Count; i++)
        {
            if (snapshots[i].time > targetTime)
            {
                newer = snapshots[i];
                older = snapshots[i - 1];
                break;
            }
        }

        float t = Mathf.Clamp01(Mathf.InverseLerp(older.time, newer.time, targetTime));

        interpolated.position = Vector3.Lerp(older.position, newer.position, t);
        interpolated.velocity = Vector3.Lerp(older.velocity, newer.velocity, t);
        interpolated.isGrounded = newer.isGrounded;
        interpolated.isAlive = newer.isAlive;
        interpolated.coinScore = newer.coinScore;
        interpolated.distanceScore = newer.distanceScore;

        return true;
    }

    public void Clear() => snapshots.Clear();
}