using UnityEngine;
using System.Collections.Generic;

public class VillageManager : MonoBehaviour
{
    public static VillageManager Instance { get; private set; }
    
    [Header("Village Settings")]
    public int gridWidth = 20;
    public int gridHeight = 20;
    public float tileSize = 1f;
    
    [Header("Building Placement")]
    public List<BuildingPlacement> placedBuildings = new List<BuildingPlacement>();
    private bool[,] occupiedTiles;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        occupiedTiles = new bool[gridWidth, gridHeight];
    }
    
    public bool CanPlaceBuilding(int x, int y, int width, int height)
    {
        if (x + width > gridWidth || y + height > gridHeight)
            return false;
        
        for (int i = x; i < x + width; i++)
        {
            for (int j = y; j < y + height; j++)
            {
                if (occupiedTiles[i, j])
                    return false;
            }
        }
        return true;
    }
    
    public void PlaceBuilding(Building building, int x, int y, int width, int height)
    {
        if (CanPlaceBuilding(x, y, width, height))
        {
            for (int i = x; i < x + width; i++)
            {
                for (int j = y; j < y + height; j++)
                {
                    occupiedTiles[i, j] = true;
                }
            }
            
            placedBuildings.Add(new BuildingPlacement()
            {
                building = building,
                x = x,
                y = y,
                width = width,
                height = height
            });
        }
    }
    
    public void RemoveBuilding(Building building)
    {
        var placement = placedBuildings.Find(p => p.building == building);
        if (placement != null)
        {
            for (int i = placement.x; i < placement.x + placement.width; i++)
            {
                for (int j = placement.y; j < placement.y + placement.height; j++)
                {
                    occupiedTiles[i, j] = false;
                }
            }
            placedBuildings.Remove(placement);
        }
    }
    
    public Vector3 GridToWorldPosition(int x, int y)
    {
        return new Vector3(x * tileSize, 0, y * tileSize);
    }
}

[System.Serializable]
public class BuildingPlacement
{
    public Building building;
    public int x;
    public int y;
    public int width = 1;
    public int height = 1;
}
