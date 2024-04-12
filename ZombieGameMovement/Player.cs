namespace ZombieGameMovement
{
    internal class Player
    {
        public Image PlayerImg { get; private set; }
        public int PlayerHealth { get; set; }
        public int PlayerSpeed { get; }
        public int PlayerX { get; set; }
        public int PlayerY { get; set; }
        public int PlayerWidth { get; }
        public int PlayerHeight { get; }
        private int Steps { get; set; }
        private int SlowDownFrameRate { get; set; }
        public bool GoLeft { get; set; }
        public bool GoRight { get; set; }
        public bool GoUp { get; set; }
        public bool GoDown { get; set; }
        public bool Attack { get; set; }

        public Player(int playerHealth, int playerSpeed, int playerX, int playerY)
        {
            PlayerImg = SpriteContainer.walkRight[0];
            PlayerHealth = playerHealth;
            PlayerSpeed = playerSpeed;
            PlayerX = playerX;
            PlayerY = playerY;
            PlayerWidth = 100;
            PlayerHeight = 100;
            SlowDownFrameRate = 0;
            GoLeft = false;
            GoRight = false;
            GoUp = false;
            GoDown = false;
            Attack = false;
        }

        public void AnimatePlayer(Image[] source)
        {
            int end = source.Length - 1;

            SlowDownFrameRate++;
            if (SlowDownFrameRate == 4)
            {
                Steps++;
                SlowDownFrameRate = 0;
            }

            if (Steps > end)
            {
                Steps = 0;
            }

            PlayerImg = source[Steps];
        }
    }
}
