using System;
public class MoneyStorage
{
    public static MoneyStorage instance;

    public static MoneyStorage Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new MoneyStorage();
            }
            return instance;
        }
    }

    private float _money;

    public Action<float> OnMoneyChanged;
    public float MoneyAmount => _money;

    public void Init()
    {
        SaveLoadService saveLoadService = new SaveLoadService();
        _money = saveLoadService.playerData.MoneyAmount;
    }

    public void AddMoney(float amount)
    {
        if(IsValidTransaction(amount))
        {
            _money += amount;

            SaveLoadService saveLoadService = new SaveLoadService();
            saveLoadService.playerData.MoneyAmount = _money;
            saveLoadService.SaveData();
            OnMoneyChanged?.Invoke(_money);
        }
    }

    public bool IsValidTransaction(float amount) =>
        _money + amount >= 0;
}
