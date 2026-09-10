using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("Player Data")]
    public int playerLevel = 1;
    public int townHallLevel = 1;
    public int xp = 0;
    public int trophies = 0;
    public int gems = 0;
    
    [Header("Resources")]
    public int gold = 1000;
    public int elixir = 1000;
    public int darkElixir = 0;
    public int maxGoldCapacity = 10000;
    public int maxElixirCapacity = 10000;
    
    [Header("Buildings")]
    public List<Building> buildings = new List<Building>();
    public int availableBuilders = 1;
    public int maxBuilders = 5;
    
    [Header("Troops")]
    public Dictionary<string, TroopData> troops = new Dictionary<string, TroopData>();
    public int armyCapacity = 20;
    public int currentArmySize = 0;
    
    [Header("Dragons")]
    public List<Dragon> dragons = new List<Dragon>();
    public int dragonHallCapacity = 5;
    
    [Header("Heroes")]
    public List<Hero> heroes = new List<Hero>();
    
    [Header("Save System")]
    public string savePath;
    private SaveData saveData;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        savePath = Application.persistentDataPath + "/village_save.json";
        LoadGame();
    }
    
    private void Start()
    {
        InitializeGame();
    }
    
    private void Update()
    {
        UpdateResourceGeneration();
    }
    
    public void InitializeGame()
    {
        if (townHallLevel == 1)
        {
            // First time setup
            InitializeStarterBuildings();
            InitializeStarterTroops();
            InitializeHeroes();
        }
    }
    
    private void InitializeStarterBuildings()
    {
        // Add Town Hall
        Building townHall = new Building()
        {
            buildingId = "town_hall_1",
            buildingName = "Town Hall",
            level = 1,
            health = 100,
            maxHealth = 100,
            positionX = 5,
            positionY = 5
        };
        buildings.Add(townHall);
        
        // Add starting resources buildings
        buildings.Add(new Building() { buildingId = "gold_mine_1", buildingName = "Gold Mine", level = 1 });
        buildings.Add(new Building() { buildingId = "elixir_collector_1", buildingName = "Elixir Collector", level = 1 });
    }
    
    private void InitializeStarterTroops()
    {
        troops.Add("Barbarian", new TroopData() { name = "Barbarian", level = 1, count = 0 });
        troops.Add("Archer", new TroopData() { name = "Archer", level = 1, count = 0 });
        troops.Add("Dragon", new TroopData() { name = "Dragon", level = 1, count = 0 });
    }
    
    private void InitializeHeroes()
    {
        heroes.Add(new Hero() { heroId = 1, heroName = "Barbarian King", level = 1 });
        heroes.Add(new Hero() { heroId = 2, heroName = "Archer Queen", level = 1 });
        heroes.Add(new Hero() { heroId = 3, heroName = "Grand Warden", level = 1 });
        heroes.Add(new Hero() { heroId = 4, heroName = "Royal Champion", level = 1 });
    }
    
    private void UpdateResourceGeneration()
    {
        // Gold generation from Gold Mines
        foreach (var building in buildings)
        {
            if (building.buildingName == "Gold Mine" && gold < maxGoldCapacity)
            {
                gold += (int)(0.5f * building.level * Time.deltaTime);
                if (gold > maxGoldCapacity) gold = maxGoldCapacity;
            }
            
            if (building.buildingName == "Elixir Collector" && elixir < maxElixirCapacity)
            {
                elixir += (int)(0.5f * building.level * Time.deltaTime);
                if (elixir > maxElixirCapacity) elixir = maxElixirCapacity;
            }
        }
    }
    
    public bool UpgradeBuilding(Building building, int upgradeCost)
    {
        if (gold >= upgradeCost)
        {
            gold -= upgradeCost;
            building.level++;
            SaveGame();
            return true;
        }
        return false;
    }
    
    public void AddGold(int amount)
    {
        gold += amount;
        if (gold > maxGoldCapacity) gold = maxGoldCapacity;
        SaveGame();
    }
    
    public void AddElixir(int amount)
    {
        elixir += amount;
        if (elixir > maxElixirCapacity) elixir = maxElixirCapacity;
        SaveGame();
    }
    
    public void UpgradeTownHall()
    {
        if (townHallLevel < 100)
        {
            townHallLevel++;
            UpdateCapacities();
            SaveGame();
        }
    }
    
    private void UpdateCapacities()
    {
        maxGoldCapacity = 10000 + (townHallLevel * 500);
        maxElixirCapacity = 10000 + (townHallLevel * 500);
        maxBuilders = Mathf.Min(5, 1 + townHallLevel / 20);
        armyCapacity = 20 + (townHallLevel * 2);
        dragonHallCapacity = 1 + townHallLevel / 10;
    }
    
    public void TrainTroop(string troopName, int count)
    {
        if (troops.ContainsKey(troopName))
        {
            troops[troopName].count += count;
            currentArmySize += count;
            SaveGame();
        }
    }
    
    public void HatchDragon(Dragon dragon)
    {
        if (dragons.Count < dragonHallCapacity)
        {
            dragons.Add(dragon);
            SaveGame();
        }
    }
    
    public void UpgradeHero(Hero hero, int upgradeCost)
    {
        if (darkElixir >= upgradeCost)
        {
            darkElixir -= upgradeCost;
            hero.level++;
            SaveGame();
        }
    }
    
    // Developer Mode
    public void ActivateDeveloperMode(string password)
    {
        if (password == "jaripraj")
        {
            gold = 999999;
            elixir = 999999;
            darkElixir = 999999;
            gems = 999999;
            
            // Max all buildings
            foreach (var building in buildings)
            {
                building.level = 100;
                building.health = 9999;
            }
            
            // Max all troops
            foreach (var troop in troops.Values)
            {
                troop.level = 100;
            }
            
            // Max all heroes
            foreach (var hero in heroes)
            {
                hero.level = 100;
            }
            
            townHallLevel = 100;
            SaveGame();
            Debug.Log("🔓 Developer Mode Activated!");
        }
    }
    
    public void SaveGame()
    {
        saveData = new SaveData()
        {
            playerLevel = this.playerLevel,
            townHallLevel = this.townHallLevel,
            gold = this.gold,
            elixir = this.elixir,
            darkElixir = this.darkElixir,
            gems = this.gems,
            trophies = this.trophies
        };
        
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(savePath, json);
        Debug.Log("💾 Game Saved!");
    }
    
    public void LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            saveData = JsonUtility.FromJson<SaveData>(json);
            
            playerLevel = saveData.playerLevel;
            townHallLevel = saveData.townHallLevel;
            gold = saveData.gold;
            elixir = saveData.elixir;
            darkElixir = saveData.darkElixir;
            gems = saveData.gems;
            trophies = saveData.trophies;
            
            Debug.Log("📂 Game Loaded!");
        }
    }
}

[System.Serializable]
public class Building
{
    public string buildingId;
    public string buildingName;
    public int level = 1;
    public int health = 100;
    public int maxHealth = 100;
    public int positionX;
    public int positionY;
    public float upgradeTimeRemaining = 0;
}

[System.Serializable]
public class TroopData
{
    public string name;
    public int level = 1;
    public int count = 0;
    public int trainingTime = 60;
}

[System.Serializable]
public class Dragon
{
    public string dragonId;
    public string dragonType;
    public int level = 1;
    public int health = 100;
    public float hatchTime = 0;
}

[System.Serializable]
public class Hero
{
    public int heroId;
    public string heroName;
    public int level = 1;
    public int health = 100;
    public bool isAlive = true;
    public float healTime = 0;
}

[System.Serializable]
public class SaveData
{
    public int playerLevel;
    public int townHallLevel;
    public int gold;
    public int elixir;
    public int darkElixir;
    public int gems;
    public int trophies;
}
