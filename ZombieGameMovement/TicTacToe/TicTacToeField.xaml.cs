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
    /// Interaction logic for TicTacToeField.xaml
    /// </summary>
    public partial class TicTacToeField : Page
    {
        readonly GameOptions gameOptions;
        int steps = 0;
        int maxSteps;
        public TicTacToeField(GameOptions gameOptions)
        {
            this.gameOptions = gameOptions;
            maxSteps = gameOptions.FieldSize * gameOptions.FieldSize;
            InitializeComponent();
            FieldGenerator(gameOptions.FieldSize);
        }

        private void ChangeIcon_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            button.Content = gameOptions.CurrentPlayerIcon;
            button.FontWeight = FontWeights.Bold;
            button.IsEnabled = false;
            if (gameOptions.CheckGameState(myGrid, Grid.GetRow(button), Grid.GetColumn(button), out (int, int)[] arr))
            {
                ColorWinnerCharacters(arr);

                MessageBox.Show("Player with the icon " + gameOptions.CurrentPlayerIcon + " Won!!!");

                DisableButtons();
            }

            gameOptions.CurrentPlayerIcon = gameOptions.CurrentPlayerIcon.Equals('X') ? 'O' : 'X';

            steps++;
            if (steps == maxSteps)
            {
                FullRedColor();
                MessageBox.Show("Draw!!");
            }
        }

        private void ColorWinnerCharacters((int, int)[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                var cell = myGrid.Children.Cast<UIElement>().FirstOrDefault(e => Grid.GetRow(e) == arr[i].Item1 && Grid.GetColumn(e) == arr[i].Item2);
                ((Button)cell).Style = this.FindResource("WinButtonStyle") as Style;
            }
        }
        private void FullRedColor()
        {
            for (int i = 0; i < myGrid.ColumnDefinitions.Count; i++)
            {
                for (int j = 0; j < myGrid.RowDefinitions.Count; j++)
                {
                    var cell = myGrid.Children.Cast<UIElement>().FirstOrDefault(e => Grid.GetRow(e) == j && Grid.GetColumn(e) == i);
                    ((Button)cell).Style = this.FindResource("DrawStyle") as Style;

                }
            }
        }
        private void DisableButtons()
        {
            for (int i = 0; i < myGrid.RowDefinitions.Count; i++)
            {
                for (int j = 0; j < myGrid.ColumnDefinitions.Count; j++)
                {
                    var cell = myGrid.Children.Cast<UIElement>().FirstOrDefault(e => Grid.GetRow(e) == i && Grid.GetColumn(e) == j);
                    ((Button)cell).IsEnabled = false;
                }
            }
        }
        private void FieldGenerator(int size)
        {
            for (int i = 0; i < size; i++)
            {
                RowDefinition row = new();
                myGrid.RowDefinitions.Add(row);
                for (int j = 0; j < size; j++)
                {
                    if (i == 0)
                    {
                        ColumnDefinition column = new();
                        myGrid.ColumnDefinitions.Add(column);
                    }

                    Button button = new()
                    {
                        Width = 40,
                        Height = 40
                    };

                    button.Click += new RoutedEventHandler(ChangeIcon_Click);
                    Grid.SetColumn(button, j);
                    Grid.SetRow(button, i);
                    myGrid.Children.Add(button);
                }
            }
        }
    }
}
