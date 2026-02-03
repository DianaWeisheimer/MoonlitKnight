using UnityEngine;
using UnityEngine.InputSystem;

public class MouseTracker
{
    public Vector2 mouseDelta { get; private set; }
    public float speedX;
    public float speedY;
    private Vector2 lastMousePosition;

    public MouseTracker(Vector2 _lastMousePosition)
    {
        lastMousePosition = _lastMousePosition;
    }

    public void Tick()
    {
        MouseSpeed();
    }

    private void MouseSpeed()
    {
        mouseDelta = Mouse.current.position.ReadValue() - lastMousePosition;
        lastMousePosition = Input.mousePosition;

        float deltaTime = Time.deltaTime;
        if (deltaTime > 0)
        {
            speedX = mouseDelta.x / deltaTime * 0.001f;
            speedY = mouseDelta.y / deltaTime * 0.001f;
        }
    }
}
