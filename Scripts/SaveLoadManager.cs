using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance { get; private set; }
    
    private string savePath;
    private const string SAVE_FILE = "/village_save.json";
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        savePath = Application.persistentDataPath;
    }
    
    public void SaveVillageData()
    {
        GameManager gm = GameManager.Instance;
        
        VillageSaveData villageData = new VillageSaveData()
        {
            playerLevel = gm.playerLevel,
            townHallLevel = gm.townHallLevel,
            gold = gm.gold,
            elixir = gm.elixir,
            darkElixir = gm.darkElixir,
            gems = gm.gems,
            trophies = gm.trophies,
            xp = gm.xp,
            timestamp = System.DateTime.Now.ToString()
        };
        
        string json = JsonUtility.ToJson(villageData, true);
        File.WriteAllText(savePath + SAVE_FILE, json);
        Debug.Log("💾 Village saved successfully!");
    }
    
    public void LoadVillageData()
    {
        string path = savePath + SAVE_FILE;
        
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            VillageSaveData villageData = JsonUtility.FromJson<VillageSaveData>(json);
            
            GameManager gm = GameManager.Instance;
            gm.playerLevel = villageData.playerLevel;
            gm.townHallLevel = villageData.townHallLevel;
            gm.gold = villageData.gold;
            gm.elixir = villageData.elixir;
            gm.darkElixir = villageData.darkElixir;
            gm.gems = villageData.gems;
            gm.trophies = villageData.trophies;
            gm.xp = villageData.xp;
            
            Debug.Log($"📂 Village loaded! Last saved: {villageData.timestamp}");
        }
        else
        {
            Debug.LogWarning("No save file found. Starting new game!");
        }
    }
    
    public void DeleteSaveData()
    {
        string path = savePath + SAVE_FILE;
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("🗑️ Save data deleted!");
        }
    }
}

[System.Serializable]
public class VillageSaveData
{
    public int playerLevel;
    public int townHallLevel;
    public int gold;
    public int elixir;
    public int darkElixir;
    public int gems;
    public int trophies;
    public int xp;
    public string timestamp;
}
