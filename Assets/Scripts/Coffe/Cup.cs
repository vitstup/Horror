using UnityEngine;

[RequireComponent(typeof(Pickable), typeof(Animator))]
public class Cup : MonoBehaviour
{
    public Animator animator { get; private set; }

    public bool filledWithCoffe { get; private set; } = false;

    [field: SerializeField] public Transform LidTransform { get; private set; }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Fill()
    {
        filledWithCoffe = true;
    }
}