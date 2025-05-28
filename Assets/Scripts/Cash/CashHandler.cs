using System;
using UnityEngine;

public class CashHandler : MonoBehaviour
{
    public event Action<int, int> cashAmountChanged;

    [field: SerializeField] public int cash { get; private set; }

    [SerializeField] private FloatingText floatingPrefab;

    public void AddCash(int cash, Vector3 from)
    {
        AddCash(cash);

        var go = Instantiate(floatingPrefab, from, Quaternion.identity);
        go.GetComponent<FloatingText>().Init($"{cash}$");
    }

    public void AddCash(int cash)
    {
        this.cash += cash;

        cashAmountChanged?.Invoke(this.cash, cash);
    }
}