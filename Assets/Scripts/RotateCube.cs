using UnityEngine;

public class RotateCube : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 0.2f;
    [SerializeField] float damping = 5f;

    private Vector2 lastPos;
    private Vector2 velocity;
    private bool dragging;

    void Update()
    {
        if (InputState.IsZooming)
        {
            dragging = false;
            return;
        }

        Vector2 currentPos = Vector2.zero;
        bool pressDown = false;
        bool pressUp = false;
        bool isPressing = false;
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            currentPos = touch.position;
            pressDown = touch.phase == TouchPhase.Began;
            pressUp = touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled;
            isPressing = touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary;
        }
        else
        {
            currentPos = Input.mousePosition;
            pressDown = Input.GetMouseButtonDown(0);
            pressUp = Input.GetMouseButtonUp(0);
            isPressing = Input.GetMouseButton(0);
        }
        if (pressDown)
        {
            dragging = true;
            lastPos = currentPos;
            velocity = Vector2.zero;
        }
        if (pressUp)
        {
            dragging = false;
        }
        if (dragging && isPressing)
        {
            InputState.IsRotating = true; 
            Vector2 delta = currentPos - lastPos;
            velocity = delta * rotateSpeed;
            Rotate(velocity);
            lastPos = currentPos;
        }
        else
        {
            InputState.IsRotating = false;
            velocity = Vector2.Lerp(velocity, Vector2.zero, damping * Time.deltaTime);
            Rotate(velocity);
        }
    }

    void Rotate(Vector2 delta)
    {
        Vector3 axis = new Vector3(delta.y, -delta.x, 0f);
        float angle = axis.magnitude;
        if (angle < 0.001f) return;
        transform.Rotate(axis, angle, Space.World);
    }
}