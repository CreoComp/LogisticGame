using System;

public class Experience
{
    private static Experience instance;
    public static Experience Instance
    {
        get
        {
            if (instance == null)
                instance = new Experience();
            return instance;
        }
    }

    private int experienceToUpLevel;
    public Action OnChangeExperienceAmount;

    public void AddExperience(int amount)
    {
        SaveLoadService saveLoadService = new SaveLoadService();
        experienceToUpLevel = 100 + saveLoadService.playerData.Level * 100;

        saveLoadService.playerData.Experience += amount;

        while (saveLoadService.playerData.Experience >= experienceToUpLevel)
        {
            saveLoadService.playerData.Level++;
            saveLoadService.playerData.Experience -= experienceToUpLevel;
            experienceToUpLevel = 100 + saveLoadService.playerData.Level * 100;
        }

        saveLoadService.SaveData();
        OnChangeExperienceAmount?.Invoke();

    }
}
