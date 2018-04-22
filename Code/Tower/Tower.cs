using JamUtilities;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Audio;

namespace JamTemplate.Tower
{
    class Tower : SmartSprite
    {
        public int tx = 0;
        public int ty = 0;

        private StateTower state;

        

        public int level = 1;

        public int levelDamage = 1;
        public int levelRange = 1;
        public int levelRate = 1;

        public Enemy target = null;
        public float shootTimer = 0;
        private float range = GP.WorldTileSizeInPixel * 2.5f;

        private Shape MenuBG;
        public bool showMenu = false;
        public TextIconButton tbDamage;
        public TextIconButton tbRange;
        public TextIconButton tbRate;
       
        private long costDamage = 2; 
        private long costRate = 2;
        private long costRange = 2;

        private static SoundBuffer sndbufPowerUp = null;
        private Sound sndPowerUp;

        public Tower (int x, int y, StateTower s) : base ("../GFX/tower.png")
        {
            state = s;
            tx = x;
            ty = y;
            ReloadSprite();
            MenuBG = new RectangleShape(new Vector2f(200, 150));
            tbDamage = new TextIconButton("","../GFX/ic_dmg.png", LevelUpDmg);
            tbRange = new TextIconButton("", "../GFX/ic_rng.png", LevelUpRange);
            tbRate = new TextIconButton("", "../GFX/ic_rt.png", LevelUpRate);

            if (sndbufPowerUp == null)
                sndbufPowerUp = new SoundBuffer("../SFX/powerup.wav");
            sndPowerUp = new Sound(sndbufPowerUp);
        }


        private void LevelUpDmg()
        {
            this.Flash(Color.Green, 0.25f);
            T.TraceD("level up DMG");
            Resources.money -= costDamage;
            levelDamage++;
            costDamage = (long)(2 * Math.Pow(levelDamage, GP.TowerLevelUpCostExponent));
            sndPowerUp.Play();

        }


        private void LevelUpRange()
        {
            this.Flash(Color.Green, 0.25f);
            T.TraceD("level up rng");
            Resources.money -= costRange;
            levelRange++;
            range = GP.WorldTileSizeInPixel * (2.5f + 0.45f * levelRange);
            costRange = (long)(2 * Math.Pow(levelRange, GP.TowerLevelUpCostExponent));
            sndPowerUp.Play();
        }

        private void LevelUpRate()
        {
            this.Flash(Color.Green, 0.25f);
            T.TraceD("level up rt");
            Resources.money -= costRate;
            levelRate++;
            costRate = (long)(2 * Math.Pow(levelRate, GP.TowerLevelUpCostExponent));
            sndPowerUp.Play();
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
            UpdateMenu(to);
        }

        private void UpdateMenu(TimeObject to)
        {
            tbDamage.Update(to);
            tbRange.Update(to);
            tbRate.Update(to);
            tbDamage.text = "l." + levelDamage + " c." + costDamage;
            tbRange.text = "l." + levelRange + " c." + costRange;
            tbRate.text = "l." + levelRate+ " c." + costRate;

            MenuBG.Position = this.Position + new Vector2f(this.Sprite.GetLocalBounds().Width, this.Sprite.GetLocalBounds().Height)
                + new Vector2f(32, -32);

            tbDamage.active = Resources.money >= costDamage;
            tbRange.active = Resources.money >= costRange;
            tbRate.active = Resources.money >= costRate;

            tbDamage.SetPosition(new Vector2f(MenuBG.Position.X + 4, MenuBG.Position.Y + 4));
            tbRange.SetPosition(new Vector2f(MenuBG.Position.X + 4, MenuBG.Position.Y + 4 + 48));
            tbRate.SetPosition(new Vector2f(MenuBG.Position.X + 4, MenuBG.Position.Y + 4 + 48 + 48));
        }

        private void handleShooting(float elapsed)
        {
            shootTimer -= elapsed;

            if (shootTimer <= 0)
            {
                target = getClosestTarget();

                if (target != null && !target.IsDead())
                {
                    shootTimer = GP.TowerReloadTime * (0.25f + 0.75f * (float)Math.Pow(levelRate, -0.8));
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
                tbDamage.Draw(rw);
                tbRange.Draw(rw);
                tbRate.Draw(rw);
            }
        }

        private Enemy getClosestTarget()
        {
            Enemy en = null;

            float d = range;
            foreach(Enemy e in state.allEnemies)
            {
                Vector2f dir = new Vector2f(e.GetPosition().X - (GetPosition().X + 32), e.GetPosition().Y - (GetPosition().Y+ 32));
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



            if (showMenu)
            {
                tbDamage.GetInput();
                tbRange.GetInput();
                tbRate.GetInput();
            }
        }

        

    }
}
