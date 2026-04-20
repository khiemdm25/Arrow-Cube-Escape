using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SimpleUIController : MonoBehaviour
{
    [SerializeField] private string playSceneName = "PlayScene";
    [SerializeField] private string menuSceneName = "MenuScene";

    [SerializeField] private Button nextButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button homeButton;

    private void Start()
    {
        SetupNext();
        SetupBack();
        SetupHome();
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
            SceneManager.LoadScene(menuSceneName);
        });
    }

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