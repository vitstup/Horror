using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Screamer : MonoBehaviour
{
    [field: SerializeField] public Transform lookTo { get; private set; }

    private Animator animator;

    [SerializeField] private string stairingAnim;
    [SerializeField] private string screamingAnim;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Stare()
    {
        animator.Play(stairingAnim, layer: 0, normalizedTime: 0f);
    }

    public void Jumpscare()
    {
        animator.Play(screamingAnim, layer: 0, normalizedTime: 0f);
    }
}