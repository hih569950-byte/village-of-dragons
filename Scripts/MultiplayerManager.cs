using UnityEngine;
using System.Collections.Generic;

public class MultiplayerManager : MonoBehaviour
{
    public static MultiplayerManager Instance { get; private set; }
    
    [Header("Connection Settings")]
    public bool isConnected = false;
    public string connectedPlayerName = "";
    public ConnectionType connectionType;
    
    public enum ConnectionType
    {
        Bluetooth,
        WiFi,
        LAN,
        None
    }
    
    [Header("Battle Data")]
    public PlayerData localPlayer;
    public PlayerData remotePlayer;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    public void ConnectViaBluetoothm()
    {
        connectionType = ConnectionType.Bluetooth;
        Debug.Log("📱 Connecting via Bluetooth...");
        isConnected = true;
    }
    
    public void ConnectViaWiFi(string ipAddress)
    {
        connectionType = ConnectionType.WiFi;
        Debug.Log($"📡 Connecting to {ipAddress} via WiFi...");
        isConnected = true;
    }
    
    public void ConnectViaLAN()
    {
        connectionType = ConnectionType.LAN;
        Debug.Log("🌐 Connecting via LAN...");
        isConnected = true;
    }
    
    public void Disconnect()
    {
        isConnected = false;
        connectedPlayerName = "";
        connectionType = ConnectionType.None;
        Debug.Log("❌ Disconnected!");
    }
    
    public void SendBattleInvite(string playerName)
    {
        if (isConnected)
        {
            Debug.Log($"⚔️ Battle invite sent to {playerName}");
        }
    }
    
    public void StartMultiplayerBattle(PlayerData opponent)
    {
        remotePlayer = opponent;
        Debug.Log($"⚔️ Starting battle with {opponent.playerName}!");
    }
}

[System.Serializable]
public class PlayerData
{
    public string playerName;
    public int townHallLevel;
    public int trophies;
    public int defenseWins;
    public int attackWins;
    public List<Building> buildings = new List<Building>();
    public List<TroopData> troops = new List<TroopData>();
    public int gold;
    public int elixir;
}
