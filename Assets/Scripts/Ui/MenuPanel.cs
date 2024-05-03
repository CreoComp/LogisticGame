using UnityEngine;

public class MenuPanel : MonoBehaviour
{
    [SerializeField] private GameObject _routesPanel;
    [SerializeField] private GameObject _transportPanel;
    [SerializeField] private GameObject ordersPanel;


    [SerializeField] private Transform _routesContentParent;
    [SerializeField] private Transform _transportContentParent;
    [SerializeField] private Transform _orderContentParent;
    
    
    private GameObject _routesPrefab;
    private GameObject _transportPrefab;
    private GameObject _orderPrefab;

    private void Start()
    {
        _routesPrefab = Resources.Load<GameObject>("MenuItems/CreateRoute/RouteItem");
        _transportPrefab = Resources.Load<GameObject>("MenuItems/Transport/TransportItem");
        _orderPrefab = Resources.Load<GameObject>("MenuItems/Orders/OrderItem");

        ConstructRoutes();
        ConstructTransport();
        ConstructOrders();
    }

    public void OpenRoutes()
    {
        _routesPanel.SetActive(true);
        _transportPanel.SetActive(false);
        ordersPanel.SetActive(false);
    }
    public void OpenTransports()
    {
        _routesPanel.SetActive(false);
        _transportPanel.SetActive(true); 
        ordersPanel.SetActive(false);
    }
    public void OpenOrders()
    {
        _routesPanel.SetActive(false);
        _transportPanel.SetActive(false);
        ordersPanel.SetActive(true);
    }

    public void ConstructRoutes()
    {
        DestroyChildrens(_orderContentParent);

        foreach(RouteInfo routeInfo in SaveLoadService.Instance.playerData.routes)
        {
            var item = Instantiate(_routesPrefab, _orderContentParent).GetComponent<RouteMenuItem>();
            item.Construct(routeInfo);
        }
    }

    public void ConstructTransport()
    {
        DestroyChildrens(_transportContentParent);

        foreach (CarInfo carInfo in SaveLoadService.Instance.playerData.rentedTransport)
        {
            var item = Instantiate(_routesPrefab, _transportContentParent).GetComponent<TransportItem>();
            item.Construct(carInfo);
        }
        foreach (CarInfo carInfo in SaveLoadService.Instance.playerData.unRentedTransport)
        {
            var item = Instantiate(_routesPrefab, _orderContentParent).GetComponent<TransportItem>();
            item.Construct(carInfo);
        }
    }

    public void ConstructOrders()
    {
        DestroyChildrens(_orderContentParent);

        foreach (OrderInfo orderInfo in SaveLoadService.Instance.playerData.freeOrders)
        {
            var item = Instantiate(_routesPrefab, _orderContentParent).GetComponent<OrderItem>();
            item.Construct(orderInfo);
        }
    }

    private void DestroyChildrens(Transform parent)
    {
        if (parent.childCount == 0)
            return;

        foreach (var children in parent.GetComponentsInChildren<GameObject>())
        {
            Destroy(children.gameObject);
        }
    }
}
