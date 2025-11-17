using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, 5, -10);
    [SerializeField] private float followSpeed = 5f;
    [SerializeField] private bool followZOnly = true;

    private Vector3 initialOffset;

    // Store the initial offset from the target
    private void Start()
    {
        if (target != null)
        {
            initialOffset = transform.position - target.position;
        }
    }

    // Smoothly follow the target each frame
    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;

        if (followZOnly)
        {
            desiredPosition.x = transform.position.x;
            desiredPosition.y = transform.position.y;
        }

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            followSpeed * Time.deltaTime
        );
    }
}