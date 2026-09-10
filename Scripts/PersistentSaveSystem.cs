using UnityEngine;
using System.IO;
using System.Collections;

public class PersistentSaveSystem : MonoBehaviour
{
    public static PersistentSaveSystem Instance { get; private set; }
    
    [Header("Auto Save Settings")]
    public float autoSaveInterval = 5f; // Save every 5 seconds
    private float autoSaveTimer = 0f;
    
    [Header("Save Paths")]
    private string persistentDataPath;
    private string villageDataFile;
    private string playerDataFile;
    private string buildingsDataFile;
    private string troopsDataFile;
    private string dragonsDataFile;
    private string heroesDataFile;
    
    private bool isGameLoaded = false;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        // Initialize paths
        persistentDataPath = Application.persistentDataPath;
        villageDataFile = Path.Combine(persistentDataPath, "village_data.json");
        playerDataFile = Path.Combine(persistentDataPath, "player_data.json");
        buildingsDataFile = Path.Combine(persistentDataPath, "buildings_data.json");
        troopsDataFile = Path.Combine(persistentDataPath, "troops_data.json");
        dragonsDataFile = Path.Combine(persistentDataPath, "dragons_data.json");
        heroesDataFile = Path.Combine(persistentDataPath, "heroes_data.json");
        
        Debug.Log($"💾 Save Path: {persistentDataPath}");
    }
    
    private void Start()
    {
        // Load game data on start
        LoadAllGameData();
        isGameLoaded = true;
    }
    
    private void Update()
    {
        // Auto save every X seconds
        if (isGameLoaded)
        {
            autoSaveTimer += Time.deltaTime;
            if (autoSaveTimer >= autoSaveInterval)
            {
                SaveAllGameData();
                autoSaveTimer = 0f;
            }
        }
    }
    
    // ===== SAVE FUNCTIONS =====
    
    public void SaveAllGameData()
    {
        SaveVillageData();
        SavePlayerData();
        SaveBuildingsData();
        SaveTroopsData();
        SaveDragonsData();
        SaveHeroesData();
    }
    
    private void SaveVillageData()
    {
        GameManager gm = GameManager.Instance;
        
        VillageData data = new VillageData()
        {
            townHallLevel = gm.townHallLevel,
            gold = gm.gold,
            elixir = gm.elixir,
            darkElixir = gm.darkElixir,
            gems = gm.gems,
            maxGoldCapacity = gm.maxGoldCapacity,
            maxElixirCapacity = gm.maxElixirCapacity,
            availableBuilders = gm.availableBuilders,
            maxBuilders = gm.maxBuilders,
            armyCapacity = gm.armyCapacity,
            currentArmySize = gm.currentArmySize,
            dragonHallCapacity = gm.dragonHallCapacity,
            lastSaveTime = System.DateTime.Now.ToString()
        };
        
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(villageDataFile, json);
    }
    
    private void SavePlayerData()
    {
        GameManager gm = GameManager.Instance;
        
        PlayerData data = new PlayerData()
        {
            playerLevel = gm.playerLevel,
            xp = gm.xp,
            trophies = gm.trophies,
            lastSaveTime = System.DateTime.Now.ToString()
        };
        
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(playerDataFile, json);
    }
    
    private void SaveBuildingsData()
    {
        GameManager gm = GameManager.Instance;
        
        BuildingsListData data = new BuildingsListData()
        {
            buildings = gm.buildings,
            lastSaveTime = System.DateTime.Now.ToString()
        };
        
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(buildingsDataFile, json);
    }
    
    private void SaveTroopsData()
    {
        GameManager gm = GameManager.Instance;
        
        TroopsListData data = new TroopsListData()
        {
            troopsList = new System.Collections.Generic.List<TroopData>(gm.troops.Values),
            lastSaveTime = System.DateTime.Now.ToString()
        };
        
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(troopsDataFile, json);
    }
    
    private void SaveDragonsData()
    {
        GameManager gm = GameManager.Instance;
        
        DragonsListData data = new DragonsListData()
        {
            dragons = gm.dragons,
            lastSaveTime = System.DateTime.Now.ToString()
        };
        
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(dragonsDataFile, json);
    }
    
    private void SaveHeroesData()
    {
        GameManager gm = GameManager.Instance;
        
        HeroesListData data = new HeroesListData()
        {
            heroes = gm.heroes,
            lastSaveTime = System.DateTime.Now.ToString()
        };
        
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(heroesDataFile, json);
    }
    
    // ===== LOAD FUNCTIONS =====
    
    public void LoadAllGameData()
    {
        LoadVillageData();
        LoadPlayerData();
        LoadBuildingsData();
        LoadTroopsData();
        LoadDragonsData();
        LoadHeroesData();
    }
    
    private void LoadVillageData()
    {
        if (File.Exists(villageDataFile))
        {
            string json = File.ReadAllText(villageDataFile);
            VillageData data = JsonUtility.FromJson<VillageData>(json);
            
            GameManager gm = GameManager.Instance;
            gm.townHallLevel = data.townHallLevel;
            gm.gold = data.gold;
            gm.elixir = data.elixir;
            gm.darkElixir = data.darkElixir;
            gm.gems = data.gems;
            gm.maxGoldCapacity = data.maxGoldCapacity;
            gm.maxElixirCapacity = data.maxElixirCapacity;
            gm.availableBuilders = data.availableBuilders;
            gm.maxBuilders = data.maxBuilders;
            gm.armyCapacity = data.armyCapacity;
            gm.currentArmySize = data.currentArmySize;
            gm.dragonHallCapacity = data.dragonHallCapacity;
            
            Debug.Log($"✅ Village loaded! Last saved: {data.lastSaveTime}");
        }
        else
        {
            Debug.Log("🆕 No save file found. Starting fresh game!");
            GameManager.Instance.InitializeGame();
        }
    }
    
    private void LoadPlayerData()
    {
        if (File.Exists(playerDataFile))
        {
            string json = File.ReadAllText(playerDataFile);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            
            GameManager gm = GameManager.Instance;
            gm.playerLevel = data.playerLevel;
            gm.xp = data.xp;
            gm.trophies = data.trophies;
        }
    }
    
    private void LoadBuildingsData()
    {
        if (File.Exists(buildingsDataFile))
        {
            string json = File.ReadAllText(buildingsDataFile);
            BuildingsListData data = JsonUtility.FromJson<BuildingsListData>(json);
            GameManager.Instance.buildings = data.buildings;
        }
    }
    
    private void LoadTroopsData()
    {
        if (File.Exists(troopsDataFile))
        {
            string json = File.ReadAllText(troopsDataFile);
            TroopsListData data = JsonUtility.FromJson<TroopsListData>(json);
            
            GameManager gm = GameManager.Instance;
            gm.troops.Clear();
            foreach (var troop in data.troopsList)
            {
                gm.troops.Add(troop.name, troop);
            }
        }
    }
    
    private void LoadDragonsData()
    {
        if (File.Exists(dragonsDataFile))
        {
            string json = File.ReadAllText(dragonsDataFile);
            DragonsListData data = JsonUtility.FromJson<DragonsListData>(json);
            GameManager.Instance.dragons = data.dragons;
        }
    }
    
    private void LoadHeroesData()
    {
        if (File.Exists(heroesDataFile))
        {
            string json = File.ReadAllText(heroesDataFile);
            HeroesListData data = JsonUtility.FromJson<HeroesListData>(json);
            GameManager.Instance.heroes = data.heroes;
        }
    }
    
    // ===== DELETE/RESET FUNCTIONS =====
    
    public void DeleteAllSaveData()
    {
        try
        {
            if (File.Exists(villageDataFile)) File.Delete(villageDataFile);
            if (File.Exists(playerDataFile)) File.Delete(playerDataFile);
            if (File.Exists(buildingsDataFile)) File.Delete(buildingsDataFile);
            if (File.Exists(troopsDataFile)) File.Delete(troopsDataFile);
            if (File.Exists(dragonsDataFile)) File.Delete(dragonsDataFile);
            if (File.Exists(heroesDataFile)) File.Delete(heroesDataFile);
            
            Debug.Log("🗑️ All save data deleted!");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"❌ Error deleting save data: {e.Message}");
        }
    }
    
    public bool HasSaveData()
    {
        return File.Exists(villageDataFile) && 
               File.Exists(playerDataFile) && 
               File.Exists(buildingsDataFile);
    }
    
    public string GetLastSaveTime()
    {
        if (File.Exists(villageDataFile))
        {
            string json = File.ReadAllText(villageDataFile);
            VillageData data = JsonUtility.FromJson<VillageData>(json);
            return data.lastSaveTime;
        }
        return "No save data";
    }
    
    public long GetSaveDataSize()
    {
        long totalSize = 0;
        
        if (File.Exists(villageDataFile)) totalSize += new FileInfo(villageDataFile).Length;
        if (File.Exists(playerDataFile)) totalSize += new FileInfo(playerDataFile).Length;
        if (File.Exists(buildingsDataFile)) totalSize += new FileInfo(buildingsDataFile).Length;
        if (File.Exists(troopsDataFile)) totalSize += new FileInfo(troopsDataFile).Length;
        if (File.Exists(dragonsDataFile)) totalSize += new FileInfo(dragonsDataFile).Length;
        if (File.Exists(heroesDataFile)) totalSize += new FileInfo(heroesDataFile).Length;
        
        return totalSize;
    }
}

// ===== DATA STRUCTURES =====

[System.Serializable]
public class VillageData
{
    public int townHallLevel;
    public int gold;
    public int elixir;
    public int darkElixir;
    public int gems;
    public int maxGoldCapacity;
    public int maxElixirCapacity;
    public int availableBuilders;
    public int maxBuilders;
    public int armyCapacity;
    public int currentArmySize;
    public int dragonHallCapacity;
    public string lastSaveTime;
}

[System.Serializable]
public class PlayerData
{
    public int playerLevel;
    public int xp;
    public int trophies;
    public string lastSaveTime;
}

[System.Serializable]
public class BuildingsListData
{
    public System.Collections.Generic.List<Building> buildings;
    public string lastSaveTime;
}

[System.Serializable]
public class TroopsListData
{
    public System.Collections.Generic.List<TroopData> troopsList;
    public string lastSaveTime;
}

[System.Serializable]
public class DragonsListData
{
    public System.Collections.Generic.List<Dragon> dragons;
    public string lastSaveTime;
}

[System.Serializable]
public class HeroesListData
{
    public System.Collections.Generic.List<Hero> heroes;
    public string lastSaveTime;
}
