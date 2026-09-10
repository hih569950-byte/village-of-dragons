# 🔐 Developer Mode Guide - Village of Dragons

## Accessing Developer Mode

### Password
```
jaripraj
```

### How to Unlock

1. Launch **Village of Dragons**
2. Go to **Settings** → **Developer Mode**
3. Enter Password: `jaripraj`
4. Click **Submit**
5. Status: ✅ **Developer Mode ACTIVATED!**

---

## Features Unlocked

### 💰 Unlimited Resources

**Before:** Limited by storage capacity
**After:** Unlimited gold, elixir, dark elixir, gems

```
Gold: 999,999
Elixir: 999,999
Dark Elixir: 999,999
Gems: 999,999
```

---

### 🏗️ Max All Buildings

All 50+ buildings instantly upgraded to **Level 100**:
- Town Halls (1-100)
- Resource Buildings
- Army Buildings
- Defense Buildings
- Support Buildings

**Effect:** No upgrade costs, instant completion

---

### ⚔️ Max All Troops

All 20+ troop types instantly **Level 100**:
- Barbarian
- Archer
- Dragon
- Wizard
- P.E.K.K.A
- *And 15 more...*

**Effect:** Max damage, max health

---

### 🐉 Unlock All Dragons

All 15 dragon types instantly available:
1. Fire Dragon
2. Ice Dragon
3. Lightning Dragon
4. Earth Dragon
5. Wind Dragon
6. Shadow Dragon
7. Light Dragon
8. Metal Dragon
9. Poison Dragon
10. Lava Dragon
11. Crystal Dragon
12. Void Dragon
13. Celestial Dragon
14. Mythic Dragon
15. Ancient Dragon

**Each:** Level 100, Max Health

---

### 👑 Max All Heroes

All 8 heroes instantly **Level 100**:
- Barbarian King
- Archer Queen
- Grand Warden
- Royal Champion
- Shadow Champion
- Light Guardian
- Dragon Master
- Void Keeper

**Effect:** Max abilities, full health, no cooldown

---

### 🏰 Town Hall 100

Instantly upgrade to **Town Hall 100**:
- All 100 levels unlocked
- All features available
- Max resource capacity
- Max army capacity
- Ultimate village design

---

### ⏱️ No Timers

**Timers Disabled:**
- Building upgrades: Instant ✅
- Troop training: Instant ✅
- Research: Instant ✅
- Hero healing: Instant ✅
- Dragon breeding: Instant ✅

---

### 🛡️ God Mode

**Village Invincible:**
- All buildings HP: 9,999
- Defense buildings: Unlimited damage
- No resource loss on attack
- Win every battle

---

### 📊 Unlimited Everything

```
✅ Unlimited Gold
✅ Unlimited Elixir
✅ Unlimited Dark Elixir
✅ Unlimited Gems
✅ Unlimited Troops
✅ Unlimited Dragons
✅ Unlimited Research
✅ Unlimited Army Capacity
✅ Unlimited Storage
```

---

## Developer Mode Buttons

### 1️⃣ Unlimited Resources Button

**Effect:**
```
Gold: 999,999
Elixir: 999,999
Dark Elixir: 999,999
Gems: 999,999
```

### 2️⃣ Max All Buildings Button

**Effect:**
- All buildings → Level 100
- All health → 9,999
- No upgrade costs

### 3️⃣ Max All Troops Button

**Effect:**
- All troops → Level 100
- Max damage
- Training instant

### 4️⃣ Max All Dragons Button

**Effect:**
- All dragons → Level 100
- All abilities available
- Breeding instant

### 5️⃣ Max All Heroes Button

**Effect:**
- All heroes → Level 100
- Max abilities
- No healing needed

### 6️⃣ Town Hall 100 Button

**Effect:**
- Town Hall → Level 100
- All content unlocked
- Ultimate status

### 7️⃣ God Mode Button

**Effect:**
- Village invincible
- Unlimited defense
- Win all battles

### 8️⃣ Reset Game Button

**Effect:**
- Deletes all save data
- Restarts fresh
- ⚠️ No undo!

---

## Testing Features

### Test Building Placement
1. Unlock unlimited resources
2. Place buildings freely
3. Upgrade instantly
4. Test village design

### Test Battle System
1. Max all troops
2. Start attack
3. Deploy unlimited army
4. Test combat mechanics
5. Verify damage calculation

### Test Dragons
1. Unlock all dragons
2. Max all dragon levels
3. Test dragon abilities
4. Test dragon breeding

### Test Progression
1. Jump to Town Hall 100
2. Verify all buildings unlocked
3. Check all research available
4. Confirm all troops accessible

### Test Multiplayer
1. Max your village
2. Connect via Bluetooth/WiFi
3. Battle other players
4. Test trophy system

---

## Debug Console

### Quick Commands

```csharp
// Add resources
GameManager.Instance.AddGold(10000);
GameManager.Instance.AddElixir(10000);

// Upgrade buildings
GameManager.Instance.UpgradeBuilding(building, 0);

// Upgrade Town Hall
GameManager.Instance.UpgradeTownHall();

// Add dragons
DragonSystem.Instance.HatchDragon("Fire Dragon");

// Start battle
BattleSystem.Instance.StartBattle(troops);

// Save game
GameManager.Instance.SaveGame();

// Load game
GameManager.Instance.LoadGame();
```

---

## Cheat Combinations

### Ultimate Power Setup
1. Click: **Unlimited Resources**
2. Click: **Town Hall 100**
3. Click: **Max All Buildings**
4. Click: **Max All Troops**
5. Click: **Max All Dragons**
6. Click: **Max All Heroes**
7. Click: **God Mode**

**Result:** Completely maxed village ready for testing!

---

## Disable Developer Mode

### To Hide Developer Mode:

1. Open `DeveloperMode.cs`
2. Find: `developerModeButton`
3. Change: `.gameObject.SetActive(false);`
4. Save and rebuild

---

## Security Notes

⚠️ **Important for Release:**

1. **Remove password** before publishing to Play Store
2. **Disable all cheat buttons** in production
3. **Hide developer mode option** from players
4. **Keep password confidential** for internal testing only

---

## Testing Checklist

- ✅ Developer mode unlocks with correct password
- ✅ Unlimited resources work
- ✅ Building max works
- ✅ Troops max works
- ✅ Dragons unlock works
- ✅ Heroes max works
- ✅ Town Hall 100 works
- ✅ God mode works
- ✅ Timers disabled
- ✅ Game saves properly
- ✅ Game loads properly
- ✅ All features accessible

---

**Password: `jaripraj`** 🔐

**Enjoy testing!** 🐉👑✨
