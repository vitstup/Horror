using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public float ScrollInput { get; private set; }

    public Vector2 MoveInput => moveInput;
    public Vector2 LookInput => lookInput;

    private Vector2 moveInput;
    private Vector2 lookInput;

    [field: SerializeField] public KeyCode interactionKey { get; private set; }

    private void Update()
    {
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        lookInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        ScrollInput = Input.GetAxis("Mouse ScrollWheel");
    }
}