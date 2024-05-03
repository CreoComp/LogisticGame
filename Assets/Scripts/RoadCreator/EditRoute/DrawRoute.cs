using UnityEngine;

public class DrawRoute : MonoBehaviour
{
    private RouteInfo _routeInfo;
    private RouteEditor _routeEditor;
    private GameObject _lastPointGameObject;

    [SerializeField] private Transform parentPoints;
    [SerializeField] private Sprite _circleSprite;

    [SerializeField] private GameObject _stationPointPrefab;

    private GameObject _routeLines;
    private Material materialLine; 


    public void Construct(RouteEditor routeEditor, RouteInfo routeInfo)
    {
        _routeEditor = routeEditor;
        _routeInfo = routeInfo;

        materialLine = Resources.Load<Material>("RoadMaterial/RoadMaterial");
        DrawPoints();
    }

    public void Exit()
    {
        Destroy(_lastPointGameObject);
        Destroy(_routeLines);

        foreach(var point in parentPoints.GetComponentsInChildren<Transform>())
        {
            Destroy(point.gameObject);
        }
    }

    public void RemoveLastPoint()
    {
        if(_routeInfo.lastPoint == null || _routeInfo.route.Count == 0)
        {
            return;
        }

        _routeInfo.route.RemoveAt(_routeInfo.route.Count - 1);

        if (_routeInfo.route.Count != 0)
            _routeInfo.lastPoint = _routeInfo.route[_routeInfo.route.Count - 1].SecondPoint;
        else
        {
            Destroy(_lastPointGameObject);
            _routeInfo.lastPoint = null;
        }

        DrawRoad();
    }

    public void AddPointToRoute(PointInfo pointInfo)
    {
        if(_routeInfo.lastPoint == pointInfo) 
            return;

        if(_routeInfo.lastPoint == null)
        {
            _routeInfo.lastPoint = pointInfo;
            DrawLastPoint();
            return;
        }

        Edge edge = new Edge();
        edge.FirstPoint = _routeInfo.lastPoint;
        edge.SecondPoint = pointInfo;
        edge.CountWeight();

        if (Roads.Instance.GetRoadsInfo().edges.Contains(edge) )
        {
            _routeInfo.lastPoint = pointInfo;
            _routeInfo.route.Add(edge);
        }



        DrawRoad();
    }

    private void DrawRoad()
    {
        if (_routeLines != null)
            Destroy(_routeLines);

        _routeLines = new GameObject();
        _routeLines.name = "lines";
        foreach (var edge in _routeInfo.route)
        {
            var lineObj = new GameObject();
            lineObj.transform.SetParent(_routeLines.transform);

            var line = lineObj.AddComponent<LineRenderer>();
            line.positionCount = 2;
            line.SetPosition(0, new Vector3(edge.FirstPoint.GetPosition().x, edge.FirstPoint.GetPosition().y, -1f));
            line.SetPosition(1, new Vector3(edge.SecondPoint.GetPosition().x, edge.SecondPoint.GetPosition().y, -1f));
            line.numCapVertices = 8;
            line.material = materialLine;
            line.sortingLayerName = "UI";

            line.startColor = new Color(75f / 255, 0f / 255, 130f / 255f);
            line.endColor = new Color(75f / 255, 0f / 255, 130f / 255f);


            line.startWidth = 0.1f * edge.EdgeWeight;
            line.endWidth = 0.1f * edge.EdgeWeight;
        }

        DrawLastPoint();
    }

    private void DrawLastPoint()
    {
        if (_lastPointGameObject != null)
            Destroy(_lastPointGameObject);

        if (_routeInfo.lastPoint == null)
            return;

        _lastPointGameObject = new GameObject("LastPoint");
        _lastPointGameObject.transform.parent = parentPoints;
        _lastPointGameObject.AddComponent<PointInRoute>().Construct(this, _routeInfo.lastPoint, _circleSprite, 0.3f, true);
        _lastPointGameObject.transform.position = _routeInfo.lastPoint.GetPosition();
    }

    private void DrawPoints()
    {
        int index = 0;
        foreach(PointInfo pointInfo in Roads.Instance.GetRoadsInfo().points)
        {
            if (pointInfo.IsStation)
                DrawStation(pointInfo, ref index);
            else
                DrawPoint(pointInfo, ref index);
        }
    }

    private void DrawPoint(PointInfo pointInfo, ref int index)
    {
        GameObject point = new GameObject("point" + index);
        point.transform.parent = parentPoints;
        point.AddComponent<PointInRoute>().Construct(this, pointInfo, _circleSprite);
        point.transform.position = pointInfo.GetPosition();

        index++;
    }

    private void DrawStation(PointInfo pointInfo, ref int index)
    {
        GameObject point = Instantiate(_stationPointPrefab, parentPoints);
        point.transform.SetParent(parentPoints);
        point.AddComponent<PointInRoute>().ConstructStation(this, pointInfo, _circleSprite);
        point.transform.position = pointInfo.GetPosition();

        index++;
    }

}
