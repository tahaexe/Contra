# Contra — Unity remake of stage 1

A 2D run-and-gun remake of the first stage of the NES classic *Contra*, built in Unity as a project for the Techcareer Bootcamp 2023. The player fights through the jungle stage, picks up weapon upgrades and defeats the boss at the end of the level.

## Features

- **Title screen and one playable stage** — `Login` (title screen) and `GamePlay` scenes, connected by a fade transition.
- **Player movement** — run, jump, drop through platforms, crouch, aim up or diagonally, and a swimming state in water zones.
- **Three enemy types** — *Runner* (patrols between two points), *Shooter* (turns to face the player, aims up, forward or down and fires within range) and *Turret* (placeholder, no behavior yet).
- **Weapon upgrades** — a flying power-up capsule appears as the player advances; shooting it drops a random pick-up (M, F, S, R or L). Each weapon is a ScriptableObject that defines its bullet prefab, fire rate and sound.
- **Boss fight** — a health bar appears near the end of the stage; destroying the boss plays an explosion and opens the win panel.
- **UI** — settings panel (pause, restart, mute), death panel and win panel, with DOTween fades.
- **Audio** — title and stage music plus sound effects, played through a pooled `AudioManager`.

## Controls

| Key | Action |
| --- | --- |
| Left / Right arrows or A / D | Move |
| Space | Jump |
| Z (hold) | Shoot |
| Up arrow | Aim up (straight up when standing, diagonally when running) |
| Down arrow | Crouch when standing, aim diagonally down when running |
| X | Drop down through a platform |
| Enter | Start the game on the title screen; restart after death; return to the title after winning |
| P / R / M / C | Settings panel: open (pauses the game) / restart / mute / close |

## Tech stack

- Unity **2022.3.10f1** (see `ProjectSettings/ProjectVersion.txt`), C#
- Unity 2D packages: Sprite, Tilemap, Pixel Perfect
- [DOTween](http://dotween.demigiant.com/) for UI and scene fades
- TextMesh Pro
- Hierarchy 2 (editor extension for the Hierarchy window)

## Project structure

```text
Assets/
├─ _Project/
│  ├─ Animation/         Player, enemy and explosion animations
│  ├─ Audio/             Music and sound effects
│  ├─ PhysicMaterial/    2D physics material
│  ├─ Prefab/            Player, enemies, ammo, power-ups and manager prefabs
│  ├─ Scenes/            Login (title screen), GamePlay
│  ├─ Script/            Gameplay code (namespace Contra)
│  ├─ ScriptableObject/  Weapon definitions (Ammo_M, Ammo_F, Ammo_S, Ammo_R, Ammo_L)
│  └─ Sprite/            Character, enemy, power-up and level sprites
├─ Plugins/Demigiant/    DOTween
├─ Hierarchy 2/          Editor extension
└─ TextMesh Pro/
```

The code uses a few singleton managers (`GameManager`, `AudioManager`, `UIManager`, `SceneFadeManager`) and a static `EventManager` (`PlayerDead`, `PlayerWin`, `BossHealth`) so that gameplay objects and UI panels stay decoupled.

## Getting started

1. Install Unity **2022.3.10f1** through Unity Hub.
2. Add this folder as a project in Unity Hub and open it.
3. Open `Assets/_Project/Scenes/Login.unity` and press **Play**. The build settings list `Login` first and `GamePlay` second.

## Status

Completed bootcamp project (2023). It is not under active development.

## Disclaimer

*Contra* is a trademark of Konami. This is an educational fan project made for a bootcamp; it is not affiliated with or endorsed by Konami. The characters and the level art are based on the original NES game, and their rights belong to Konami.

## License

No license file is included, so the original code is © its author, all rights reserved. Third-party packages (DOTween, TextMesh Pro, Hierarchy 2) are covered by their own licenses.
