using UnityEngine;
using UnityEngine.UI;

public class ChooseTransportService : MonoBehaviour
{
    [SerializeField] private Transform parent;

    private GameObject transportPrefab;
    private CarInfo choosedCar;
    private CategoryType choosedCategory;

    private RouteEditor routeEditor;

    public void Init(RouteEditor routeEditor, CategoryType choosedCategory)
    {
        this.choosedCategory = choosedCategory;
        this.routeEditor = routeEditor;

        ResetPanel();
        ConstructItems();
    }

    private void ResetPanel()
    {
        DestroyChildrens(parent);
        choosedCar = null;
        transportPrefab = Resources.Load<GameObject>("MenuItems/CreateRoute/TransportItem");
    }
    private void DestroyChildrens(Transform parent)
    {
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

    public void ConstructItems()
    {
        SaveLoadService saveLoadService = new SaveLoadService();
        foreach (CarInfo carInfo in saveLoadService.playerData.rentedTransport)
        {
            if (carInfo.carType == choosedCategory && !saveLoadService.playerData.usedTransport.Contains(carInfo))
            {
                var item = Instantiate(transportPrefab, parent).GetComponent<RouteTransportItem>();
                item.Construct(carInfo);
                var button = item.gameObject.GetComponent<Button>();
                button.onClick.AddListener(() => ClickOnTransport(carInfo, item));
            }
        }
    }

    public void ClickOnTransport(CarInfo carInfo, RouteTransportItem routeOrderItem)
    {
        if(choosedCar == carInfo)
        {
            choosedCar = null;
            routeOrderItem.SetInactiveColor();
        }
        else
        {
            choosedCar = carInfo;
            routeOrderItem.SetActiveColor();
        }
    }

    public void Continue()
    {
        if (choosedCar == null)
            return;

        routeEditor.ChooseTransport(choosedCar);
    }
}