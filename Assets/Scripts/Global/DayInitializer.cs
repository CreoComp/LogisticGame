using UnityEngine;
using UnityEngine.SceneManagement;

public class DayInitializer : MonoBehaviour
{
    private void Awake()
    {
        MoneyStorage.Instance.Init();

        InitializeTestDay();
        SceneManager.LoadScene(1);
    }

    private void InitializeTestDay()
    {
        SaveLoadService saveLoadService = new SaveLoadService();
        saveLoadService.DeleteData();

        CarInfo carInfo = new CarInfo();
        carInfo.carType = CategoryType.Drink;
        carInfo.fuelConsumption = 12;
        carInfo.loadCapacity = 1300;
        carInfo.rentCost = 80;

        CarInfo carInfo2 = new CarInfo();
        carInfo2.carType = CategoryType.Freeze;
        carInfo2.fuelConsumption = 10;
        carInfo2.loadCapacity = 1100;
        carInfo2.rentCost = 90;

        CarInfo carInfo3 = new CarInfo();
        carInfo3.carType = CategoryType.Candy;
        carInfo3.fuelConsumption = 12;
        carInfo3.loadCapacity = 700;
        carInfo3.rentCost = 60;

        StationInfo storage = Roads.Instance.GetRoadsInfo().storages[Random.Range(0, Roads.Instance.GetRoadsInfo().storages.Count)];

        OrderInfo orderInfo = new OrderInfo();
        orderInfo.categoryType = CategoryType.Drink;
        orderInfo.payment = 40;
        orderInfo.weight = 30;
        orderInfo.stationStore = Roads.Instance.GetRoadsInfo().stores[Random.Range(0, Roads.Instance.GetRoadsInfo().stores.Count)];
        orderInfo.stationStorage = storage;

        OrderInfo orderInfo2 = new OrderInfo();
        orderInfo2.categoryType = CategoryType.Drink;
        orderInfo2.payment = 50;
        orderInfo2.weight = 50;
        orderInfo2.stationStore = Roads.Instance.GetRoadsInfo().stores[Random.Range(0, Roads.Instance.GetRoadsInfo().stores.Count)];
        orderInfo2.stationStorage = storage;

        OrderInfo orderInfo3 = new OrderInfo();
        orderInfo3.categoryType = CategoryType.Freeze;
        orderInfo3.payment = 10;
        orderInfo3.weight = 10;
        orderInfo3.stationStore = Roads.Instance.GetRoadsInfo().stores[Random.Range(0, Roads.Instance.GetRoadsInfo().stores.Count)];
        orderInfo3.stationStorage = storage;

        OrderInfo orderInfo4 = new OrderInfo();
        orderInfo4.categoryType = CategoryType.Candy;
        orderInfo4.payment = 50;
        orderInfo4.weight = 100;
        orderInfo4.stationStore = Roads.Instance.GetRoadsInfo().stores[Random.Range(0, Roads.Instance.GetRoadsInfo().stores.Count)];
        orderInfo4.stationStorage = storage;



        saveLoadService.playerData.unRentedTransport.Add(carInfo);
        saveLoadService.playerData.unRentedTransport.Add(carInfo2);
        saveLoadService.playerData.unRentedTransport.Add(carInfo3);
        saveLoadService.playerData.freeOrders.Add(orderInfo);
        saveLoadService.playerData.freeOrders.Add(orderInfo2);
        saveLoadService.playerData.freeOrders.Add(orderInfo3);
        saveLoadService.playerData.freeOrders.Add(orderInfo4);

        saveLoadService.SaveData();
    }
}