using UnityEngine;

public class NPC : MonoBehaviour, IBehaviorHandler
{
    protected BehaviorHandler handler = new BehaviorHandler();

    public BehaviorHandler Handler => handler;

    protected virtual void Awake()
    {
        RegisterBehs();
    }

    protected virtual void RegisterBehs()
    {
        var movableBeh = GetComponent<IMovableBehavior>();
        if (movableBeh != null)
            handler.RegisterBehavior(movableBeh);

        var interactionBeh = GetComponent<IInteractableBehavior>();
        if (interactionBeh != null)
            handler.RegisterBehavior(interactionBeh);
    }
}