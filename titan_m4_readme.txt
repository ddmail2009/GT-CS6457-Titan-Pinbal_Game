Titan - Milestone 4: Polish

Main Scene: Scenes/Menu.scene
WebPlayer URL: http://www.prism.gatech.edu/~xchen448/milestone4.html

---------
Team info
---------

Xiaoyu Chen
xchen448@gatech.edu
xchen448

Tzu-Wei Huang
thuang86@gatech.edu
thuang86

PoHsien Wang
ddmail2009@gmail.com
pwang316

Zizheng Wu
me@zizhengwu.com
zwu307

Meng-Hsin Tung
itswindtw@gatech.edu
mtung7

----------------------------
Requirements (UI Components)
----------------------------

Complete - The game should start with an introduction menu scene.
  - This menu scene has title of game, team name, a menu button to start and a menu button to show the credits.

Complete - The background environment of the menu should be visually appealing and animated in some fashion.
  - Two characters with a flying camera give our player a feeling that there is going to be a brutal battle on this pinball.

Complete - The menu items should be aesthetically positioned and styled.
  - no overlapping components, texts are readable and clear.
  - font is aesthetically selected. black background with white text color and shinning light remind our player this is a battlefield.
  - we use pinball icon as custom button image since it matches our total game feel.

Complete - Your menu system should be navigable with a controller or keyboard in addition to normal the mouse clicking.
  - menu items can be highlighed, clicked, go up/down.
  - although requirement suggests they should be loopable, we deicded not to do so since there are only two elements in our menu. Loopable is unnecessary.

Complete - The credits for the game should be fully informative on all contributions.

Complete - The credits for the game should be visually compelling in some way.
  - Animation and music are very good.

-------------------------------
Requirements (Particle Effects)
-------------------------------

Complete - You will need to have at least two different particle effects, each effect needs to have at least two different particle systems.
  - plunger striker explosion effect: explosion effect when striker hits the ball
    * explosion particle system
    * spark particle system
  - meteorite fire effect: fire effect when meteorite drops from sky
    * fire particle system
    * smoke particle system
  - missile ball smoke effect: smoke effect when missile ball is tracing
    * smoke particle system

Complete - In one of your particle effects, you need to leverage changing the size of the particles in a particle system with one of the modules.
  - in explosion particle system of striker explosion effect, the explosion fire will increase size during lifetime to simulate real explosion. Implemented with "Size over Lifetime".
  - in smoke particle system of meteorite fire effect, the smoke will increase size during lifetime to simulate real smoke. Implemented with "Size over Lifetime".

Complete - In one of your particle effects you need to leverage changing speed of particles with one of the modules.
  - in spark particle system of striker explosion effect, the sparks will decrease speed during lifetime after being emitted to simulate the real emission. Implemented with "Velocity over Lifetime".
  - in smoke particle system of meteorite fire effect, the smoke will change speed randomly in a range during lifetime to simulate real smoke. Implemented with "Velocity over Lifetime" and "Force over Lifetime".

Complete - In one of your particle effects you need to leverage a 2d custom material for your particles.
  - in explosion particle system of striker explosion effect, the explosion fire material is chosen to be condense, dark and powerful (texture url: http://nobacks.com/fire-fifty/). This material provides a better experience of explosion between striker and ball so that increase the attacker's game feel.
  - in meteorite fire effect, the smoke material is chosen to be dark grey (texture url: http://pngimg.com/download/967) to provide a better game feel.

Complete - In one of your particle effects you need to leverage sub-emitters to create a complicated effect.
  - in striker explosion effect, the sparks will be emitted when explosion particles die to provide a real explosion feeling.
  - in meteorite fire effect, the smoke will be emitted when fire particles are emitted.

Complete - You need to trigger one of your particle effects programmatically based on events in the game.
  - plunger striker explosion effect is triggered when the striker hits the ball
  - missile ball smoke effect is played only when the missile ball is tracing character

------------
Instructions
------------

Menu:
  - Keyboard: Use ad or left arrow, right arrow to move cursor. Use enter to select.
  - Mouse: click to select
  - PS4 Controller: Use left stick to select (Hold the stick to left or right for one second). Press 'X' to select.

Particle system:
  - Plunger explosion effect: Press 'p' to generate a ball, hold and release '[' to strike the ball.
  - Meteorite fire effect: Hit the red bumper on the top half of pinball table with ball three times and a meteorite attack will be triggered with fires. For convenience, you can press 'm' to trigger the meteorite attack.
  - Missile ball smoke effect: Make ball go through the special tunnel on the left of pinball table. The ball will then trace the player with smoke. For convenience, you can press 'o' to generate a tracing ball.

-----------------------
Resources (Milestone 4)
-----------------------

Opening music (Mechanolith) and Credit music (Faceoff) are made by Kevin MacLeod (incompetech.com)
Licensed under Creative Commons: By Attribution 3.0, http://creativecommons.org/licenses/by/3.0/

Font Dashley is licensed under OFL (SIL Open Font License) (https://fontlibrary.org/en/font/dashley)
Font DejaVu Sans is licensed under Bitstream Vera License (https://fontlibrary.org/en/font/dejavu-sans)

Striker explosion fire: http://nobacks.com/fire-fifty/
Missile ball smoke: http://nobacks.com/smoke-nineteen/
(Both free for personal use only. License: http://nobacks.com/license-and-copyrights/)

Striker explosion spark: Unity Standard Assets

Meteorite fire: http://pngimg.com/download/6025
Meteorite smoke: http://pngimg.com/download/967
