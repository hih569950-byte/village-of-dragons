using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    
    [Header("Resource Display")]
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI elixirText;
    public TextMeshProUGUI darkElixirText;
    public TextMeshProUGUI gemText;
    public TextMeshProUGUI trophiesText;
    
    [Header("Player Info")]
    public TextMeshProUGUI playerLevelText;
    public TextMeshProUGUI townHallLevelText;
    public TextMeshProUGUI xpText;
    
    [Header("Buttons")]
    public Button buildButton;
    public Button attackButton;
    public Button armyButton;
    public Button shopButton;
    public Button settingsButton;
    public Button developerModeButton;
    
    [Header("Panels")]
    public GameObject buildPanel;
    public GameObject armyPanel;
    public GameObject settingsPanel;
    public GameObject developerPanel;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    
    private void Start()
    {
        SetupButtonListeners();
    }
    
    private void SetupButtonListeners()
    {
        buildButton.onClick.AddListener(() => TogglePanel(buildPanel));
        attackButton.onClick.AddListener(() => StartBattle());
        armyButton.onClick.AddListener(() => TogglePanel(armyPanel));
        shopButton.onClick.AddListener(() => OpenShop());
        settingsButton.onClick.AddListener(() => TogglePanel(settingsPanel));
        developerModeButton.onClick.AddListener(() => TogglePanel(developerPanel));
    }
    
    private void Update()
    {
        UpdateResourceDisplay();
        UpdatePlayerInfo();
    }
    
    private void UpdateResourceDisplay()
    {
        GameManager gm = GameManager.Instance;
        
        goldText.text = $"Gold: {gm.gold}";
        elixirText.text = $"Elixir: {gm.elixir}";
        darkElixirText.text = $"Dark Elixir: {gm.darkElixir}";
        gemText.text = $"Gems: {gm.gems}";
        trophiesText.text = $"Trophies: {gm.trophies}";
    }
    
    private void UpdatePlayerInfo()
    {
        GameManager gm = GameManager.Instance;
        
        playerLevelText.text = $"Level: {gm.playerLevel}";
        townHallLevelText.text = $"TH: {gm.townHallLevel}";
        xpText.text = $"XP: {gm.xp}";
    }
    
    public void TogglePanel(GameObject panel)
    {
        if (panel != null)
        {
            panel.SetActive(!panel.activeSelf);
        }
    }
    
    private void StartBattle()
    {
        Debug.Log("⚔️ Battle mode activated!");
        // Load battle scene
    }
    
    private void OpenShop()
    {
        Debug.Log("🛒 Shop opened!");
    }
}
