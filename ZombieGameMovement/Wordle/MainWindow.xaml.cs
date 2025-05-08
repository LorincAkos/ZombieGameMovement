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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wordle.Core;

namespace Wordle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WordleControls wordleControls = new();
        WinStreak winStreak = new();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = wordleControls;
            TextBoxGenerator();
            winStreakBlock.DataContext = winStreak;
            playAgainButton.IsEnabled = false;
        }

        /// <summary>
        /// Event for handling the key inputs
        /// </summary>
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (WordleControls.GameIsDone || WordleControls.NumberOfAttemps == 6) //Checks the game state and wont accept input after it's over
            {
                if (WordleControls.NumberOfAttemps == 6)
                {
                    winStreak.RemoveStreak();
                }
                playAgainButton.IsEnabled = true;
                MessageBox.Show("Game is done! Start new or quit.");
            }
            else if (e.Key == Key.Enter)
            {
                if (wordleControls.CheckLengthOfString()) //Checks the given string if it has the right amount of characters
                {
                    if (WordPicker.CheckForValidString(wordleControls.Words[WordleControls.NumberOfAttemps])) //Checks if the given string is valid
                    {
                        wordleControls.CheckTheString(out int row, out List<int> goodPositions, out List<int> containsPositions, out bool fullMatch); //Compares the given and the solution string

                        if (!fullMatch)
                        {
                            ColoringGoodAndContainedLetters(row, goodPositions, containsPositions);
                            return;
                        }

                        FullGreenColoring(row);
                        MessageBox.Show("GG!");
                        playAgainButton.IsEnabled = true;
                        winStreak.AddStreak();
                    }
                    else
                    {
                        wordleControls.RemoveString();
                    }
                }
                else
                {
                    MessageBox.Show("Not enough letters!!");
                }
            }
            else if (e.Key == Key.Back) //Delete character from the string
            {
                if (!wordleControls.RemoveLetterFromString())
                {
                    MessageBox.Show("No more letters");
                }
            }
            else if (e.Key >= Key.A && e.Key <= Key.Z) //Add character to the string
            {
                if (!wordleControls.AddLettersToString(e.Key.ToString()))
                {
                    MessageBox.Show("Max number of letters");
                }
            }
            else
            {
                MessageBox.Show("Invalid input");
            }
        }

        /// <summary>
        /// Event to start a new game when the button is pressed.
        /// Clearing the playing field and choosing a new solution string.
        /// </summary>
        private void playAgainButton_Click(object sender, RoutedEventArgs e)
        {
            wordleControls = new();
            //wordleControls.Test();
            this.DataContext = wordleControls;
            FullWhiteColoring();
            playAgainButton.IsEnabled = false;

        }

        private void TextBoxGenerator()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Binding myBinding = new($"Words[{i}][{j}]")
                    {
                        Source = wordleControls,
                        Mode = BindingMode.OneWay
                    };

                    TextBox textbox = new()
                    {
                        VerticalContentAlignment = VerticalAlignment.Center,
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        IsReadOnly = true,
                        FontSize = 22
                    };
                    textbox.SetBinding(TextBox.TextProperty, myBinding);

                    Grid.SetColumn(textbox, j);
                    Grid.SetRow(textbox, i);

                    myGrid.Children.Add(textbox);
                }
            }
        }

        //Recolors the row to green if the string is right
        private void FullGreenColoring(int row)
        {
            for (int i = 0; i < WordleControls.NumberOfLetters; i++)
            {
                var cell = myGrid.Children.Cast<UIElement>().FirstOrDefault(e => Grid.GetColumn(e) == i && Grid.GetRow(e) == row);
                ((TextBox)cell).Background = Brushes.Green;
            }
        }

        //Colors the cells to green if the character is in the right position
        //or yellow if the choosen string contains the character
        private void ColoringGoodAndContainedLetters(int row, List<int> goodPositions, List<int> containsPositions)
        {
            for (int i = 0; i < WordleControls.NumberOfLetters; i++)
            {
                if (goodPositions.Contains(i))
                {
                    var cell = myGrid.Children.Cast<UIElement>().FirstOrDefault(e => Grid.GetColumn(e) == i && Grid.GetRow(e) == row);
                    ((TextBox)cell).Background = Brushes.Green;
                }
                else if (containsPositions.Contains(i))
                {
                    var cell = myGrid.Children.Cast<UIElement>().FirstOrDefault(e => Grid.GetColumn(e) == i && Grid.GetRow(e) == row);
                    ((TextBox)cell).Background = Brushes.Yellow;
                }
            }
        }

        //Recolors the cells to white at the end of the game
        private void FullWhiteColoring()
        {
            for (int i = 0; i < myGrid.RowDefinitions.Count; i++)
            {
                for (int j = 0; j < WordleControls.NumberOfLetters; j++)
                {
                    TextBox cell = (TextBox)myGrid.Children.Cast<UIElement>().Single(e => Grid.GetColumn(e) == j && Grid.GetRow(e) == i);
                    cell.Background = Brushes.White;

                    //Rebinding the sorce (without it the characters in the cells won't change)
                    Binding myBinding = new($"Words[{i}][{j}]")
                    {
                        Source = wordleControls,
                        Mode = BindingMode.OneWay
                    };

                    cell.SetBinding(TextBox.TextProperty, myBinding);
                }
            }
        }
    }
}
