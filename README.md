# Sync Dash

**Unity Version:** 6000.0.58f2  
**Render Pipeline:** Universal Render Pipeline (URP)  
**Build Target:** Android (.apk)

---

## 🎯 Game Overview

**Sync Dash** is a simple hyper-casual prototype focused on **real-time state synchronization** and **visual effects**.

The screen is divided into two halves:
- **Right Side →** Player controls a glowing cube.
- **Left Side →** A “ghost” cube mirrors the player’s movements, simulating a synced multiplayer opponent with slight delay.

The game tests:
- Local network-style synchronization logic  
- URP shader usage (dissolve, glow, effects)  
- Performance optimization for mobile builds  

---

## 🕹️ Gameplay Mechanics

### Core Controls
- The cube moves forward automatically.
- **Tap** (or spacebar in editor) to **jump** and avoid obstacles.
- **Collect orbs** to increase the coin score.

### Ghost Simulation
- The ghost cube’s position, velocity, and state are updated using locally recorded **snapshot data**.
- Slight delay (configurable) is added to simulate network latency.
- Smooth interpolation ensures non-jittery mirrored motion.

---

## 🧩 Systems Implemented

| System | Description |
|--------|--------------|
| **Player Controller** | Handles jump, movement, and physics. |
| **Ghost Controller** | Smoothly interpolates snapshots from player movement for real-time mirroring. |
| **Sync Manager** | Records player snapshots (time, position, velocity, scores). Interpolates data for ghost motion. |
| **Score Manager** | Tracks and displays **distance** and **coin** scores separately. |
| **Object Pooling** | Reuses obstacles and orbs for performance efficiency. |
| **Obstacle Dissolve Shader** | Custom URP shader that fades obstacles out on collision using a noise-based dissolve effect. |
| **Orb Collect FX** | Particle burst plays when an orb is collected (detached from the object before pooling). |
| **Crash FX** | Chromatic aberration + screen shake when player crashes. |

---

## 📱 UI & Game Flow
- **Main Menu:** Start and Exit buttons.  
- **In-Game HUD:** Displays Distance and Coin scores.  
- **Game Over Screen:** Restart and Main Menu options.

---

## ⚙️ Performance Optimizations
- Object pooling for orbs and obstacles.
- Lightweight materials and shaders.
- Network sync simulated locally (no server).
- URP forward rendering for mobile efficiency.
- Build size maintained under 50MB.

---

## ✨ Shaders & Effects
- **Glowing Player Shader:** Emission-based glow that intensifies with speed.  
- **Obstacle Dissolve Shader:** Noise-based dissolve effect with edge glow.  
- **Orb Particle Effect:** Small additive burst on collection.  
- **Crash Effect:** Screen distortion and shake for impact feedback.

---

## 🧠 Technical Highlights
- Real-time snapshot interpolation (smooth ghost movement).  
- Configurable network delay for realistic latency simulation.  
- Mobile-friendly physics timestep (0.0167s @ 60Hz).  
- Clean decoupling between logic, rendering, and sync.

---

## ▶️ How to Play
1. Open the project in **Unity 2021.3.45f2 (URP)**.  
2. Load the **MainScene**.  
3. Press **Play** (or build to Android).  
4. Tap or click to jump and avoid obstacles.  
5. Collect orbs to earn points.  
6. Observe the ghost cube mirroring your movement on the left side.


---

**Developed as part of the “Sync Dash” real-time state synchronization demo.**
