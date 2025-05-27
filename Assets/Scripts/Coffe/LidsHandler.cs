using UnityEngine;

[RequireComponent(typeof(TakeLidBehavior))]
public class LidsHandler : SomeItemsHandler<Lid>, IBehaviorHandler
{
    private BehaviorHandler handler = new BehaviorHandler();

    public BehaviorHandler Handler => handler;

    protected override void Awake()
    {
        base.Awake();

        handler.RegisterBehavior(GetComponent<TakeLidBehavior>());
    }
}