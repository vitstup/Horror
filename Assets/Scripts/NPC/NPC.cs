using UnityEngine;

[RequireComponent(typeof(MovementAI))]
[RequireComponent(typeof(CustomerTalkInteractionBehavior))]
[RequireComponent(typeof(CustomerTakeCofeInteractionBehavior))]
public class NPC : MonoBehaviour, IBehaviorHandler
{
    private BehaviorHandler handler = new BehaviorHandler();

    public BehaviorHandler Handler => handler;

    public CustomerTalkInteractionBehavior talkBeh { get; private set; }
    public CustomerTakeCofeInteractionBehavior takeCoffeBeh { get; private set; }

    [field: SerializeField] public Transform head { get; private set; }

    public bool ordered { get; private set; }

    private void Awake()
    {
        talkBeh = GetComponent<CustomerTalkInteractionBehavior>();
        takeCoffeBeh = GetComponent<CustomerTakeCofeInteractionBehavior>();

        handler.RegisterBehavior(talkBeh);

        handler.RegisterBehavior(GetComponent<IMovableBehavior>());
    }

    private void Update()
    {
        if (takeCoffeBeh.IsInteractable && ordered)
            handler.RegisterBehavior(takeCoffeBeh);
    }

    public void Order() => ordered = true;
}