using System.IO.Compression;
using System.Reflection;
using System.Windows.Forms;
using ZombieGameMovement.Properties;
using static ZombieGameMovement.EnumContainer;


namespace ZombieGameMovement
{
    public partial class GameWithScrollableMap : Form
    {
        private Player player;
        private DirectionType direction;
        private MapSettings mapSettings;
        private readonly int ammo;

        private bool gameOver;
        private readonly int zombieSpeed;
        private int score;

        private int steps;
        private int slowDownFrameRate;

        List<PictureBox> zombiesList = [];

        public GameWithScrollableMap()
        {
            InitializeComponent();

            #region Setup

            DoubleBuffered = true;
            player = new(100, 10, 200, 0);
            mapSettings = new(10, Height, Width, map.Height, map.Width);
            zombieSpeed = 3;
            score = 0;
            steps = 0;
            slowDownFrameRate = 0;

            #endregion

            CreateZombies();
        }


        private void MainTimerEvent(object sender, EventArgs e)
        {
            if (player.PlayerHealth > 0)
            {
                healthBar.Value = player.PlayerHealth;
            }
            else
            {
                gameOver = true;
            }

            //txtammo.Text = "Ammo: " + ammo;
            //txtscore.Text = "Kills: " + score;

            if (!player.Attack)
            {
                MapScrolling();
                PlayerMovement();

            }

            ZomMovement();

            map.Refresh();
        }

        private void ZomMovement()
        {
            foreach (PictureBox zom in zombiesList)
            {
                if (zom.Left > player.PlayerX)
                {
                    zom.Left -= zombieSpeed;
                }
                if (zom.Left < player.PlayerX)
                {
                    zom.Left += zombieSpeed;
                }

                if (zom.Top > player.PlayerY)
                {
                    zom.Top -= zombieSpeed;
                }

                if (zom.Top < player.PlayerY)
                {
                    zom.Top += zombieSpeed;
                }
            }
        }


        private void MapScrolling()
        {

            if (player.GoLeft && map.Left < mapSettings.MapStartOnXAxis && player.PlayerX < mapSettings.MapEndOnXAxisCriticalZone)
            {
                map.Left += mapSettings.MapSpeed;
            }

            if (player.GoRight && map.Left > mapSettings.MapEndOnXAxis && player.PlayerX > mapSettings.MapStartOnXAxisCriticalZone)
            {
                map.Left -= mapSettings.MapSpeed;
            }

            if (player.GoUp && map.Top < mapSettings.MapStartOnYAxis && player.PlayerY < mapSettings.MapEndOnYAxisCriticalZone)
            {
                map.Top += mapSettings.MapSpeed;
            }

            if (player.GoDown && map.Top > mapSettings.MapEndOnYAxis && player.PlayerY > mapSettings.MapStartOnYAxisCriticalZone)
            {
                map.Top -= mapSettings.MapSpeed;
            }
        }

        private void PlayerMovement()
        {

            if (player.GoLeft && player.PlayerX > 0)
            {
                player.PlayerX -= player.PlayerSpeed;
                player.AnimatePlayer(SpriteContainer.walkRight);
            }

            if (player.GoRight && player.PlayerX + player.PlayerWidth < map.Width)
            {
                player.PlayerX += player.PlayerSpeed;
                player.AnimatePlayer(SpriteContainer.walkRight);
            }

            if (player.GoUp && player.PlayerY > 500)
            {
                player.PlayerY -= player.PlayerSpeed;
            }

            if (player.GoDown && player.PlayerY + player.PlayerHeight < map.Height)
            {
                player.PlayerY += player.PlayerSpeed;
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Left))
            {
                player.GoLeft = true;
                direction = EnumContainer.DirectionType.LEFT;
            }

            if (e.KeyCode.Equals(Keys.Right))
            {
                player.GoRight = true;
                direction = EnumContainer.DirectionType.RIGHT;
            }

            if (e.KeyCode.Equals(Keys.Up))
            {
                player.GoUp = true;
                direction = EnumContainer.DirectionType.UP;
            }

            if (e.KeyCode.Equals(Keys.Down))
            {
                player.GoDown = true;
                direction = EnumContainer.DirectionType.DOWN;
            }

            if ((e.KeyCode == Keys.Space))
            {
                player.Attack = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Left))
            {
                player.GoLeft = false;
            }

            if (e.KeyCode.Equals(Keys.Right))
            {
                player.GoRight = false;
            }

            if (e.KeyCode.Equals(Keys.Up))
            {
                player.GoUp = false;
            }

            if (e.KeyCode.Equals(Keys.Down))
            {
                player.GoDown = false;
            }

            if (e.KeyCode.Equals(Keys.Space))
            {
                player.Attack = false;
                ShootBullet(direction);
            }
        }

        private void ShootBullet(EnumContainer.DirectionType direction)
        {
            Bullet bullet = new(direction, player.PlayerX, player.PlayerY);
            bullet.MakeBullet(map);
        }

        private void CreateZombies()
        {
            
        }

        private void RestartGame()
        {

        }

        private void CharacterPaintEvent(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(player.PlayerImg, player.PlayerX, player.PlayerY, player.PlayerWidth, player.PlayerHeight);

        }

        
        }
    }
