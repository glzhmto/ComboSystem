# Combo System

Simple combo system in Unity. Each combo moves the cube in a different way and plays its own sound.

| Combo | What the cube does | Sound |
|---|---|---|
| ↑ + ↑ + ↓ + ↓ + Q + A | Jumps and flips to the right | `flip_right.wav` |
| ↑ + ↑ + ↑ + ↓ + Q + A | Jumps straight up spinning | `jump_up.wav` |
| ↑ + ↑ + ↑ | Jumps and flips to the left | `flip_left.wav` |

## How it works

`Assets/Scripts/ComboSystem.cs` saves every key you press in a string (↑ = `U`, ↓ = `D`, `Q`, `A`).
When you stop pressing keys for half a second, it checks if the string is one of the combos
(for example `"UUDDQA"`), adds a force and a torque to the Rigidbody, and plays the sound.

The half second wait is needed because ↑↑↑ is also the start of ↑↑↑↓QA.

## How to run

Open the project with Unity 6 (6000.3.8f1), open `Assets/Scenes/ComboScene.unity` and press Play.
