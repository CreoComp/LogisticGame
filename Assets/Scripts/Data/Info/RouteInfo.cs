using System;
using System.Collections.Generic;

public class RouteInfo
{
    public List<Edge> route = new List<Edge>();
    public PointInfo lastPoint;
    public PointInfo parkingPoint;
    public CarInfo car;
    public CategoryType categoryType;
    public List<StationInfo> conductedStations = new List<StationInfo>();
    public List<OrderInfo> orders = new List<OrderInfo>();
    public DateTime startTime;


    public DateTime GetEndTime()
    {
        DateTime endTime = startTime;
        startTime = startTime.AddHours(1); // wait time

        RouteStatisticsService routeStatistics = new RouteStatisticsService(this);
        startTime = startTime.Add(routeStatistics.GetTimeRoute());
        return endTime;
    }

    public string GetCategoryName()
    {
        string name = "";
        switch (categoryType)
        {
            case CategoryType.Drink:
                name = "Напитки";
                break;

            case CategoryType.Freeze:
                name = "Замороженные изделия";
                break;

            case CategoryType.Candy:
                name = "Кондитерские изделия";
                break;
        }
        return name;
    }

    #region переопределение операторов
    // Переопределение Equals для сравнения всех полей
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        RouteInfo other = (RouteInfo)obj;
        return AreEqual(route, other.route) &&
               lastPoint == other.lastPoint &&
               car == other.car &&
               categoryType == other.categoryType &&
               AreEqual(conductedStations, other.conductedStations) &&
               AreEqual(orders, other.orders) &&
               startTime == other.startTime;
    }

    // Переопределение оператора ==
    public static bool operator ==(RouteInfo route1, RouteInfo route2)
    {
        if (ReferenceEquals(route1, route2))
            return true;
        if (ReferenceEquals(route1, null) || ReferenceEquals(route2, null))
            return false;

        return route1.Equals(route2);
    }

    // Переопределение оператора !=
    public static bool operator !=(RouteInfo route1, RouteInfo route2)
    {
        return !(route1 == route2);
    }

    // Определение хэш-кода
    public override int GetHashCode()
    {
        unchecked // переполнение не выбрасывает исключение
        {
            int hashCode = 17;
            hashCode = hashCode * 23 + GetListHashCode(route);
            hashCode = hashCode * 23 + (lastPoint != null ? lastPoint.GetHashCode() : 0);
            hashCode = hashCode * 23 + (car != null ? car.GetHashCode() : 0);
            hashCode = hashCode * 23 + (int)categoryType;
            hashCode = hashCode * 23 + GetListHashCode(conductedStations);
            hashCode = hashCode * 23 + GetListHashCode(orders);
            hashCode = hashCode * 23 + startTime.GetHashCode();
            return hashCode;
        }
    }

    // Метод для сравнения двух списков
    private bool AreEqual<T>(List<T> list1, List<T> list2)
    {
        if (ReferenceEquals(list1, list2))
            return true;
        if (list1 == null || list2 == null)
            return false;
        if (list1.Count != list2.Count)
            return false;

        for (int i = 0; i < list1.Count; i++)
        {
            if (!list1[i].Equals(list2[i]))
                return false;
        }
        return true;
    }

    // Метод для получения хэш-кода списка
    private int GetListHashCode<T>(List<T> list)
    {
        if (list == null)
            return 0;

        unchecked
        {
            int hashCode = 17;
            foreach (var item in list)
            {
                hashCode = hashCode * 23 + (item != null ? item.GetHashCode() : 0);
            }
            return hashCode;
        }
    }
    #endregion
}