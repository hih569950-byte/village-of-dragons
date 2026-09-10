using UnityEngine;
using System.Collections.Generic;

public class BattleSystem : MonoBehaviour
{
    public static BattleSystem Instance { get; private set; }
    
    [Header("Battle Settings")]
    public float battleDuration = 180f; // 3 minutes
    private float battleTimeRemaining;
    public bool isBattleActive = false;
    
    [Header("Battle Data")]
    public List<TroopData> deployedTroops = new List<TroopData>();
    public Building targetBuilding;
    public int damageDealt = 0;
    public int goldLooted = 0;
    public int elixirLooted = 0;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    
    public void StartBattle(List<TroopData> troops)
    {
        isBattleActive = true;
        battleTimeRemaining = battleDuration;
        deployedTroops = new List<TroopData>(troops);
        damageDealt = 0;
        goldLooted = 0;
        elixirLooted = 0;
        Debug.Log("⚔️ Battle Started!");
    }
    
    private void Update()
    {
        if (isBattleActive)
        {
            battleTimeRemaining -= Time.deltaTime;
            
            if (battleTimeRemaining <= 0)
            {
                EndBattle();
            }
            
            // Simulate troop attacks
            SimulateTroopAttacks();
        }
    }
    
    private void SimulateTroopAttacks()
    {
        foreach (var troop in deployedTroops)
        {
            if (targetBuilding != null)
            {
                int attackDamage = CalculateDamage(troop.name, troop.level);
                targetBuilding.health -= attackDamage;
                damageDealt += attackDamage;
                
                if (targetBuilding.health <= 0)
                {
                    OnBuildingDestroyed(targetBuilding);
                }
            }
        }
    }
    
    private int CalculateDamage(string troopName, int troopLevel)
    {
        int baseDamage = 10;
        
        switch (troopName)
        {
            case "Barbarian": baseDamage = 5; break;
            case "Archer": baseDamage = 4; break;
            case "Dragon": baseDamage = 15; break;
            case "Wizard": baseDamage = 12; break;
            case "Giant": baseDamage = 20; break;
        }
        
        return baseDamage * troopLevel;
    }
    
    private void OnBuildingDestroyed(Building building)
    {
        if (building.buildingName == "Gold Storage")
            goldLooted += Random.Range(100, 500) * building.level;
        else if (building.buildingName == "Elixir Storage")
            elixirLooted += Random.Range(100, 500) * building.level;
        
        targetBuilding = null;
    }
    
    public void EndBattle()
    {
        isBattleActive = false;
        
        // Calculate victory
        bool victory = damageDealt > 1000;
        int trophiesGained = victory ? 10 : -5;
        
        GameManager.Instance.trophies += trophiesGained;
        GameManager.Instance.AddGold(goldLooted);
        GameManager.Instance.AddElixir(elixirLooted);
        
        Debug.Log($"⚔️ Battle Ended! Damage: {damageDealt}, Gold: {goldLooted}, Elixir: {elixirLooted}, Result: {(victory ? "Victory" : "Defeat")}");
    }
}
