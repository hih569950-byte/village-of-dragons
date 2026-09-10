# 💾 AUTO SAVE & PERSISTENCE SYSTEM

## Overview

**Village of Dragons** features a **complete auto-save system** that saves your game data locally on your phone.

---

## How It Works

### ✅ Auto Save (Every 5 Seconds)

```
Every action triggers auto-save:
✅ Building placed
✅ Resource collected
✅ Troop trained
✅ Dragon hatched
✅ Battle completed
✅ Hero upgraded
✅ Research completed
```

**Plus:** Automatic save every 5 seconds in background

### 📱 Phone Storage (Internal)

**Save Location:**
```
Android/data/com.villagegames.dragonsvillage/files/
```

**Files Saved:**
1. `village_data.json` - Town Hall, resources, buildings
2. `player_data.json` - Player level, XP, trophies
3. `buildings_data.json` - All building levels & positions
4. `troops_data.json` - All troop levels
5. `dragons_data.json` - All dragons & levels
6. `heroes_data.json` - All heroes & levels

**Total Size:** ~50-200 KB (very small!)

---

## Persistence Features

### 🔄 Game Closed & Reopened

**What Happens:**
1. Close game completely
2. App removed from memory
3. Reopen game
4. **Auto-loads last save** ✅
5. **Game resumes exactly where you left off** ✅

### 💾 What Gets Saved

```
✅ Town Hall level (1-100)
✅ All resources (Gold, Elixir, Dark Elixir, Gems)
✅ All buildings (levels, positions, health)
✅ All troops (types, levels, counts)
✅ All dragons (types, levels, breeding status)
✅ All heroes (levels, health, cooldowns)
✅ Player level & XP
✅ Trophies
✅ Timers & upgrade status
✅ Village layout
✅ All progression
```

### ⏱️ Timers Work Offline

**Example:**
1. Start building upgrade (1 hour timer)
2. Close game
3. Wait 30 minutes (game closed)
4. Reopen game
5. **Timer continues!** ✅ 30 minutes remaining

---

## Manual Save

### Force Save Anytime

**In Settings:**
1. Open **Settings** panel
2. Click **"Save Now"** button
3. Status: `💾 Game Saved!` ✅

**Or in code:**
```csharp
PersistentSaveSystem.Instance.SaveAllGameData();
```

---

## Delete Save Data

### Warning ⚠️

**Deleting save data:**
- ❌ Erases entire village
- ❌ Loses all progress
- ❌ Cannot be recovered
- ✅ Starts fresh new game

### How to Delete

**Method 1: In Settings**
1. Go to **Settings**
2. Click **"Delete All Data"**
3. Confirm deletion
4. Game resets

**Method 2: Uninstall App**
1. Long press app icon
2. Select **"Uninstall"**
3. Choose **"Delete app data"**
4. Fresh install = fresh game

**Method 3: Clear App Data**
1. Settings → Apps
2. Find "Village of Dragons"
3. Storage → Clear Data
4. Game resets on reopen

---

## Save Status

### Check Save Status

**In Settings Panel:**
```
✅ Save Status: "Save Data Found"
📅 Last Save: 2026-09-10 15:30:45
💾 Save Size: 0.15 KB
```

### Check Last Save Time

```csharp
string lastSave = PersistentSaveSystem.Instance.GetLastSaveTime();
Debug.Log($"Last saved: {lastSave}");
```

### Check Save Data Size

```csharp
long sizeBytes = PersistentSaveSystem.Instance.GetSaveDataSize();
Debug.Log($"Save size: {sizeBytes / 1024f} KB");
```

---

## Auto Save Settings

### Change Auto Save Interval

```csharp
// In PersistentSaveSystem.cs
public float autoSaveInterval = 5f; // Save every 5 seconds

// Change to:
public float autoSaveInterval = 10f; // Save every 10 seconds
```

### Disable Auto Save

```csharp
// In Update method:
autoSaveTimer = 0f; // Skip auto save
```

---

## Data Security

### No Ads ✅

**Village of Dragons:**
- ❌ No advertisements
- ❌ No banner ads
- ❌ No popup ads
- ❌ No video ads
- ✅ 100% ad-free experience

### Privacy ✅

**Your Data:**
- ✅ Stored locally on your phone ONLY
- ✅ Never sent to servers
- ✅ Never shared with anyone
- ✅ Completely private
- ✅ Offline gameplay

### Backup Options

**Optional Cloud Backup (Future):**
```csharp
// Export save
var jsonData = File.ReadAllText(savePath);
// Upload to cloud storage
```

---

## Troubleshooting

### Save Not Working?

1. Check storage space on phone
2. Check app permissions (Storage)
3. Try manual save from Settings
4. Restart game
5. Check device logs

### Save File Corrupted?

1. Delete save data (Settings → Delete All Data)
2. Game will auto-create new save
3. Start fresh

### Lost Progress?

1. Check if file exists: `village_data.json`
2. If deleted: permanently lost
3. Restart game for fresh game

### Storage Space Issues?

**Save Data Size:** ~200 KB max
**Not a problem** - very small file

If low on space:
1. Delete unused apps
2. Clear app cache
3. Move photos/videos to cloud

---

## Save Data Format

### Example Save File

```json
{
  "townHallLevel": 50,
  "gold": 500000,
  "elixir": 450000,
  "darkElixir": 25000,
  "gems": 1500,
  "maxGoldCapacity": 35000,
  "maxElixirCapacity": 35000,
  "availableBuilders": 4,
  "maxBuilders": 5,
  "armyCapacity": 120,
  "currentArmySize": 85,
  "dragonHallCapacity": 7,
  "lastSaveTime": "2026-09-10T15:30:45"
}
```

---

## Game Lifecycle

### Start Game
```
1. App launches
2. Check for save files
3. If exists → Load saved data ✅
4. If not → Start new game
5. Game ready to play
```

### During Gameplay
```
1. Every action saves automatically
2. Every 5 seconds saves full state
3. Timers persist even if app closed
4. Resources generate offline
```

### Close Game
```
1. Final save triggered
2. All data persisted to files
3. App closes
4. Data remains on phone
```

### Reopen Game
```
1. App launches
2. Load all save files
3. Resume exactly where left off
4. Continue playing
```

---

## Features Summary

✅ **Auto Save** - Every 5 seconds + every action
✅ **Local Storage** - Phone internal storage only
✅ **No Ads** - Completely ad-free
✅ **Offline** - Works without internet
✅ **Persistent** - Data remains until deleted
✅ **Safe** - Backup before uninstall
✅ **Fast** - Instant save/load
✅ **Small** - Only 200 KB
✅ **Private** - Local data only
✅ **Manual Save** - Force save anytime

---

**Your progress is always safe!** 💾✅
