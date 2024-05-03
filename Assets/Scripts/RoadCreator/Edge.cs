public class Edge
{
    public PointInfo FirstPoint = new PointInfo();
    public PointInfo SecondPoint = new PointInfo();

    public int EdgeWeight = 0;

    public void CountWeight()
    {
        if (FirstPoint.Weight >= SecondPoint.Weight)
        {
            EdgeWeight = SecondPoint.Weight;
        }
        else
        {
            EdgeWeight = FirstPoint.Weight;
        }
    }

    public static bool operator ==(Edge a, Edge b)
    {
        if (ReferenceEquals(a, b))
            return true;

        if (a is null || b is null)
            return false;

        return (a.FirstPoint == b.FirstPoint && a.SecondPoint == b.SecondPoint) ||
               (a.FirstPoint == b.SecondPoint && a.SecondPoint == b.FirstPoint);
    }

    // Переопределение оператора !=
    public static bool operator !=(Edge a, Edge b)
    {
        return !(a == b);
    }

    // Переопределение метода Equals
    public override bool Equals(object obj)
    {
        if (obj is null || GetType() != obj.GetType())
            return false;

        Edge other = (Edge)obj;
        return this == other;
    }

    // Переопределение метода GetHashCode
    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 23 + FirstPoint.GetHashCode() + SecondPoint.GetHashCode();
            return hash;
        }
    }
}
