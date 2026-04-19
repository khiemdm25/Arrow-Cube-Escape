using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SimpleUIController : MonoBehaviour
{
    [Header("Scene Name")]
    [SerializeField] private string playSceneName = "PlayScene";
    [SerializeField] private string menuSceneName = "MenuScene";

    [Header("Buttons")]
    [SerializeField] private Button nextButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button homeButton;

    private void Start()
    {
        SetupNext();
        SetupBack();
        SetupHome();
    }

    // ========= NEXT LEVEL =========
    void SetupNext()
    {
        if (nextButton == null) return;

        nextButton.onClick.RemoveAllListeners();
        nextButton.onClick.AddListener(() =>
        {
            LevelManager.Instance.NextLevel();
        });
    }

    // ========= BACK =========
    void SetupBack()
    {
        if (backButton == null) return;

        backButton.onClick.RemoveAllListeners();
        backButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(menuSceneName);
        });
    }

    // ========= HOME =========
    void SetupHome()
    {
        if (homeButton == null) return;

        homeButton.onClick.RemoveAllListeners();
        homeButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(menuSceneName);
        });
    }
}