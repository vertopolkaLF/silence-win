# 🎤 silence! v1.1 — Auto-Updates & Navigation Tabs

> **Now with automatic updates and a fresh new look.**

---

## ✨ What's New in v1.1

### 🔄 Auto-Update System

- **Automatic Update Checks** — App checks GitHub releases on startup and notifies you when a new version is available.

- **One-Click Updates** — See the update notification in the sidebar, click "View Details", download the installer, and you're done.

- **Smart Architecture Detection** — Automatically finds the right installer for your system (x64, x86, or ARM64).

- **Toggle Auto-Check** — Don't want automatic checks? Disable it in the About page. Manual check button always available.

### 🗂️ Navigation Tabs

- **Tabbed Settings Interface** — Clean navigation between General, Appearance, and About pages.

- **Smooth Transitions** — Slide animations when switching between tabs.

- **Compact Sidebar** — Collapsible navigation with icons. Update notification adapts to collapsed state.

### 🎨 UI Improvements

- **Update Notification Badge** — Subtle indicator in the sidebar when updates are available.

- **Version Display** — Current version shown in sidebar footer and About page.

- **Improved About Page** — Now includes update status, check button, and release details.

---

## 📦 Downloads

| Architecture | Portable | Installer |
|-------------|----------|-----------|
| x64 (64-bit) | `silence-v1.1-win-x64.zip` | `silence-v1.1-x64-setup.exe` |
| x86 (32-bit) | `silence-v1.1-win-x86.zip` | `silence-v1.1-x86-setup.exe` |
| ARM64 | `silence-v1.1-win-arm64.zip` | `silence-v1.1-arm64-setup.exe` |

---

## 🔧 Technical Changes

- Centralized version management in `.csproj`
- Dynamic version detection in build scripts
- GitHub Releases API integration for update checks

---

<p align="center">
  <b>Updates? We got 'em. Automatically.</b>
</p>

---
---

# 🎤 silence! v1.0 — Initial Release

> **Your meetings just got less awkward.**

We're thrilled to announce the first official release of **silence!** — a lightweight, no-bullshit microphone mute utility for Windows.

---

## ✨ What's New (Everything, it's v1.0!)

### 🎯 Core Features

- **Global Hotkey Muting** — Mute/unmute your microphone from absolutely anywhere. Gaming? Browsing? In Excel pretending to work? Doesn't matter, hotkey works everywhere.

- **System Tray Integration** — Lives quietly in your system tray. Green icon = you're live. Red icon = you're safe. No rocket science required.

- **One-Click Toggle** — Click the tray icon to toggle mute. Double-click opens settings. Your grandma could use this.

### ⌨️ Hotkey System

- **Full Modifier Support** — Create complex hotkeys like `Ctrl + Alt + M` or keep it simple with `F13`, `Pause`, whatever floats your boat.

- **Flexible Modifier Matching** — Enable "Ignore extra modifiers" so your `Shift + F23` hotkey also fires when you accidentally hit `Ctrl + Shift + F23`. We got you.

### 🎨 Modern UI

- **Mica/Acrylic Backdrop** — Windows 11 gets Mica, Windows 10 gets Acrylic. Everyone wins.

- **Smooth Animations** — Buttery smooth state transitions because we're not animals.

- **Adaptive Theme** — Follows your system theme. Dark mode gang rise up.

### ⚙️ Convenience

- **Microphone Selection** — Pick which mic to control. Useful if you have 47 audio devices like a normal person.

- **Auto-Start with Windows** — Enable it once, forget about it forever.

- **Start Minimized** — Boot straight to system tray. No window popping up in your face.

- **Portable** — No MSIX installer bullshit. Extract → Run → Profit.

---

## 📦 Downloads

| Architecture | File |
|-------------|------|
| x64 (64-bit) | `silence-v1.0-win-x64.zip` |
| x86 (32-bit) | `silence-v1.0-win-x86.zip` |
| ARM64 | `silence-v1.0-win-arm64.zip` |

---

## 💻 System Requirements

- Windows 10 version 1809 (build 17763) or later
- Windows 11 (any version)
- That's it. No .NET installation needed, it's self-contained.

---

## 🚀 Getting Started

1. Download the ZIP for your architecture
2. Extract anywhere you want
3. Run `silence!.exe`
4. Set your preferred hotkey
5. Minimize to tray
6. Never fumble with mute buttons again

---

## 🔧 Built With

- .NET 8.0
- WinUI 3
- NAudio (audio device management)
- H.NotifyIcon (system tray)

---

## 🐛 Known Issues

- First release, let us know if something's broken!

---

## 📝 Feedback

Found a bug? Have a feature request? Open an issue on GitHub!

---

<p align="center">
  <b>Made for people who are tired of that "you're on mute" moment.</b>
</p>



