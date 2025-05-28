using TMPro;
using UnityEngine;

[RequireComponent(typeof(CashHandler))]
public class CashHudHandler : MonoBehaviour
{
    private CashHandler handler;

    [SerializeField] private TextMeshProUGUI cashText;

    private void Awake()
    {
        handler = GetComponent<CashHandler>();

        ChangeText(handler.cash);

        handler.cashAmountChanged += CashChanged;
    }

    private void CashChanged(int newCash, int changeAmount)
    {
        ChangeText(newCash);
    }

    private void OnDestroy()
    {
        handler.cashAmountChanged -= CashChanged;
    }

    private void ChangeText(int cash)
    {
        cashText.text = $"{cash}$";
    }
}