using System.Collections;
using UnityEngine;

public class TransportMovement : MonoBehaviour
{
    private RouteInfo _routeInfo;
    private RouteDrawer _routeDrawer;

    private GameObject _transport;
    private GameObject _transportPrefab;
    private PointInfo _positionStart;
    private PointInfo _positionEnd;
    private int _nowEdgeIndex;
    private int _nowPartRouteIndex;
    private float _speedMove;
    private bool isStation;

    private float journeyLength;
    private float startTime;

    private bool isRouteEnded;
    public void Init(RouteInfo routeInfo, RouteDrawer routeDrawer)
    {
        _routeInfo = routeInfo;
        _routeDrawer = routeDrawer;
        _nowEdgeIndex = 0;
        _nowPartRouteIndex = 0;
        _transportPrefab = Resources.Load<GameObject>("Transport/Transport");
        _routeDrawer.DrawPartOfRoute(_routeDrawer.Edges[0]);

        SetNewEdge();

    }

    private void Update()
    {
        if (!isStation && !isRouteEnded)
            MoveTransport();
    }

    private void MoveTransport()
    {
        float distCovered = (Time.time - startTime) * _speedMove * GameTime.Instance.TimeMultiplier;
        float fracJourney = distCovered / journeyLength;
        _transport.transform.position = Vector3.Lerp(_positionStart.GetPosition(), _positionEnd.GetPosition(), fracJourney);
        if (Vector2.Distance(_transport.transform.position, _positionEnd.GetPosition()) < Constants.EpsilonDistance)
        {
            _nowEdgeIndex++;
            SetNewEdge();
        }
    }

    private void SetNewEdge()
    {
        if(isRouteEnded)
            return;

        if (_nowEdgeIndex > _routeDrawer.Edges[_nowPartRouteIndex].Count - 1)
        {
            _nowEdgeIndex = 0;
            _nowPartRouteIndex++;

            if(_nowPartRouteIndex > _routeDrawer.Edges.Count - 1)
            {
                Debug.Log("route ended");
                RouteStatisticsService routeStatisticsService = new RouteStatisticsService(_routeInfo);
                MoneyStorage.Instance.AddMoney(routeStatisticsService.GetPayment() - routeStatisticsService.GetFuelCost());
                Experience.Instance.AddExperience(100);
                _routeDrawer.RemoveLastPartOfRoute();
                isRouteEnded = true;
                return;
            }
            else
            {
                _routeDrawer.DrawPartOfRoute(_routeDrawer.Edges[_nowPartRouteIndex]);
                StartCoroutine(StopInStation());
            }
        }

        switch(_routeDrawer.Edges[_nowPartRouteIndex][_nowEdgeIndex].GetWeight())
        {
            case 1:
                _speedMove = 20 * Constants.MapScale * 2;
                break;

            case 2:
                _speedMove = 40 * Constants.MapScale * 2;
                break;

            case 3:
                _speedMove = 60 * Constants.MapScale * 2;
                break;
        }

        _positionStart = _routeDrawer.Edges[_nowPartRouteIndex][_nowEdgeIndex].FirstPoint;
        _positionEnd = _routeDrawer.Edges[_nowPartRouteIndex][_nowEdgeIndex].SecondPoint;

        if(_transport == null)
        {
            _transport = Instantiate(_transportPrefab, _positionStart.GetPosition(), Quaternion.identity, transform);
        }
        _transport.transform.position = _positionStart.GetPosition();
        journeyLength = Vector3.Distance(_positionStart.GetPosition(), _positionEnd.GetPosition());
        startTime = Time.time;
    }

    IEnumerator StopInStation()
    {
        isStation = true;
        yield return new WaitForSeconds(Constants.TimeAtTheStation / GameTime.Instance.TimeMultiplier);
        isStation = false;
    }


}