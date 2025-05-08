using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Wordle.Core
{
    internal class WordleControls : ObservableObject
    {
        private ObservableCollection<string> words;
        private string solution;
        private static int numberOfLetters;
        private static int numberOfAttemps;
        private static bool gameIsDone;

        public WordleControls()
        {
            Words = new() { "", "", "", "", "", "" };
            Solution = WordPicker.GetString();
            NumberOfLetters = 5;
            NumberOfAttemps = 0;
            GameIsDone = false;
        }

        public ObservableCollection<string> Words
        {
            get => words;
            set
            {
                words = value;
                OnPropertyChanged();
            }
        }
        public static int NumberOfLetters { get => numberOfLetters; set => numberOfLetters = value; }
        public static int NumberOfAttemps { get => numberOfAttemps; set => numberOfAttemps = value; }
        public string Solution 
        {
            get => solution;
            set
            {
                solution = value;
                OnPropertyChanged();
            }
        }
        public static bool GameIsDone { get => gameIsDone; set => gameIsDone = value; }

        /// <summary>
        /// Checks the length of the string if it's not five then appends a character to the string.
        /// </summary>
        /// <returns> True if the string is less than five characters long, false if not. </returns>
        public bool AddLettersToString(string letter)
        {
            if (!CheckLengthOfString())
            {
                Words[NumberOfAttemps] += letter;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if length of the string if is's not zero then removes a letter.
        /// </summary>
        /// <returns> True if the is more than zero characters long, false if zero. </returns>
        public bool RemoveLetterFromString()
        {
            if (Words[NumberOfAttemps].Length != 0)
            {
                Words[NumberOfAttemps] = Words[NumberOfAttemps].Remove(Words[NumberOfAttemps].Length - 1);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Removes the whole string int the NumberOfAttempts position.
        /// </summary>
        public void RemoveString()
        {
            Words[NumberOfAttemps] = "";
        }

        /// <summary>
        /// Compares the given string with the solution and increases the NumberOfAttempts variable by one.
        /// </summary>
        public void CheckTheString(out int row, out List<int> goodPositions, out List<int> containsPositions, out bool fullMatch)
        {
            row = NumberOfAttemps;
            fullMatch = false;
            goodPositions = new();
            containsPositions = new();

            //If both string is the same then returns a true value with the fullMatch variable.
            if (Words[NumberOfAttemps].Equals(Solution))
            {
                fullMatch = true;
                GameIsDone = true;
                return;
            }

            StringBuilder tmp = new(Words[NumberOfAttemps]);
            StringBuilder solutiontmp = new(Solution);

            //Checks the letters if they are in the good position,saves their positons
            //and replaces them with a junk character to avoid repeat.
            for (int i = 0; i < NumberOfLetters; i++)
            {
                if (tmp[i].Equals(solutiontmp[i]))
                {
                    goodPositions.Add(i);
                    tmp[i] = '!';
                    solutiontmp[i] = '!';
                }
            }

            //Checks the letters if the solution contains them then saves their position
            //and replaces them with a junk character to avoid repeat.
            for (int i = 0; i < NumberOfLetters; i++)
            {
                if (solutiontmp.ToString().Contains(tmp[i]))
                {
                    containsPositions.Add(i);
                    solutiontmp[solutiontmp.ToString().IndexOf(tmp[i])] = '!';
                    tmp[i] = '!';
                }
            }

            NumberOfAttemps++;
        }

        /// <summary>
        /// Checks if the string reachd the max numer of characters
        /// </summary>
        /// <returns> True if the string is five characters long, false if not. </returns>
        public bool CheckLengthOfString()
        {
            if (Words[NumberOfAttemps].Length != NumberOfLetters)
            {
                return false;
            }
            return true;
        }

        public void Reset()
        {
            for (int i = 0; i < Words.Count; i++)
            {
                Words[i] = "";
            }
            Solution = WordPicker.GetString();
            NumberOfLetters = 5;
            NumberOfAttemps = 0;
            GameIsDone = false;
        }
    }
}
