using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Win")]
    public GameObject vfx;
    public GameObject winPanel;

    [Header("Lose")]
    public GameObject gameOverPanel;
    [SerializeField] private int maxLives = 3;
    private int currentLives;

    [Header("UI Hearts")]
    [SerializeField] private Transform heartParent;   
    [SerializeField] private GameObject heartPrefab;  
    [SerializeField] private Sprite heartFull;        
    [SerializeField] private Sprite heartEmpty;      

    private Image[] hearts;

    private int arrowCount;
    private bool isWin = false;
    private bool isGameOver = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        arrowCount = FindObjectsOfType<LineDestroyer>().Length;

        if (winPanel != null) winPanel.SetActive(false);
        if (vfx != null) vfx.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);

        currentLives = maxLives;

        CreateHearts();

        isWin = false;
        isGameOver = false;
    }

    private void CreateHearts()
    {
        hearts = new Image[maxLives];

        for (int i = 0; i < maxLives; i++)
        {
            GameObject heart = Instantiate(heartPrefab, heartParent);
            hearts[i] = heart.GetComponent<Image>();
        }

        UpdateHeartsUI();
    }

    private void UpdateHeartsUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentLives)
                hearts[i].sprite = heartFull;
            else
                hearts[i].sprite = heartEmpty;
        }
    }

    public void LoseLife()
    {
        if (isWin || isGameOver) return;

        currentLives--;
        Debug.Log("Lives: " + currentLives);

        UpdateHeartsUI(); 

        if (currentLives <= 0)
        {
            isGameOver = true;
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over");

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }

    public void OnArrowDestroyed()
    {
        if (isWin || isGameOver) return;

        arrowCount--;
        Debug.Log("Arrow left: " + arrowCount);

        if (arrowCount <= 0)
        {
            isWin = true;
            StartCoroutine(Win());
        }
    }

    IEnumerator Win()
    {
        if (vfx != null) vfx.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        if (winPanel != null) winPanel.SetActive(true);

        LevelManager.Instance.WinLevel();
    }
}