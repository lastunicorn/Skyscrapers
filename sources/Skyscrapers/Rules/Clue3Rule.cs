using System;
using System.Collections.Generic;

namespace DustInTheWind.Skyscrapers.Rules
{
    internal class Clue3Rule : IRule
    {
        private readonly IReadOnlyList<Cell> cells;

        public bool IsSolved { get; private set; }

        public Clue3Rule(int clue, IReadOnlyList<Cell> cells)
        {
            if (cells == null) throw new ArgumentNullException(nameof(cells));

            bool isClueOutOfRange = clue < 0 || clue > cells.Count;
            if (isClueOutOfRange)
            {
                string message = $"The clue must be between 1 and {cells.Count} (the length of the cell array), including these values.";
                throw new ArgumentOutOfRangeException(nameof(clue), message);
            }

            this.cells = cells;

            if (clue != 3)
                IsSolved = true;
        }

        public bool TrySolve()
        {
            if (IsSolved)
                return false;

            return SolveClue3();
        }

        private bool SolveClue3()
        {
            bool isChanged = false;
            bool isCell0Solved = false;
            bool isCell2Solved = false;
            bool isCell3Solved = false;

            isChanged |= cells[0].RemovePossibility(3);
            isChanged |= cells[0].RemovePossibility(4);

            if (cells[0].IsSolved)
            {
                if (cells[0].Is(1))
                {
                    // if cell0 == 1 => cell1 != 4 and cell2 != 3

                    isChanged |= cells[1].RemovePossibility(4);
                    isChanged |= cells[2].RemovePossibility(3);
                }
                else if (cells[0].Is(2))
                {
                    // if cell0 == 2 => cell1 != 4 and cell3 != 3

                    isChanged |= cells[1].RemovePossibility(4);
                    isChanged |= cells[3].RemovePossibility(3);
                }

                isCell0Solved = true;
            }

            if (cells[2].IsSolved)
            {
                // if cell2 == 4 => cell1 != 1

                if (cells[2].Is(4))
                    isChanged |= cells[1].RemovePossibility(1);

                isCell2Solved = true;
            }

            if (cells[3].IsSolved)
            {
                // if cell3 == 4 => cell1 != 2

                if (cells[3].Is(4))
                    isChanged |= cells[1].RemovePossibility(2);

                isCell3Solved = true;
            }

            IsSolved = isCell0Solved && isCell2Solved && isCell3Solved;

            return isChanged;
        }
    }
}