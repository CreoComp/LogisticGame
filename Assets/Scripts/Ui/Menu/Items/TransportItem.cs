using TMPro;
using UnityEngine;

public class TransportItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI CategoryNameText;
    [SerializeField] private TextMeshProUGUI LoadCapacityText;
    [SerializeField] private TextMeshProUGUI RentCostText;

    [SerializeField] private GameObject ButtonRent;
    [SerializeField] private GameObject ButtonUnRent;

    private CarInfo carInfo;

    public void Construct(CarInfo car)
    {
        carInfo = car;
        CategoryNameText.text = car.GetCategoryName();
        LoadCapacityText.text = $"Грузоподъемность: <color=red>{car.loadCapacity}</color> кг.";
        RentCostText.text = $"Стоимость аренды в сутки: <color=red>{car.rentCost}</color> руб.";
    }

    public void Rent()
    {
        SaveLoadService saveLoadService = new SaveLoadService();

        if (saveLoadService.playerData.unRentedTransport.Contains(carInfo))
        {
            saveLoadService.playerData.rentedTransport.Add(carInfo);
            saveLoadService.playerData.unRentedTransport.Remove(carInfo);

            ButtonRent.SetActive(false);
            ButtonUnRent.SetActive(true);
        }
        saveLoadService.SaveData();
    }

    public void UnRent()
    {
        SaveLoadService saveLoadService = new SaveLoadService();

        if (saveLoadService.playerData.rentedTransport.Contains(carInfo))
        {
            saveLoadService.playerData.unRentedTransport.Add(carInfo);
            saveLoadService.playerData.rentedTransport.Remove(carInfo);

            ButtonRent.SetActive(true);
            ButtonUnRent.SetActive(false);
        }
        saveLoadService.SaveData();
    }
}