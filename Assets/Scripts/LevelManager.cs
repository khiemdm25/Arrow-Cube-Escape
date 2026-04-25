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

        if (levelData != null && levelData.alllevel.Count > 0)
        {
            highestUnlockedLevel = Mathf.Clamp(
                highestUnlockedLevel,
                0,
                levelData.alllevel.Count - 1
            );
        }
    }

    void SaveProgress()
    {
        PlayerPrefs.SetInt(SAVE_KEY, highestUnlockedLevel);
        PlayerPrefs.Save();
    }

    public LevelSO GetCurrentLevel()
    {
        if (levelData == null || levelData.alllevel.Count == 0)
        {
            Debug.LogError("LevelData rỗng!");
            return null;
        }
        currentLevelIndex = highestUnlockedLevel;
        return levelData.alllevel[currentLevelIndex];
    }
    public void Play()
    {
        currentLevelIndex = highestUnlockedLevel;
        SceneManager.LoadScene("PlayScene");
    }
    public void WinLevel()
    {
        Debug.Log("Win Level: " + currentLevelIndex);

        if (currentLevelIndex == highestUnlockedLevel)
        {
            highestUnlockedLevel++;
            if (highestUnlockedLevel >= levelData.alllevel.Count)
            {
                highestUnlockedLevel = levelData.alllevel.Count - 1;
            }
            SaveProgress();
            Debug.Log("Unlock Level: " + highestUnlockedLevel);
        }
    }

    public void NextLevel()
    {
        if (currentLevelIndex < highestUnlockedLevel)
        {
            currentLevelIndex++;
        }
        SceneManager.LoadScene("PlayScene");
    }
}