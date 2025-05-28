using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MovementAI : MonoBehaviour, IMovableBehavior
{
    private NavMeshAgent agent;

    public bool IsTargetReached => agent.remainingDistance < 0.5f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveTo(Vector3 to)
    {
        agent.destination = to;
    }
}