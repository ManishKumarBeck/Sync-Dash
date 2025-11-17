using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("UI References")]
    [SerializeField] private TMP_Text distanceText;
    [SerializeField] private TMP_Text coinText;

    [Header("Settings")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float distanceMultiplier = 1f;
    [SerializeField] private int coinValue = 10;

    private float startZ;
    private int coinScore = 0;
    private int distanceScore = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        if (playerTransform != null)
            startZ = playerTransform.position.z;

        UpdateUI();
    }

    private void Update()
    {
        if (playerTransform == null) return;

        float distance = Mathf.Max(0, playerTransform.position.z - startZ);
        distanceScore = Mathf.FloorToInt(distance * distanceMultiplier);
        UpdateUI();
    }

    public void AddCoin()
    {
        coinScore += coinValue;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (distanceText != null)
            distanceText.text = "Distance: " + distanceScore;

        if (coinText != null)
            coinText.text = "Coins: " + coinScore;
    }

    public int GetCoinScore() => coinScore;
    public int GetDistanceScore() => distanceScore;

    public void ResetScores()
    {
        coinScore = 0;
        if (playerTransform != null)
            startZ = playerTransform.position.z;
        UpdateUI();
    }
}