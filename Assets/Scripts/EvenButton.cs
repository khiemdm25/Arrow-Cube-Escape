using DG.Tweening;
using UnityEngine;

public class EvenButton : MonoBehaviour
{
    [SerializeField] GameObject menuPanel;
    [SerializeField] RectTransform levelPanel;

    [SerializeField] float slideDistance = 800f;
    [SerializeField] float duration = 0.5f;

    private Vector2 levelOriginalPos;

    void Start()
    {
        levelOriginalPos = levelPanel.anchoredPosition;
        levelPanel.anchoredPosition = levelOriginalPos + Vector2.up * slideDistance;
        levelPanel.gameObject.SetActive(false);
    }

    public void PlayMenu()
    {

        menuPanel.transform.DOScale(0f, 0.3f).SetEase(Ease.InBack).OnComplete(() => menuPanel.SetActive(false));
        levelPanel.gameObject.SetActive(true);
        levelPanel.DOAnchorPos(levelOriginalPos, duration).SetEase(Ease.OutCubic);
    }
    public void BackToMenu()
    {
        levelPanel.DOAnchorPos(levelOriginalPos + Vector2.up * slideDistance, duration).SetEase(Ease.InCubic).OnComplete(() => levelPanel.gameObject.SetActive(false));

        menuPanel.SetActive(true);
        menuPanel.transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack);
    }
}
