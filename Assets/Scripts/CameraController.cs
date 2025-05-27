using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool controllable = true;

    [SerializeField] private InputHandler inputHandler;

    [SerializeField] private Transform cameraRoot;

    [SerializeField] private Transform mainCamera;

    [SerializeField] private float upperLimit = -40f;
    [SerializeField] private float bottomLimit = 70f;

    [SerializeField] private float mouseSensitivity = 22f;

    [SerializeField] private Rigidbody playerRigidbody;

    private float xRot;

    private void LateUpdate()
    {
        CameraMovements();
    }

    private void CameraMovements()
    {
        var x = inputHandler.LookInput.x;
        var y = inputHandler.LookInput.y;

        mainCamera.position = cameraRoot.position;

        xRot -= y * mouseSensitivity * Time.smoothDeltaTime;

        xRot = Mathf.Clamp(xRot, upperLimit, bottomLimit);

        if (!controllable)
            return;

        mainCamera.localRotation = Quaternion.Euler(xRot, 0, 0);

        playerRigidbody.MoveRotation(playerRigidbody.rotation * Quaternion.Euler(0, x * mouseSensitivity * Time.smoothDeltaTime, 0));
    }
}