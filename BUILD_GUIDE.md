# 🚀 BUILD GUIDE - Village of Dragons APK

## Prerequisites

### Required Software:
1. **Unity 2022 LTS or newer**
   - Download: https://unity.com/download
   - Install with Android Build Support

2. **Android SDK**
   - Included with Android Studio
   - Download: https://developer.android.com/studio

3. **Java Development Kit (JDK)**
   - JDK 11 or newer
   - Download: https://www.oracle.com/java/technologies/javase/jdk11-archive.html

---

## Step 1: Clone Repository

```bash
git clone https://github.com/hih569950-byte/village-of-dragons.git
cd village-of-dragons
```

---

## Step 2: Open Project in Unity

1. Open Unity Hub
2. Click "Open Project"
3. Select the `village-of-dragons` folder
4. Wait for project to load (first time takes 2-3 minutes)

---

## Step 3: Configure Android Settings

### In Unity Editor:

1. **File** → **Build Settings**
2. **Platform** → Select **Android**
3. Click **Switch Platform**
4. Wait for compilation

### Configure Player Settings:

1. **Edit** → **Project Settings** → **Player**
2. Go to **Android** tab
3. Set:
   - **Company Name:** Village Games
   - **Product Name:** Village of Dragons
   - **Package Name:** `com.villagegames.dragonsvillage`
   - **Target Android Version:** Android 10 (API 29) or higher
   - **Minimum Android Version:** Android 8.0 (API 26)

### Graphics Settings:

1. **Edit** → **Project Settings** → **Quality**
2. Set quality level to **Medium** for mobile devices
3. Reduce shadow distance for performance

---

## Step 4: Build APK

### Method 1: Development Build (Testing)

1. In **Build Settings**:
   - Check: **Development Build**
   - Check: **Script Debugging**
2. Click **Build**
3. Choose location (e.g., Desktop)
4. Name: `VillageOfDragons-dev.apk`
5. Wait for build completion (5-10 minutes)

### Method 2: Release Build (Final APK)

1. In **Build Settings**:
   - Uncheck: **Development Build**
   - Uncheck: **Script Debugging**
2. Click **Build and Run** (if device connected) or **Build**
3. Name: `VillageOfDragons.apk`
4. Wait for build completion (10-15 minutes)

---

## Step 5: Install on Android Phone

### Option A: Direct Connection

1. Connect Android phone via USB cable
2. Enable Developer Mode on phone:
   - Settings → About Phone
   - Tap Build Number 7 times
   - Go back → Developer Options
   - Enable "USB Debugging"
3. In Unity: **File** → **Build Settings** → **Build and Run**
4. App installs automatically

### Option B: Manual Installation

1. Transfer APK to phone
2. On phone: Settings → Security → Enable "Unknown Sources"
3. Open File Manager
4. Locate APK file
5. Tap to install
6. Launch from App Drawer

### Option C: Using ADB (Android Debug Bridge)

```bash
adb install VillageOfDragons.apk
```

---

## Step 6: Test Developer Mode

1. Launch app
2. Go to **Settings**
3. Find **Developer Mode** option
4. Enter Password: `jaripraj`
5. Unlock unlimited resources:
   - Unlimited Gold ✅
   - Unlimited Elixir ✅
   - Unlimited Dark Elixir ✅
   - Unlimited Gems ✅
   - All Buildings Level 100 ✅
   - All Troops Level 100 ✅
   - All Dragons Unlocked ✅
   - All Heroes Level 100 ✅
   - Town Hall 100 ✅
   - God Mode ✅

---

## Troubleshooting

### Build Errors

**Error: "Android SDK not found"**
- Solution: Install Android Studio
- In Unity: Edit → Preferences → External Tools → Android SDK Path

**Error: "Gradle build failed"**
- Solution: Delete `Temp` and `Library` folders in project
- Reimport project

**Error: "Target SDK not found"**
- Solution: Install required SDK version in Android Studio

### Performance Issues

- Reduce graphics quality
- Lower resolution
- Disable shadows
- Close background apps

### APK Size Large

- Use **IL2CPP** instead of Mono
- Enable **Managed Code Stripping**
- Remove unused assets

---

## File Location After Build

```
VillageOfDragons/
├── VillageOfDragons.apk (Final APK)
├── VillageOfDragons-dev.apk (Development APK)
└── VillageOfDragons.aab (Android App Bundle)
```

---

## APK Distribution

### Testing
- Share APK directly with friends via:
  - WhatsApp
  - Telegram
  - Email
  - Google Drive
  - GitHub Releases

### Play Store
- Create Google Play Developer Account
- Upload APK/AAB
- Add screenshots and description
- Submit for review

---

## Performance Optimization

### For Weaker Devices:
```csharp
QualitySettings.SetQualityLevel(0); // Lowest quality
Application.targetFrameRate = 30; // 30 FPS instead of 60
```

### For Stronger Devices:
```csharp
QualitySettings.SetQualityLevel(3); // High quality
Application.targetFrameRate = 60; // Full 60 FPS
```

---

## Next Steps

1. ✅ Build and test APK
2. ✅ Test all features
3. ✅ Test Developer Mode
4. ✅ Test multiplayer (Bluetooth/WiFi)
5. ✅ Check save/load system
6. ✅ Optimize performance
7. ✅ Share with friends
8. ✅ Publish to Play Store (optional)

---

**Happy Building! 🐉👑**
