# Pulsar Game Prototype

## Setup Instructions

1. **Create the Scripts folder** (if it doesn't exist):
   - Right-click in Assets folder → Create → Folder → Name it "Scripts"

2. **Add all the scripts** to the Scripts folder:
   - GameManager.cs
   - PlayerController.cs
   - Enemy.cs
   - Pulse.cs
   - CameraController.cs
   - UIController.cs
   - GameSetup.cs

3. **Setup the game scene**:
   - Create an empty GameObject in your scene
   - Add the `GameSetup` component to it
   - Right-click on the GameSetup component and select "Setup Game Scene"
   - This will automatically create all necessary game objects and UI

4. **Play the game**:
   - Press Play in Unity
   - **Left Click**: Fire pulses (Active mode only)
   - **Right Click**: Toggle between Active/Idle modes
   - **Active Mode**: Manual pulse firing, full damage taken, higher DPS
   - **Idle Mode**: Automatic pulse firing, reduced damage taken, lower DPS

## Game Features

- **Player**: Blue circle at center of screen
- **Enemies**: Red circles that spawn outside camera view and move toward player
- **Pulses**: Yellow circles fired from player
- **Mode Switching**: Visual indicators with colored outlines
- **Souls System**: Enemies drop souls when killed
- **UI**: Shows current mode and soul count

## Controls

- **Left Mouse Button**: Fire pulse (Active mode only)
- **Right Mouse Button**: Toggle between Active/Idle modes

## Game Mechanics

- **Active Mode**:
  - Manual pulse firing with mouse clicks
  - Higher DPS and soul rewards
  - Full damage taken from enemies

- **Idle Mode**:
  - Automatic pulse firing at closest enemy
  - 50% damage reduction
  - Lower DPS and soul rewards

The prototype implements all core mechanics from the PRD and provides a solid foundation for further development.
