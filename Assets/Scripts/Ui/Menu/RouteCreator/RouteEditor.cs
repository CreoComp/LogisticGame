using System.Collections.Generic;
using UnityEngine;

public class RouteEditor : MonoBehaviour
{
    [SerializeField] private DrawRoute drawRoute;
    [SerializeField] private RouteInitializer routeInitializer;
    [SerializeField] private GameObject panelEditRoute;
    [SerializeField] private GameObject panelMenuRoute;

    [SerializeField] private GameObject panelChooseOrders;
    [SerializeField] private GameObject panelChooseTransport;

    [SerializeField] private MenuPanel menuPanel;
    [SerializeField] private Day _day;
    [SerializeField] private DrawStations _drawStations;

    private RouteInfo _routeInfo;


    public void CreateRoute()
    {
        _routeInfo = new RouteInfo();
        panelChooseOrders.SetActive(true);
        panelChooseOrders.GetComponent<ChooseOrderService>().Init(this);
    }

    public void BackOrders()
    {
        panelChooseOrders.SetActive(false);
    }

    public void BackTransport()
    {
        panelChooseTransport.SetActive(false);
        panelChooseOrders.SetActive(true);

        panelChooseOrders.GetComponent<ChooseOrderService>().Init(this);
    }

    public void ChooseOrders(List<OrderInfo> orders, CategoryType category)
    {
        _routeInfo.orders = orders;
        _routeInfo.categoryType = category;

        panelChooseOrders.SetActive(false);
        panelChooseTransport.SetActive(true);
        panelChooseTransport.GetComponent<ChooseTransportService>().Init(this, category);
    }

    public void ChooseTransport(CarInfo car)
    {
        SaveLoadService saveLoadService = new SaveLoadService();

        if(saveLoadService.playerData.rentedTransport.Contains(car))
        _routeInfo.car = car;

        panelChooseTransport.SetActive(false);
        ConstructRoute();
    }

    public void ConstructRoute()
    {
        _drawStations.RemoveStations();
        panelEditRoute.SetActive(true);

        panelMenuRoute.SetActive(false);
        panelChooseOrders.SetActive(false);
        panelChooseTransport.SetActive(false);

        drawRoute.Construct(this, _routeInfo);
    }

    public void ConfirmRoute(RouteInfo routeInfo)
    {
        _drawStations.Draw();
        _routeInfo = routeInfo;

        SaveLoadService saveLoadService = new SaveLoadService();
        saveLoadService.playerData.routes.Add(routeInfo);

        saveLoadService.playerData.usedTransport.Add(routeInfo.car);

        foreach(var order in _routeInfo.orders)
        {
            if(saveLoadService.playerData.freeOrders.Contains(order))
            {
                saveLoadService.playerData.freeOrders.Remove(order);
                saveLoadService.playerData.takenOrders.Add(order);
            }
        }

        saveLoadService.SaveData();

        panelEditRoute.SetActive(false);
        panelMenuRoute.SetActive(true);
        drawRoute.Exit();

        menuPanel.Construct();
        routeInitializer.InitializeRoute(_routeInfo);
        _day.AddRoute(_routeInfo);
    }

    public void ExitRouteEditor()
    {
        panelEditRoute.SetActive(false);
        panelMenuRoute.SetActive(true);
        drawRoute.Exit();
    }
}
