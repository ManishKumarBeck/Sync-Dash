using UnityEngine;


public class GhostController : MonoBehaviour
{
    public float laneX = -2.5f;
    public float interpolationFactor = 15f;  // controls smoothing
    private Rigidbody rb;

    private Vector3 renderPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    private void FixedUpdate()
    {
        
    }

    private void Update()
    {
        if (SyncManager.Instance == null)
            return;

        SyncManager.PlayerSnapshot snap;
        if (!SyncManager.Instance.TryGetInterpolatedSnapshot(Time.time, out snap))
            return;

        Vector3 target = snap.position;
        target.x = laneX;
        
        target.y = Mathf.Lerp(renderPosition.y, snap.position.y, 0.25f);

        // Smooth interpolation for visual render
        renderPosition = Vector3.Lerp(renderPosition == Vector3.zero ? target : renderPosition, target, interpolationFactor * Time.deltaTime);
        rb.MovePosition(renderPosition);
    }
}