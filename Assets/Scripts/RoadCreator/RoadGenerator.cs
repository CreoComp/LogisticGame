using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    public StationInfo info;

    public void SavePoints()
    {
        List<PointInfo> pointsInfo = new List<PointInfo>();

        foreach(Point point in FindObjectsOfType<Point>())
        {
            PointInfo pointInfo = new PointInfo();
            pointInfo.SetPosition(point.transform.position);
            pointInfo.Weight = point.Weight;

            pointsInfo.Add(pointInfo);
        }

        Roads.Instance.SavePointsToInfo(pointsInfo);
    }
}
