# Taskbar Breakout

A retro Breakout game that lives in your Windows taskbar — transparent, always-on-top, and playable without leaving your desktop. Press a key to go fullscreen when you want the full experience.

---

## Concept

The game runs as a slim horizontal strip overlaid on your taskbar. Only the game elements are visible — walls, bricks, ball, and paddle — everything else is transparent. The world below stays usable.

**Taskbar mode**

```
┌──────────────────────────────────────────────────────────────┐  ← thin wall
│ [██] [██] [██] [██] [██] [██] [██] [██] [██] [██] [██] [██] │  ← bricks
│                         ●                                    │  ← ball
│                      [═══]                                   │  ← paddle
└──────────────────────────────────────────────────────────────┘  ← thin wall
```

Everything outside the walls → transparent (click-through, taskbar still works).

**Fullscreen mode** — same gameplay, background image, full UI.

---

## Gameplay

### Paddle
- Paddle sits at the bottom of the play area
- **Hover** over the paddle to activate it
- **Hold + drag** the mouse to move it left or right
- When not interacting, the paddle is passive

### Ball
- Bounces off all four walls and the paddle
- Angle reflects based on where it hits the paddle (center = straight, edges = sharp angle)
- BBtan-style corner bouncing — precise angles matter

### Bricks
- Arranged in rows at the top
- Each brick has a hit point count (shown by color)
- Clear all bricks to advance to the next level

### Modes
| | Taskbar | Fullscreen |
|---|---|---|
| Window | Full screen width × taskbar height | Full screen |
| Background | Transparent | Custom image |
| Walls | Thin visible lines | Solid borders |
| HUD | Minimal (ball count, lives) | Full (score, level, power-ups) |
| Switch | `F11` | `F11` / menu |

State is preserved on mode switch — score, lives, ball position, active power-ups.

---

## Roadmap

### Phase 1 — UniWindowController Integration
> _Window management foundation_

- [ ] Import UniWindowController via UPM (`#upm` branch)
- [ ] Transparent borderless window (Alpha mode, URP compatible)
- [ ] Always-on-top
- [ ] Taskbar position detection (`SHAppBarMessage`)
- [ ] Window snaps to taskbar bounds on launch
- [ ] DPI-aware sizing
- [ ] Automatic hit-test: transparent pixels → click-through, opaque pixels → clickable

### Phase 2 — Core Gameplay
> _Horizontal breakout loop_

- [ ] Orthographic camera, horizontal layout
- [ ] Ball — continuous bounce, corner reflection
- [ ] Paddle — hover-to-activate, hold+drag to move
- [ ] Brick grid — hit points, color by health
- [ ] Wall colliders (top, left, right)
- [ ] Ball lost on bottom edge → lose a life
- [ ] Level complete when all bricks cleared

### Phase 3 — Taskbar Visual Layer
> _Only game elements visible, everything else transparent_

- [ ] Thin wall lines rendered at taskbar edges
- [ ] Bricks, ball, paddle drawn with pixel-art sprites
- [ ] Background fully transparent (camera alpha = 0)
- [ ] Raycast hit-test on paddle and ball colliders
- [ ] Taskbar outside game area remains fully functional

### Phase 4 — Fullscreen Mode
> _Same game, full experience_

- [ ] `F11` toggles between taskbar and fullscreen
- [ ] Custom background image in fullscreen
- [ ] Full HUD: score, level name, lives, power-ups
- [ ] Pause menu (ESC)
- [ ] State preserved across mode switch

### Phase 5 — Content & Polish
> _Levels, power-ups, feel_

- [ ] 5+ levels with distinct brick patterns
- [ ] Power-ups: MultiBall, WidePaddle, SlowDown, Laser
- [ ] Boss brick (multi-hit, color shifts)
- [ ] Retro sound effects
- [ ] CRT scanline effect in fullscreen
- [ ] Windows installer

---

## Architecture

```
Assets/_Game/
├── Core/
│   ├── Interfaces/        IWindowMode, IBrickPattern, IPowerUp, IInputStrategy
│   ├── Window/            TaskbarDetector, NativeWindowManager
│   ├── Modes/             TaskbarMode, FullscreenMode, ModeSwitcher
│   └── StateMachine/      Playing, Paused, GameOver states
├── Gameplay/
│   ├── Ball/              BallController, BallLauncher
│   ├── Paddle/            PaddleController (hover-activate, drag-to-move)
│   ├── Bricks/            Brick, BrickGrid, pattern classes
│   └── PowerUps/
├── Systems/               ScoreSystem, LivesSystem, CameraAdapter, AudioSystem
├── UI/
│   ├── Taskbar/           Minimal HUD
│   └── Fullscreen/        Full HUD, menus
└── Data/                  GameConfig, LevelData ScriptableObjects
```

SOLID principles throughout — each class has one responsibility, new levels/power-ups/modes added without touching existing code.

---

## Technical Notes

- **Engine**: Unity 2022 LTS, 2D URP
- **Window library**: [UniWindowController](https://github.com/kirurobo/UniWindowController) — transparent window, hit-test, position/size control
- **Transparency**: Alpha mode (per-pixel), URP: HDR=Off, AlphaProcessing=On, D3D11 (DXGI Flip Model=Off)
- **Hit-test**: Raycast mode — colliders on game objects, transparent background passes through
- **Platform**: Windows 10 / 11

---

## License

MIT
