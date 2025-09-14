# Product Requirements Document (PRD)
**Game Title:** TBD
**Genre:** Idle/Incremental game with roguelite-inspired elements
**Platform:** Unity (prototype with simple shapes, e.g. circles)

---

## 1. Core Concept
- The game is primarily an **idle game**, but rewards players who actively engage.
- **Visuals** are minimal (circles for player, enemies, pulses).
- **Enemies** spawn outside the camera view and move toward the player, trying to damage them.
- **Player** is always at the center of the screen.
- **Main mechanic:** waves or pulses fired from the player.

---

## 2. Modes
- The game has two modes: **Active** and **Idle**.
- Players can toggle between modes with **right click**.
- **Outline cue:**
  - Idle mode → subtle light blue outline around the screen.
  - Active mode → subtle reddish outline around the screen.

---

## 3. Active Mode
- Pulses are fired **manually** by clicking and pointing with the cursor.
- **Risk/reward:** higher DPS and more souls, but full incoming damage (no defensive buff).

---

## 4. Idle Mode
- Pulses are fired **automatically** at the closest enemy on a fixed frequency.
- Player receives a **defensive buff** (reduced damage taken) while idle.
- Designed to be viable for survival without constant player input.
- Tradeoff: lower DPS / fewer souls compared to active mode.

---

## 5. Pulses
- **Origin:** Always from the player.
- **Active mode:** triggered by click + cursor direction.
- **Idle mode:** emitted automatically on a set frequency.

---

## 6. Enemies
- Spawn outside the camera view.
- Move toward the player.
- On collision → damage the player.
- On death → drop **souls** (1 or more depending on health/difficulty).

---

## 7. Souls & Progression
- Enemies drop **souls** on death.
- Souls can be used for:
  - **Skill tree upgrades** (not fully defined yet).
  - **Random upgrade choices**:
    - 3 random upgrades are presented, player chooses 1.
    - Rarity system:
      - Common (green)
      - Rare (blue)
      - Epic (purple)
      - Legendary (yellow)
      - Unique (red)
