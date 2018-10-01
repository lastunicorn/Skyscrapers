using System;
using System.Collections.Generic;
using System.Linq;

namespace DustInTheWind.Skyscrapers.Rules
{
    internal class SingleValueRule : IRule
    {
        private readonly List<Cell> cellLine;

        public bool IsSolved => false;

        public SingleValueRule(List<Cell> cellLine)
        {
            this.cellLine = cellLine ?? throw new ArgumentNullException(nameof(cellLine));
        }

        public bool TrySolve()
        {
            // If a value can be found in only one cell, remove any other values from that cell.

            bool isChanged = false;

            for (int value = 1; value <= cellLine.Count; value++)
            {
                List<Cell> cellsWithValue = cellLine
                    .Where(t => t.Contains(value))
                    .ToList();

                if (cellsWithValue.Count == 1)
                    isChanged |= cellsWithValue[0].Solve(value);
            }

            return isChanged;
        }
    }
}