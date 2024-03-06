using System.IO.Compression;
using ZombieGameMovement.Properties;

namespace ZombieGameMovement
{
    public partial class Form1 : Form
    {

        bool goUp, goDown, goLeft, goRight, gameOver;
        EnumContainer.DirectionType direction = EnumContainer.DirectionType.UP;
        int playerHealth = 100;
        int speed = 10;
        int mapspeed = 10;
        int ammo = 10;
        int zombieSpeed = 3;
        int score = 0;
        PictureBox player;


        Random random = new Random();

        List<PictureBox> zombiesList = new List<PictureBox>();



        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            player = CreatePlayer();
            map.Controls.Add(player);
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
            if (goLeft && player.Left > 0)
            {
                player.Left -= speed;
            }

            if (goRight && player.Left + player.Width < map.Width)
            {
                player.Left += speed;
            }

            if (goUp && player.Top > 40)
            {
                player.Top -= speed;
            }

            if (goDown && player.Top + player.Height < map.Height)
            {
                player.Top += speed;
            }

            //Map moving
            if (goLeft && map.Left < 12)
            {
                map.Left += mapspeed;
            }

            if (goRight && map.Left > -4090)
            {
                map.Left -= mapspeed;
            }

            if (goUp && map.Top < 42)
            {
                map.Top += mapspeed;
            }

            if (goDown && map.Top > -2164)
            {
                map.Top -= mapspeed;
            }

            //Zombie movement
            foreach (PictureBox zom in zombiesList)
            {
                if (zom.Left > player.Left)
                {
                    zom.Left -= zombieSpeed;
                }
                if (zom.Left < player.Left)
                {
                    zom.Left += zombieSpeed;
                }

                if (zom.Top > player.Top)
                {
                    zom.Top -= zombieSpeed;
                }

                if (zom.Top < player.Top)
                {
                    zom.Top += zombieSpeed;
                }
            }

        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Left))
            {
                goLeft = true;
                direction = EnumContainer.DirectionType.LEFT;
                player.Image = Properties.Resources.left;
            }

            if (e.KeyCode.Equals(Keys.Right))
            {
                goRight = true;
                direction = EnumContainer.DirectionType.RIGHT;
                player.Image = Properties.Resources.right;
            }

            if (e.KeyCode.Equals(Keys.Up))
            {
                goUp = true;
                direction = EnumContainer.DirectionType.UP;
                player.Image = Properties.Resources.up;
            }

            if (e.KeyCode.Equals(Keys.Down))
            {
                goDown = true;
                direction = EnumContainer.DirectionType.DOWN;
                player.Image = Properties.Resources.down;
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
                bulletLeft = player.Left + (player.Width / 2),
                bulletTop = player.Top + (player.Height / 2)
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
            PictureBox player = new();
            player.Image = Properties.Resources.up;
            player.Left = 200;
            player.Top = 200;
            return player;
        }

        private void RestartGame()
        {

        }

    }
}