using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MovementAnimContAI : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private float walkAnimSpeed = 1f; // параметр в Blend Tree для ходьбы
    [SerializeField] private float runAnimSpeed = 3f;  // параметр в Blend Tree для бега
    [SerializeField] private float walkThreshold = 1.1f; // граница между ходьбой и бегом

    private NavMeshAgent agent;

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

        float factor;
        if (speed < 0.1f)
        {
            factor = 0f; // стоим
        }
        else if (speed < walkThreshold)
        {
            factor = walkAnimSpeed / speed; // нормализуем под walkSpeed
        }
        else
        {
            factor = runAnimSpeed / speed;  // нормализуем под runSpeed
        }

        // Подставляем нормализованные значения в аниматор
        float forward = localVelocity.z * factor;
        float side = localVelocity.x * factor;

        animator.SetFloat("Forward", forward);
        animator.SetFloat("Side", side);
    }
}
