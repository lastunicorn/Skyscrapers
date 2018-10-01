using System;
using System.Collections.Generic;

namespace DustInTheWind.Skyscrapers.Rules
{
    internal class Clue2Rule : IRule
    {
        private readonly IReadOnlyList<Cell> cells;

        public bool IsSolved { get; private set; }

        public Clue2Rule(int clue, IReadOnlyList<Cell> cells)
        {
            if (cells == null) throw new ArgumentNullException(nameof(cells));

            bool isClueOutOfRange = clue < 0 || clue > cells.Count;
            if (isClueOutOfRange)
            {
                string message = $"The clue must be between 1 and {cells.Count} (the length of the cell array), including these values.";
                throw new ArgumentOutOfRangeException(nameof(clue), message);
            }

            this.cells = cells;

            if (clue != 2)
                IsSolved = true;
        }

        public bool TrySolve()
        {
            if (IsSolved)
                return false;

            return SolveClue2();
        }

        private bool SolveClue2()
        {
            bool isChanged = false;
            bool isCell0Solved = false;
            bool isCell2Solved = false;
            bool isCell3Solved = false;

            // set cell0 != 4
            isChanged |= cells[0].RemovePossibility(4);


            if (cells[0].IsSolved)
            {
                if (cells[0].Is(1))
                {
                    // if cell0 == 1 => set cell1 = 4

                    isChanged |= cells[1].Solve(4);
                }
                else if (cells[0].Is(2))
                {
                    // if cell0 == 2 => set cell1 != 3 and cell3 != 4

                    isChanged |= cells[1].RemovePossibility(3);
                    isChanged |= cells[3].RemovePossibility(4);
                }

                isCell0Solved = true;
            }


            if (cells[2].IsSolved)
            {
                // if cell2 == 4 => set cell0 != 1

                if (cells[2].Is(4))
                    isChanged |= cells[0].RemovePossibility(1);

                isCell2Solved = true;
            }


            if (cells[3].IsSolved)
            {
                // if cell3 == 4 => set cell0 = 3

                if (cells[3].Is(4))
                    isChanged |= cells[0].Solve(3);

                isCell3Solved = true;
            }

            IsSolved = isCell0Solved && isCell2Solved && isCell3Solved;

            return isChanged;
        }
    }
}