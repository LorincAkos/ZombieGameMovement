using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly GameOptions gameOptions;
        TicTacToeField ticTacToe;

        public MainWindow()
        {
            InitializeComponent();

            FieldSlider.Value = 5;
            StartGame.IsEnabled = false;

            gameOptions = new('O', (int)FieldSlider.Value);
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            ticTacToe = new(gameOptions);
            gameOptions.FillBoard(gameOptions.FieldSize);
            MainFrame.Content = ticTacToe;
        }

        private void FieldSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (gameOptions is null) return;
            gameOptions.FieldSize = (int)((Slider)sender).Value;
        }

        private void SwitchIconToX_Click(object sender, RoutedEventArgs e)
        {
            gameOptions.CurrentPlayerIcon = SwitchIconToX.Content.ToString()![0];
            StartGame.IsEnabled = true;
        }

        private void SwitchIconToO_Click(object sender, RoutedEventArgs e)
        {
            gameOptions.CurrentPlayerIcon = SwitchIconToO.Content.ToString()![0];
            StartGame.IsEnabled = true;
        }

        private void winSize_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Int32.TryParse(((TextBox)sender).Text, out int winSize))
            {
                if (winSize < 5 || winSize > gameOptions.FieldSize)
                {
                    MessageBox.Show("Couldn't set number lower than five or higher then the size of the field!");
                    return;
                }
                gameOptions.WinSize = winSize;
            }
            else
            {
                gameOptions.WinSize = 5;
                MessageBox.Show("Only numbers can be accepted!");
            }
        }
    }
}
