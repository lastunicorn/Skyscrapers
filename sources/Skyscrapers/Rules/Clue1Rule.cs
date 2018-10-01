using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DustInTheWind.Skyscrapers.Rules
{
    internal class Clue1Rule : IRule
    {
        private readonly IReadOnlyList<Cell> cells;

        public bool IsSolved { get; private set; }

        public Clue1Rule(int clue, IReadOnlyList<Cell> cells)
        {
            if (cells == null) throw new ArgumentNullException(nameof(cells));

            bool isClueOutOfRange = clue < 0 || clue > cells.Count;
            if (isClueOutOfRange)
            {
                string message = $"The clue must be between 1 and {cells.Count} (the length of the cell array), including these values.";
                throw new ArgumentOutOfRangeException(nameof(clue), message);
            }

            this.cells = cells;

            if (clue != 1)
                IsSolved = true;
        }

        public bool TrySolve()
        {
            if (IsSolved)
                return false;

            return SolveClue1();
        }

        private bool SolveClue1()
        {
            bool isChanged = cells[0].Solve(cells.Count);
            IsSolved = true;

            return isChanged;
        }
    }
}