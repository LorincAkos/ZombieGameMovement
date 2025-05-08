using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace TicTacToe
{
    public class GameOptions
    {
        public char CurrentPlayerIcon { get; set; }
        public int FieldSize { get; set; }
        public int WinSize { get; set; }
        public int CheckFieldSize { get; set; }
        public char[,] Board { get; set; }

        public GameOptions(char currentPlayerIcon, int fieldSize, int winsize = 5, int checkFieldSize = 9)
        {
            CurrentPlayerIcon = currentPlayerIcon;
            FieldSize = fieldSize;
            WinSize = winsize;
            CheckFieldSize = checkFieldSize;
            Board = new char[fieldSize, fieldSize];
        }

        public void FillBoard(int size)
        {
            Board = new char[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Board[i, j] = '#';
                }
            }
        }

        public void GetValuesForRowAndColEnd(int len, int row, int col, out int rowStart, out int colStart, out int colEnd)
        {
            int n = ((WinSize - 5 + len) / 2) + 1;
            int size = Board.GetLength(0);

            //if (size > 8)
            //{
            //    rowStart = row - n > 0 ? row - n : 0;
            //    colStart = col - n > 0 ? col - n : 0;
            //    colEnd = col + n < size ? col + n : size - 1;
            //    return;
            //}
            rowStart = row;
            colStart = col;
            colEnd = col;
            int i = 0;

            while (rowStart != 0 && colStart != 0 && i != n)
            {
                rowStart--;
                colStart--;
                i++;
            }

            colEnd = col + n < size ? col + n : size-1; 

            //rowStart = 0;
            //colStart = 0;
            //colEnd = size - 1;
        }

        public bool MonitoringProgress(int row, int col, out (int, int)[] arr)
        {
            int lenght = CheckFieldSize <= Board.GetLength(0) ? CheckFieldSize : Board.GetLength(0);
            GetValuesForRowAndColEnd(lenght, row, col, out int rowStart, out int colStart, out int colEnd);

            (int, int)[] arr1 = new (int, int)[WinSize];
            (int, int)[] arr2 = new (int, int)[WinSize];
            (int, int)[] arr3 = new (int, int)[WinSize];
            (int, int)[] arr4 = new (int, int)[WinSize];
            int k1 = 0, k2 = 0, k3 = 0, k4 = 0;
            int size = Board.GetLength(0);

            for (int i = 0, j = 0; i < size && j < size; i++, j++)
            {
                if (Board[row, j].Equals(CurrentPlayerIcon))
                {
                    arr1[k1] = (row, j);
                    k1++;
                    if (k1 == WinSize)
                    {
                        arr = arr1;
                        return true;
                    }
                }
                else k1 = 0;

                if (Board[i, col].Equals(CurrentPlayerIcon))
                {
                    arr2[k2] = (i, col);
                    k2++;
                    if (k2 == WinSize)
                    {
                        arr = arr2;
                        return true;
                    }
                }
                else k2 = 0;
            }
            for (int i = rowStart, j = colStart, z = 0; i < size && j < size && colEnd - z >= 0; i++, j++, z++)
            {

                if (Board[i, j].Equals(CurrentPlayerIcon))
                {
                    arr3[k3] = (i, j);
                    k3++;
                    if (k3 == WinSize)
                    {
                        arr = arr3;
                        return true;
                    }
                }
                else k3 = 0;

                if (Board[i, colEnd - z].Equals(CurrentPlayerIcon))
                {
                    arr4[k4] = (i, colEnd - z);
                    k4++;
                    if (k4 == WinSize)
                    {
                        arr = arr4;
                        return true;
                    }
                }
                else k4 = 0;

            }


            ////WORKS
            //arr = new (int, int)[5];

            ////Horizontal check
            //int k = 0, numberOfContinousChars = 0;
            //for (int i = colStart; i <= colEnd; i++)
            //{
            //    if (Board[row, i].Equals(CurrentPlayerIcon))
            //    {
            //        arr[k] = (row, i);
            //        k++;
            //        numberOfContinousChars++;
            //        if (numberOfContinousChars == 5) return true;
            //    }
            //    else
            //    {
            //        k = 0;
            //        numberOfContinousChars = 0;
            //    }
            //}
            //numberOfContinousChars = 0;

            ////Vertical check
            //for (int i = rowStart; i <= rowEnd; i++)
            //{
            //    if (Board[i, col].Equals(CurrentPlayerIcon))
            //    {
            //        arr[k] = (i, col);
            //        k++;
            //        numberOfContinousChars++;
            //        if (numberOfContinousChars == 5) return true;
            //    }
            //    else
            //    {
            //        k = 0;
            //        numberOfContinousChars = 0;
            //    }
            //}
            //k = 0;
            //numberOfContinousChars = 0;

            ////Diagonal check
            //for (int i = rowStart, j = colStart; i <= rowEnd && j <= colEnd; i++, j++)
            //{

            //    if (Board[i, j].Equals(CurrentPlayerIcon))
            //    {
            //        numberOfContinousChars++;
            //        arr[k] = (i, j);
            //        k++;
            //        if (numberOfContinousChars == 5) return true;
            //    }
            //    else
            //    {
            //        k = 0;
            //        numberOfContinousChars = 0;
            //    }
            //}
            //k = 0;
            //numberOfContinousChars = 0;

            //for (int i = rowStart, j = colEnd; i <= rowEnd && j >= colStart; i++, j--)
            //{
            //    if (Board[i, j].Equals(CurrentPlayerIcon))
            //    {
            //        numberOfContinousChars++;
            //        arr[k] = (i, j);
            //        k++;
            //        if (numberOfContinousChars == 5)
            //        {
            //            return true;
            //        }
            //    }
            //    else
            //    {
            //        k = 0;
            //        numberOfContinousChars = 0;
            //    }
            //}
            arr = null;
            return false;
        }

        public bool CheckGameState(Grid grid, int row, int col, out (int, int)[] arr)
        {
            Board[row, col] = CurrentPlayerIcon;
            if (MonitoringProgress(row, col, out arr))
            {
                return true;
            }

            return false;
        }
    }
}

