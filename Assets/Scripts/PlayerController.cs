using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float forwardSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Spawner spawner;
    [SerializeField] private float speedIncreaseRate = 0.1f;
    [SerializeField] private float maxSpeed = 20f;

    private float fallThreshold = -5f;

    private Rigidbody rb;
    private bool isGrounded;
    public bool isAlive = true;

    [HideInInspector] public bool collectedOrbThisFrame = false;
    [HideInInspector] public bool hitObstacleThisFrame = false;

    public ScoreManager scoreManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        if (ScoreManager.Instance != null)
            scoreManager = ScoreManager.Instance;
    }

    private void Update()
    {
        if (!isAlive) return;

        bool jumpInput = Input.GetMouseButtonDown(0);

        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
                jumpInput = true;
        }

        if (jumpInput && isGrounded)
            Jump();

        if (transform.position.y < fallThreshold)
        {
            Die();
            GameManager.Instance.GameOver();
        }

        forwardSpeed = Mathf.Min(forwardSpeed + speedIncreaseRate * Time.deltaTime, maxSpeed);
    }

    private void FixedUpdate()
    {
        if (!isAlive) return;

        Vector3 velocity = rb.linearVelocity;
        velocity.z = forwardSpeed;
        rb.linearVelocity = velocity;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        if (spawner != null)
            spawner.SpawnMoreAhead(transform.position.z);

        RecordNetworkSnapshot();

        collectedOrbThisFrame = false;
        hitObstacleThisFrame = false;
    }

    private void Jump()
    {
        Vector3 vel = rb.linearVelocity;
        vel.y = 0f;
        rb.linearVelocity = vel;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void RecordNetworkSnapshot()
    {
        if (SyncManager.Instance == null) return;

        int coinScore = 0;
        int distanceScore = 0;

        if (scoreManager != null)
        {
            coinScore = scoreManager.GetCoinScore();
            distanceScore = scoreManager.GetDistanceScore();
        }

        SyncManager.PlayerSnapshot snap = new SyncManager.PlayerSnapshot
        {
            time = Time.time,
            position = transform.position,
            velocity = rb.linearVelocity,
            isGrounded = isGrounded,
            isAlive = isAlive,
            coinScore = coinScore,
            distanceScore = distanceScore
        };

        SyncManager.Instance.RecordSnapshot(snap);
    }

    public void Die()
    {
        isAlive = false;
        rb.linearVelocity = Vector3.zero;
    }
}
