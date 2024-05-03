using UnityEngine;

public class PointInfo
{
    public int Weight;
    public float[] Position = {0, 0};


    public bool IsStation;
    public StationInfo StationInfo;


    public Vector2 GetPosition() =>
        new Vector2(Position[0], Position[1]);

    public void SetPosition(Vector2 vector)
    {
        Position[0] = vector.x;
        Position[1] = vector.y;
    }

    public void SetPointAsStation(StationInfo stationInfo)
    {
        StationInfo = stationInfo;
        IsStation = true;
    }

    public void DeletePointAsStation()
    {
        StationInfo = null;
        IsStation = false;
    }

    public static bool operator ==(PointInfo a, PointInfo b)
    {
        if (ReferenceEquals(a, b))
            return true;

        if (a is null || b is null)
            return false;

        return a.Position[0] == b.Position[0] && a.Position[1] == b.Position[1];
    }

    // Переопределение оператора !=
    public static bool operator !=(PointInfo a, PointInfo b)
    {
        return !(a == b);
    }

    // Переопределение метода Equals
    public override bool Equals(object obj)
    {
        if (obj is null || GetType() != obj.GetType())
            return false;

        PointInfo other = (PointInfo)obj;
        return this == other;
    }

    // Переопределение метода GetHashCode
    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 23 + Position[0].GetHashCode();
            hash = hash * 23 + Position[1].GetHashCode();
            return hash;
        }
    }
}