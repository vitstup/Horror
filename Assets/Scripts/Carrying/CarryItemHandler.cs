using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CarryItemHandler : MonoBehaviour
{
    public event Action<Pickable> OnPickableTakeStarted;
    public event Action<Pickable, Transform> OnPickableDropStarted;

    public event Action<Pickable> OnPickableTaked;
    public event Action<Pickable, Transform> OnPickableDroped;

    [SerializeField] private Transform handBone; // кость, куда крепим

    [field: SerializeField] public float timeToPick { get; private set; }

    public Pickable carriedItem { get; private set; }

    public bool HasItem => carriedItem != null;

    public async UniTask<bool> TryPickItem(GameObject obj)
    {
        if (obj.TryGetComponent<Pickable>(out var pickable))
        {
            return await TryPickItem(pickable);
        }
        return false;
    }

    public async UniTask<bool> TryPickItem(Pickable pickable)
    {
        if (HasItem) return false;

        var itemTransform = pickable.transform;
        carriedItem = pickable;

        OnPickableTakeStarted?.Invoke(pickable);

        await UniTask.WaitForSeconds(timeToPick);

        itemTransform.SetParent(handBone);
        itemTransform.localPosition = pickable.localPositionOffset;
        itemTransform.localEulerAngles = pickable.localRotationOffset;

        OnPickableTaked?.Invoke(pickable);

        return true;
    }

    public async void DropItem(Transform to)
    {
        if (!HasItem) return;

        OnPickableDropStarted?.Invoke(carriedItem, to);

        await UniTask.WaitForSeconds(timeToPick);

        carriedItem.transform.SetParent(to);
        carriedItem.transform.localPosition = Vector3.zero;
        carriedItem.transform.localEulerAngles = Vector3.zero;

        OnPickableDroped?.Invoke(carriedItem, to);

        carriedItem = null;
    }
}