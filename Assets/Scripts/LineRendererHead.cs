using UnityEngine;

[ExecuteAlways]
public class LineRendererHead : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float rotationOffset = 0f;

    private void LateUpdate()
    {
        if (!lineRenderer || lineRenderer.positionCount < 2) return;
        int last = lineRenderer.positionCount - 1;
        Vector3 end = lineRenderer.GetPosition(last);
        Vector3 prev = lineRenderer.GetPosition(last - 1);

        transform.localPosition = end;

        Vector3 dir = (end - prev).normalized;
        if (dir.sqrMagnitude < 0.0000000001f) return;

        //transform.rotation = Quaternion.LookRotation(dir) * Quaternion.Euler(0, rotationOffset, 0);
    }
}