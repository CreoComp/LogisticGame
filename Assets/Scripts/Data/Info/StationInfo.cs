using System;

[Serializable]
public class StationInfo
{
    public string Name;
    public StationType StationType;
    public string LogoName;
    public PointInfo PointInfo;

    #region переопределение операторов
    // Переопределение Equals для сравнения всех полей
    public override bool Equals(object obj)
    {
        if (obj == null || !(obj is StationInfo))
        {
            return false;
        }

        StationInfo other = (StationInfo)obj;
        return this.Name == other.Name &&
               this.StationType == other.StationType &&
               this.LogoName == other.LogoName &&
               this.PointInfo.Equals(other.PointInfo);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 23 + (Name != null ? Name.GetHashCode() : 0);
            hash = hash * 23 + StationType.GetHashCode();
            hash = hash * 23 + (LogoName != null ? LogoName.GetHashCode() : 0);
            hash = hash * 23 + (PointInfo != null ? PointInfo.GetHashCode() : 0);
            return hash;
        }
    }

    public static bool operator ==(StationInfo station1, StationInfo station2)
    {
        if (ReferenceEquals(station1, station2))
        {
            return true;
        }

        if (station1 is null || station2 is null)
        {
            return false;
        }

        return station1.Equals(station2);
    }

    public static bool operator !=(StationInfo station1, StationInfo station2)
    {
        return !(station1 == station2);
    }
    #endregion
}