using UnityEngine;
using UnityEngine.EventSystems;

public class LineClick : MonoBehaviour, IPointerClickHandler
{
    private LineAnimation _lineAnimation;
    private LineHitChecker _lineHitChecker;
    private LineDestroyer _lineDestroyer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _lineAnimation = GetComponent<LineAnimation>();
        _lineHitChecker = GetComponent<LineHitChecker>();
        _lineDestroyer = GetComponent<LineDestroyer>();

        if (_lineHitChecker != null)
            _lineHitChecker.OnlineHit += HandleLineHit;
    }
    private void HandleLineHit()
    {
        _lineAnimation.Play(false);
        _lineDestroyer.StopCountdown();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("CLICKED!");
        _lineDestroyer.StartCountdown();
        _lineAnimation.Play(true);
        _lineHitChecker.StartChecking();
    }
}