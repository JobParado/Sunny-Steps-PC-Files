using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI Hierarchy")]
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject gameOverGroup;
    [SerializeField] private GameObject victoryGroup;
    [SerializeField] private GameObject pauseGroup;
    [SerializeField] private GameObject scoreGroup;
    [SerializeField] private GameObject pauseButton;

    // NEW: Reference the actual Text object directly to force it on
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Audio Settings")]
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioSource gameOverMusic;
    [SerializeField] private AudioSource victoryMusic;

    public void disablePauseButton()
    {
        pauseButton.SetActive(false);
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        if (gameUI != null) gameUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PauseButton()
    {
        GameManager.Instance.TogglePause();
    }
    private void ShowEndUI(GameObject groupToShow)
    {
        gameUI.SetActive(true);
        mainPanel.SetActive(true);

        // Turn off all main groups first
        gameOverGroup.SetActive(false);
        victoryGroup.SetActive(false);
        pauseGroup.SetActive(false);

        // Turn on the specific group (Win or Loss)
        groupToShow.SetActive(true);

        // Force enable the Score Group AND the Text child
        if (scoreGroup != null)
        {
            scoreGroup.SetActive(true);
            if (scoreText != null)
            {
                scoreText.gameObject.SetActive(true);
                // This "Dirty" call forces TMP to redraw itself immediately
                scoreText.SetAllDirty();
            }
        }
    }

    public void TriggerGameOver(float delay)
    {
        if (backgroundMusic != null) backgroundMusic.Stop();
        StartCoroutine(GameOverRoutine(delay));
    }

    private IEnumerator GameOverRoutine(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        ShowEndUI(gameOverGroup);
        if (gameOverMusic != null) gameOverMusic.Play();
        Time.timeScale = 0f;
    }

    public void TriggerVictory()
    {
        if (backgroundMusic != null) backgroundMusic.Stop();
        ShowEndUI(victoryGroup);
        if (victoryMusic != null && !victoryMusic.isPlaying) victoryMusic.Play();
        Time.timeScale = 0f;
    }

    public void TogglePause()
    {
        gameUI.SetActive(true);
        mainPanel.SetActive(true);
        pauseGroup.SetActive(true);

        Time.timeScale = 0f;
        backgroundMusic.Pause();

        gameOverGroup.SetActive(false);
        victoryGroup.SetActive(false);
        scoreGroup.SetActive(false);
        pauseButton.SetActive(false);
    }

    public void Resume()
    {
        gameUI.SetActive(false);
        mainPanel.SetActive(false);
        pauseGroup.SetActive(false);
        gameOverGroup.SetActive(false);
        victoryGroup.SetActive(false);
        scoreGroup.SetActive(false);
        Time.timeScale = 1f;
        backgroundMusic.Play();
        pauseButton.SetActive(true);
    }

    public void RetryLevel()
    {
        // Reset the time scale to normal before reloading the scene
        Time.timeScale = 1f;

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {

        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}