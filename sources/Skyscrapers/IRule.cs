namespace DustInTheWind.Skyscrapers
{
    internal interface IRule
    {
        bool IsSolved { get; }

        bool TrySolve();
    }
}