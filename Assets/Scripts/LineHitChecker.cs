using System;
using UnityEngine;

public class LineHitChecker : MonoBehaviour
{
    private LineRayCastGun _lineRaycastGun;
    private bool _active;

    public Action OnlineHit;

    private void Start()
    {
        _lineRaycastGun = GetComponent<LineRayCastGun>();
    }

    public void StartChecking()
    {
        _active = true;
    }

    private void Update()
    {
        if (!_active) return;
        if (!_lineRaycastGun.Shoot()) return;

        _active = false;
        OnlineHit?.Invoke();
    }
}