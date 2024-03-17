using System.IO.Compression;
using System.Reflection;
using System.Windows.Forms;
using ZombieGameMovement.Properties;
using static ZombieGameMovement.EnumContainer;


namespace ZombieGameMovement
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GameWithFixMap game = new()
            {
                FormBorderStyle = FormBorderStyle.None,
                WindowState = FormWindowState.Maximized
            };
            game.Show();
        }

        private void button2_Click(object sender, EventArgs e)
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