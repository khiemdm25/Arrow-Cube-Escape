using UnityEngine;
using UnityEngine.EventSystems;

public class LineClick : MonoBehaviour, IPointerClickHandler
{
    private LineAnimation _lineAnimation;
    private LineHitChecker _lineHitChecker;
    private LineDestroyer _lineDestroyer;
    private LineShake _lineShake;

    void Start()
    {
        _lineAnimation = GetComponent<LineAnimation>();
        _lineHitChecker = GetComponent<LineHitChecker>();
        _lineDestroyer = GetComponent<LineDestroyer>();
        _lineShake = GetComponent<LineShake>();

        if (_lineHitChecker != null)
            _lineHitChecker.OnlineHit += HandleLineHit;
    }

    private void HandleLineHit()
    {
        _lineAnimation.Play(false);
        _lineDestroyer.StopCountdown();

        if (_lineShake != null)
            _lineShake.PlayShake();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("click");

        _lineDestroyer.StartCountdown();
        _lineAnimation.Play(true);

        if (_lineHitChecker != null)
        {
            _lineHitChecker.StartChecking();
        }
    }
}