using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DrawRoute : MonoBehaviour
{
    private RouteInfo _routeInfo;
    private RouteEditor _routeEditor;
    private GameObject _lastPointGameObject;

    [SerializeField] private Transform parentPoints;
    [SerializeField] private Transform parentStations;
    [SerializeField] private Sprite _circleSprite;
    [SerializeField] private RouteStatistics _routeStatistics;

    [SerializeField] private GameObject _stationPointPrefab;

    private GameObject _routeLines;
    private Material materialLine; 

    public void Construct(RouteEditor routeEditor, RouteInfo routeInfo)
    {
        _routeEditor = routeEditor;
        _routeInfo = routeInfo;

        materialLine = Resources.Load<Material>("RoadMaterial/RoadMaterial");
        DrawPoints();
        _routeStatistics.UpdateStatistics(_routeInfo);
    }

    public void Exit()
    {
        Destroy(_lastPointGameObject);
        Destroy(_routeLines);

        Transform[] children = new Transform[parentPoints.childCount];
        for (int i = 0; i < parentPoints.childCount; i++)
        {
            children[i] = parentPoints.GetChild(i);
        }

        foreach (Transform child in children)
        {
            Destroy(child.gameObject);
        }
    }

    public void RemoveLastPoint()
    {
        if(_routeInfo.route.Count == 0)
        {
            return;
        }

        _routeInfo.route.RemoveAt(_routeInfo.route.Count - 1);

        if (_routeInfo.route.Count != 0)
        {

            if (_routeInfo.conductedStations.Count > 0)
            {
                if (_routeInfo.conductedStations[_routeInfo.conductedStations.Count - 1].PointInfo == _routeInfo.lastPoint)
                {
                    _routeInfo.conductedStations.RemoveAt(_routeInfo.conductedStations.Count - 1);
                }
            }

            _routeInfo.lastPoint = _routeInfo.route[_routeInfo.route.Count - 1].SecondPoint;
        }
        else
        {
            Destroy(_lastPointGameObject);
            _routeInfo.lastPoint = _routeInfo.parkingPoint;
        }

        DrawRoad();
    }

    public void AddStationToRoute(StationInfo stationInfo)
    {
        PointInfo pointInfo = stationInfo.PointInfo;
        if (_routeInfo.lastPoint == pointInfo)
            return;

        if (_routeInfo.lastPoint == null)
        {
            _routeInfo.lastPoint = pointInfo;
            DrawLastPoint();
            return;
        }

        Edge edge = new Edge();
        edge.FirstPoint = _routeInfo.lastPoint;
        edge.SecondPoint = pointInfo;
        edge.CountWeight();

        if (!_routeInfo.conductedStations.Contains(stationInfo))
        {
            _routeInfo.conductedStations.Add(stationInfo);
        }

            if (Roads.Instance.GetRoadsInfo().edges.Contains(edge))
            {
                _routeInfo.lastPoint = pointInfo;
                _routeInfo.route.Add(edge);
            }

        DrawRoad();
        _routeStatistics.UpdateStatistics(_routeInfo);
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
        _routeStatistics.UpdateStatistics(_routeInfo);
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
        List<PointInfo> drawedPoints = new List<PointInfo>();

        foreach(var order in _routeInfo.orders)
        {
            if(!drawedPoints.Contains(order.stationStorage.PointInfo))
            {
                drawedPoints.Add(order.stationStorage.PointInfo);
                DrawStation(order.stationStorage, ref index);
            }
            if (!drawedPoints.Contains(order.stationStore.PointInfo))
            {
                drawedPoints.Add(order.stationStore.PointInfo);
                DrawStation(order.stationStore, ref index);
            }
        }

        foreach(var station in Roads.Instance.GetRoadsInfo().parkings)
        {
            DrawStation(station, ref index);
            _routeInfo.parkingPoint = station.PointInfo;
            drawedPoints.Add(station.PointInfo);
            _routeInfo.lastPoint = _routeInfo.parkingPoint;
        }
/*        foreach (var station in Roads.Instance.GetRoadsInfo().stores)
        {
            DrawStation(station, ref index);
            drawedPoints.Add(station.PointInfo);
        }
        foreach (var station in Roads.Instance.GetRoadsInfo().storages)
        {
            DrawStation(station, ref index);
            drawedPoints.Add(station.PointInfo);
        }*/

        foreach (PointInfo pointInfo in Roads.Instance.GetRoadsInfo().points)
        {
            if(!drawedPoints.Contains(pointInfo))
            {
                DrawPoint(pointInfo, ref index);
            }
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

    private void DrawStation(StationInfo stationInfo, ref int index)
    {
        GameObject point = Instantiate(_stationPointPrefab, parentPoints);
        point.AddComponent<PointInRoute>().ConstructStation(this, stationInfo, _circleSprite);
        point.transform.position = stationInfo.PointInfo.GetPosition();

        index++;
    }

    public void ConfirmRoute()
    {
        if(IsValidRoute())
        {
            _routeInfo.startTime = GameTime.Instance.GetTime;
            _routeEditor.ConfirmRoute(_routeInfo);
        }
    }

    public bool IsValidRoute()
    {
        bool isHaveStorage = false;
        bool isHaveStores = false;
        bool isHaveParking = false;

        foreach(var order in _routeInfo.orders)
        {
            if (_routeInfo.conductedStations.Count == 0)
                break;

            if(order.stationStorage == _routeInfo.conductedStations[0])
                isHaveStorage = true;
        }

        List<StationInfo> stores = new List<StationInfo>();

        foreach (var order in _routeInfo.orders)
        {
            stores.Add(order.stationStore);
        }

        int conductedStores = 0;
        foreach(var station in _routeInfo.conductedStations)
        {
            foreach(var store in stores)
            {
                if (store == station)
                {
                    conductedStores++;
                    break;
                }
            }
        }

        if (conductedStores == stores.Count)
            isHaveStores = true;

        if (_routeInfo.conductedStations.Count != 0)
        {
            if (_routeInfo.conductedStations[_routeInfo.conductedStations.Count - 1].PointInfo == _routeInfo.parkingPoint)
                isHaveParking = true;

        }
        /*Debug.Log(isHaveParking + " is have parking\n" +
            isHaveStorage + " is have storage\n" +
            isHaveStores + " is have stores\n");*/


        return (isHaveParking && isHaveStorage && isHaveStores);
    }

}
