using UnityEngine;

public class FirstPersonMouseLock : MonoBehaviour
{
    [Header("Mouse Look Settings")]
    public float mouseSensitivity = 100f;
    public bool invertYAxis = false;

    [Header("Rotation Limits")]
    public float minVerticalAngle = -90f;
    public float maxVerticalAngle = 90f;

    private float xRotation = 0f;
    private Transform playerBody;
    private Camera playerCamera;

    void Start()
    {
        playerBody = transform;
        playerCamera = GetComponentInChildren<Camera>();

        // Lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Handle vertical rotation (up/down)
        xRotation += (invertYAxis ? mouseY : -mouseY);
        xRotation = Mathf.Clamp(xRotation, minVerticalAngle, maxVerticalAngle);

        // Apply vertical rotation to the camera
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Handle horizontal rotation (left/right) for the player body
        playerBody.Rotate(Vector3.up * mouseX);
    }

    void OnDisable()
    {
        // Release the cursor when this script is disabled
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
