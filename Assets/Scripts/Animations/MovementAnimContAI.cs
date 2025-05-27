using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MovementAnimContAI : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private float walkAnimSpeed = 1f; // �������� � Blend Tree ��� ������
    [SerializeField] private float runAnimSpeed = 3f;  // �������� � Blend Tree ��� ����
    [SerializeField] private float walkThreshold = 1.1f; // ������� ����� ������� � �����

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Vector3 velocity = agent.velocity;
        velocity.y = 0f; // ���������� ���������

        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.magnitude;

        float factor;
        if (speed < 0.1f)
        {
            factor = 0f; // �����
        }
        else if (speed < walkThreshold)
        {
            factor = walkAnimSpeed / speed; // ����������� ��� walkSpeed
        }
        else
        {
            factor = runAnimSpeed / speed;  // ����������� ��� runSpeed
        }

        // ����������� ��������������� �������� � ��������
        float forward = localVelocity.z * factor;
        float side = localVelocity.x * factor;

        animator.SetFloat("Forward", forward);
        animator.SetFloat("Side", side);
    }
}
