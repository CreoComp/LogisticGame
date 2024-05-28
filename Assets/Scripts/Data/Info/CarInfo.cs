public class CarInfo
{
    public CategoryType carType;
    public float loadCapacity;
    public int rentCost;
    public float fuelConsumption;

    public string GetCategoryName()
    {
        string name = "";
        switch (carType)
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
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        CarInfo other = (CarInfo)obj;
        return carType == other.carType &&
               loadCapacity == other.loadCapacity &&
               rentCost == other.rentCost &&
               fuelConsumption == other.fuelConsumption;
    }

    // Переопределение оператора ==
    public static bool operator ==(CarInfo car1, CarInfo car2)
    {
        if (ReferenceEquals(car1, car2))
            return true;
        if (ReferenceEquals(car1, null) || ReferenceEquals(car2, null))
            return false;

        return car1.Equals(car2);
    }

    // Переопределение оператора !=
    public static bool operator !=(CarInfo car1, CarInfo car2)
    {
        return !(car1 == car2);
    }

    // Определение хэш-кода
    public override int GetHashCode()
    {
        unchecked // переполнение не выбрасывает исключение
        {
            int hashCode = (int)carType;
            hashCode = (hashCode * 397) ^ loadCapacity.GetHashCode();
            hashCode = (hashCode * 397) ^ rentCost;
            hashCode = (hashCode * 397) ^ fuelConsumption.GetHashCode();
            return hashCode;
        }
    }
    #endregion
}