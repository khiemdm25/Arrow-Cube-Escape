using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class LineSegmeniColliderSpawner2D : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;

    [SerializeField] private GameObject segmentPrefab;

    [SerializeField] private float thickness = 0.2f;

    [SerializeField] private float extraLength = 0.2f;

    [SerializeField] private bool autoUpdateInPlayMode = true;

    private readonly List<GameObject> _spawnedSegments = new();

    [ContextMenu("rebuild")]
    private void RebuildSegmentsContextMenu()
    {
        RebuildSegments();
    }

    private void RebuildSegments()
    {
        if (!lineRenderer || !segmentPrefab) return;

        int count = lineRenderer.positionCount;
        if (count < 2)
        {
            ClearSegments();
            return;
        }

        ClearSegments();

        bool useWorld = lineRenderer.useWorldSpace;

        for (int i = 0; i < count - 1; i++)
        {
            Vector3 a = lineRenderer.GetPosition(i);
            Vector3 b = lineRenderer.GetPosition(i + 1);
            if (!useWorld)
            {
                a = lineRenderer.transform.TransformPoint(a);
                b = lineRenderer.transform.TransformPoint(b);
            }

            Vector3 dir = b - a;
            float length = dir.magnitude;
            if (length < 0.001f) continue;

            Vector3 mid = (a + b) * 0.5f;
            Quaternion rotation = Quaternion.LookRotation(dir);

            GameObject segment = Instantiate(segmentPrefab, mid, rotation, transform);
            _spawnedSegments.Add(segment);

            BoxCollider box = segment.GetComponent<BoxCollider>();

            if (box != null)
            {
                Vector3 size = box.size;
                size.z = length + extraLength;
                size.x = thickness;
                size.y = thickness;
                box.size = size;
                box.center = Vector3.zero;

            }
        }
    }

    public void ClearSegments()
    {
        foreach (var go in _spawnedSegments)
        {
            if (!go) continue;

            if (!Application.isPlaying) DestroyImmediate(go);
            else Destroy(go);

        }

        _spawnedSegments.Clear();
    }
}
