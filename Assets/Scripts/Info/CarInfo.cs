public class CarInfo
{
    public CategoryType carType;
    public string carName;
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
}
