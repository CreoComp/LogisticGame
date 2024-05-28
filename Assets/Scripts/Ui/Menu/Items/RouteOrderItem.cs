using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RouteOrderItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI CategoryNameText;
    [SerializeField] private TextMeshProUGUI WeightText;
    [SerializeField] private TextMeshProUGUI PaymentText;

    [SerializeField] private Color ActiveColor;
    [SerializeField] private Color InactiveColor;

    private Image image;

    public void Construct(OrderInfo orderInfo)
    {
        image = GetComponent<Image>();
        SetInactiveColor();

        CategoryNameText.text = orderInfo.GetCategoryName();
        WeightText.text = $"Масса: <color=red>{orderInfo.weight}</color> кг.";
        PaymentText.text = $"Оплата: <color=red>{orderInfo.payment}</color> руб.";
    }

    public void SetActiveColor()
    {
        image.color = ActiveColor;
    }

    public void SetInactiveColor()
    {
        image.color = InactiveColor;
    }
}
