using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DeveloperMode : MonoBehaviour
{
    [Header("UI Elements")]
    public InputField passwordInput;
    public Button submitButton;
    public TextMeshProUGUI statusText;
    
    [Header("Cheat Options")]
    public Button unlimitedResourcesButton;
    public Button maxAllBuildingsButton;
    public Button maxAllTroopsButton;
    public Button maxAllDragonsButton;
    public Button maxAllHeroesButton;
    public Button townHall100Button;
    public Button godModeButton;
    public Button resetGameButton;
    
    private bool isDeveloperModeActive = false;
    private const string CORRECT_PASSWORD = "jaripraj";
    
    private void Start()
    {
        submitButton.onClick.AddListener(() => CheckPassword());
        
        // Only enable if developer mode is active
        unlimitedResourcesButton.onClick.AddListener(() => UnlimitedResources());
        maxAllBuildingsButton.onClick.AddListener(() => MaxAllBuildings());
        maxAllTroopsButton.onClick.AddListener(() => MaxAllTroops());
        maxAllDragonsButton.onClick.AddListener(() => MaxAllDragons());
        maxAllHeroesButton.onClick.AddListener(() => MaxAllHeroes());
        townHall100Button.onClick.AddListener(() => TownHall100());
        godModeButton.onClick.AddListener(() => GodMode());
        resetGameButton.onClick.AddListener(() => ResetGame());
    }
    
    private void CheckPassword()
    {
        if (passwordInput.text == CORRECT_PASSWORD)
        {
            isDeveloperModeActive = true;
            statusText.text = "✅ Developer Mode ACTIVATED!";
            statusText.color = Color.green;
            GameManager.Instance.ActivateDeveloperMode(CORRECT_PASSWORD);
        }
        else
        {
            statusText.text = "❌ Wrong Password!";
            statusText.color = Color.red;
        }
    }
    
    private void UnlimitedResources()
    {
        if (!isDeveloperModeActive) return;
        
        GameManager gm = GameManager.Instance;
        gm.gold = 999999;
        gm.elixir = 999999;
        gm.darkElixir = 999999;
        gm.gems = 999999;
        statusText.text = "💰 Unlimited Resources!";
    }
    
    private void MaxAllBuildings()
    {
        if (!isDeveloperModeActive) return;
        
        foreach (var building in GameManager.Instance.buildings)
        {
            building.level = 100;
        }
        statusText.text = "🏗️ All Buildings Maxed!";
    }
    
    private void MaxAllTroops()
    {
        if (!isDeveloperModeActive) return;
        
        foreach (var troop in GameManager.Instance.troops.Values)
        {
            troop.level = 100;
        }
        statusText.text = "⚔️ All Troops Maxed!";
    }
    
    private void MaxAllDragons()
    {
        if (!isDeveloperModeActive) return;
        
        foreach (var dragon in GameManager.Instance.dragons)
        {
            dragon.level = 100;
        }
        statusText.text = "🐉 All Dragons Maxed!";
    }
    
    private void MaxAllHeroes()
    {
        if (!isDeveloperModeActive) return;
        
        foreach (var hero in GameManager.Instance.heroes)
        {
            hero.level = 100;
        }
        statusText.text = "👑 All Heroes Maxed!";
    }
    
    private void TownHall100()
    {
        if (!isDeveloperModeActive) return;
        
        GameManager.Instance.townHallLevel = 100;
        statusText.text = "🏰 Town Hall 100!";
    }
    
    private void GodMode()
    {
        if (!isDeveloperModeActive) return;
        
        // Make all buildings invincible
        foreach (var building in GameManager.Instance.buildings)
        {
            building.health = 9999;
        }
        statusText.text = "🛡️ God Mode Activated!";
    }
    
    private void ResetGame()
    {
        if (!isDeveloperModeActive) return;
        
        SaveLoadManager.Instance.DeleteSaveData();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
