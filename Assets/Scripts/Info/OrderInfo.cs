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
}
