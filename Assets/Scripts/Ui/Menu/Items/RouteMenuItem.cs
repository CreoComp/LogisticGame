using System;
using TMPro;
using UnityEngine;

public class RouteMenuItem: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI CategoryNameText;
    [SerializeField] private TextMeshProUGUI DistanceText;
    [SerializeField] private TextMeshProUGUI LoadCapacityText;
    [SerializeField] private TextMeshProUGUI EndTimeText;

    public void Construct(RouteInfo routeInfo)
    {
        RouteStatisticsService statisticsService = new RouteStatisticsService(routeInfo);
        CategoryNameText.text = routeInfo.GetCategoryName();
        DistanceText.text = $"Расстояние: {Math.Round(statisticsService.GetDistance(),2)}км";
        LoadCapacityText.text = $"Загруженность: {(Math.Round(statisticsService.GetLoadCapacity()/routeInfo.car.loadCapacity,2) * 100f)}%";
        EndTimeText.text = $"Время начала: {routeInfo.startTime.ToShortTimeString()}";
    }
}
