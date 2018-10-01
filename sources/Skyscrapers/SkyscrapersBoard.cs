using System;
using System.Collections.Generic;
using System.Linq;
using DustInTheWind.Skyscrapers.Rules;

namespace DustInTheWind.Skyscrapers
{
    /// <summary>
    /// The Skyscrapers game.
    /// </summary>
    /// <remarks>
    /// The main concepts:
    /// - SkyscrapersBoard - This is the class that represents the game. It created the Matrix (playing board) and creates the list of rules to be applied on each line in order to solve the game.
    /// - Matrix - It is a n x n board that stores in each cell a list of possible values that can be removed one by one base on the rules until a solution is found.
    /// - Cell - Represents a cell in the Matrix. It can store a list of possible values.
    /// - IRule - The rules that removes from the Matrix values based on some conditions.
    /// <para>
    /// The description of the problem can be found here: https://www.codewars.com/kata/4-by-4-skyscrapers
    /// </para>
    /// </remarks>
    public class SkyscrapersBoard
    {
        private readonly Matrix matrix;
        private readonly List<IRule> rules;

        public SkyscrapersBoard(int[] clues)
        {
            if (clues == null) throw new ArgumentNullException(nameof(clues));

            if (clues.Length % 4 != 0)
            {
                const string message = "The number of clues must be multiple of 4 (4 times the rank of the matrix).";
                throw new ArgumentException(message, nameof(clues));
            }

            int n = clues.Length / 4;

            bool isAnyClueOutOfRange = clues.Any(x => x < 0 || x > n);
            if (isAnyClueOutOfRange)
            {
                string message = $"The clues must be between 0 and {n} (the rank of the matrix), including these values.";
                throw new ArgumentException(message, nameof(clues));
            }

            matrix = new Matrix(n);

            rules = new List<IRule>();
            CreateRulesForClues(clues);
        }

        private void CreateRulesForClues(IReadOnlyList<int> values)
        {
            for (int i = 0; i < values.Count; i++)
            {
                List<Cell> line = matrix.GetLine(i).ToList();

                if (values[i] == 1)
                    rules.Add(new Clue1Rule(values[i], line));

                if (values[i] == 2)
                    rules.Add(new Clue2Rule(values[i], line));

                if (values[i] == 3)
                    rules.Add(new Clue3Rule(values[i], line));

                if (values[i] == values.Count)
                    rules.Add(new ClueNRule(values[i], line));

                rules.Add(new SingleValueRule(line));
            }
        }

        public int[,] SolvePuzzle()
        {
            bool isChanged = true;

            while (isChanged)
            {
                isChanged = false;

                IEnumerable<IRule> unsolvedClues = rules.Where(x => !x.IsSolved);

                foreach (IRule rule in unsolvedClues)
                    isChanged |= rule.TrySolve();
            }

            if (!matrix.IsSolved)
                throw new Exception("The game cannot be solved.");

            return matrix.GetSolution();
        }

        //public void PrintMatrix()
        //{
        //    for (int i = 0; i < 4; i++)
        //    {
        //        for (int j = 0; j < 4; j++)
        //        {
        //            Cell cell = matrix[i, j];
        //            Console.Write("[{0}] ", string.Join(",", cell.Possibilities));
        //        }

        //        Console.WriteLine();
        //    }
        //}
    }
}