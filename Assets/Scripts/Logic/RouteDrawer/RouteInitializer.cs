using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteInitializer : MonoBehaviour
{
    public List<RouteInfo> unInitializedRoutes = new List<RouteInfo>();
    private RouteInfo _routeInfo;

    [SerializeField] private Transform parentRoutes;

    public void InitializeRoute(RouteInfo routeInfo)
    {
        _routeInfo = routeInfo;
        StartCoroutine(WaitAndInitialize(routeInfo));
    }

    IEnumerator WaitAndInitialize(RouteInfo routeInfo)
    {
        while ((GameTime.Instance.GetTime - _routeInfo.startTime).TotalMinutes < 60)
        {
            yield return new WaitForSeconds(1);
        }

        Debug.Log("start route");

        GameObject route = new GameObject("route " + routeInfo.GetCategoryName());
        route.transform.SetParent(parentRoutes);
        route.transform.localScale = Vector3.one;
        RouteDrawer routeDrawer = route.AddComponent<RouteDrawer>();
        routeDrawer.Init(routeInfo);
    }
}
