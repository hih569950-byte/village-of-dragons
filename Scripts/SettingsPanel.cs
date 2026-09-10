using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [Header("Settings UI")]
    public TextMeshProUGUI saveStatusText;
    public TextMeshProUGUI lastSaveTimeText;
    public TextMeshProUGUI saveSizeText;
    public Button manualSaveButton;
    public Button resetGameButton;
    public Button deleteDataButton;
    public Toggle autoSaveToggle;
    
    [Header("Info")]
    public TextMeshProUGUI gameVersionText;
    public TextMeshProUGUI savePathText;
    
    private void Start()
    {
        SetupButtonListeners();
        UpdateSettingsDisplay();
    }
    
    private void SetupButtonListeners()
    {
        manualSaveButton.onClick.AddListener(() => ManualSave());
        resetGameButton.onClick.AddListener(() => ResetGame());
        deleteDataButton.onClick.AddListener(() => DeleteAllData());
        autoSaveToggle.onValueChanged.AddListener((value) => ToggleAutoSave(value));
    }
    
    private void Update()
    {
        UpdateSettingsDisplay();
    }
    
    private void UpdateSettingsDisplay()
    {
        // Update last save time
        lastSaveTimeText.text = $"Last Save: {PersistentSaveSystem.Instance.GetLastSaveTime()}";
        
        // Update save data size
        long sizeInBytes = PersistentSaveSystem.Instance.GetSaveDataSize();
        string sizeInKB = (sizeInBytes / 1024f).ToString("F2");
        saveSizeText.text = $"Save Size: {sizeInKB} KB";
        
        // Update save status
        if (PersistentSaveSystem.Instance.HasSaveData())
        {
            saveStatusText.text = "✅ Save Data Found";
            saveStatusText.color = Color.green;
        }
        else
        {
            saveStatusText.text = "⚠️ No Save Data";
            saveStatusText.color = Color.yellow;
        }
        
        // Game version
        gameVersionText.text = $"Version: 1.0.0";
        
        // Save path
        savePathText.text = $"Path: {Application.persistentDataPath}";
    }
    
    private void ManualSave()
    {
        PersistentSaveSystem.Instance.SaveAllGameData();
        saveStatusText.text = "💾 Game Saved!";
        saveStatusText.color = Color.green;
        Debug.Log("💾 Manual save completed!");
    }
    
    private void ResetGame()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MainScene")
        {
            // Confirm dialog
            if (ShowConfirmDialog("Reset game?"))
            {
                PersistentSaveSystem.Instance.DeleteAllSaveData();
                UnityEngine.SceneManagement.SceneManager.LoadScene(
                    UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
                );
            }
        }
    }
    
    private void DeleteAllData()
    {
        if (ShowConfirmDialog("Delete all save data? This cannot be undone!"))
        {
            PersistentSaveSystem.Instance.DeleteAllSaveData();
            saveStatusText.text = "🗑️ Data Deleted!";
            saveStatusText.color = Color.red;
        }
    }
    
    private void ToggleAutoSave(bool isEnabled)
    {
        // Auto save toggle functionality
        Debug.Log($"Auto Save: {(isEnabled ? "Enabled" : "Disabled")}");
    }
    
    private bool ShowConfirmDialog(string message)
    {
        // Simple confirmation - can be replaced with proper UI dialog
        return UnityEngine.EditorUtility.DisplayDialog("Confirm", message, "Yes", "No");
    }
}
