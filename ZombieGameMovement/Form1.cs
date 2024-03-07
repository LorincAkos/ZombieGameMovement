using System.IO.Compression;
using ZombieGameMovement.Properties;

namespace ZombieGameMovement
{
    public partial class Form1 : Form
    {
        private Image player;
        private EnumContainer.DirectionType direction = EnumContainer.DirectionType.UP;
        private readonly int playerHealth = 100;
        private readonly int speed = 10;
        private readonly int ammo = 10;

        private bool goUp, goDown, goLeft, goRight, gameOver;

        private readonly int mapSpeed = 10;
        private int mapStartOnXAxis;
        private int mapStartOnYAxis;
        private int mapEndOnXAxis;
        private int mapEndOnYAxis;
        private int mapStartOnXAxisCriticalZone;
        private int mapStartOnYAxisCriticalZone;
        private int mapEndOnXAxisCriticalZone;
        private int mapEndOnYAxisCriticalZone;
        private readonly int zombieSpeed = 3;
        private int score = 0;

        int steps = 0;
        int slowDownFrameRate = 0;
        private int playerX = 200;
        private int playerY = 0;
        private int playerHeight = 100;
        private int playerWidth = 100;


        Random random = new Random();

        List<PictureBox> zombiesList = new List<PictureBox>();
        List<string> movement = new();

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            
            SetMapCoordinates();
            //player = CreatePlayer();
            //map.Controls.Add(player);
            player = Image.FromFile("Sprites/Kuno_walk1.png");
            movement = Directory.GetFiles("Sprites").ToList();
            CreateZombies();
        }

        private void MainTimerEvent(object sender, EventArgs e)
        {
            if (playerHealth > 0)
            {
                healthBar.Value = playerHealth;
            }
            else
            {
                gameOver = true;
            }

            txtammo.Text = "Ammo: " + ammo;
            txtscore.Text = "Kills: " + score;

            //Player movement
            if (goLeft && playerX > 0)
            {
                playerX -= speed;
                AnimatePlayer(0, 3);
            }

            if (goRight && playerX + player.Width < map.Width)
            {
                playerX += speed;
                AnimatePlayer(0, 3);
            }

            if (goUp && playerY > 500)
            {
                playerY -= speed;
            }

            if (goDown && playerY + player.Height < map.Height)
            {
                playerY += speed;
            }

            //Map moving
            if (goLeft && map.Left < mapStartOnXAxis && playerX < mapEndOnXAxisCriticalZone * -1)
            {
                map.Left += mapSpeed;
            }

            if (goRight && map.Left > mapEndOnXAxis && playerX > mapStartOnXAxisCriticalZone * -1)
            {
                map.Left -= mapSpeed;
            }

            if (goUp && map.Top < mapStartOnYAxis && playerY < mapEndOnYAxisCriticalZone * -1)
            {
                map.Top += mapSpeed;
            }

            if (goDown && map.Top > mapEndOnYAxis && playerY > mapStartOnYAxisCriticalZone * -1)
            {
                map.Top -= mapSpeed;
            }

            //Zombie movement
            foreach (PictureBox zom in zombiesList)
            {
                if (zom.Left > playerX)
                {
                    zom.Left -= zombieSpeed;
                }
                if (zom.Left < playerX)
                {
                    zom.Left += zombieSpeed;
                }

                if (zom.Top > playerY)
                {
                    zom.Top -= zombieSpeed;
                }

                if (zom.Top < playerY)
                {
                    zom.Top += zombieSpeed;
                }
            }

            map.Invalidate();
            this.Invalidate();
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Left))
            {
                goLeft = true;
                direction = EnumContainer.DirectionType.LEFT;
            }

            if (e.KeyCode.Equals(Keys.Right))
            {
                goRight = true;
                direction = EnumContainer.DirectionType.RIGHT;
            }

            if (e.KeyCode.Equals(Keys.Up))
            {
                goUp = true;
                direction = EnumContainer.DirectionType.UP;
            }

            if (e.KeyCode.Equals(Keys.Down))
            {
                goDown = true;
                direction = EnumContainer.DirectionType.DOWN;
            }

        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Left))
            {
                goLeft = false;
            }

            if (e.KeyCode.Equals(Keys.Right))
            {
                goRight = false;
            }

            if (e.KeyCode.Equals(Keys.Up))
            {
                goUp = false;
            }

            if (e.KeyCode.Equals(Keys.Down))
            {
                goDown = false;
            }

            if (e.KeyCode.Equals(Keys.Space))
            {
                ShootBullet(direction);
            }
        }

        private void ShootBullet(EnumContainer.DirectionType direction)
        {
            Bullet bullet = new()
            {
                direction = direction,
                bulletLeft = playerX + (player.Width / 2),
                bulletTop = playerY + (player.Height / 2),
            };
            bullet.MakeBullet(map);
        }

        private void CreateZombies()
        {
            PictureBox zom = new()
            {
                Image = Properties.Resources.zright,
                Top = 42,
                Left = 12
            };
            zombiesList.Add(zom);
            map.Controls.Add(zom);
        }

        private PictureBox CreatePlayer()
        {
            PictureBox player = new()
            {
                Image = Properties.Resources.up,
                Left = 1920,
                Top = 1080,
                SizeMode = PictureBoxSizeMode.AutoSize
            };
            return player;
        }

        private void SetMapCoordinates()
        {
            mapStartOnXAxis = 0;
            mapStartOnYAxis = 0;
            mapEndOnXAxis = (map.Width - this.Width) * -1;
            mapEndOnYAxis = (map.Height - this.Height) * -1;

            mapStartOnXAxisCriticalZone = mapStartOnXAxis - this.Width / 2;
            mapStartOnYAxisCriticalZone = mapStartOnYAxis - this.Height / 2;
            mapEndOnXAxisCriticalZone = (map.Width - this.Width / 2) * -1;
            mapEndOnYAxisCriticalZone = (map.Height - this.Height / 2) * -1;

        }

        private void RestartGame()
        {

        }

        private void CharacterPaintEvent(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(player, playerX, playerY, playerWidth, playerHeight);
        }

        private void AnimatePlayer(int start, int end)
        {
            slowDownFrameRate++;
            if (slowDownFrameRate == 4)
            {
                steps++;
                slowDownFrameRate = 0;
            }

            if (steps > end || steps < start)
            {
                steps = start;
            }

            player = Image.FromFile(movement[steps]);
        }
    }
}