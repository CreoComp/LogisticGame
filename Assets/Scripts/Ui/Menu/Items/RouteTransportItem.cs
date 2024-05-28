using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RouteTransportItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI CategoryNameText;
    [SerializeField] private TextMeshProUGUI LoadCapacityText;
    [SerializeField] private TextMeshProUGUI RentCostText;

    [SerializeField] private Color ActiveColor;
    [SerializeField] private Color InactiveColor;

    private Image image;

    public void Construct(CarInfo car)
    {
        image = GetComponent<Image>();
        SetInactiveColor();

        CategoryNameText.text = car.GetCategoryName();
        LoadCapacityText.text = $"Грузоподъемность: <color=red>{car.loadCapacity}</color> кг.";
        RentCostText.text = $"Стоимость аренды в сутки: <color=red>{car.rentCost}</color> руб.";
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
