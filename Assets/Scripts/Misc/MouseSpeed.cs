using UnityEngine;
using UnityEngine.InputSystem;

public class MouseSpeed : MonoBehaviour
{
    public float speedMultiplier = 1.0f; // Adjust for desired sensitivity
    private Vector2 lastMousePosition;
    public Vector2 mouseDelta { get; private set; }

    public float speedX;
    public float speedY;

    void Start()
    {
        lastMousePosition = Mouse.current.position.ReadValue();
        InvokeRepeating("Position", 0, 1);
    }

    void Position()
    {
        mouseDelta = Mouse.current.position.ReadValue() - lastMousePosition;
        lastMousePosition = Input.mousePosition;

        // To get speed in pixels per second (or any other units)
        float deltaTime = Time.deltaTime;
        if (deltaTime > 0)
        {
            speedX = mouseDelta.x / deltaTime * speedMultiplier;
            speedY = mouseDelta.y / deltaTime * speedMultiplier;
            // You can use speedX and speedY (in pixels per second, or other units) for your logic
        }
    }
}
