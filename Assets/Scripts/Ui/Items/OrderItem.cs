using TMPro;
using UnityEngine;

public class OrderItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI CategoryNameText;
    [SerializeField] private TextMeshProUGUI WeightText;
    [SerializeField] private TextMeshProUGUI PaymentText;

    public void Construct(OrderInfo orderInfo)
    {
        CategoryNameText.text = orderInfo.GetCategoryName();
        WeightText.text = $"Масса: <color=red>{orderInfo.weight}</color> кг.";
        PaymentText.text = $"Оплата: <color=red>{orderInfo.payment}</color> руб.";
    }
}