using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharaController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private InputHandler inputHandler;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 move = transform.right * inputHandler.MoveInput.x + transform.forward * inputHandler.MoveInput.y;
        Vector3 velocity = move * moveSpeed;
        Vector3 currentVelocity = rb.linearVelocity;

        rb.linearVelocity = new Vector3(velocity.x, currentVelocity.y, velocity.z);
    }
}