using System;

namespace DustInTheWind.Skyscrapers
{
    internal class CellSolvedEventArgs : EventArgs
    {
        public Cell Cell { get; }

        public CellSolvedEventArgs(Cell cell)
        {
            Cell = cell ?? throw new ArgumentNullException(nameof(cell));
        }
    }
}