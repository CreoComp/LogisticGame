using UnityEngine;

public class RouteEditor : MonoBehaviour
{
    [SerializeField] private DrawRoute drawRoute;
    [SerializeField] private GameObject panelEditRoute;

    private RouteInfo _routeInfo;

    public void EditRoute(int indexRoute)
    {
        _routeInfo = SaveLoadService.Instance.playerData.routes[indexRoute];
        ConstructRoute();
    }

    public void CreateRoute()
    {
        _routeInfo = new RouteInfo();
    }

    public void AddStation(StationInfo stationInfo)
    {
        if (!_routeInfo.conductedStations.Contains(stationInfo))
            _routeInfo.conductedStations.Add(stationInfo);
    }
    public void RemoveStation(StationInfo stationInfo)
    {
        if (_routeInfo.conductedStations.Contains(stationInfo))
            _routeInfo.conductedStations.Remove(stationInfo);
    }

    public void ChooseTransport(CarInfo car)
    {
        if(SaveLoadService.Instance.playerData.rentedTransport.Contains(car))
        _routeInfo.car = car;
    }

    public void ConstructRoute()
    {
        drawRoute.Construct(this, _routeInfo);
        panelEditRoute.SetActive(true);
    }

    public void ExitRouteEditor()
    {
        panelEditRoute.SetActive(false);
        drawRoute.Exit();
    }
}