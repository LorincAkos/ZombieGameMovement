using static ZombieGameMovement.Bullet;

namespace ZombieGameMovement
{
    public partial class GameWithFixMap : Form
    {
        public static int FormWidth { get; private set; }
        public static int FormHeight { get; private set; }

        private Player Player { get; set; }
        private DirectionType Direction { get; set; }
        private readonly int ammo;
        private int ZombieSpeed { get; }
        private int Score { get; set; }
        private int MaxEnemy { get; set; }

        private int Steps { get; set; }
        private int SlowDownFrameRate { get; set; }

        private List<Zombie> ZombiesList { get; set; }
        private List<Bullet> BulletList { get; set; }


        public GameWithFixMap()
        {
            InitializeComponent();

            #region Setup

            FormWidth = this.Width;
            FormHeight = this.Height;
            DoubleBuffered = true;
            Player = new(100, 10, FormWidth/2, FormHeight/2);
            ZombieSpeed = 2;
            Score = 0;
            Steps = 0;
            SlowDownFrameRate = 0;
            ZombiesList = [];
            BulletList = [];
            MaxEnemy = 10;
            this.BackgroundImage = Properties.Resources.FixMap;

            #endregion

            CreateZombie();
        }

        private void MainTimerEvent(object sender, EventArgs e)
        {
            if (Player.PlayerHealth > 0)
            {
                healthBar.Value = Player.PlayerHealth;
            }
            else
            {
                healthBar.Value = 0;
                GameOver();
                //GameOver = true;
                //MessageBox.Show("Ded");
            }

            //txtammo.Text = "Ammo: " + ammo;
            txtscore.Text = "Kills: " + Score;

            if (!Player.Attack)
            {
                PlayerMovement();
            }

            if (ZombiesList.Count < MaxEnemy)
            {
                CreateZombie();
            }

            ZombieMovement();
            BulletMovement();
            CheckHit();
            this.Refresh();
        }

        private void ZombieMovement()
        {
            foreach (Zombie zombie in ZombiesList)
            {
                if (zombie.EnemyX > Player.PlayerX)
                {
                    zombie.EnemyX -= ZombieSpeed;
                    zombie.AnimateEnemy(SpriteContainer.zombieWalkRight, zombie);
                }
                if (zombie.EnemyX < Player.PlayerX)
                {
                    zombie.EnemyX += ZombieSpeed;
                    zombie.AnimateEnemy(SpriteContainer.zombieWalkRight, zombie);
                }

                if (zombie.EnemyY > Player.PlayerY)
                {
                    zombie.EnemyY -= ZombieSpeed;
                }

                if (zombie.EnemyY < Player.PlayerY)
                {
                    zombie.EnemyY += ZombieSpeed;
                }
            }
        }

        private void PlayerMovement()

        {

            if (Player.GoLeft && Player.PlayerX > 0)
            {
                Player.PlayerX -= Player.PlayerSpeed;
                Player.AnimatePlayer(SpriteContainer.walkRight);
            }

            if (Player.GoRight && Player.PlayerX + Player.PlayerWidth < this.Width)
            {
                Player.PlayerX += Player.PlayerSpeed;
                Player.AnimatePlayer(SpriteContainer.walkRight);
            }

            if (Player.GoUp && Player.PlayerY > 150)
            {
                Player.PlayerY -= Player.PlayerSpeed;
            }

            if (Player.GoDown && Player.PlayerY + Player.PlayerHeight < this.Height)
            {
                Player.PlayerY += Player.PlayerSpeed;
            }
        }

        private void BulletMovement()
        {
            if (BulletList.Count == 0)
            {
                return;
            }
            foreach (Bullet bullet in BulletList)
            {
                if (bullet.Direction.Equals(DirectionType.LEFT))
                {
                    bullet.BulletImg.Left -= bullet.Speed;
                }

                else if (bullet.Direction.Equals(DirectionType.RIGHT))
                {
                    bullet.BulletImg.Left += bullet.Speed;
                }

                else if (bullet.Direction.Equals(DirectionType.UP))
                {
                    bullet.BulletImg.Top -= bullet.Speed;
                }

                else if (bullet.Direction.Equals(DirectionType.DOWN))
                {
                    bullet.BulletImg.Top += bullet.Speed;
                }

                else if (bullet.BulletImg.Left < 0 || bullet.BulletImg.Left > GameWithFixMap.FormWidth || bullet.BulletImg.Top < 0 || bullet.BulletImg.Top > GameWithFixMap.FormHeight)
                {
                    bullet.BulletTimer.Stop();
                    bullet.BulletTimer.Dispose();
                    bullet.BulletImg.Dispose();
                }
            }
        }

        private void CheckHit()
        {
            foreach (Zombie zombie in ZombiesList)
            {
                if ((zombie.EnemyX <= Player.PlayerX && zombie.EnemyX + zombie.EnemyWidth >= Player.PlayerX && zombie.EnemyY <= Player.PlayerY && zombie.EnemyY + zombie.EnemyHeight >= Player.PlayerY) ||
                     (zombie.EnemyX <= Player.PlayerX + Player.PlayerWidth && zombie.EnemyX + zombie.EnemyWidth >= Player.PlayerX + Player.PlayerWidth && zombie.EnemyY <= Player.PlayerY && zombie.EnemyY + zombie.EnemyHeight >= Player.PlayerY) ||
                    (zombie.EnemyX <= Player.PlayerX && zombie.EnemyX + zombie.EnemyWidth >= Player.PlayerX && zombie.EnemyY <= Player.PlayerY + Player.PlayerHeight && zombie.EnemyY + zombie.EnemyHeight >= Player.PlayerY + Player.PlayerHeight) ||
                     (zombie.EnemyX <= Player.PlayerX + Player.PlayerWidth && zombie.EnemyX + zombie.EnemyWidth >= Player.PlayerX + Player.PlayerWidth && zombie.EnemyY <= Player.PlayerY + Player.PlayerHeight && zombie.EnemyY + zombie.EnemyHeight >= Player.PlayerY + Player.PlayerHeight)
                    )
                {
                    //ZombiesList.Remove(zombie);
                    Player.PlayerHealth = Player.PlayerHealth -25;

                }
                    foreach (Bullet bullet in BulletList)
                {
                    if (zombie.EnemyX <= bullet.BulletImg.Left && zombie.EnemyX + zombie.EnemyWidth >= bullet.BulletImg.Left &&
                        zombie.EnemyY <= bullet.BulletImg.Top && zombie.EnemyY + zombie.EnemyHeight >= bullet.BulletImg.Top)
                    {
                        Score++;
                        //zombie.ZomImage.Dispose();
                        bullet.BulletImg.Dispose();
                        ZombiesList.Remove(zombie);
                        BulletList.Remove(bullet);
                        return;
                    }
                    //if (zombie.EnemyX.Equals(bullet.BulletImg.Left) || zombie.EnemyY.Equals(bullet.BulletImg.Top))
                    //{
                    //    Score++;
                    //    zombie.ZomImage.Dispose();
                    //    bullet.BulletImg.Dispose();
                    //    ZombiesList.Remove(zombie);
                    //    BulletList.Remove(bullet);
                    //    return;
                    //}
                }
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Left))
            {
                Player.GoLeft = true;
                Direction =DirectionType.LEFT;
            }

            if (e.KeyCode.Equals(Keys.Right))
            {
                Player.GoRight = true;
                Direction = DirectionType.RIGHT;
            }

            if (e.KeyCode.Equals(Keys.Up))
            {
                Player.GoUp = true;
                Direction = DirectionType.UP;
            }

            if (e.KeyCode.Equals(Keys.Down))
            {
                Player.GoDown = true;
                Direction = DirectionType.DOWN;
            }

            if ((e.KeyCode.Equals(Keys.Space)))
            {
                Player.Attack = true;
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
                Player.GoLeft = false;
            }

            if (e.KeyCode.Equals(Keys.Right))
            {
                Player.GoRight = false;
            }

            if (e.KeyCode.Equals(Keys.Up))
            {
                Player.GoUp = false;
            }

            if (e.KeyCode.Equals(Keys.Down))
            {
                Player.GoDown = false;
            }

            if (e.KeyCode.Equals(Keys.Space))
            {
                Player.Attack = false;
                ShootBullet(Direction);
            }
        }

        private void ShootBullet(DirectionType direction)
        {
            Bullet bullet = new(direction, Player.PlayerX, Player.PlayerY + Player.PlayerHeight/2);
            bullet.BulletTimer.Tick += new EventHandler(MainTimerEvent!);
            BulletList.Add(bullet);
            bullet.MakeBullet(this);
        }

        private void CreateZombie()
        {
            Zombie zombie = new();
            ZombiesList.Add(zombie);
        }

        private void GameOver()
        {
            //TODO: Store the important data before closing the form!!!
            this.Close();
        }


        private void CharacterPaintEvent(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(Player.PlayerImg, Player.PlayerX, Player.PlayerY, Player.PlayerWidth, Player.PlayerHeight);
            foreach (Zombie zombie in ZombiesList)
            {
                g.DrawImage(zombie.ZombieImage, zombie.EnemyX, zombie.EnemyY, zombie.EnemyWidth, zombie.EnemyHeight);
            }
        }

        
    }
}