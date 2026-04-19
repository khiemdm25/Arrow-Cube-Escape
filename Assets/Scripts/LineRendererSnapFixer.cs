using UnityEngine;
[ExecuteAlways]
[RequireComponent(typeof(LineRenderer))]
public class LineRendererSnapFixer : MonoBehaviour
{
    public float snapSize = 1f;
    public bool sanpInEditor = true;
    public bool snapAtRunTime = false;

    [SerializeField] private LineRenderer lr;

    private void Awake()
    {
        if (lr == null)
            lr = GetComponent<LineRenderer>();
    }

    private void LateUpdate()
    {
        if (!lr) return;

        if (Application.isPlaying && !sanpInEditor) return;

        if (Application.isPlaying && !snapAtRunTime) return;

        SnapPosition();
    }

    private void SnapPosition()
    {
        int count = lr.positionCount;
        if (count == 0) return;

        for (int i = 0; i < count; i++)
        {
            Vector3 p = lr.GetPosition(i);
            p = Snap(p);
            lr.SetPosition(i, p);
        }
    }

    private void Clear()
    {
        lr.positionCount = 0;
    }

    private Vector3 Snap(Vector3 v)
    {
        v.x = (float)Mathf.Round(v.x * 1000f) / 1000f;
        v.y = (float)Mathf.Round(v.y * 1000f) / 1000f;
        v.z = (float)Mathf.Round(v.z * 1000f) / 1000f;
        return v;
    }
}