public class OrderInfo
{
    public int payment;
    public int weight;
    public StationInfo stationStorage;
    public StationInfo stationStore;
    public CategoryType categoryType;

    public string GetCategoryName()
    {
        string name = "";
        switch (categoryType)
        {
            case CategoryType.Drink:
                name = "Напитки";
                break;

            case CategoryType.Freeze:
                name = "Замороженные изделия";
                break;

            case CategoryType.Candy:
                name = "Кондитерские изделия";
                break;
        }
        return name;
    }

    #region переопределение операторов
    // Переопределение Equals для сравнения всех полей
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        OrderInfo other = (OrderInfo)obj;
        return payment == other.payment &&
               weight == other.weight &&
               stationStorage == other.stationStorage &&
               stationStore == other.stationStore &&
               categoryType == other.categoryType;
    }

    // Переопределение оператора ==
    public static bool operator ==(OrderInfo order1, OrderInfo order2)
    {
        if (ReferenceEquals(order1, order2))
            return true;
        if (ReferenceEquals(order1, null) || ReferenceEquals(order2, null))
            return false;

        return order1.Equals(order2);
    }

    // Переопределение оператора !=
    public static bool operator !=(OrderInfo order1, OrderInfo order2)
    {
        return !(order1 == order2);
    }

    // Определение хэш-кода (важно, если объект будет использоваться в HashSet или Dictionary)
    public override int GetHashCode()
    {
        unchecked // переполнение не выбрасывает исключение
        {
            int hashCode = payment;
            hashCode = (hashCode * 397) ^ weight;
            hashCode = (hashCode * 397) ^ (stationStorage != null ? stationStorage.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (stationStore != null ? stationStore.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (int)categoryType;
            return hashCode;
        }
    }
    #endregion
}