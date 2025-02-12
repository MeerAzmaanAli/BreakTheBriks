This is is the unity project you can clone this repo and open it in unity, from scene folder
open samplescene

To run the game you can build it through unity selecting android platform
or just download it from the link:
https://drive.google.com/file/d/1oSyTDRRL212w7ZIeWv0HV9Nh9m2m8Z4U/view?usp=sharing

---

**Game Design Breakdown: BreakTheBricks**

**Game Overview:**
BreakTheBricks is a classic brick breaker game with a modern and minimalistic design. 
The player controls a paddle to bounce a ball and break bricks of varying strengths. 
The game gradually increases in difficulty by adding more rows and columns of bricks as the player progresses. 

Because technology was not mentioned in the task document, I went for unity for unity to develop this game
as I applied for Unity developer Position

---

**Visual Design:**
- **Art Style:** Minimalistic with a monochrome base enhanced by vibrant red and orange elements.
- **Bricks:**
  - White Brick: Weakest
  - Grey Brick: Medium Strength
  - Almost Black Brick: Hardest
  - Bomb Brick: Vibrant orange; explodes upon impact, destroying nearby bricks.
- **Paddle:**
  - A simple rectangle with a slightly curved top face to influence ball bounce mechanics.
  - Darker than the hardest brick to signify its sturdiness.
- **Ball:** Vibrant red to stand out against the background and bricks.
- **Borders:** Darker than the hardest brick, indicating they are indestructible.
- **Fx**: Didn't used particle system because of perfomance issue, simply made sprites for broken briks and also cracks sprite

---

**Gameplay Mechanics:**
- **Brick Breaking:**
  - The ball bounces off the paddle and hits bricks, shattering them upon impact.
  - Harder bricks require multiple hits to break.
  - Bomb bricks explode and destroy adjacent bricks.
- **Paddle Mechanics:**
  - Ball bounces at different angles depending on where it hits the paddle.
  - Slight curvature on the paddle’s top face influences the ball’s trajectory.
- **Ball Movement:**
  - The ball moves at a consistent speed, increasing slightly as levels progress.

---

**Controls:**
Players can control the paddle using one of three methods:
1. **Drag Control:** The player moves the paddle by dragging it left or right.
2. **Tilt Control:** The player tilts their device to move the paddle.
3. **Vertical Slider:** A slider allows the player to control the paddle precisely while maintaining full visibility of the game space. This is the most user-friendly option.

---

**Level Progression:**
- Bricks are initially spawned in a grid formation.
- The number of rows and columns increases as the player advances.
- Levels become progressively harder by adding more bricks and increasing ball speed.

---

**Additional Features:**
- **Explosive Mechanics:** Bomb bricks introduce strategic destruction of multiple bricks.
- **Minimalist UI:** A clean interface to keep the player’s focus on gameplay.
- **Smooth Animations:** Brick shattering and ball movement are visually satisfying.

---

