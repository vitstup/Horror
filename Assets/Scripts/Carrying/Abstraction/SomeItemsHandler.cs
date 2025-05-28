using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class SomeItemsHandler<T> : MonoBehaviour where T : MonoBehaviour
{
    [HideInInspector] public List<T> items;

    [field: SerializeField] public Transform PlaceNewObjectsTo { get; private set; }

    protected virtual void Awake()
    {
        items = GetComponentsInChildren<T>(false).ToList();
    }

    public T GetAvailableItem()
    {
        var item = items.OrderByDescending(c => c.transform.localPosition.y).FirstOrDefault();
        PlaceNewObjectsTo.position = item is not null ? item.transform.position : Vector3.zero;

        return item;
    }
}