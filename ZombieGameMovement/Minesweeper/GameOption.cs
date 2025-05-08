using System.Drawing;

namespace Minesweeper
{
    public class GameOption
    {
        private int Height { get; set; }
        private int Width { get; set; }
        public string[,] Field { get; set; }
        private int FieldCellNumber { get; set; }
        public List<Point> Flags { get; set; }
        public List<Point> Bombs { get; set; }
        public List<Point> Revealed { get; set; }
        public bool IsFlagChoosen { get; set; }
        private int BombNumber { get; set; }

        public GameOption(int height, int width, int bomb)
        {
            Height = height+2;
            Width = width+2;
            Field = new string[height + 2, width + 2];
            FieldCellNumber = height * width;
            Flags = [];
            Bombs = [];
            Revealed = [];
            IsFlagChoosen = false;
            BombNumber = bomb;
            FillField();
            SetBomb(BombNumber);
            SetNumbers();
        }

        private void FillField()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Field[i, j] = "*";
                }
            }
        }

        private void SetBomb(int bombNumber)
        {
            Random rand = new();
            //int height;
            //int width;

            //while (bombNumber > 0)
            //{
            //    height = rand.Next(1, Height - 1);
            //    width = rand.Next(1, Width - 1);

            //    if (!Bombs.Contains(new Point(height - 1, width - 1)))
            //    {
            //        Bombs.Add(new Point(height - 1, width - 1));
            //        Field[height, width] = "B";
            //        bombNumber--;
            //    }
            //}

            List<Point> tmp = [];

            for (int i = 1;i < Height-1; i++)
            {
                for(int j = 1;j < Width-1; j++)
                {
                    tmp.Add(new Point(i,j));
                }
            }

            while(bombNumber > 0)
            {
                int index = rand.Next(0,tmp.Count-1);
                Bombs.Add(tmp[index]);
                Field[tmp[index].X, tmp[index].Y] = "B";
                tmp.RemoveAt(index);
                bombNumber--;
            }
        }

        private void SetNumbers()
        {
            for (int i = 1; i < Height - 1; i++)
            {
                for (int j = 1; j < Width - 1; j++)
                {
                    if (!Field[i, j].Equals("B"))
                    {
                        Field[i, j] = CalculateSurroundingBombs(i, j).ToString();
                    }
                }
            }
        }

        private int CalculateSurroundingBombs(int row, int col)
        {
            int numberOfBombsAroundCell = 0;

            for (int r = -1; r <= 1; r++)
            {
                for (int c = -1; c <= 1; c++)
                {
                    if (r != 0 || c != 0)
                    {
                        if (Field[row + r, col + c].Equals("B"))
                            numberOfBombsAroundCell++;
                    }
                }
            }

            return numberOfBombsAroundCell;
        }

        public bool CheckBomb(int row, int col)
        {
            if (Field[row, col].Equals("B"))
            {
                return true;
            }

            return false;
        }

        public void UncoverField(int row, int col)
        {
            Point currentPoint = new(row , col );

            if (Revealed.Contains(currentPoint) || Field[row, col].Equals("*"))
                return;

            Revealed.Add(currentPoint);

            if (Convert.ToInt32(Field[row, col]) > 0)
            {
                return;
            }

            for (int r = -1; r <= 1; r++)
            {
                for (int c = -1; c <= 1; c++)
                {
                    if (r != 0 || c != 0)
                    {
                        UncoverField(row + r, col + c);
                    }
                }
            }
            //UncoverField(row + 1, col);
            //UncoverField(row - 1, col);
            //UncoverField(row, col + 1);
            //UncoverField(row, col - 1);
        }

        public bool CheckGameState()
        {
            return Revealed.Count == FieldCellNumber - BombNumber;
        }
    }
}
