namespace ZombieGameMovement
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            GameWithFixMap game = new()
            {
                //FormBorderStyle = FormBorderStyle.None,
                //WindowState = FormWindowState.Maximized
            };
            game.Show();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            GameWithScrollableMap game = new()
            {
                FormBorderStyle = FormBorderStyle.None,
                WindowState = FormWindowState.Maximized
            };
            game.Show();
        }
    }
}