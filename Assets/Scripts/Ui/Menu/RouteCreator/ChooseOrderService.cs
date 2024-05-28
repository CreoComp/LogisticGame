using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseOrderService: MonoBehaviour
{
    [SerializeField] private Transform parent;
    private GameObject orderPrefab;

    private List<OrderInfo> orderInfos = new List<OrderInfo>();
    private CategoryType choosedCategory;

    private RouteEditor routeEditor;

    public void Init(RouteEditor routeEditor)
    {
        this.routeEditor = routeEditor;
        ResetPanel();
        ConstructItems();
    }

    private void ResetPanel()
    {
        DestroyChildrens(parent);
        orderInfos = new List<OrderInfo>();
        orderPrefab = Resources.Load<GameObject>("MenuItems/CreateRoute/OrderItem");
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
        foreach (OrderInfo orderInfo in saveLoadService.playerData.freeOrders)
        {
            var item = Instantiate(orderPrefab, parent).GetComponent<RouteOrderItem>();
            item.Construct(orderInfo);
            var button = item.gameObject.GetComponent<Button>();
            button.onClick.AddListener(() => ClickOnOrder(orderInfo, item));
        }
    }

    public void ClickOnOrder(OrderInfo orderInfo, RouteOrderItem routeOrderItem)
    {
        if(orderInfos.Count == 0)
        {
            choosedCategory = orderInfo.categoryType;
            AddToList(orderInfo, routeOrderItem);
            return;
        }

        if (orderInfo.categoryType != choosedCategory)
            return;

        if(orderInfo.categoryType == choosedCategory)
        {
            if (orderInfos.Contains(orderInfo))
                RemoveFromList(orderInfo, routeOrderItem);
            else
                AddToList(orderInfo, routeOrderItem);
        }
    }

    private void AddToList(OrderInfo orderInfo, RouteOrderItem routeOrderItem)
    {
        orderInfos.Add(orderInfo);
        routeOrderItem.SetActiveColor();
    }

    private void RemoveFromList(OrderInfo orderInfo, RouteOrderItem routeOrderItem)
    {
        orderInfos.Remove(orderInfo);
        routeOrderItem.SetInactiveColor();

        CheckCategory();
    }

    private void CheckCategory()
    {
        if (orderInfos.Count == 0)
            choosedCategory = CategoryType.None;
    }

    public void Continue()
    {
        if (orderInfos.Count == 0)
            return;

        routeEditor.ChooseOrders(orderInfos, choosedCategory);
    }
}
