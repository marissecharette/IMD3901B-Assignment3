using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class PlayerController : NetworkBehaviour
{
    public float speed = 5f;
    public float mouseSensitivity = 2f;

    // Gravity stuff
    public float gravity = -9.81f;
    public float groundedGravity = -2f;

    public CharacterController controller;
    public Transform cameraTransform;

    public Camera playerCamera;

    float xRotation = 0f;
    float yVelocity = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (!IsOwner)
        {
            // Disable the camera for the other player so they aren't sharing a camera, I was having a weird bug with this earlier
            playerCamera.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner)
        {
            return;
        }

        // Movement
        Vector2 moveInput = Keyboard.current != null ? new Vector2 
            (
                (Keyboard.current.aKey.isPressed ? -1 : 0) + (Keyboard.current.dKey.isPressed ? 1 : 0),
                (Keyboard.current.sKey.isPressed ? -1 : 0) + (Keyboard.current.wKey.isPressed ? 1 : 0)
            ) : Vector2.zero;
        
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;

        // Gravity to keep the player grounded to slopes
        if (controller.isGrounded)
        {
            if (yVelocity < 0)
            {
                yVelocity = groundedGravity;
            }
        }
        else
        {
            yVelocity += gravity * Time.deltaTime;
        }

        Vector3 verticalMove = Vector3.up * yVelocity;

        controller.Move((move * speed + verticalMove) * Time.deltaTime);

        // Mouse
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        float mouseX = mouseDelta.x * mouseSensitivity * Time.deltaTime;
        float mouseY = mouseDelta.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
        

    }
}