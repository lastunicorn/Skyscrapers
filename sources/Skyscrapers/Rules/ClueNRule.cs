using System;
using System.Collections.Generic;

namespace DustInTheWind.Skyscrapers.Rules
{
    internal class ClueNRule : IRule
    {
        private readonly IReadOnlyList<Cell> cells;

        public bool IsSolved { get; private set; }

        public ClueNRule(int clue, IReadOnlyList<Cell> cells)
        {
            if (cells == null) throw new ArgumentNullException(nameof(cells));

            bool isClueOutOfRange = clue < 0 || clue > cells.Count;
            if (isClueOutOfRange)
            {
                string message = $"The clue must be between 1 and {cells.Count} (the length of the cell array), including these values.";
                throw new ArgumentOutOfRangeException(nameof(clue), message);
            }

            this.cells = cells;

            if (clue != cells.Count)
                IsSolved = true;
        }

        public bool TrySolve()
        {
            if (IsSolved)
                return false;

            return SolveClueN();
        }

        private bool SolveClueN()
        {
            bool isChanged = false;

            for (int i = 0; i < cells.Count; i++)
                isChanged |= cells[i].Solve(i + 1);

            IsSolved = true;

            return isChanged;
        }
    }
}