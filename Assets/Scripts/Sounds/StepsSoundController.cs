using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class StepsSoundController : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private AudioSource source;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        var velocity = rb.linearVelocity.magnitude;

        if (velocity > 0.25f && !source.isPlaying)
            source.Play();
    }
}