using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject winPanel;
    private int arrowCount;
    private bool isWin = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        arrowCount = FindObjectsOfType<LineDestroyer>().Length;

        if (winPanel != null)
            winPanel.SetActive(false);

        isWin = false;
    }

    public void OnArrowDestroyed()
    {
        if (isWin) return;
        arrowCount--;
        if (arrowCount <= 0)
        {
            isWin = true;
            Win();
        }
    }

    void Win()
    {
        LevelManager.Instance.WinLevel();

        if (winPanel != null)
            winPanel.SetActive(true);
    }

}