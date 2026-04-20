using UnityEngine;
using UnityEngine.InputSystem;

public class RotateCube : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 0.2f;
    [SerializeField] float damping = 5f;

    private Vector2 lastMousePos;
    private Vector2 velocity;
    private bool dragging;

    void Update()
    {
        var mouse = Mouse.current;

        if (mouse.leftButton.wasPressedThisFrame)
        {
            dragging = true;
            lastMousePos = mouse.position.ReadValue();
            velocity = Vector2.zero;
        }

        if (mouse.leftButton.wasReleasedThisFrame)
        {
            dragging = false;
        }

        if (dragging)
        {
            Vector2 currentPos = mouse.position.ReadValue();
            Vector2 delta = currentPos - lastMousePos;

            velocity = delta * rotateSpeed;

            Rotate(velocity);
            lastMousePos = currentPos;
        }
        else
        {
            velocity = Vector2.Lerp(velocity, Vector2.zero, damping * Time.deltaTime);
            Rotate(velocity);
        }
    }

    void Rotate(Vector2 delta)
    {
        float rotX = -delta.y;
        float rotY = delta.x;

        transform.Rotate(Vector3.up, rotY, Space.World);
        transform.Rotate(Vector3.right, rotX, Space.Self);
    }
}
