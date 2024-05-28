using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class Roads
{
    public GameObject lines;

    private Material materialLine = Resources.Load<Material>("RoadMaterial/RoadMaterial");
    private RoadsInfo roadsInfo = new RoadsInfo();

    public static Roads Instance
    {
        get
        {
            if(instance == null)
                instance = new Roads();

            return instance;
        }
    }

    private static Roads instance;


    public void AddEdge(Edge edge)
    {
        LoadRoadsInfo();
        roadsInfo.edges.Add(edge);
        //SaveRoadsInfo();

        DrawRoads();
    }

    /*public void AddStations()
    {
        LoadRoadsInfo();
        roadsInfo.stores.Clear();
        roadsInfo.parkings.Clear();
        roadsInfo.storages.Clear();

        int iterations = 0;
        foreach(var point in roadsInfo.points)
        {

            if (point.StationInfo != null)
            {
                switch (point.StationInfo.StationType)
                {
                    case StationType.Parking:
                        point.StationInfo.PointInfo = point;
                        roadsInfo.parkings.Add(point.StationInfo);

                        break;

                    case StationType.Store:
                        point.StationInfo.PointInfo = point;
                        roadsInfo.stores.Add(point.StationInfo);
                        break;

                    case StationType.Storage:
                        point.StationInfo.PointInfo = point;
                        roadsInfo.storages.Add(point.StationInfo);
                        break;
                }
            }

            point.StationInfo = null;
            point.IsStation = false;
            iterations++;

        }

        Debug.Log("before save iterations " + iterations + "\n" +
            roadsInfo.parkings.Count + " parkings count \n" +
            roadsInfo.storages.Count + " storages count \n" +
            roadsInfo.stores.Count + " stores count ");

        string filePath = Path.Combine(Application.persistentDataPath, "RoadsInfoFileNew.txt");
        string json = JsonConvert.SerializeObject(roadsInfo);
        File.WriteAllText(filePath, json);
    }*/

    public void DrawNewRoad()
    {
        LoadRoadsInfo();
        DrawRoads();
    }

    public void DrawRoads()
    {
        RemoveRoads();
        lines = new GameObject();
        lines.name = "lines";
        foreach(var edge in roadsInfo.edges)
        {
            var lineObj = new GameObject();
            lineObj.transform.SetParent(lines.transform);

          var line = lineObj.AddComponent<LineRenderer>();
            line.positionCount = 2;
            line.SetPosition(0, edge.FirstPoint.GetPosition());
            line.SetPosition(1, edge.SecondPoint.GetPosition());
            line.numCapVertices = 8;
            line.material = materialLine;

            line.startColor = new Color(105f/255, 105f / 255, 105f / 255);
            line.endColor = new Color(105f / 255, 105f / 255, 105f / 255);

            line.startWidth = 0.1f * edge.EdgeWeight;
            line.endWidth = 0.1f * edge.EdgeWeight;
        }
    }

    public void RemoveRoads()
    {
        UnityEngine.Object.DestroyImmediate(lines);
    }

    public void DestroyEdges()
    {
        roadsInfo = new RoadsInfo();
        //SaveRoadsInfo();
    }

/*    public void SaveToFile()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "RoadsInfoFileNew.txt");
        string jsonFile = PlayerPrefs.GetString("RoadsInfo");
        File.WriteAllText(filePath, jsonFile);
    }

    public void LoadFromFile()
    {
        string file = Resources.Load<TextAsset>("RoadsData/RoadsInfoFile").text;
        RoadsInfo info = JsonConvert.DeserializeObject<RoadsInfo>(file);

        Debug.Log("loaded " + info.edges.Count + " edges\n" +
          "loaded " + info.points.Count + " points");
    }*/

/*    private void SaveRoadsInfo()
    {
        string json = JsonConvert.SerializeObject(roadsInfo);
        PlayerPrefs.SetString("RoadsInfo", json);
        Debug.Log("saved " + roadsInfo.edges.Count + " edges\n" +
                  "saved " + roadsInfo.points.Count + " points");
    }*/

    private void LoadRoadsInfo()
    {
        string file = Resources.Load<TextAsset>("RoadsData/RoadsInfoFile").text;
        roadsInfo = JsonConvert.DeserializeObject<RoadsInfo>(file);

       /* Debug.Log("loaded " + roadsInfo.edges.Count + " edges\n" +
          "loaded " + roadsInfo.points.Count + " points\n" +
        roadsInfo.parkings.Count + " parkings count \n" +
        roadsInfo.storages.Count + " storages count \n" +
        roadsInfo.stores.Count + " stores count ");*/
    }


    public void SavePointsToInfo(List<PointInfo> pointsInfo)
    {
        roadsInfo.points = pointsInfo;
        //SaveRoadsInfo();
    }

    public RoadsInfo GetRoadsInfo()
    {
        LoadRoadsInfo();
        return roadsInfo;
    }
}
