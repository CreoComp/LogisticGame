using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RoadGenerator))]
public class RoadGeneratorEditor : Editor
{
    private int _weight = 1;

    RoadGenerator _generator;

    void InstantiatePoint()
    {
        var point = new GameObject();
        point.AddComponent<Point>().Weight = _weight;
        point.name = "point";
    }

    void Generate()
    {
        foreach(var obj in Selection.gameObjects)
        {
            if (obj.GetComponent<Point>() == null)
                return;
        }
        
        for (int i = 0; i < Selection.gameObjects.Length - 1; i++)
        {
            AddEdge(Selection.gameObjects[i], Selection.gameObjects[i + 1]);
        }
    }

    private void AddEdge(GameObject FirstPoint, GameObject SecondPoint)
    {
        Edge edge = new Edge();

        edge.FirstPoint.SetPosition(FirstPoint.GetComponent<Point>().transform.position);
        edge.SecondPoint.SetPosition(SecondPoint.GetComponent<Point>().transform.position);

        if (FirstPoint.GetComponent<Point>().Weight >= SecondPoint.GetComponent<Point>().Weight)
        {
            edge.EdgeWeight = SecondPoint.GetComponent<Point>().Weight;
        }
        else
        {
            edge.EdgeWeight = FirstPoint.GetComponent<Point>().Weight;
        }

        Roads.Instance.AddEdge(edge);
    }


    public override void OnInspectorGUI()
    {
        _generator = (RoadGenerator)target;
        _weight = EditorGUILayout.IntSlider(_weight, 1, 3);

/*        if (GUILayout.Button("Save file"))
        {
            Roads.Instance.SaveToFile();
        }
        if (GUILayout.Button("Load file"))
        {
            Roads.Instance.LoadFromFile();
        }*/

        /*if (GUILayout.Button("Instantiate Point"))
        {
            InstantiatePoint();
        }


        if (GUILayout.Button("Generate Edge"))
        {
            Generate();
        }*/

        /*if (GUILayout.Button("Destroy Edges"))
        {
            Roads.Instance.DestroyEdges();
        }*/

        /*if (GUILayout.Button("Save Points"))
        {
            _generator.SavePoints();
        }*/

        if (GUILayout.Button("Draw Road"))
        {
            Roads.Instance.DrawNewRoad();
        }

        if (GUILayout.Button("SetPointAsStation"))
        {
            PointInfo pointInfo = new PointInfo();
            pointInfo.SetPosition(Selection.gameObjects[0].transform.position);
            pointInfo.Weight = Selection.gameObjects[0].GetComponent<Point>().Weight;

            EditorSerializer serializer = _generator.GetComponent<EditorSerializer>();
            Roads.Instance.SavePointAsStation(serializer.stationInfo, pointInfo);
        }

        if (GUILayout.Button("DeletePointAsStation"))
        {
            PointInfo pointInfo = new PointInfo();
            pointInfo.SetPosition(Selection.gameObjects[0].transform.position);
            pointInfo.Weight = Selection.gameObjects[0].GetComponent<Point>().Weight;

            Roads.Instance.DestroyPointAsStation(pointInfo);
        }

    }
}
