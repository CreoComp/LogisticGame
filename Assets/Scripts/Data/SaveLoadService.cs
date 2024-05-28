using Newtonsoft.Json;
using UnityEngine;

public class SaveLoadService
{
    public PlayerData playerData;

    public SaveLoadService()
    {
        playerData = new PlayerData();
        LoadData();
    }

    public void SaveData()
    {
        // Проверяем, что playerData не является нулевым
        if (playerData != null)
        {
            string json = JsonConvert.SerializeObject(playerData);
            PlayerPrefs.SetString("playerData", json);
        }
        else
        {
            Debug.LogWarning("playerData is null, cannot save data.");
        }
    }

    public void LoadData()
    {
        string json = PlayerPrefs.GetString("playerData");
        // Проверяем, что строка json не пустая
        if (!string.IsNullOrEmpty(json))
        {
            playerData = JsonConvert.DeserializeObject<PlayerData>(json);
        }
        else
        {
            Debug.LogWarning("No saved data found.");
        }
    }

    public void DeleteData()
    {
        PlayerPrefs.DeleteKey("playerData");
        playerData = new PlayerData();
        SaveData();
    }
}
