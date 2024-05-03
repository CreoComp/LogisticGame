using System;
using System.Collections.Generic;
using UnityEngine;

public class RouteStatisticsService
{
    private RouteInfo routeInfo;

    public RouteStatisticsService(RouteInfo _routeInfo)
    {
        routeInfo = _routeInfo;
    }

    public float GetDistance()
    {
        float distance = 0;
        foreach (var edge in routeInfo.route)
        {
            distance += Vector2.Distance(edge.FirstPoint.GetPosition(), edge.SecondPoint.GetPosition());
        }
        return distance;
    }
    public int GetPointsCount()
    {
        if (routeInfo.route.Count == 0)
            return 0;
        if (routeInfo.route.Count == 1)
            return 2;

        return routeInfo.route.Count + 1;
    }

    public float GetFuelCost() =>
        ((routeInfo.car.fuelConsumption / 100f) * GetDistance()) * 2.57f;

    public TimeSpan GetTimeRoute()
    {
        TimeSpan time = TimeSpan.FromHours(GetDistance() / 35f);

        foreach(var station in routeInfo.conductedStations)
        {
            time += TimeSpan.FromMinutes(20);
        }
        return time;
    }

    public float GetLoadCapacity()
    {
        if (routeInfo.orders == null)
            Debug.Log("addedOrders = null");

        float capacity = 0;
        foreach (var order in routeInfo.orders)
        {
            capacity += order.weight;
        }

        return capacity;
    }
    public float GetPayment()
    {
        if (routeInfo.orders == null)
            Debug.Log("addedOrders = null");

        float payment = 0;
        foreach (var order in routeInfo.orders)
        {
            payment += order.payment;
        }
        return payment;
    }
}
