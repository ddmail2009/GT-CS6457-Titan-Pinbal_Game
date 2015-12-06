Titan - Pinball

Main Scene: Scenes/Menu.scene
WebPlayer URL: http://www.prism.gatech.edu/~mtung7/Pinball.html

---------
Team info
---------

Xiaoyu Chen (xchen448)
xchen448@gatech.edu
- Scene Design and Modeling
- Branch Integration and Tweaking
- GamePad Support, Sound effect
- Plunger, MedKit, Particle effect

Tzu-Wei Huang (thuang86)
thuang86@gatech.edu
- Secret weapons (Meteorite, Invert directions)
- Sound effect, Particle effects and Jumping action

PoHsien Wang (pwang316)
ddmail2009@gmail.com
- Character Design (Model and Animation)
- Portal, Flippers, MedKit and Shocking waves

Zizheng Wu (zwu307)
me@zizhengwu.com
- One-sided bumpers, Bomb and Item Box

Meng-Hsin Tung (mtung7)
itswindtw@gatech.edu
- Project Management
- Branch Integration and Tweaking
- Player and Camera Scripting
- Missile Ball AI, Power Pellets, Game Menu

----------------------
Game Play Instructions
----------------------

Dodger Control
  - Use GamePad (PS4 tested)
    - [TODO]
  - Use Keyboard
    - WASD to move
    - Spacebar to jump
    - Ctrl to crouch and release shocking waves

Attacker Control
  - Keyboard
    - '.' to move left flipper, '/' to move right flipper
    - '[' to fire a new ball using plunger

During game, You can press Esc [TODO: GamePad?] to pause.

Dodger's path to win
  - Collect MedKits
  - Make Attacker lose balls
    - Use Shocking waves
    - Use Power Pellets
  - Use Bomb
    - Temporarily disable flippers
    - Get more powerful item from boxes

Attacker's path to win
  - Fire more balls
    - Increase chance to lose. Be Careful!
  - Use Portal
    - When a ball passes through a portal, it will split into from 2 to 5 balls randomly.
  - Use Missile Ball
    - http://www.prism.gatech.edu/~mtung7/titan_m3.png
    - Missle ball will automatically alter its course to strike dodger
    - Even better with Portals
  - Triggering secret weapons
    - Hit orange target among 4 bummpers.
    - Meteorite
      - Massive Destructive Weapon!
    - Control Inversion
      - Dodger's left becomes right and vice versa.
    - Control Inversion (backfired)
      - Attacker's left flipper becomes right flipper and vice versa.

------------
Requirements
------------

Complete - It must be a 3D game!
  - Achieveable goals, Versus Bar to indicate who's winning, replay on sucess or failure.
  - Game flow (Menu -> Game -> Pause)

Complete - Precursors to Fun Gameplay
  - Examples of interesting choices
    - Attacker can shot many balls to increase pressure on Dodger, but it might suffer from losing many balls.
    - Attacker can trigger secret weapons, but sometimes it might backfire to attacker.
    - Dodger can use shocking wave to block balls, but he needs to slow down his movement and definitve gets damages if he misses the timing.
    - Dodger can either use bomb to disable flippers or to get more powerful items from boxes.
    - Dodger can decide when to use items. Use it as soon as possible or keep it until there are too many balls or secret weapon is triggered.
  - Power pellets make characters of Dodger and Attacker switched. Originally, Attacker wants balls to hit Dodger but Attacker would avoid his attempt during power pellets in effect.
  - Progression of difficulty and learning
    - In early game, there are only MedKits and Attacker only gets a few balls on board. There are lower pressures on both side. They can explore and get familiar with game mechanisms.
    - In late game, there are special items, secret weapons, missile balls...etc. Both side would take more pressures and Game becomes more challenging.

Complete - Skeletal-Animated 3D Mesh Character Controller with Real-Time Control
  - Mecanim Controlled and Blendtree enabled character
  - Intuitive control with a small set of special actions (Jump, Crouch, Use Item)

Complete - 3D World with Physics and Spatial Simulation
  - A crafted pinball board that everybody is familiar with
  - Balls, Meteorites, Flippers fit physical interaction in real world.

Complete - Artifical Intelligence
  - Multiple states of Missile Ball
  - Three different characteristics of Missile balls. Make them not alway so reliable for Attacker.
  - AI behavior fits our Game feel
  - Force Dodger to master jumpping to counter Missile Ball

Complete - Polish
  - Menu -> Game -> Pause -> Exit
  - GUI element are styled aesthetically
  - Sound effects and Particle effects improve experience on physical interactions
  - Consistent artistic style
  - Stable game!

------------------
External Resources
------------------

Opening music (Mechanolith) and Credit music (Faceoff) are made by Kevin MacLeod (incompetech.com)
Licensed under Creative Commons: By Attribution 3.0, http://creativecommons.org/licenses/by/3.0/
Messiah by Handel is from YouTube Audio Library

Font Dashley is licensed under OFL (SIL Open Font License) (https://fontlibrary.org/en/font/dashley)
Font DejaVu Sans is licensed under Bitstream Vera License (https://fontlibrary.org/en/font/dejavu-sans)

Character model is made by Mixamo fuse
All character animations are from mixamo.com

Pinball sounds are from http://www.vpforums.org/Tutorials/Sounds/SndLib1.html
Character voices are from http://www.pacdv.com/sounds/index.html
Medkit sound is selected from Sound Pack Free Edition at Unity asset store

Freesound.org:
  Small Rock and Stone Hits.wav by lolamadeus
  Bomb - Small by Zangrutz
  MAGICAL_EFFECT.wav by StephenSaldanha
  transport 2.wav by Corsica_S.
  SFX_MAGIC_FIREBALL_001.wav by JoelAudio
  Tick Tock by FoolBoyMedia
  woman running on pavement stops hiheels 3.wav by bulbastre

Camera scripts are based on http://www.3dbuzz.com/training/view/3rd-person-character-system

Rock models are from Yughues Free Rocks (https://www.assetstore.unity3d.com/en/#!/content/13568)
Medkit model is from Small Survival Pack (https://www.assetstore.unity3d.com/en/#!/content/20565)

KY Magic Effects Free is used for shockwave effect.
GradientGUIBars is used for Manabar.

Striker explosion fire texture: http://nobacks.com/fire-fifty/  Missile ball smoke texture: http://nobacks.com/smoke-nineteen/
(Both free for personal use only. License: http://nobacks.com/license-and-copyrights/)
Striker explosion spark: Unity Standard Assets

Meteorite fire: http://pngimg.com/download/6025  Meteorite smoke: http://pngimg.com/download/967
