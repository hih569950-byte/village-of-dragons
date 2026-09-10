using UnityEngine;
using System.Collections.Generic;

public class DragonSystem : MonoBehaviour
{
    public static DragonSystem Instance { get; private set; }
    
    [Header("Dragon Data")]
    public List<Dragon> ownedDragons = new List<Dragon>();
    public List<DragonBreed> dragonBreeds = new List<DragonBreed>();
    
    [Header("Breeding")]
    public float breedingTime = 3600f; // 1 hour
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        InitializeDragonBreeds();
    }
    
    private void InitializeDragonBreeds()
    {
        dragonBreeds.Add(new DragonBreed() { name = "Fire Dragon", type = 1 });
        dragonBreeds.Add(new DragonBreed() { name = "Ice Dragon", type = 2 });
        dragonBreeds.Add(new DragonBreed() { name = "Lightning Dragon", type = 3 });
        dragonBreeds.Add(new DragonBreed() { name = "Earth Dragon", type = 4 });
        dragonBreeds.Add(new DragonBreed() { name = "Wind Dragon", type = 5 });
        dragonBreeds.Add(new DragonBreed() { name = "Shadow Dragon", type = 6 });
        dragonBreeds.Add(new DragonBreed() { name = "Light Dragon", type = 7 });
        dragonBreeds.Add(new DragonBreed() { name = "Metal Dragon", type = 8 });
        dragonBreeds.Add(new DragonBreed() { name = "Poison Dragon", type = 9 });
        dragonBreeds.Add(new DragonBreed() { name = "Lava Dragon", type = 10 });
        dragonBreeds.Add(new DragonBreed() { name = "Crystal Dragon", type = 11 });
        dragonBreeds.Add(new DragonBreed() { name = "Void Dragon", type = 12 });
        dragonBreeds.Add(new DragonBreed() { name = "Celestial Dragon", type = 13 });
        dragonBreeds.Add(new DragonBreed() { name = "Mythic Dragon", type = 14 });
        dragonBreeds.Add(new DragonBreed() { name = "Ancient Dragon", type = 15 });
    }
    
    public void HatchDragon(string dragonType)
    {
        if (GameManager.Instance.dragons.Count < GameManager.Instance.dragonHallCapacity)
        {
            Dragon newDragon = new Dragon()
            {
                dragonId = "dragon_" + System.DateTime.Now.Ticks,
                dragonType = dragonType,
                level = 1,
                health = 100
            };
            
            GameManager.Instance.HatchDragon(newDragon);
            Debug.Log($"🐉 {dragonType} Hatched!");
        }
        else
        {
            Debug.LogWarning("Dragon Hall is full!");
        }
    }
    
    public Dragon BreedDragons(Dragon dragon1, Dragon dragon2)
    {
        if (dragon1.level >= 5 && dragon2.level >= 5)
        {
            // Random new dragon type
            int randomType = Random.Range(1, 16);
            string newDragonType = dragonBreeds[randomType - 1].name;
            
            Dragon newDragon = new Dragon()
            {
                dragonId = "dragon_" + System.DateTime.Now.Ticks,
                dragonType = newDragonType,
                level = 1,
                health = 100,
                hatchTime = breedingTime
            };
            
            Debug.Log($"🐉 Breeding successful! New {newDragonType} coming!");
            return newDragon;
        }
        
        return null;
    }
    
    public void UpgradeDragon(Dragon dragon, int cost)
    {
        if (GameManager.Instance.darkElixir >= cost && dragon.level < 100)
        {
            GameManager.Instance.darkElixir -= cost;
            dragon.level++;
            dragon.health = 100 + (dragon.level * 5);
            Debug.Log($"🐉 {dragon.dragonType} upgraded to level {dragon.level}!");
        }
    }
}

[System.Serializable]
public class DragonBreed
{
    public string name;
    public int type;
    public int unlockTownHall = 1;
}
