
using System;

namespace GameOfLife.Models
{
    public class TheGame
    {
        private bool[,] _gameGrid;

        private int _rows;
        private int _cols;

        public TheGame(bool[] gameGrid, int rows, int cols)
        {
            _rows = rows;
            _cols = cols;
            _gameGrid = Make2DArray(gameGrid, rows, cols);
        }

        private bool[,] Make2DArray(bool[] input, int height, int width)
        {
            bool[,] output = new bool[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    output[i, j] = input[i * width + j];
                }
            }
            return output;
        }

        private bool[] Make1DArray(bool[,] input, int height, int width)
        {
            bool[] output = new bool[height * width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    output[i * width + j] = input[i, j];
                }
            }
            return output;
        }
    
        public bool[] NextMove()
        {
            bool[,] next = new bool[_rows, _cols];
            for (int i = 0; i < _rows; i++)
                for (int j = 0; j < _cols; j++)
                    next[i,j] = GetCellNextValue(i, j);

            _gameGrid = next;
            return Make1DArray(_gameGrid, _rows, _cols);
        }

        private int GetCellValue(int i, int j)
        {
            if (i < 0 || i >= _rows || j < 0 || j >= _cols)
                return 0;

            return _gameGrid[i, j] ? 1 : 0;
        }

        private int GetNumberOfNeighbors(int i, int j)
        {
            int count = 0;
            for (int a = i - 1; a <= i + 1; a++)
                for (int b = j - 1; b <= j + 1; b++)
                    if (!(a == i && b == j))
                        count += GetCellValue(a, b);
            return count;
        }

        private bool GetCellNextValue(int i, int j)
        {
            int n = GetNumberOfNeighbors(i, j);

            // Each cell with one or no neighbors dies, as if by solitude.
            if (n <= 1)
                return false;

            // Each cell with four or more neighbors dies, as if by overpopulation.
            if (n >= 4)
                return false;

            // Each cell with two or three neighbors survives.
            if (n == 2)
                return _gameGrid[i,j];

            return true;
        }


    }
}
