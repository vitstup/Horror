using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CustomerTakeCofeInteractionBehavior : BasicInteracatiobBehavior
{
    [SerializeField] private Transform player;

    [SerializeField] private CarryItemHandler carryingHandler;

    [SerializeField] private CashHandler cashHandler;

    [SerializeField] private Transform takeCoffeTransform;

    [SerializeField] private Rig rig;

    private bool givingNow;

    public override string ToDoText => "отдать кофе";
    
    private bool playerIsInFrontOf;

    public override bool IsInteractable => isInteractable && playerIsInFrontOf;

    public bool CoffeWasTaken { get; private set; }

    protected override void OnAwake()
    {
        isInteractable = false;
    }

    private void Update()
    {
        isInteractable = carryingHandler.HasItem && carryingHandler.carriedItem.TryGetComponent(out Cup cup)
            && cup.filledWithCoffe && !givingNow; // не самый производительный вариант, но из за экономии времени оставлю так

        Vector3 toPlayer = (player.position - transform.position).normalized;

        toPlayer.y = 0f;

        float angle = Vector3.Angle(transform.forward, toPlayer);

        playerIsInFrontOf = Mathf.Abs(angle) < 45f;
    }

    public override async void Interact()
    {
        isInteractable = false;

        givingNow = true;

        carryingHandler.DropItem(takeCoffeTransform);

        float duration = carryingHandler.timeToPick;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            rig.weight = Mathf.Lerp(0, 1f, t);
            await UniTask.Yield();
        }

        rig.weight = 1f;

        givingNow = false;

        cashHandler.AddCash(2, takeCoffeTransform.position);

        CoffeWasTaken = true;
    }

}