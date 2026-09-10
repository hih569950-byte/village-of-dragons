using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class AdvancedDragonSystem : MonoBehaviour
{
    public static AdvancedDragonSystem Instance { get; private set; }
    
    [Header("Dragon Management")]
    public List<Dragon> myDragons = new List<Dragon>();
    public List<DragonEgg> dragonEggs = new List<DragonEgg>();
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    
    public void AddDragonToArmy(Dragon dragon)
    {
        myDragons.Add(dragon);
        dragon.isInArmy = true;
        Debug.Log($"🐉 {dragon.dragonType} added to army! (Level {dragon.level})");
    }
    
    public void UpgradeDragon(Dragon dragon, int cost)
    {
        if (GameManager.Instance.darkElixir >= cost && dragon.level < 100)
        {
            GameManager.Instance.darkElixir -= cost;
            dragon.level++;
            dragon.health = 100 + (dragon.level * 10);
            dragon.attackPower = 10 + (dragon.level * 2);
            Debug.Log($"🐉 {dragon.dragonType} upgraded to Level {dragon.level}!");
        }
    }
    
    public DragonEgg BreedDragons(Dragon dragonA, Dragon dragonB)
    {
        if (dragonA.level < 5 || dragonB.level < 5)
        {
            Debug.LogWarning("Dragons must be level 5+ to breed!");
            return null;
        }
        
        string[] allDragons = new string[] 
        { 
            "Fire Dragon", "Ice Dragon", "Lightning Dragon", "Earth Dragon", "Wind Dragon",
            "Shadow Dragon", "Light Dragon", "Metal Dragon", "Poison Dragon", "Lava Dragon",
            "Crystal Dragon", "Void Dragon", "Celestial Dragon", "Mythic Dragon", "Ancient Dragon"
        };
        
        string offspring = allDragons[Random.Range(0, allDragons.Length)];
        
        DragonEgg egg = new DragonEgg()
        {
            eggId = "egg_" + System.DateTime.Now.Ticks,
            dragonType = offspring,
            parentA = dragonA.dragonType,
            parentB = dragonB.dragonType,
            hatchTimeRemaining = 14400f // 4 hours
        };
        
        dragonEggs.Add(egg);
        Debug.Log($"🥚 Breeding Started! {dragonA.dragonType} + {dragonB.dragonType} = {offspring}");
        return egg;
    }
    
    public List<Dragon> SelectBestDragonsForBattle(int count)
    {
        return myDragons
            .OrderByDescending(d => d.level * d.attackPower)
            .Take(count)
            .ToList();
    }
    
    public List<Dragon> GetArmyDragons()
    {
        return myDragons.Where(d => d.isInArmy).ToList();
    }
    
    public float GetArmyPower()
    {
        float totalPower = 0;
        foreach (var dragon in myDragons)
        {
            totalPower += dragon.level * dragon.attackPower;
        }
        return totalPower;
    }
}

[System.Serializable]
public class DragonEgg
{
    public string eggId;
    public string dragonType;
    public string parentA;
    public string parentB;
    public float hatchTimeRemaining;
}
