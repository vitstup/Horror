using Cysharp.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(CoffeMachine))]

public class GetCoffeBehavior : BasicInteracatiobBehavior
{
    [SerializeField] private CarryItemHandler carryingHandler;

    [SerializeField] private AudioSource audioSource;

    private CoffeMachine coffeMachine;

    public override string ToDoText => "взять кофе";

    private Cup cup;

    public void SetInitData(Cup cup)
    {
        this.cup = cup;
    }

    protected override void OnAwake()
    {
        coffeMachine = GetComponent<CoffeMachine>();

        ChangeInteractability(true);
    }

    public override void Interact()
    {
        cup.Fill();

        carryingHandler.TryPickItem(cup.gameObject).Forget();

        audioSource.Play();

        coffeMachine.ChangeToPartOne();
    }
}