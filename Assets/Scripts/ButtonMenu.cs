using UnityEngine;
using DG.Tweening;

public class MenuAnimation : MonoBehaviour
{
    [SerializeField] RectTransform[] buttons; // tất cả button
    [SerializeField] RectTransform playButton; // nút Play

    [Header("Drop Settings")]
    [SerializeField] float dropDistance = 500f;
    [SerializeField] float dropDuration = 0.5f;
    [SerializeField] float delayBetween = 0.1f;

    [Header("Play Button Pulse")]
    [SerializeField] float scaleUp = 1.2f;
    [SerializeField] float pulseDuration = 0.6f;

    void Start()
    {
        AnimateButtons();
    }

    void AnimateButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            RectTransform btn = buttons[i];

            Vector2 originalPos = btn.anchoredPosition;

            // đặt button lên trên trước
            btn.anchoredPosition = originalPos + Vector2.up * dropDistance;

            // rơi xuống
            btn.DOAnchorPos(originalPos, dropDuration)
                .SetEase(Ease.OutBounce)
                .SetDelay(i * delayBetween);
        }

        // hiệu ứng riêng cho nút Play
        PlayPulse();
    }

    void PlayPulse()
    {
        playButton
            .DOScale(scaleUp, pulseDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }
}