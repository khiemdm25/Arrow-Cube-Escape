using DG.Tweening;
using UnityEngine;

public class EvenButton : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] GameObject menuPanel;
    [SerializeField] RectTransform levelPanel;

    [Header("Animation")]
    [SerializeField] float slideDistance = 800f;
    [SerializeField] float duration = 0.5f;

    private Vector2 levelOriginalPos;

    void Start()
    {
        // lưu vị trí gốc của level panel
        levelOriginalPos = levelPanel.anchoredPosition;

        // ẩn level panel ban đầu (đưa lên trên)
        levelPanel.anchoredPosition = levelOriginalPos + Vector2.up * slideDistance;
        levelPanel.gameObject.SetActive(false);
    }

    public void PlayMenu()
    {
        // ẩn menu (có animation)
        menuPanel.transform
            .DOScale(0f, 0.3f)
            .SetEase(Ease.InBack)
            .OnComplete(() => menuPanel.SetActive(false));

        // hiện level panel
        levelPanel.gameObject.SetActive(true);

        // trượt xuống
        levelPanel
            .DOAnchorPos(levelOriginalPos, duration)
            .SetEase(Ease.OutCubic);
    }
    public void BackToMenu()
    {
        // trượt level panel lên trên
        levelPanel
            .DOAnchorPos(levelOriginalPos + Vector2.up * slideDistance, duration)
            .SetEase(Ease.InCubic)
            .OnComplete(() => levelPanel.gameObject.SetActive(false));
        // hiện menu (có animation)
        menuPanel.SetActive(true);
        menuPanel.transform
            .DOScale(1f, 0.3f)
            .SetEase(Ease.OutBack);
    }
}
