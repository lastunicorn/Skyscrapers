using System;
using System.Diagnostics;
using DustInTheWind.ConsoleTools;
using DustInTheWind.ConsoleTools.InputControls;

namespace DustInTheWind.Skyscrapers.Cli
{
    public class Program
    {
        static void Main(string[] args)
        {
            // https://www.codewars.com/kata/4-by-4-skyscrapers

            int[] clues1 =
            {
                0, 0, 1, 2,
                0, 2, 0, 0,
                0, 3, 0, 0,
                0, 1, 0, 0
            };

            int[] clues2 =
            {
                2, 2, 1, 3,
                2, 2, 3, 1,
                1, 2, 2, 3,
                3, 2, 1, 3
            };

            int[][] matrix = null;
            Measure(() =>
            {
                matrix = SolvePuzzle(clues2);
            });

            Console.WriteLine();
            PrintMatrix(matrix);
            Pause.QuickDisplay();
        }

        private static void Measure(Action action)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            action();
            stopwatch.Stop();

            Console.WriteLine();

            ValueView<string>.QuickWrite("Elapsed Time:", stopwatch.Elapsed.ToString());
        }

        static int[][] SolvePuzzle(int[] clues)
        {
            SkyscrapersBoard skyscrapersBoard = new SkyscrapersBoard(clues);
            int[,] solution = skyscrapersBoard.SolvePuzzle();
            return ToJaggedArray(solution);
        }

        private static void PrintMatrix(int[][] matrix)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int value = matrix[i][j];
                    Console.Write("[{0}] ", value);
                }

                Console.WriteLine();
            }
        }

        private static int[][] ToJaggedArray(int[,] values)
        {
            int length0 = values.GetLength(0);
            int length1 = values.GetLength(1);

            int[][] result = new int[length0][];

            for (int y = 0; y < length0; y++)
            {
                result[y] = new int[length1];

                for (int x = 0; x < length1; x++)
                    result[y][x] = values[y, x];
            }

            return result;
        }
    }
}