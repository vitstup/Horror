using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavmeshStepsSoundController : MonoBehaviour
{
    private NavMeshAgent agent;

    [SerializeField] private AudioSource source;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Vector3 velocity = agent.velocity;
        velocity.y = 0f; // игнорируем вертикаль

        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.magnitude;

        if (speed > 0.25f && !source.isPlaying)
            source.Play();
    }
}