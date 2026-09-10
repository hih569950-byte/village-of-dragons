# 🌐 MULTIPLAYER API REFERENCE - Village of Dragons

## Overview

**Village of Dragons** supports local multiplayer battles via:
- 🔵 **Bluetooth** (Wireless)
- 📡 **WiFi/LAN** (Local Network)
- 🏠 **Hotspot** (Mobile Hotspot)

---

## Connection Types

### 🔵 Bluetooth Connection

**Speed:** Slow but reliable
**Range:** 10-100 meters
**Best For:** 1v1 battles

```csharp
MultiplayerManager.Instance.ConnectViaBluetooth();
```

### 📡 WiFi Connection

**Speed:** Fast
**Range:** Entire building
**Best For:** Multiple players

```csharp
MultiplayerManager.Instance.ConnectViaWiFi("192.168.1.100");
```

### 🏠 Hotspot Connection

**Speed:** Medium
**Range:** 10-50 meters
**Best For:** Outdoor gaming

```csharp
MultiplayerManager.Instance.ConnectViaLAN();
```

---

## Connection Flow

### Step 1: Enable Connection

```csharp
public class MultiplayerManager : MonoBehaviour
{
    public void InitializeConnection()
    {
        // Check connection type
        ConnectionType type = GetConnectionType();
        
        switch(type)
        {
            case ConnectionType.Bluetooth:
                ConnectViaBluetooth();
                break;
            case ConnectionType.WiFi:
                ConnectViaWiFi(GetLocalIP());
                break;
            case ConnectionType.LAN:
                ConnectViaLAN();
                break;
        }
    }
}
```

### Step 2: Find Players

```csharp
public void FindNearbyPlayers()
{
    List<PlayerData> nearbyPlayers = new List<PlayerData>();
    
    // Scan for available players
    // Send broadcast signal
    // Collect responses
    
    Debug.Log($"Found {nearbyPlayers.Count} players nearby");
}
```

### Step 3: Send Battle Invite

```csharp
public void SendBattleInvite(PlayerData opponent)
{
    BattleInvite invite = new BattleInvite()
    {
        senderName = GameManager.Instance.playerName,
        senderTownHallLevel = GameManager.Instance.townHallLevel,
        senderTrophies = GameManager.Instance.trophies,
        timestamp = System.DateTime.Now
    };
    
    // Send via Bluetooth/WiFi
    SendPacket(opponent, invite);
}
```

### Step 4: Accept Invite & Start Battle

```csharp
public void AcceptBattleInvite(BattleInvite invite)
{
    // Send acceptance
    SendAcceptance(invite.senderName);
    
    // Load opponent village
    LoadOpponentVillage(invite.opponentData);
    
    // Start battle
    BattleSystem.Instance.StartMultiplayerBattle(invite.opponentData);
}
```

---

## Data Structures

### PlayerData

```csharp
[System.Serializable]
public class PlayerData
{
    public string playerName;           // Player's name
    public int townHallLevel;           // TH 1-100
    public int trophies;                // Total trophies
    public int defenseWins;             // Defense victories
    public int attackWins;              // Attack victories
    public List<Building> buildings;    // Village buildings
    public List<TroopData> troops;      // Available troops
    public List<Dragon> dragons;        // Available dragons
    public int gold;                    // Current gold
    public int elixir;                  // Current elixir
}
```

### BattleInvite

```csharp
[System.Serializable]
public class BattleInvite
{
    public string inviteId;             // Unique invite ID
    public string senderName;           // Who sent invite
    public int senderTownHallLevel;     // Sender's TH level
    public int senderTrophies;          // Sender's trophies
    public PlayerData opponentData;     // Full opponent data
    public System.DateTime timestamp;   // When sent
    public bool isAccepted;             // Acceptance status
}
```

### BattleResult

```csharp
[System.Serializable]
public class BattleResult
{
    public string battleId;             // Unique battle ID
    public string winner;               // Winner's name
    public string loser;                // Loser's name
    public int goldStolen;              // Gold looted
    public int elixirStolen;            // Elixir looted
    public int trophiesGained;          // Trophies earned
    public int starCount;               // 1-3 stars
    public float destroyPercentage;     // Village % destroyed
    public System.DateTime timestamp;   // When battle occurred
}
```

---

## Bluetooth Communication

### Packet Format

```csharp
[System.Serializable]
public class BluetoothPacket
{
    public string type;                 // "INVITE", "ACCEPT", "DATA", "RESULT"
    public string senderId;             // Sender's device ID
    public string receiverId;           // Receiver's device ID
    public string payload;              // JSON data
    public long timestamp;              // Unix timestamp
    public string checksum;             // Data integrity
}
```

### Send Packet

```csharp
public void SendBluetoothPacket(BluetoothPacket packet)
{
    string json = JsonUtility.ToJson(packet);
    byte[] data = System.Text.Encoding.UTF8.GetBytes(json);
    
    // Send via Bluetooth stream
    bluetoothStream.Write(data, 0, data.Length);
    bluetoothStream.Flush();
}
```

### Receive Packet

```csharp
public void ReceiveBluetoothPacket()
{
    byte[] buffer = new byte[1024];
    int bytesRead = bluetoothStream.Read(buffer, 0, buffer.Length);
    
    string json = System.Text.Encoding.UTF8.GetString(buffer, 0, bytesRead);
    BluetoothPacket packet = JsonUtility.FromJson<BluetoothPacket>(json);
    
    HandlePacket(packet);
}
```

---

## WiFi Communication

### Network Discovery

```csharp
public List<string> DiscoverPlayersOnWiFi()
{
    List<string> players = new List<string>();
    string baseIP = GetNetworkBaseIP();  // e.g., "192.168.1"
    
    for (int i = 1; i < 255; i++)
    {
        string ip = $"{baseIP}.{i}";
        
        if (PingHost(ip, 500))  // 500ms timeout
        {
            // Check if it's a Village of Dragons player
            if (CheckIfGamePlayer(ip))
            {
                players.Add(ip);
            }
        }
    }
    
    return players;
}
```

### UDP Socket Communication

```csharp
using System.Net;
using System.Net.Sockets;

public void SendDataViaUDP(string targetIP, string data)
{
    UdpClient client = new UdpClient();
    byte[] sendBytes = System.Text.Encoding.ASCII.GetBytes(data);
    
    client.Send(sendBytes, sendBytes.Length, targetIP, 5000);
    client.Close();
}

public void ReceiveDataViaUDP()
{
    UdpClient server = new UdpClient(5000);
    IPEndPoint remoteIP = new IPEndPoint(IPAddress.Any, 0);
    
    while (true)
    {
        byte[] data = server.Receive(ref remoteIP);
        string message = System.Text.Encoding.ASCII.GetString(data);
        
        Debug.Log($"Received from {remoteIP}: {message}");
    }
}
```

---

## Battle Synchronization

### Send Troop Deployment

```csharp
public void SendTroopDeployment(Troop troop, Vector3 position)
{
    TroopDeploymentPacket packet = new TroopDeploymentPacket()
    {
        troopType = troop.name,
        troopLevel = troop.level,
        position = position,
        timestamp = System.DateTime.Now.Ticks
    };
    
    SendPacket(packet);
}
```

### Sync Building Damage

```csharp
public void SyncBuildingDamage(Building building, int damageDealt)
{
    BuildingDamagePacket packet = new BuildingDamagePacket()
    {
        buildingId = building.buildingId,
        newHealth = building.health - damageDealt,
        timestamp = System.DateTime.Now.Ticks
    };
    
    SendPacket(packet);
}
```

### Battle End Sync

```csharp
public void SyncBattleEnd(BattleResult result)
{
    BattleEndPacket packet = new BattleEndPacket()
    {
        winner = result.winner,
        goldStolen = result.goldStolen,
        elixirStolen = result.elixirStolen,
        trophiesGained = result.trophiesGained,
        destroyPercentage = result.destroyPercentage
    };
    
    SendPacket(packet);
    
    // Save battle result
    SaveBattleResult(result);
}
```

---

## Leaderboard System

### Local Leaderboard

```csharp
public class LocalLeaderboard
{
    public List<LeaderboardEntry> entries = new List<LeaderboardEntry>();
    
    public void AddBattleResult(BattleResult result)
    {
        // Find or create player entry
        var entry = entries.Find(e => e.playerName == result.winner);
        
        if (entry == null)
        {
            entry = new LeaderboardEntry()
            {
                playerName = result.winner,
                rank = entries.Count + 1
            };
            entries.Add(entry);
        }
        
        // Update stats
        entry.wins++;
        entry.trophies += result.trophiesGained;
        entry.goldStolen += result.goldStolen;
        
        // Sort by trophies
        entries.Sort((a, b) => b.trophies.CompareTo(a.trophies));
        
        // Update ranks
        for (int i = 0; i < entries.Count; i++)
        {
            entries[i].rank = i + 1;
        }
    }
}

[System.Serializable]
public class LeaderboardEntry
{
    public int rank;
    public string playerName;
    public int wins;
    public int losses;
    public int trophies;
    public int goldStolen;
    public int townHallLevel;
}
```

---

## Error Handling

### Connection Errors

```csharp
public enum ConnectionError
{
    NoBluetoothDevice,
    BluetoothDisabled,
    ConnectionTimeout,
    DataCorrupted,
    PacketLoss,
    Disconnected
}

public void HandleConnectionError(ConnectionError error)
{
    switch(error)
    {
        case ConnectionError.NoBluetoothDevice:
            Debug.LogError("No Bluetooth device found!");
            break;
        case ConnectionError.ConnectionTimeout:
            Debug.LogError("Connection timeout!");
            ReconnectToPlayer();
            break;
        case ConnectionError.Disconnected:
            Debug.LogError("Player disconnected!");
            EndBattle();
            break;
    }
}
```

### Retry Logic

```csharp
public int maxRetries = 3;
public float retryDelay = 2f;

public IEnumerator RetryConnection()
{
    for (int i = 0; i < maxRetries; i++)
    {
        yield return new WaitForSeconds(retryDelay);
        
        if (AttemptConnection())
        {
            Debug.Log("Reconnected!");
            yield break;
        }
    }
    
    Debug.LogError("Failed to reconnect!");
    DisconnectPlayer();
}
```

---

## Testing Multiplayer

### Test Locally (2 Emulators)

```bash
# Terminal 1: Run first emulator
adb emu avd name emulator-5554

# Terminal 2: Run second emulator
adb emu avd name emulator-5556

# Both run on same WiFi, can connect
```

### Test on Real Devices

1. **Both on same WiFi**
2. **Enable Bluetooth**
3. **Launch game on both**
4. **Find opponent**
5. **Send invite & battle**

---

## Performance Tips

### Network Optimization

- ✅ Compress data before sending
- ✅ Send only changes (delta compression)
- ✅ Batch multiple updates
- ✅ Use efficient serialization
- ✅ Minimize packet size

### Latency Reduction

```csharp
// Predict opponent moves
public Vector3 PredictTroopPosition(Troop troop, float latency)
{
    // Calculate where troop will be
    return troop.position + (troop.velocity * latency);
}
```

---

## Security Considerations

### Data Validation

```csharp
public bool ValidatePacket(BluetoothPacket packet)
{
    // Check checksum
    string calculatedChecksum = CalculateChecksum(packet.payload);
    if (packet.checksum != calculatedChecksum)
        return false;
    
    // Check timestamp (prevent replay attacks)
    long timeDiff = System.DateTime.Now.Ticks - packet.timestamp;
    if (timeDiff > 5000)  // 5 second window
        return false;
    
    return true;
}
```

### Anti-Cheat

```csharp
public bool ValidateBattleData(BattleResult result)
{
    // Verify attacker's troops existed
    if (!ValidateTroopDeployment(result))
        return false;
    
    // Verify damage calculation
    if (!ValidateDamageCalc(result))
        return false;
    
    // Verify loot amount
    if (!ValidateLootAmount(result))
        return false;
    
    return true;
}
```

---

## API Summary

```csharp
// Connection
MultiplayerManager.Instance.ConnectViaBluetooth();
MultiplayerManager.Instance.ConnectViaWiFi(ip);
MultiplayerManager.Instance.Disconnect();

// Player Discovery
MultiplayerManager.Instance.FindNearbyPlayers();

// Battle Management
MultiplayerManager.Instance.SendBattleInvite(player);
MultiplayerManager.Instance.AcceptBattleInvite(invite);
MultiplayerManager.Instance.StartMultiplayerBattle(opponent);

// Leaderboard
Leaderboard.AddBattleResult(result);
Leaderboard.GetTopPlayers(10);

// Utility
MultiplayerManager.Instance.IsConnected();
MultiplayerManager.Instance.GetConnectionType();
MultiplayerManager.Instance.GetLatency();
```

---

**Enjoy Multiplayer Battles!** 🔵⚔️🎮
