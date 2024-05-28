using UnityEditor;
using UnityEngine;

public class MenuPanel : MonoBehaviour
{
    [SerializeField] private GameObject _routesPanel;
    [SerializeField] private GameObject _transportPanel;
    [SerializeField] private GameObject _ordersPanel;


    [SerializeField] private Transform _routesContentParent;
    [SerializeField] private Transform _transportContentParent;
    [SerializeField] private Transform _orderContentParent;
    
    
    private GameObject _routesPrefab;
    private GameObject _transportPrefab;
    private GameObject _orderPrefab;
    SaveLoadService saveLoadService;

    private void Start()
    {
        _routesPrefab = Resources.Load<GameObject>("MenuItems/CreateRoute/RouteItem");
        _transportPrefab = Resources.Load<GameObject>("MenuItems/Transport/TransportItem");
        _orderPrefab = Resources.Load<GameObject>("MenuItems/Orders/OrderItem");

        Construct();
    }

    public void Construct()
    {
        saveLoadService = new SaveLoadService();

        ConstructRoutes();
        ConstructTransport();
        ConstructOrders();
    }

    public void OpenRoutes()
    {
        _routesPanel.SetActive(true);
        _transportPanel.SetActive(false);
        _ordersPanel.SetActive(false);
    }
    public void OpenTransports()
    {
        _routesPanel.SetActive(false);
        _transportPanel.SetActive(true); 
        _ordersPanel.SetActive(false);
    }
    public void OpenOrders()
    {
        _routesPanel.SetActive(false);
        _transportPanel.SetActive(false);
        _ordersPanel.SetActive(true);
    }

    public void ConstructRoutes()
    {
        DestroyChildrens(_orderContentParent);

        foreach(RouteInfo routeInfo in saveLoadService.playerData.routes)
        {
            var item = Instantiate(_routesPrefab, _routesContentParent).GetComponent<RouteMenuItem>();
            item.Construct(routeInfo);
        }
    }

    public void ConstructTransport()
    {
        DestroyChildrens(_transportContentParent);

        foreach (CarInfo carInfo in saveLoadService.playerData.rentedTransport)
        {
            var item = Instantiate(_transportPrefab, _transportContentParent).GetComponent<TransportItem>();
            item.Construct(carInfo);
        }
        foreach (CarInfo carInfo in saveLoadService.playerData.unRentedTransport)
        {
            var item = Instantiate(_transportPrefab, _transportContentParent).GetComponent<TransportItem>();
            item.Construct(carInfo);
        }
    }

    public void ConstructOrders()
    {
        DestroyChildrens(_orderContentParent);

        foreach (OrderInfo orderInfo in saveLoadService.playerData.freeOrders)
        {
            var item = Instantiate(_orderPrefab, _orderContentParent).GetComponent<OrderItem>();
            item.Construct(orderInfo);
        }
    }

    private void DestroyChildrens(Transform parent)
    {
        if (parent.childCount == 0)
            return;

        Transform[] children = new Transform[parent.childCount];
        for (int i = 0; i < parent.childCount; i++)
        {
            children[i] = parent.GetChild(i);
        }

        foreach (Transform child in children)
        {
            Destroy(child.gameObject);
        }
    }
}
