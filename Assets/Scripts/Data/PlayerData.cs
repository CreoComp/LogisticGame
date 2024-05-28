using System;
using System.Collections.Generic;

public class PlayerData
{
    public List<CarInfo> rentedTransport = new List<CarInfo>();
    public List<CarInfo> unRentedTransport = new List<CarInfo>();
    public List<CarInfo> usedTransport = new List<CarInfo>(); 

    public List<RouteInfo> routes = new List<RouteInfo>();

    public List<OrderInfo> freeOrders = new List<OrderInfo>();
    public List<OrderInfo> takenOrders = new List<OrderInfo>();

    public DateTime NowDate = new DateTime(2024, 1, 1);
    public float MoneyAmount = 100;

    public int Experience;
    public int Level = 1;

    public void RentCar(CarInfo car)
    {
        rentedTransport.Add(car);
    }
}
