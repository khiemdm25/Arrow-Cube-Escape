using UnityEngine;

public class LineDestroyer : MonoBehaviour
{
    [SerializeField] private float destroyDelay = 3f;
    private float _countdown;
    private bool _counting;

    public void StartCountdown()
    {
        _countdown = destroyDelay;
        _counting = true;
    }

    public void StopCountdown()
    {
        _counting = false;
    }

    private void Update()
    {
        if (!_counting) return;
        _countdown -= Time.deltaTime;
        if (_countdown <= 0f)
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        if (!gameObject.scene.isLoaded) return;
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnArrowDestroyed();
        }
    }
}