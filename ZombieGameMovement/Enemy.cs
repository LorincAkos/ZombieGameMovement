using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieGameMovement
{
    internal class Enemy
    {

        public Image ZomImage {  get; set; }
        public int EnemyX { get; set; }
        public int EnemyY { get; set; }
        public int EnemyWidth { get; }
        public int EnemyHeight { get; }
        public int SlowDownEnemyFrameRate { get; set; }
        public int SpriteIndex { get; set; }
        public Enemy()
        {
            RandNumGenerator.GetZomCoordinate(out int x, out int y);
            ZomImage = SpriteContainer.zomWalkRight[0];
            EnemyX = x;
            EnemyY = y;
            EnemyWidth = 100;
            EnemyHeight = 100;
            SlowDownEnemyFrameRate = 0;
            SpriteIndex = 0;
        }
    }
}
