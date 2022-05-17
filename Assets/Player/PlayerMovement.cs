using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float forceMagnitude;
    [SerializeField] float maxVelocity;
    [SerializeField] float rotationSpeed;
    Rigidbody rb;
    Camera mainCamera;
    Vector3 movementDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        ProcessInput();

        KeepPlayerOnScreen();

        RotateToFaceVelocity();
    }

    void FixedUpdate()
    {
        if(movementDirection == Vector3.zero) return;

        rb.AddForce(movementDirection * forceMagnitude * Time.fixedDeltaTime, ForceMode.Force);

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);    
    }

    void ProcessInput()
    {
        if(Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();

            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);

            movementDirection = transform.position - worldPosition;
            movementDirection.z = 0f;
            movementDirection.Normalize();
        }
        else
        {
            movementDirection = Vector3.zero;
        }
    }

    void KeepPlayerOnScreen()
    {
        Vector3 newPosition = transform.position;

        Vector3 viewportPosition =  mainCamera.WorldToViewportPoint(transform.position);

        if(viewportPosition.x > 1f)
        {
            newPosition.x = -newPosition.x + 0.1f;
        }
        else if(viewportPosition.x < 0f)
        {
            newPosition.x = -newPosition.x - 0.1f;
        }

        if(viewportPosition.y > 1f)
        {
            newPosition.y = -newPosition.y + 0.1f;
        }
        else if(viewportPosition.y < 0f)
        {
            newPosition.y = -newPosition.y - 0.1f;
        }

        transform.position = newPosition;
    }

    void RotateToFaceVelocity()
    {

        if(rb.velocity == Vector3.zero) return;
        
        Quaternion targetRotation = Quaternion.LookRotation(rb.velocity, Vector3.back);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

}
