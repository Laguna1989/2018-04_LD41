using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Audio;

namespace JamUtilities
{
    public class GP
    {
        public static RenderWindow Window { get; set; } = null;
        public static View WindowGameView { get; set; } = null;
        public static Vector2u WindowSize { get { return Window.Size; } }

        public static string WindowGameName { get; private set; } = "$GameTitle$";

        public static int WorldTileSizeInPixel { get; set; } = 32;


        public static float EnemyMoveTimerMax { get; set; } = 0.75f;

        public static float TowerReloadTime { get; set; } = 2.05f;
        
        public static float TowerLevelUpCostExponent { get; set; } = 1.2f;

        public static float ShotSpeed { get; set; } = 200;
        public static float ShotDamageBase { get; set; } = 0.5f;
        
        public static float ShotMaxLifeTime { get; set; } = 2.5f;

        public static Music music { get; set; }
        

    }
}
