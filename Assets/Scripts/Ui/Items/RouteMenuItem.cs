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
        DistanceText.text = $"Расстояние: {statisticsService.GetDistance()}км";
        LoadCapacityText.text = $"Загруженность: {(int)(statisticsService.GetLoadCapacity()/routeInfo.car.loadCapacity)}%";
        EndTimeText.text = $"Время окончания: 16:32";
    }
}
