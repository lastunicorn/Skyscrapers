using System;
using System.Collections.Generic;
using System.Linq;

namespace DustInTheWind.Skyscrapers
{
    internal class Matrix
    {
        private readonly int n;
        private readonly Cell[,] matrix;

        public Matrix(int n)
        {
            if (n <= 0) throw new ArgumentOutOfRangeException(nameof(n));
            this.n = n;

            matrix = new Cell[n, n];

            InitMatrix();
        }

        public bool IsSolved
        {
            get
            {
                return matrix
                    .Cast<Cell>()
                    .All(x => x.IsSolved);
            }
        }

        private void InitMatrix()
        {
            for (int y = 0; y < n; y++)
            {
                for (int x = 0; x < n; x++)
                {
                    Cell cell = new Cell(x, y, n);
                    cell.Solved += HandleCellSolved;

                    matrix[y, x] = cell;
                }
            }
        }

        private void HandleCellSolved(object sender, EventArgs e)
        {
            Cell cell = (Cell)sender;

            int value = cell.GetSolution();
            RemoveValueFromCross(value, cell.X, cell.Y);
        }

        private void RemoveValueFromCross(int value, int x, int y)
        {
            for (int i = 0; i < n; i++)
            {
                // Look on the row.
                if (i != x)
                    matrix[y, i].RemovePossibility(value);

                // Look on the column.
                if (i != y)
                    matrix[i, x].RemovePossibility(value);
            }
        }

        public IEnumerable<Cell> GetLine(int i)
        {
            if (i >= 0 && i < n)
                return GetColumn(i);

            if (i >= n && i < 2 * n)
                return GetRow(i - n, Direction.Inverse);

            if (i >= 2 * n && i < 3 * n)
                return GetColumn(3 * n - 1 - i, Direction.Inverse);

            if (i >= 3 * n && i < 4 * n)
                return GetRow(4 * n - 1 - i);

            throw new ArgumentOutOfRangeException(nameof(i));
        }

        public IEnumerable<Cell> GetColumn(int x, Direction direction = Direction.Normal)
        {
            switch (direction)
            {
                case Direction.Normal:
                    for (int i = 0; i < n; i++)
                        yield return matrix[i, x];
                    break;

                case Direction.Inverse:
                    for (int i = n - 1; i >= 0; i--)
                        yield return matrix[i, x];
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        public IEnumerable<Cell> GetRow(int y, Direction direction = Direction.Normal)
        {
            switch (direction)
            {
                case Direction.Normal:
                    for (int i = 0; i < n; i++)
                        yield return matrix[y, i];
                    break;

                case Direction.Inverse:
                    for (int i = n - 1; i >= 0; i--)
                        yield return matrix[y, i];
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        public int[,] GetSolution()
        {
            int[,] result = new int[n, n];

            for (int y = 0; y < n; y++)
            {
                for (int x = 0; x < n; x++)
                    result[y, x] = matrix[y, x].GetSolution();
            }

            return result;
        }
    }
}