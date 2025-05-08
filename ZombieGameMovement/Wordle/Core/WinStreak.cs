using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordle.Core
{
    class WinStreak : ObservableObject
    {
        private int winStreakCount = 0;

        public int WinStreakCount
        {
            get { return winStreakCount; }
            set
            {
                winStreakCount = value;
                OnPropertyChanged();
            }
        }

        //Increases the winstreak by one if the user found the right string
        public void AddStreak()
        {
            WinStreakCount++;
        }

        //Sets winstreak to zero if the user doesn't find the right string
        //and runs out of attempts
        public void RemoveStreak()
        {
            WinStreakCount = 0;
        }
    }
}
