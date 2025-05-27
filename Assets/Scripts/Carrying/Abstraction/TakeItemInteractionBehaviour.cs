using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class TakeItemInteractionBehaviour<T> : BasicInteracatiobBehavior where T : MonoBehaviour
{
    private SomeItemsHandler<T> handler;

    [SerializeField] private CarryItemHandler carryHandler;

    [SerializeField] private AudioSource audioSource;

    public override string ToDoText => (handler.items.Count > 0 && !IsCarryingAlready) ? GetText : SetText;

    private bool IsCarryingAlready;

    private bool CarryingSameType;

    protected abstract string GetText { get; }
    protected abstract string SetText { get; }

    protected override void OnAwake()
    {
        handler = GetComponent<SomeItemsHandler<T>>();
    }

    private void Update()
    {
        isInteractable = (handler.items.Count > 0 && !IsCarryingAlready) ||
            (IsCarryingAlready && CarryingSameType); 
    }

    public override void Interact()
    {
        if (!isInteractable)
            return;

        if (carryHandler.HasItem)
        {
            var carried = carryHandler.carriedItem.GetComponent<T>();

            if (carried == null) return;

            carryHandler.DropItem(handler.PlaceNewObjectsTo);

            handler.items.Add(carried);
        }
        else
        {
            var item = handler.GetAvailableItem();
            if (item == null) return;

            carryHandler.TryPickItem(item.gameObject).Forget();

            handler.items.Remove(item);
        }

        audioSource?.Play();

        RefreshOutline().Forget();
    }

    private async UniTask RefreshOutline()
    {
        await UniTask.Delay(1000);

        outline.Refresh();
    }

    private void Picked(Pickable pickable)
    {
        IsCarryingAlready = true;

        CarryingSameType = pickable.TryGetComponent(out T c);
    }

    private void Droped(Pickable pickable, Transform transform)
    {
        IsCarryingAlready = false;

        CarryingSameType = false;
    }

    private void OnEnable()
    {
        carryHandler.OnPickableTakeStarted += Picked;
        carryHandler.OnPickableDropStarted += Droped;
    }

    private void OnDisable()
    {
        carryHandler.OnPickableTakeStarted -= Picked;
        carryHandler.OnPickableDropStarted -= Droped;
    }
}