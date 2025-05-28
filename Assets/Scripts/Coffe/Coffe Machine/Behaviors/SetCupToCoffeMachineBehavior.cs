using UnityEngine;

[RequireComponent(typeof(CoffeMachine))]
public class SetCupToCoffeMachineBehavior : BasicInteracatiobBehavior
{
    private CoffeMachine coffeMachine;

    [SerializeField] private CarryItemHandler carryHandler;

    [SerializeField] private Transform CapPos;

    private Cup cup;

    public override string ToDoText => "поставить стаканчик";

    protected override void OnAwake()
    {
        coffeMachine = GetComponent<CoffeMachine>();

        ChangeInteractability(false);
    }

    public override void Interact()
    {
        carryHandler.DropItem(CapPos);

        ChangeInteractability(false);

        coffeMachine.ChangeToPartTwo(cup);
    }

    private void Picked(Pickable pickable)
    {
        if (pickable.TryGetComponent<Cup>(out var cup) && !cup.filledWithCoffe)
        {
            this.cup = cup;
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