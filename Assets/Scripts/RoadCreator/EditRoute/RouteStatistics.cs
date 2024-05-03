using System.Collections.Generic;
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
        _distanceText.text = $"<color=red>{statisticsService.GetDistance()}км</color>";
        _fuelCostText.text = $"<color=red>{statisticsService.GetFuelCost()}л</color>";
        _routeTimeText.text = $"<color=red>{statisticsService.GetTimeRoute()}ч</color>";
        _rentCostText.text = $"<color=red>{statisticsService.GetLoadCapacity()}кг/{routeInfo.car.loadCapacity}кг</color>";
        _paymentText.text = $"<color=#56D842>{statisticsService.GetPayment()}руб.</color>";
    }
}
