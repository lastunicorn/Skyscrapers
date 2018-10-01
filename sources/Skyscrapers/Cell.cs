using System;
using System.Collections.Generic;
using System.Linq;

namespace DustInTheWind.Skyscrapers
{
    internal sealed class Cell
    {
        private readonly List<int> possibilities;

        public int Y { get; }
        public int X { get; }

        public bool IsSolved => possibilities.Count == 1;

        public event EventHandler Solved;

        public Cell(int x, int y, int n)
        {
            X = x;
            Y = y;

            possibilities = Enumerable.Range(1, n).ToList();
        }

        public bool Solve(int possibility)
        {
            if (IsSolved)
            {
                if (possibilities[0] == possibility)
                    return false;

                throw new Exception("Cell is already solved.");
            }

            possibilities.Clear();
            possibilities.Add(possibility);
            OnSolved();

            return true;
        }

        public bool RemovePossibility(int possibility)
        {
            if (IsSolved)
            {
                if (possibilities[0] == possibility)
                    throw new Exception("Cell is already solved.");

                return false;
            }

            bool removed = possibilities.Remove(possibility);

            if (removed && possibilities.Count == 1)
                OnSolved();

            return true;
        }

        public int GetSolution()
        {
            if (!IsSolved)
                throw new Exception("Cell is not solved yet.");

            return possibilities[0];
        }

        public bool Is(int value)
        {
            return IsSolved && possibilities[0] == value;
        }

        public bool Contains(int value)
        {
            return possibilities.Contains(value);
        }

        private void OnSolved()
        {
            Solved?.Invoke(this, EventArgs.Empty);
        }
    }
}