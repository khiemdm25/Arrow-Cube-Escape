using UnityEngine;

public class Zoom : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private float zoomSpeed = 0.01f;
    [SerializeField] private float minDistance = 3f;
    [SerializeField] private float maxDistance = 15f;
    [SerializeField] private float smooth = 10f;

    private float currentDistance;
    private float targetDistance;

    void Start()
    {
        if (target != null)
        {
            currentDistance = Vector3.Distance(transform.position, target.position);
            targetDistance = currentDistance;
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        Renderer rend = target.GetComponentInChildren<Renderer>();
        if (rend != null)
        {
            float radius = rend.bounds.extents.magnitude;
            minDistance = radius * 1.75f;
        }
        currentDistance = Vector3.Distance(transform.position, target.position);
        targetDistance = currentDistance;
    }

    void Update()
    {
        if (target == null) return;
        if (InputState.IsRotating)
        {
            InputState.IsZooming = false;
            return;
        }
        float delta = 0f;
        if (Input.touchCount == 2)
        {
            InputState.IsZooming = true;
            InputState.IsRotating = false;
            Touch t0 = Input.GetTouch(0);
            Touch t1 = Input.GetTouch(1);
            Vector2 prev0 = t0.position - t0.deltaPosition;
            Vector2 prev1 = t1.position - t1.deltaPosition;
            float prevDist = (prev0 - prev1).magnitude;
            float currDist = (t0.position - t1.position).magnitude;
            delta = currDist - prevDist;
        }
        else
        {
            InputState.IsZooming = false;
            delta = Input.mouseScrollDelta.y * 100f;
        }

        if (Mathf.Abs(delta) > 0.01f)
        {
            targetDistance -= delta * zoomSpeed;
            targetDistance = Mathf.Clamp(targetDistance, minDistance, maxDistance);
        }
        currentDistance = Mathf.Lerp(currentDistance, targetDistance, smooth * Time.deltaTime);
        Vector3 dir = (transform.position - target.position).normalized;
        transform.position = target.position + dir * currentDistance;
        transform.LookAt(target);
    }
}