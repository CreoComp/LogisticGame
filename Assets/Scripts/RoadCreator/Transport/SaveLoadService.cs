using Newtonsoft.Json;
using UnityEngine;

public class SaveLoadService
{
    public static SaveLoadService Instance
    {
        get
        {
            if (instance == null)
                instance = new SaveLoadService();

            return instance;
        }
    }

    private static SaveLoadService instance;


    public PlayerData playerData;
    public void SaveData()
    {
        string json = JsonConvert.SerializeObject(playerData);
        PlayerPrefs.SetString("playerData", json);
        Debug.Log("saved");
    }

    public void LoadData()
    {
        string json = PlayerPrefs.GetString("playerData");
        playerData = JsonConvert.DeserializeObject<PlayerData>(json);
        Debug.Log("loaded");
    }
}
