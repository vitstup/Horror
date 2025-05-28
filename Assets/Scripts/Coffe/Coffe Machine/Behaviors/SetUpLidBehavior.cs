using UnityEngine;

[RequireComponent(typeof(CoffeMachine))]
public class SetUpLidBehavior : BasicInteracatiobBehavior
{
    [SerializeField] private CarryItemHandler carryHandler;

    [SerializeField] private AudioSource audioSource;

    private CoffeMachine coffeMachine;

    public override string ToDoText => "закрыть крышкой";

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
        carryHandler.DropItem(cup.LidTransform);

        audioSource.Play();

        ChangeInteractability(false);

        coffeMachine.ChangeToPartFour(cup);
    }

    private void Picked(Pickable pickable)
    {
        if (pickable.TryGetComponent<Lid>(out var lid))
        {
            ChangeInteractability(true);
        }
        else ChangeInteractability(false);
    }

    private void Droped(Pickable pickable, Transform to)
    {
        ChangeInteractability(false);
    }

    private void OnEnable()
    {
        carryHandler.OnPickableTaked += Picked;
        carryHandler.OnPickableDropStarted += Droped;
    }

    private void OnDisable()
    {
        carryHandler.OnPickableTaked -= Picked;
        carryHandler.OnPickableDropStarted -= Droped;
    }
}