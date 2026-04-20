using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public LevelData levelData;
    public int currentLevelIndex = -1;
    public int highestUnlockedLevel = 0;
    private const string SAVE_KEY = "LEVEL_UNLOCK";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadProgress();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void LoadProgress()
    {
        highestUnlockedLevel = PlayerPrefs.GetInt(SAVE_KEY, 0);
    }

    void SaveProgress()
    {
        PlayerPrefs.SetInt(SAVE_KEY, highestUnlockedLevel);
    }

    public void SelectLevel(int index)
    {
        currentLevelIndex = index;
    }

    public LevelSO GetCurrentLevel()
    {
        if (currentLevelIndex < 0)
            currentLevelIndex = highestUnlockedLevel;

        return levelData.alllevel[currentLevelIndex];
    }

    public void Play()
    {
        if (currentLevelIndex < 0)
            currentLevelIndex = highestUnlockedLevel;

        SceneManager.LoadScene("PlayScene");
    }

    public void WinLevel()
    {
        if (currentLevelIndex >= highestUnlockedLevel)
        {
            highestUnlockedLevel = currentLevelIndex + 1;
            SaveProgress();
        }
    }

    public void NextLevel()
    {
        currentLevelIndex++;

        if (currentLevelIndex >= levelData.alllevel.Count)
            currentLevelIndex = 0;

        SceneManager.LoadScene("PlayScene");
    }
}