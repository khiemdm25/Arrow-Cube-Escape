using UnityEngine;

public class LineRayCastGun : MonoBehaviour
{
    private Transform head;
    private float rayLength = 5f;

    [SerializeField] private float offset = 0.5f;
    [SerializeField] private LayerMask layerMask = ~0;
    [SerializeField] private bool shootEveryFrame = true;

    [SerializeField] private bool drawGizmos = true;

    public RaycastHit LastHit { get; private set; }

    private void Reset()
    {
        if (!head) head = transform;
    }

    private void Update()
    {
        if (!shootEveryFrame) return;

        Shoot();

    }

    public bool Shoot()
    {
        if (!ensureHead()) return false;

        Vector3 origin = GetOrigin();
        Vector3 direction = transform.forward.normalized;

        RaycastHit hitInfo;
        bool hit = Physics.Raycast(origin, direction, out hitInfo, rayLength, layerMask);

        if (hit)
        {
            LastHit = hitInfo;
            return true;
        }

        return false;
    }


    private bool ensureHead()
    {
        if (head != null) return true;
        head = transform;
        return head != null;
    }
    private Vector3 GetOrigin()
    {
        return head.position + head.up * offset;
    }
    private void OnDrawGizmos()
    {
        if (!drawGizmos) return;
        if (!ensureHead()) return;
        Vector3 origin = GetOrigin();
        Vector3 direction = head.up;

        float length = rayLength;
        Vector3 end = origin + direction * length;
        Color rayColor = Color.red;

        if (Application.isPlaying && LastHit.collider != null)
        {
            end = LastHit.point;
            rayColor = Color.green;
        }

        Gizmos.color = rayColor;
        Gizmos.DrawLine(origin, end);
        Gizmos.DrawSphere(end, 0.06f);
    }
}
