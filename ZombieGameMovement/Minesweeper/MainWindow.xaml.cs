using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Minesweeper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameOption gameOption;
        public MainWindow()
        {
            gameOption = null!;
            InitializeComponent();
            heightSlider.Value = 10;
        }

        private void FieldGenerator()
        {
            for (int i = 0; i < heightSlider.Value; i++)
            {
                RowDefinition row = new();
                fieldGrid.RowDefinitions.Add(row);
                for (int j = 0; j < widthSlider.Value; j++)
                {
                    if (i == 0)
                    {
                        ColumnDefinition column = new();
                        fieldGrid.ColumnDefinitions.Add(column);
                    }

                    Button button = new()
                    {
                        Width = 22,
                        Height = 22,
                        FontWeight = FontWeights.Bold,
                        Style = this.FindResource("GameField") as Style,
                    };

                    Grid.SetColumn(button, j);
                    Grid.SetRow(button, i);
                    fieldGrid.Children.Add(button);
                    //button.Click += new RoutedEventHandler(ChangeIcon_Click);
                    button.PreviewMouseLeftButtonDown += new(MouseLeftButtonDown);
                    button.PreviewMouseRightButtonDown += new(MouseRightButtonDown);
                }
            }
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            fieldGrid.Children.Clear();
            fieldGrid.RowDefinitions.Clear();
            fieldGrid.ColumnDefinitions.Clear();

            FieldGenerator();

            gameOption = new((int)heightSlider.Value, (int)widthSlider.Value, (int)SetBombNumber.Value);

            Cheat();
        }

        private void MouseRightButtonDown(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int row = Grid.GetRow(button) + 1;
            int col = Grid.GetColumn(button) + 1;

            
                System.Drawing.Point tmp = new(row, col);
                if (gameOption.Flags.Contains(tmp))
                {
                    UnmarkFlag(row, col);
                    gameOption.Flags.Remove(tmp);
                }
                else
                {
                    MarkFlag(row, col);
                    gameOption.Flags.Add(tmp);
                }
            
        }

        private void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            Button button = (Button)sender;
            int row = Grid.GetRow(button)+1;
            int col = Grid.GetColumn(button)+1;
            MessageBox.Show(row +"\t"+ col);

            if (gameOption.CheckBomb(row, col))
            {
                MarkBomb(gameOption.Bombs);
                MessageBox.Show("Game Over");
                DisableButtons();
            }
            else
            {
                gameOption.UncoverField(row , col );
                ChangeGoodCells(gameOption.Revealed);
                if (gameOption.CheckGameState())
                {
                    MessageBox.Show("All bombs have been found!");
                }
            }
        }



        private void FlagButton_Click(object sender, RoutedEventArgs e)
        {
            gameOption.IsFlagChoosen = !gameOption.IsFlagChoosen;
        }

        private void MarkFlag(int row, int col)
        {
            Button field = (Button)fieldGrid.Children
                .Cast<UIElement>()
                .FirstOrDefault(e => Grid.GetRow(e) == row - 1 && Grid.GetColumn(e) == col-1)!;
            field.Background = Brushes.Green;
        }

        private void UnmarkFlag(int row, int col)
        {
            Button field = (Button)fieldGrid.Children
                .Cast<UIElement>()
                .FirstOrDefault(e => Grid.GetRow(e) == row-1 && Grid.GetColumn(e) == col - 1)!;
            field.Background = Brushes.LightGray;
        }

        private void MarkBomb(List<System.Drawing.Point> bombs)
        {
            for (int i = 0; i < bombs.Count; i++)
            {
                Button field = (Button)fieldGrid.Children
                    .Cast<UIElement>()
                    .FirstOrDefault(e => Grid.GetRow(e) == bombs[i].X-1 && Grid.GetColumn(e) == bombs[i].Y - 1)!;
                field.Background = Brushes.Red;
            }
        }

        private void ChangeGoodCells(List<System.Drawing.Point> cells)
        {
            for (int i = 0; i < cells.Count; i++)
            {
                Button field = (Button)fieldGrid.Children
                    .Cast<UIElement>()
                    .FirstOrDefault(e => Grid.GetRow(e) == cells[i].X-1 && Grid.GetColumn(e) == cells[i].Y-1)!;
                field.IsEnabled = false;
                field.Background = Brushes.Turquoise;
                field.Content = gameOption.Field[cells[i].X, cells[i].Y];
            }
        }

        private void DisableButtons()
        {
            for (int i = 0; i < fieldGrid.RowDefinitions.Count; i++)
            {
                for (int j = 0; j < fieldGrid.ColumnDefinitions.Count; j++)
                {
                    Button field = (Button)fieldGrid.Children
                        .Cast<UIElement>()
                        .FirstOrDefault(e => Grid.GetRow(e) == i && Grid.GetColumn(e) == j)!;
                    field.IsEnabled = false;
                }
            }
        }

        private void Cheat()
        {
            for (int i = 0; i < fieldGrid.RowDefinitions.Count; i++)
            {
                for (int j = 0; j < fieldGrid.ColumnDefinitions.Count; j++)
                {
                    Button field = (Button)fieldGrid.Children
                        .Cast<UIElement>()
                        .FirstOrDefault(e => Grid.GetRow(e) == i && Grid.GetColumn(e) == j)!;
                    field.Content = gameOption.Field[i + 1, j + 1];
                }
            }
        }

        private void SetBombNumber_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = MyRegex();
            try
            {
                e.Handled = regex.IsMatch(e.Text) && Convert.ToInt32(e.Text) <= (heightSlider.Value + widthSlider.Value);

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void widthSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (heightSlider is null)
            {
                return;
            }
            SetBombNumber.Maximum = (widthSlider.Value - 1) * (heightSlider.Value - 1);
        }

        private void heightSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetBombNumber.Maximum = (widthSlider.Value - 1) * (heightSlider.Value - 1);
        }

        [GeneratedRegex("[^0-9]+")]
        private static partial Regex MyRegex();
    }
}