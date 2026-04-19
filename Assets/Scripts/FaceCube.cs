using UnityEngine;

[ExecuteAlways]
public class FaceCube : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform cube;
    [SerializeField] private Transform pra;
    [SerializeField] private GameObject arrowPrefab;

    [Header("Arrow Settings")]
    [SerializeField] private int totalArrow = 6;
    [SerializeField] private float surfaceOffset = 0.5f;

    [Header("Grid Settings")]
    [SerializeField] private int gridX = 5;
    [SerializeField] private int gridY = 5;

    private Vector3[] corners = new Vector3[8];

    private void UpdateCorners()
    {
        if (cube == null) cube = transform;

        MeshFilter mf = cube.GetComponent<MeshFilter>();
        if (mf == null || mf.sharedMesh == null) return;

        Vector3[] vertices = mf.sharedMesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = cube.TransformPoint(vertices[i]);
        }

        Vector3 min = vertices[0];
        Vector3 max = vertices[0];

        foreach (var v in vertices)
        {
            min = Vector3.Min(min, v);
            max = Vector3.Max(max, v);
        }

        corners[0] = new Vector3(min.x, min.y, min.z);
        corners[1] = new Vector3(max.x, min.y, min.z);
        corners[2] = new Vector3(min.x, max.y, min.z);
        corners[3] = new Vector3(max.x, max.y, min.z);
        corners[4] = new Vector3(min.x, min.y, max.z);
        corners[5] = new Vector3(max.x, min.y, max.z);
        corners[6] = new Vector3(min.x, max.y, max.z);
        corners[7] = new Vector3(max.x, max.y, max.z);
    }

    [ContextMenu("Spawn Arrows")]
    public void SpawnArrows()
    {
        UpdateCorners();

        int[,] faces = new int[,]
        {
        {0,1,3,2},
        {4,5,7,6},
        {0,1,5,4},
        {2,3,7,6},
        {0,2,6,4},
        {1,3,7,5}
        };

        int faceCount = 6;

        // 🔹 mỗi mặt ít nhất 1 arrow
        int baseCount = Mathf.Max(1, totalArrow / faceCount);
        int remaining = totalArrow - baseCount * faceCount;

        for (int f = 0; f < faceCount; f++)
        {
            int spawnCount = baseCount;

            // 🔥 random cộng thêm từ phần dư
            if (remaining > 0 && Random.value > 0.5f)
            {
                spawnCount++;
                remaining--;
            }

            Vector3 A = corners[faces[f, 0]];
            Vector3 B = corners[faces[f, 1]];
            Vector3 C = corners[faces[f, 2]];
            Vector3 D = corners[faces[f, 3]];

            Vector3 normal = Vector3.Cross(B - A, D - A).normalized;

            for (int i = 0; i < spawnCount; i++)
            {
                float tx = Random.Range(0f, 1f);
                float ty = Random.Range(0f, 1f);

                Vector3 left = Vector3.Lerp(A, D, ty);
                Vector3 right = Vector3.Lerp(B, C, ty);
                Vector3 pos = Vector3.Lerp(left, right, tx);

                pos += normal * surfaceOffset;

                Quaternion rot = Quaternion.LookRotation(normal);

                GameObject arrow = Instantiate(arrowPrefab, pos, rot);

                arrow.transform.SetParent(pra, true);
                arrow.transform.localScale = Vector3.one;
            }
        }
    }

    private void OnDrawGizmos()
    {
        UpdateCorners();

        Gizmos.color = Color.green;

        int[,] faces = new int[,]
        {
            {0,1,3,2},
            {4,5,7,6},
            {0,1,5,4},
            {2,3,7,6},
            {0,2,6,4},
            {1,3,7,5}
        };

        for (int f = 0; f < 6; f++)
        {
            Vector3 A = corners[faces[f, 0]];
            Vector3 B = corners[faces[f, 1]];
            Vector3 C = corners[faces[f, 2]];
            Vector3 D = corners[faces[f, 3]];

            for (int i = 0; i <= gridX; i++)
            {
                float t = i / (float)gridX;
                Vector3 left = Vector3.Lerp(A, D, t);
                Vector3 right = Vector3.Lerp(B, C, t);
                Gizmos.DrawLine(left, right);
            }

            for (int j = 0; j <= gridY; j++)
            {
                float t = j / (float)gridY;
                Vector3 top = Vector3.Lerp(A, B, t);
                Vector3 bottom = Vector3.Lerp(D, C, t);
                Gizmos.DrawLine(top, bottom);
            }
        }
    }
}