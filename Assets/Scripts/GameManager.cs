using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private PlayerController player;
    [SerializeField] private GhostController ghost;

    private bool gameOver = false;

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
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    public void GameOver()
    {
        if (gameOver) return;
        gameOver = true;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        if (player != null)
            player.forwardSpeed = 0f;

        var crash = FindObjectOfType<CrashEffect>();
        if (crash != null) crash.TriggerCrash();

        if (CameraShake.Instance != null)
            CameraShake.Instance.Shake();
    }

    public void OnRestartButton()
    {
        if (SyncManager.Instance != null)
            SyncManager.Instance.Clear();

        if (GameEventRecorder.Instance != null)
            GameEventRecorder.Instance.ClearEvents();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnMainMenuButton()
    {
        if (SyncManager.Instance != null)
            SyncManager.Instance.Clear();

        SceneManager.LoadScene("MainMenu");
    }
}