# Mission: Electrician

Mission: Electrician is a 2D puzzle-platformer I made for GMTK Game Jam 2025 in Unity. The core mechanic is that every time the player connects power to a new checkpoint, they leave another cable segment behind, so movement and puzzle-solving become the same decision: route electricity through the level without crossing your own wire or touching hazards.

[Play the game on itch.io](https://clawboomgames.itch.io/mission-electrician)

## Controls

- Move: `A/D` or left/right arrow keys
- Jump: `W`, `Up Arrow`, or `Space`
- Connect a checkpoint: `E`

## Tech Stack

- Unity 6 (`6000.1.9f1`)
- Universal Render Pipeline
- Cinemachine
- DOTween
- Unity 2D / pixel-art workflow with Aseprite assets

## What I Built

- A player controller with moving-platform support and death/reset flow
- A cable system that leaves persistent line segments behind as the player powers new checkpoints
- A line-intersection failure rule so crossing your own cable electrocutes the player
- Multiple checkpoint types, including lights and moving platforms
- A level manager for progression, unlocks, win/loss flow, and scene transitions
- A shader-driven circular wipe system for moving between scenes

## Project Structure

- `Assets/Scripts/PlayerController.cs`: player movement and electrocution flow
- `Assets/Scripts/CableManager.cs`: cable creation, live segment updates, and crossing detection
- `Assets/Scripts/CheckpointTypes/`: checkpoint interaction types
- `Assets/Scripts/LevelManager.cs`: level state, unlocks, and win/loss progression
- `Assets/Scripts/WipeToNextScene.cs`: scene transition logic

## Credits

- Code / Design: ClawBoomGames
- Art: Abigail G.
- SFX: Zapsplat

## Notes

This repository is the Unity project source. To open it locally, use Unity `6000.1.9f1` or the closest compatible Unity 6 version.
