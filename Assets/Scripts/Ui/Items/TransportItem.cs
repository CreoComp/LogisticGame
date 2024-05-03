using TMPro;
using UnityEngine;

public class TransportItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI CategoryNameText;
    [SerializeField] private TextMeshProUGUI LoadCapacityText;
    [SerializeField] private TextMeshProUGUI RentCostText;

    public void Construct(CarInfo car)
    {
        CategoryNameText.text = car.GetCategoryName();
        LoadCapacityText.text = $"Грузоподъемность: <color=red>{car.loadCapacity}</color> кг.";
        RentCostText.text = $"Стоимость аренды в сутки: <color=red>{car.rentCost}</color> руб.";
    }
}