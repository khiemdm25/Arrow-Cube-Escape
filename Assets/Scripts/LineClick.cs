using UnityEngine;
using UnityEngine.EventSystems;

public class LineClick : MonoBehaviour, IPointerClickHandler
{
    private LineAnimation _lineAnimation;
    private LineHitChecker _lineHitChecker;
    private LineDestroyer _lineDestroyer;
    private LineShake _lineShake;
    private LineRayCastGun _lineRaycastGun;

    private bool isBusy = false;

    void Start()
    {
        _lineAnimation = GetComponent<LineAnimation>();
        _lineHitChecker = GetComponent<LineHitChecker>();
        _lineDestroyer = GetComponent<LineDestroyer>();
        _lineShake = GetComponent<LineShake>();
        _lineRaycastGun = GetComponent<LineRayCastGun>();

        if (_lineHitChecker != null) _lineHitChecker.OnlineHit += HandleLineHit;
    }

    private void HandleLineHit()
    {
        _lineAnimation.StopAndReset(); 
        _lineDestroyer.StopCountdown();
        isBusy = false;
        if (_lineShake != null) _lineShake.PlayShake();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (InputState.IsInteracting) return;
        if (isBusy) return;

        if (_lineRaycastGun != null && _lineRaycastGun.Shoot())
        {
            _lineAnimation.StopAndReset();
            if (_lineShake != null) _lineShake.PlayShake();

            GameManager.Instance.LoseLife();
            return;
        }
        isBusy = true;
        _lineDestroyer.StartCountdown();
        _lineAnimation.Play(true);

        if (_lineHitChecker != null)
        {
            _lineHitChecker.StartChecking();
        }
    }
}