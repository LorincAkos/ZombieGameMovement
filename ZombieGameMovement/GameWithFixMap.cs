using System.IO.Compression;
using System.Reflection;
using System.Windows.Forms;
using ZombieGameMovement.Properties;
using static ZombieGameMovement.EnumContainer;


namespace ZombieGameMovement
{
    public partial class GameWithFixMap : Form
    {
        private Player player;
        private DirectionType direction;
        private readonly int ammo;

        private bool gameOver;
        private readonly int zombieSpeed;
        private int score;
        private int maxEnemy;

        private int steps;
        private int slowDownFrameRate;

        List<Enemy> zombiesList = [];

        public GameWithFixMap()
        {
            InitializeComponent();

            #region Setup

            DoubleBuffered = true;
            player = new(100, 10, 200, 0);
            zombieSpeed = 3;
            score = 0;
            steps = 0;
            slowDownFrameRate = 0;
            maxEnemy = 10;
            this.BackgroundImage = Properties.Resources.FixMap;

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
                PlayerMovement();

            }

            if (zombiesList.Count < maxEnemy)
            {
                CreateZombies();
            }

            ZomMovement();

            this.Refresh();
        }

        private void ZomMovement()
        {
            foreach (Enemy zom in zombiesList)
            {
                if (zom.EnemyX > player.PlayerX)
                {
                    zom.EnemyX -= zombieSpeed;
                    zom.ZomImage = AnimateEnemy(SpriteContainer.zomWalkRight,zom);
                }
                if (zom.EnemyX < player.PlayerX)
                {
                    zom.EnemyX += zombieSpeed;
                    zom.ZomImage = AnimateEnemy(SpriteContainer.zomWalkRight,zom);
                }

                if (zom.EnemyY > player.PlayerY)
                {
                    zom.EnemyY -= zombieSpeed;
                }

                if (zom.EnemyY < player.PlayerY)
                {
                    zom.EnemyY += zombieSpeed;
                }
            }
        }

        private void PlayerMovement()
        {

            if (player.GoLeft && player.PlayerX > 0)
            {
                player.PlayerX -= player.PlayerSpeed;
                AnimatePlayer(SpriteContainer.walkRight);
            }

            if (player.GoRight && player.PlayerX + player.PlayerWidth < this.Width)
            {
                player.PlayerX += player.PlayerSpeed;
                AnimatePlayer(SpriteContainer.walkRight);
            }

            if (player.GoUp && player.PlayerY > 150)
            {
                player.PlayerY -= player.PlayerSpeed;
            }

            if (player.GoDown && player.PlayerY + player.PlayerHeight < this.Height)
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

            if ((e.KeyCode.Equals(Keys.Space)))
            {
                player.Attack = true;
            }

            if (e.KeyCode.Equals(Keys.K))
            {
                this.Close();
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
            Bullet bullet = new()
            {
                direction = direction,
                bulletLeft = player.PlayerX + (player.PlayerWidth / 2),
                bulletTop = player.PlayerY + (player.PlayerHeight / 2),
            };
            bullet.MakeBullet(this);
        }

        private void CreateZombies()
        {
            Enemy zom = new();
            zombiesList.Add(zom);
        }

        private void RestartGame()
        {

        }

        private void CharacterPaintEvent(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(player.PlayerImg, player.PlayerX, player.PlayerY, player.PlayerWidth, player.PlayerHeight);
            foreach (Enemy zom in zombiesList)
            {
                g.DrawImage(zom.ZomImage,zom.EnemyX,zom.EnemyY,zom.EnemyWidth,zom.EnemyHeight);
            }

        }

        private void AnimatePlayer(Image[] source)
        {
            int end = source.Length - 1;

            slowDownFrameRate++;
            if (slowDownFrameRate == 4)
            {
                steps++;
                slowDownFrameRate = 0;
            }

            if (steps > end)
            {
                steps = 0;
            }

            player.PlayerImg = source[steps];
        }

        private Image AnimateEnemy(Image[] source,Enemy zom)
        {
            int end = source.Length - 1;

            zom.SlowDownEnemyFrameRate++;
            if (zom.SlowDownEnemyFrameRate == 4)
            {
                zom.SpriteIndex++;
                zom.SlowDownEnemyFrameRate = 0;
            }

            if (zom.SpriteIndex > end)
            {
                zom.SpriteIndex = 0;
            }

            return source[steps];
        }
    }
}