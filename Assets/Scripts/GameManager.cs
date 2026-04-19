using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject winPanel;

    private int arrowCount;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        arrowCount = FindObjectsOfType<LineDestroyer>().Length;
        winPanel.SetActive(false);
    }

    public void OnArrowDestroyed()
    {
        arrowCount--;

        if (arrowCount <= 0)
        {
            Win();
        }
    }

    void Win()
    {
        LevelManager.Instance.WinLevel();
        winPanel.SetActive(true);
    }
}