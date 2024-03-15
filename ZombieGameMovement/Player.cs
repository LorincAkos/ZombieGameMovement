using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieGameMovement
{
    internal class Player
    {
        public Image PlayerImg { get; set; }
        public int PlayerHealth { get; set; }
        public int PlayerSpeed { get; }
        public int PlayerX { get; set; }
        public int PlayerY { get; set; }
        public int PlayerWidth { get; }
        public int PlayerHeight { get; }
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
            GoLeft = false;
            GoRight = false;
            GoUp = false;
            GoDown = false;
            Attack = false;
        }


    }
}
