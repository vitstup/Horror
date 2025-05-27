using UnityEngine;

[RequireComponent(typeof(TakeCupBehavior))]
public class CupsHandler : SomeItemsHandler<Cup>, IBehaviorHandler
{
    private BehaviorHandler handler = new BehaviorHandler();

    public BehaviorHandler Handler => handler;

    protected override void Awake()
    {
        base.Awake();

        handler.RegisterBehavior(GetComponent<TakeCupBehavior>());
    }
}