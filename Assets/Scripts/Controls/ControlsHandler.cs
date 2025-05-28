using UnityEngine;

public class ControlsHandler : MonoBehaviour
{
    [SerializeField] private CharaController charaController;
    [SerializeField] private CameraController cameraController;

    private void Start()
    {
        DisableCursor();
    }

    public void DisableCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void EnableCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void ChangeMovementStatus(bool enabled)
    {
        charaController.enabled = enabled;
        cameraController.controllable = enabled;
    }

    public void DisableControls()
    {
        EnableCursor();
        ChangeMovementStatus(false);
    }

    public void EnableControls()
    {
        DisableCursor();
        ChangeMovementStatus(true);
    }
}