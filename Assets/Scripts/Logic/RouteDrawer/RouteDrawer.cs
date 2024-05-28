using System.Collections.Generic;
using UnityEngine;

public class RouteDrawer : MonoBehaviour
{
    private RouteInfo _routeInfo;

    private Dictionary<int, List<Edge>> _edges;
    public Dictionary<int, List<Edge>> Edges => _edges;

    private GameObject _drawedPartOfRoute;
    private Material _materialRoute;
    private Color _routeColor;

    public void Init(RouteInfo routeInfo)
    {
        _routeInfo = routeInfo;
        _materialRoute = Resources.Load<Material>("RoadMaterial/RoadMaterial");
        _routeColor = Constants.basicColors[Random.Range(0, Constants.basicColors.Count)];

        _edges = DivisionRoute(_routeInfo);

        RemoveLastPartOfRoute();
        gameObject.AddComponent<TransportMovement>().Init(_routeInfo, this);


        /*Debug.Log(routeInfo.conductedStations.Count + " count stations\n");
        Debug.Log(_edges.Count + " count division routes\n" +
            routeInfo.route.Count + " all edges count");
        foreach(List<Edge> edge in _edges.Values)
        {
            Debug.Log(edge.Count + " edges in division route");
        }*/
    }

    public void RemoveLastPartOfRoute()
    {
        if(_drawedPartOfRoute == null) 
            return;

        Destroy(_drawedPartOfRoute);
    }

    public void DrawPartOfRoute(List<Edge> edgesPart)
    {
        RemoveLastPartOfRoute();
        _drawedPartOfRoute = new GameObject("PartOfRoute");

        foreach (var edge in edgesPart)
        {
            var lineObj = new GameObject();
            lineObj.transform.SetParent(_drawedPartOfRoute.transform);

            var line = lineObj.AddComponent<LineRenderer>();
            line.positionCount = 2;
            line.SetPosition(0, new Vector3(edge.FirstPoint.GetPosition().x, edge.FirstPoint.GetPosition().y, 0));
            line.SetPosition(1, new Vector3(edge.SecondPoint.GetPosition().x, edge.SecondPoint.GetPosition().y, 0));
            line.numCapVertices = 8;
            line.material = _materialRoute;
            line.sortingLayerName = "Default";

            line.startColor = _routeColor;
            line.endColor = _routeColor;


            line.startWidth = 0.1f * edge.EdgeWeight;
            line.endWidth = 0.1f * edge.EdgeWeight;
        }
    }

    private Dictionary<int, List<Edge>> DivisionRoute(RouteInfo routeInfo)
    {
        Dictionary<int, List<Edge>> edges = new Dictionary<int, List<Edge>>();

        edges.Add(0, new List<Edge>());
        int routeIndex = 0;

        for (int i = 0; i < routeInfo.route.Count; i++)
        {
            edges[routeIndex].Add(routeInfo.route[i]);

            if(routeIndex < routeInfo.conductedStations.Count - 1)
            {
                if (routeInfo.route[i].SecondPoint == routeInfo.conductedStations[routeIndex].PointInfo)
                {
                    routeIndex++;
                    edges.Add(routeIndex, new List<Edge>());
                }
            }
        }

        return edges;
    }
}
