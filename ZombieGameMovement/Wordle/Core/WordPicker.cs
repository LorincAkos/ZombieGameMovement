using System;
using System.IO;
using System.Linq;

namespace Wordle.Core
{
    internal class WordPicker
    {
        static readonly string path = "Wordlewords.txt";
        private static readonly string[] validWords = File.ReadAllLines(path);

        //Picks a random string from the array
        public static string GetString()
        {
            Random random = new Random();
            return validWords[random.Next(validWords.Length)].ToUpper();
            //return dictionary[0];
        }

        //Checks if the array contains the given string
        public static bool CheckForValidString(string word)
        {
            if (validWords.Contains(word))
            {
                return true;
            }
            return false;
        }
    }
}
