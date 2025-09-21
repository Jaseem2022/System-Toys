# System Toys 🎮

**System Toys** is a sandbox Unity project where I experiment with fun gameplay mechanics inspired by popular games (Tracer’s rewind/blink, Junkrat’s trap, gravity flips, and more). The goal is to explore mechanics in isolation and build them into a toybox of systems.

---

## 🚀 Features Implemented

### 🕹️ Player Core

* WASD movement + Jump
* Cinemachine follow camera

### ✨ Player Abilities

* **Rewind** → Travel back 5 seconds in time (Tracer-style)
* **Blink** → Short dash with cooldown
* **Clone Spawning** -> Spawns a clone of player prefab and destroys itself after a specific time
* **Clone Destruction** -> Ability to destroy the clone manually before the timer runs out

### 🌍 Environment Systems

* **Gravity Flip** → Invert gravity for all rigidbodies in the scene
* **Traps**
   - Bear Trap : Arrests the movement of player
   - Magnetic Trap : Attract player towards the trap and slows down the movement of player till escaping the radius.
   - Explosive Trap : Triggers and explodes the trap while in range and stuns the player for 3 seconds.
* **Collectibles**
   - Diamond : A coin like collectible
   - Shrink : Shrinks the player
   - Expand : Expands the player if already shrunk
             
### 🖥️ Scene Management & UI

* Quit game (Escape key)
* Reload current scene (R key)
* Small instruction panel showing quit/reload keys
* Ability cooldown timers
* Ability key mappings
* Coin Pickup count

---

## 🛠️ Tech Stack

* **Engine:** Unity (URP)
* **Language:** C#
* **Version Control:** Git + GitHub
* **Camera:** Cinemachine

---

## 🎯 Project Goal

This isn’t meant to be a polished game — it’s a **mechanics playground**.
Each “toy” is modular and can inspire mechanics for bigger projects later.

---
