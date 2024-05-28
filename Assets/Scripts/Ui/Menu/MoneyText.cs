using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class MoneyText : MonoBehaviour
{
    private TextMeshProUGUI text;
    private void OnEnable()
    {
        text = GetComponent<TextMeshProUGUI>();
        SetMoney(MoneyStorage.Instance.MoneyAmount);
        MoneyStorage.Instance.OnMoneyChanged += SetMoney;
    }

    private void OnDisable()
    {
        MoneyStorage.Instance.OnMoneyChanged -= SetMoney;
    }

    private void SetMoney(float money)
    {
        text.text = Math.Round(money,2) + "р";
    }
}
