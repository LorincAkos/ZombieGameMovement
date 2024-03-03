namespace ZombieGameMovement
{
    public partial class Form1 : Form
    {

        bool goUp, goDown, goLeft, goRight, gameOver;
        EnumContainer.DirectionType direction = EnumContainer.DirectionType.UP;
        int playerHealth = 100;
        int speed = 10;
        int ammo = 10;
        int zombieSpeed = 3;
        int score = 0;

        Random random = new Random();

        List<PictureBox> zombiesList = new List<PictureBox>();



        public Form1()
        {
            InitializeComponent();
        }

        private void MainTimerEvent(object sender, EventArgs e)
        {
            if (playerHealth > 1)
            {
                healthBar.Value = playerHealth;
            }
            else
            {
                gameOver = true;
            }

            txtammo.Text = "Ammo: " + ammo;
            txtscore.Text = "Kills: " + score;

            if (goLeft && player.Left > 0) 
            {
                player.Left -= speed;
            }

            if (goRight && player.Left + player.Width < this.ClientSize.Width)
            {
                player.Left += speed;
            }

            if(goUp && player.Top > 40)
            {
                player.Top -= speed;
            }

            if (goDown && player.Top +player.Height < this.ClientSize.Height)
            {
                player.Top += speed;
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode.Equals(Keys.Left))
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

            if(e.KeyCode.Equals(Keys.Space))
            {
                ShootBullet(direction);
            }
        }

        private void ShootBullet(EnumContainer.DirectionType direction)
        {
            Bullet bullet = new Bullet();
            bullet.direction = direction;
            bullet.bulletLeft = player.Left + (player.Width /2); 
            bullet.bulletTop = player.Top + (player.Height /2);
            bullet.MakeBullet(this);
        }

        private void MakeZombies()
        {

        }

        private void RestartGame()
        {

        }
    }
}