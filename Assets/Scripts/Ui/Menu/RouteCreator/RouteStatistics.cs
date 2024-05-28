using System;
using TMPro;
using UnityEngine;

public class RouteStatistics: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _pointsCountText;
    [SerializeField] private TextMeshProUGUI _distanceText;
    [SerializeField] private TextMeshProUGUI _fuelCostText;
    [SerializeField] private TextMeshProUGUI _routeTimeText;
    [SerializeField] private TextMeshProUGUI _rentCostText;
    [SerializeField] private TextMeshProUGUI _paymentText;

    public void UpdateStatistics(RouteInfo routeInfo)
    {
        RouteStatisticsService statisticsService = new RouteStatisticsService(routeInfo);

        _pointsCountText.text = $"<color=red>{statisticsService.GetPointsCount()}</color>";
        _distanceText.text = $"<color=red>{Math.Round(statisticsService.GetDistance(),2)}км</color>";
        _fuelCostText.text = $"<color=red>{Math.Round(statisticsService.GetFuelCost(),2)}р</color>";
        _routeTimeText.text = $"<color=red>{statisticsService.GetTimeRoute().Hours}:{statisticsService.GetTimeRoute().Minutes}</color>";
        _rentCostText.text = $"<color=red>{statisticsService.GetLoadCapacity()}кг/{routeInfo.car.loadCapacity}кг</color>";
        _paymentText.text = $"{statisticsService.GetPayment()}руб.";
    }
}
