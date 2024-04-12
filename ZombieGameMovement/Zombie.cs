namespace ZombieGameMovement
{
    internal class Zombie
    {
        public Image ZombieImage {  get; private set; }
        public int EnemyX { get; set; }
        public int EnemyY { get; set; }
        public int EnemyWidth { get; }
        public int EnemyHeight { get; }
        private int SlowDownEnemyFrameRate { get; set; }
        private int SpriteIndex { get; set; }
        private int Steps { get; set; }
        public Zombie()
        {
            RandNumGenerator.GetZomCoordinate(out int x, out int y);
            ZombieImage = SpriteContainer.zombieWalkRight[0];
            EnemyX = x;
            EnemyY = y;
            EnemyWidth = 100;
            EnemyHeight = 100;
            SlowDownEnemyFrameRate = 0;
            SpriteIndex = 0;
            Steps = 0;
        }

        public void AnimateEnemy(Image[] source, Zombie zombie)
        {
            int end = source.Length - 1;

            zombie.SlowDownEnemyFrameRate++;
            if (zombie.SlowDownEnemyFrameRate == 4)
            {
                zombie.SpriteIndex++;
                zombie.SlowDownEnemyFrameRate = 0;
            }

            if (zombie.SpriteIndex > end)
            {
                zombie.SpriteIndex = 0;
            }

            zombie.ZombieImage =  source[Steps];
        }

    }
}
