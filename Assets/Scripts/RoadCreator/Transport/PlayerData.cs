using System.Collections.Generic;

public class PlayerData
{
    public List<CarInfo> rentedTransport = new List<CarInfo>();
    public List<CarInfo> unRentedTransport = new List<CarInfo>();

    public List<RouteInfo> routes = new List<RouteInfo>();

    public List<OrderInfo> freeOrders = new List<OrderInfo>();
    public List<OrderInfo> takenOrders = new List<OrderInfo>();

    public void RentCar(CarInfo car)
    {
        rentedTransport.Add(car);
    }
}
