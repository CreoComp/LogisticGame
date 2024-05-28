using UnityEngine;

public class DrawStations : MonoBehaviour
{
    [SerializeField] private Transform parentStations;
    [SerializeField] private GameObject _stationPointPrefab;
    [SerializeField] private Sprite _circleSprite;

    public void Draw()
    {
        RemoveStations();

        foreach(var station in Roads.Instance.GetRoadsInfo().parkings)
        {
            GameObject point = Instantiate(_stationPointPrefab, parentStations);
            point.GetComponent<Station>().DrawStation(station, _circleSprite);
            point.transform.position = station.PointInfo.GetPosition();
        }
        foreach (var station in Roads.Instance.GetRoadsInfo().stores)
        {
            GameObject point = Instantiate(_stationPointPrefab, parentStations);
            point.GetComponent<Station>().DrawStation(station, _circleSprite);
            point.transform.position = station.PointInfo.GetPosition();
        }
        foreach (var station in Roads.Instance.GetRoadsInfo().storages)
        {
            GameObject point = Instantiate(_stationPointPrefab, parentStations);
            point.GetComponent<Station>().DrawStation(station, _circleSprite);
            point.transform.position = station.PointInfo.GetPosition();
        }
    }

    public void RemoveStations()
    {
        Transform[] children = new Transform[parentStations.childCount];
        for (int i = 0; i < parentStations.childCount; i++)
        {
            children[i] = parentStations.GetChild(i);
        }

        foreach (Transform child in children)
        {
            Destroy(child.gameObject);
        }
    }
}
