using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SimpleUIController : MonoBehaviour
{
    [SerializeField] private string playSceneName = "PlayScene";
    [SerializeField] private GameObject pauseMenu;

    private bool isPaused = false;

    [SerializeField] private Button nextButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button homeButton;

    [SerializeField] private Button pauseButton;
    [SerializeField] private Button playButton;
    [SerializeField] private Button homePauseButton;
    [SerializeField] private Button RestartButton;

    private void Start()
    {
        SetupNext();
        SetupBack();
        SetupHome();

        SetupPause();
        ResumeGame();
        HomeFromPause();
        RestartLevel();
    }

    void SetupNext()
    {
        if (nextButton == null) return;

        nextButton.onClick.RemoveAllListeners();
        nextButton.onClick.AddListener(() =>
        {
            LevelManager.Instance.NextLevel();
        });
    }

    void SetupBack()
    {
        if (backButton == null) return;

        backButton.onClick.RemoveAllListeners();
        backButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
    }

    void SetupHome()
    {
        if (homeButton == null) return;

        homeButton.onClick.RemoveAllListeners();
        homeButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }

    public void SetupPause()
    {
        if (pauseButton == null) return;

        pauseButton.onClick.RemoveAllListeners();
        pauseButton.onClick.AddListener(TogglePause);
    }

    void TogglePause()
    {
        isPaused = !isPaused;

        pauseMenu.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void ResumeGame()
    {
        if (playButton == null) return;
        playButton.onClick.RemoveAllListeners();
        playButton.onClick.AddListener(() =>
        {
            isPaused = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        });
    }

    public void HomeFromPause()
    {
        if (homePauseButton == null) return;
        homePauseButton.onClick.RemoveAllListeners();
        homePauseButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }

    public void RestartLevel()
    {
        if (RestartButton == null) return;
        RestartButton.onClick.RemoveAllListeners();
        RestartButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
    }
}