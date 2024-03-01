namespace ZombieGameMovement
{
    public partial class Form1 : Form
    {

        bool goUp, goDown, goLeft, goRight, gameOver;
        string facing = "up";
        int playerHealth = 100;
        int speed = 2;
        int ammo = 10;
        int zombieSpeed = 3;
        int score = 0;

        private int tileSize = 32; // Example tile size (adjust as needed)
        // Define map dimensions and viewport dimensions
        private int mapWidth = Properties.Resources.TestMap.Width;
        private int mapHeight = Properties.Resources.TestMap.Height;
        private int viewportWidth;
        private int viewportHeight;

        // Player position and initial viewport position
        private int playerX = 0;
        private int playerY = 0;
        private int viewportX = 0;
        private int viewportY = 0;

        Image fullMap = Properties.Resources.TestMap;


        Random random = new Random();

        List<PictureBox> zombiesList = new List<PictureBox>();



        public Form1()
        {
            InitializeComponent();
            viewportHeight = pictureBox1.Height;
            viewportWidth = pictureBox1.Width;

            // Calculate viewport position relative to player
            viewportX = playerX - viewportWidth / 2;
            viewportY = playerY - viewportHeight / 2;

            // Ensure viewport stays within map bounds
            viewportX = Math.Max(0, Math.Min(mapWidth - viewportWidth, viewportX));
            viewportY = Math.Max(0, Math.Min(mapHeight - viewportHeight, viewportY));

            playerX = player.Left;
            playerY = player.Top;
            UpdateViewport();
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

            if (goRight && player.Left + player.Width < pictureBox1.Width)
            {
                player.Left += speed;
            }

            if (goUp && player.Top > 40)
            {
                player.Top -= speed;
            }

            if (goDown && player.Top + player.Height < pictureBox1.Height)
            {
                player.Top += speed;
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Left))
            {
                goLeft = true;
                facing = "left";
                player.Image = Properties.Resources.left;
                playerX = playerX - 1;
            }

            if (e.KeyCode.Equals(Keys.Right))
            {
                goRight = true;
                facing = "right";
                player.Image = Properties.Resources.right;
                playerX = playerX + 1;
            }

            if (e.KeyCode.Equals(Keys.Up))
            {
                goUp = true;
                facing = "up";
                player.Image = Properties.Resources.up;
                playerY = playerY - 1;
            }

            if (e.KeyCode.Equals(Keys.Down))
            {
                goDown = true;
                facing = "down";
                player.Image = Properties.Resources.down;
                playerY = playerY + 1;
            }

            UpdateViewport();
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
                ShootBullet(facing);
            }
        }

        private void ShootBullet(string direction)
        {
            Bullet bullet = new Bullet();
            bullet.direction = direction;
            bullet.bulletLeft = player.Left + (player.Width / 2);
            bullet.bulletTop = player.Top + (player.Height / 2);
            bullet.MakeBullet(this);
        }

        private void UpdateViewport()
        {
            // Calculate viewport position relative to player
            viewportX = playerX - viewportWidth / 2;
            viewportY = playerY - viewportHeight / 2;

            // Ensure viewport stays within map bounds
            viewportX = Math.Max(0, Math.Min(mapWidth - viewportWidth, viewportX));
            viewportY = Math.Max(0, Math.Min(mapHeight - viewportHeight, viewportY));

            // Calculate the portion of the map image to be displayed within the viewport
            Rectangle sourceRect = new Rectangle(viewportX * tileSize, viewportY * tileSize, viewportWidth * tileSize, viewportHeight * tileSize);

            // Create a Bitmap to hold the portion of the map image
            Bitmap viewportBitmap = new Bitmap(viewportWidth, viewportHeight);

            // Copy the portion of the map image to the Bitmap
            using (Graphics g = Graphics.FromImage(viewportBitmap))
            {
                g.DrawImage(fullMap, new Rectangle(0, 0, viewportWidth * tileSize, viewportHeight * tileSize), sourceRect, GraphicsUnit.Pixel);
            }

            // Display the portion of the map image within the PictureBox control
            pictureBox1.Image = viewportBitmap;
        }

        private void MakeZombies()
        {

        }

        private void RestartGame()
        {

        }
    }
}