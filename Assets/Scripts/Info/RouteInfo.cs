using System;
using System.Collections.Generic;
public class RouteInfo
{
    public List<Edge> route = new List<Edge>();
    public PointInfo lastPoint;

    public CarInfo car;
    public CategoryType categoryType;
    public List<StationInfo> conductedStations;
    public List<OrderInfo> orders;
    public DateTime startTime;


    public DateTime GetEndTime()
    {
        DateTime endTime = startTime;
        startTime.AddHours(1); // wait time

        RouteStatisticsService routeStatistics = new RouteStatisticsService(this);
        startTime.Add(routeStatistics.GetTimeRoute());
        return endTime;
    }

    public string GetCategoryName()
    {
        string name = "";
        switch(categoryType)
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
}
