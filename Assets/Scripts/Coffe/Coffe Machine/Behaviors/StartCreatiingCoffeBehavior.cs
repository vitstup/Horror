using Cysharp.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(CoffeMachine))]
public class StartCreatiingCoffeBehavior : BasicInteracatiobBehavior
{
    private CoffeMachine coffeMachine;

    [SerializeField] private Animator coffeMachineAnim;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private string machineAnim;
    [SerializeField] private string cupAnim;

    [SerializeField] private float timeToWork;

    private Cup cup;

    public override string ToDoText => "сварить кофе";

    public void SetInitData(Cup cup)
    {
        this.cup = cup;
    }

    protected override void OnAwake()
    {
        coffeMachine = GetComponent<CoffeMachine>();

        ChangeInteractability(true);
    }

    public async override void Interact()
    {
        ChangeInteractability(false);

        coffeMachineAnim.enabled = true;
        cup.animator.enabled = true;

        coffeMachineAnim.Play(machineAnim, layer: 0, normalizedTime: 0f);
        cup.animator.Play(cupAnim, layer: 0, normalizedTime: 0f);

        audioSource.Play();

        await UniTask.WaitForSeconds(timeToWork);

        coffeMachineAnim.enabled = false;
        cup.animator.enabled = false;

        coffeMachine.ChangeToPartThree(cup);

        ChangeInteractability(true);
    }
}