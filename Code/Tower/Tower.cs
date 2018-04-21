using JamUtilities;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;

namespace JamTemplate.Tower
{
    class Tower : SmartSprite
    {
        public int tx = 0;
        public int ty = 0;

        private StateTower state;

        public bool showMenu = false;

        public int level = 0;

        public Enemy target = null;
        public float shootTimer = 0;
        private float range = 100;

        private Shape MenuBG;


        public Tower (int x, int y, StateTower s) : base ("../GFX/tower.png")
        {
            state = s;
            tx = x;
            ty = y;
            ReloadSprite();
            MenuBG = new RectangleShape(new Vector2f(100, 100));
        }

        private void ReloadSprite()
        {
            _texture = TextureManager.GetTextureFromFileName("../GFX/tower.png");
            _sprite = new SFML.Graphics.Sprite(_texture);
            generalSetup();

            SetPosition(new SFML.Window.Vector2f(tx * GP.WorldTileSizeInPixel, ty * GP.WorldTileSizeInPixel - 2));
        }

        public override void Update(TimeObject to)
        {
            base.Update(to);
            handleShooting(to.ElapsedGameTime);

            MenuBG.Position = this.Position + new Vector2f(this.Sprite.GetLocalBounds().Width, this.Sprite.GetLocalBounds().Height)
                + new Vector2f(32, -32);
        }

        private void handleShooting(float elapsed)
        {
            shootTimer -= elapsed;

            if (shootTimer <= 0)
            {
                target = getClosestTarget();

                if (target != null && !target.IsDead())
                {
                    shootTimer = GP.TowerReloadTime;
                    Shot s = new Shot(this, target);
                    state.SpawnShot(s);
                }
            }
        }

        public override void Draw(RenderWindow rw)
        {
            base.Draw(rw);
            
        }

        public void DrawMenu(RenderWindow rw)
        {
            if (showMenu)
            {
                rw.Draw(MenuBG);
            }
        }

        private Enemy getClosestTarget()
        {
            Enemy en = null;
            float d = range;
            foreach(Enemy e in state.allEnemies)
            {
                Vector2f dir = new Vector2f(e.GetPosition().X - GetPosition().X, e.GetPosition().Y - GetPosition().Y);
                float dt = MathStuff.GetLength(dir);
                
                if (dt <= d)
                {
                    d = dt;
                    en = e;
                }
            }
            return en;
        }

        public override void GetInput()
        {
            if (SFML.Window.Mouse.IsButtonPressed(SFML.Window.Mouse.Button.Right))
            {
                state.CloseAllMenus();
            }
            if (JamUtilities.Mouse.justPressed)
            {
                
                if (containsPoint(JamUtilities.Mouse.MousePositionInWorld))
                {
                    state.CloseAllMenus();
                    showMenu = true;
                    T.TraceD("hit");
                    // todo nice level up menu?
                    level++;
                }
            }
            
        }

        

    }
}
