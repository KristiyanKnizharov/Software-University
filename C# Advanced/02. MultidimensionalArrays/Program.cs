using System;
using System.Linq;

namespace Diagonal_Difference
{
    class Program
    {
        static void Main(string[] args)
        {
            int sizeMatrix = int.Parse(Console.ReadLine());

            var matrix = new int[sizeMatrix, sizeMatrix];
            FillMatrix(sizeMatrix, matrix);

            var sumLeft = 0;
            var sumRight = 0;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                sumLeft += matrix[i, i];
                sumRight += matrix[i, matrix.GetLength(1) - i - 1];
            }

            Console.WriteLine(Math.Abs(sumLeft - sumRight));
        }

        private static void FillMatrix(int sizeMatrix, int[,] matrix)
        {
            for (int row = 0; row < sizeMatrix; row++)
            {
                var input = Console.ReadLine().Split().Select(int.Parse).ToArray();
                for (int col = 0; col < sizeMatrix; col++)
                {
                    matrix[row, col] = input[col];
                }
            }
        }
    }
}


using System;
using System.Linq;

namespace Squares_in_Matrix
{
    class Program
    {
        static void Main(string[] args)
        {
            var rowAndCol = Console.ReadLine().Split().Select(int.Parse).ToArray();

            var matrix = new string[rowAndCol[0], rowAndCol[1]];

            var count = 0;

            FillMatrix(matrix);

            for (int row = 0; row < matrix.GetLength(0) - 1; row++)
            {
                for (int col = 0; col < matrix.GetLength(1) - 1; col++)
                {

                    var firstChar = matrix[row, col];
                    if (matrix[row, col + 1] == firstChar && matrix[row + 1, col] == firstChar && matrix[row + 1, col + 1] == firstChar)
                    {
                        count++;
                    }
                }
            }

            Console.WriteLine(count);
        }

        private static void FillMatrix(string[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                var input = Console.ReadLine().Split();
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = input[col];

                }
            }
        }
    }
}

using System;
using System.Linq;

namespace Maximal_Sum
{
    class Program
    {
        static void Main(string[] args)
        {

            var rowAndCol = Console.ReadLine().Split().Select(int.Parse).ToArray();

            if (rowAndCol[0] >= 3 && rowAndCol[1] >= 3)
            {

                var matrix = new int[rowAndCol[0], rowAndCol[1]];

                FillMatrix(rowAndCol, matrix);

                var maximalSum = int.MinValue;
                var startRow = 0;
                var startCol = 0;

                for (int row = 0; row < rowAndCol[0] - 2; row++)
                {
                    for (int col = 0; col < rowAndCol[1] - 2; col++)
                    {
                        var sumRowFirst = matrix[row, col] + matrix[row, col + 1] + matrix[row, col + 2];
                        var sumRowTwo = matrix[row + 1, col] + matrix[row + 1, col + 1] + matrix[row + 1, col + 2];
                        var sumRowThree = matrix[row + 2, col] + matrix[row + 2, col + 1] + matrix[row + 2, col + 2];
                        var allSum = sumRowFirst + sumRowTwo + sumRowThree;
                        if (allSum > maximalSum)
                        {
                            maximalSum = allSum;
                            startRow = row;
                            startCol = col;
                        }
                    }
                }

                Console.WriteLine($"Sum = {maximalSum}");
                PrintMatrix(matrix, startRow, startCol);
            }

        }

        private static void FillMatrix(int[] rowEndCol, int[,] matrix)
        {
            for (int row = 0; row < rowEndCol[0]; row++)
            {
                var input = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                for (int col = 0; col < rowEndCol[1]; col++)
                {
                    matrix[row, col] = input[col];
                }
            }
        }

        private static void PrintMatrix(int[,] matrix, int startRow, int startCol)
        {
            for (int row = startRow; row <= startRow + 2; row++)
            {
                for (int col = startCol; col <= startCol + 2; col++)
                {
                    Console.Write(matrix[row, col] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}

using System;
using System.Linq;

namespace Matrix_Shuffling
{
    class Program
    {
        static void Main(string[] args)
        {
            var rowAndCol = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var matrix = new string[rowAndCol[0], rowAndCol[1]];
            FillMartix(matrix);

            while (true)
            {
                var command = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

                if (command[0] == "END")
                {
                    break;
                }
                else if (command[0] == "swap" && command.Length == 5)
                {
                    SwapEndPrintMatrixOrError(matrix, command);
                }
                else
                {
                    Console.WriteLine("Invalid input!");
                }
            }
        }

        private static void SwapEndPrintMatrixOrError(string[,] matrix, string[] command)
        {
            var firstRow = int.Parse(command[1]);
            var firstCol = int.Parse(command[2]);
            var secondRow = int.Parse(command[3]);
            var secondCol = int.Parse(command[4]);

            bool checkIndex = firstRow >= 0 && firstRow < matrix.GetLength(0)
                           && firstCol >= 0 && firstCol < matrix.GetLength(1)
                           && secondRow >= 0 && secondRow < matrix.GetLength(0)
                           && secondCol >= 0 && secondCol < matrix.GetLength(1);
            if (checkIndex)
            {
                var itemFirst = matrix[firstRow, firstCol];
                matrix[firstRow, firstCol] = matrix[secondRow, secondCol];
                matrix[secondRow, secondCol] = itemFirst;

                PrintMatrix(matrix);
            }
            else
            {
                Console.WriteLine("Invalid input!");
            }
        }

        private static void FillMartix(string[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                var input = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = input[col];
                }

            }
        }

        private static void PrintMatrix(string[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    Console.Write(matrix[row, col] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}

using System;
using System.Linq;

namespace Snake_Moves
{
    class Program
    {
        static void Main(string[] args)
        {
            var rowAndCol = Console.ReadLine().Split().Select(int.Parse).ToArray();

            var input = Console.ReadLine().ToCharArray();

            var matrix = new char[rowAndCol[0], rowAndCol[1]];
            var countIndex = 0;

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                if (row % 2 == 0)
                {
                    for (int col = 0; col < matrix.GetLength(1); col++)
                    {
                        matrix[row, col] = input[countIndex];
                        countIndex++;
                        if (countIndex >= input.Length)
                        {
                            countIndex = 0;
                        }
                    }
                }
                else
                {
                    for (int col = matrix.GetLength(1) - 1; col >= 0; col--)
                    {
                        matrix[row, col] = input[countIndex];
                        countIndex++;
                        if (countIndex >= input.Length)
                        {
                            countIndex = 0;
                        }
                    }
                }
            }
            PrintMatrix(matrix);
        }

        private static void PrintMatrix(char[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    Console.Write(matrix[row, col]);
                }
                Console.WriteLine();
            }
        }
    }
}

using System;
using System.Linq;

namespace Jagged_Array_Manipulator
{
    class Program
    {
        static void Main(string[] args)
        {
            var rows = int.Parse(Console.ReadLine());

            var jaggedArray = new double[rows][];

            FillArray(rows, jaggedArray);

            AnalyzingArray(rows, jaggedArray);

            while (true)
            {
                var command = Console.ReadLine().Split();

                if (command[0] == "End")
                {
                    break;
                }
                
                var row = int.Parse(command[1]);
                var col = int.Parse(command[2]);
                var value = int.Parse(command[3]);

                bool checkIndex = row >= 0 && row < jaggedArray.Length
                               && col >= 0 && col < jaggedArray[row].Length;

                if (command[0] == "Add" && checkIndex)
                {

                    jaggedArray[row][col] += value;
                }
                else if (command[0] == "Subtract" && checkIndex)
                {
                     
                    jaggedArray[row][col] -= value;
                }
            }

            for (int row = 0; row < jaggedArray.Length; row++)
            {
                Console.WriteLine(string.Join(" ", jaggedArray[row]));
            }
        }

        private static void AnalyzingArray(int rows, double[][] jaggedArray)
        {
            for (int row = 0; row < rows - 1; row++)
            {
                if (jaggedArray[row].Length == jaggedArray[row + 1].Length)
                {
                    for (int col = 0; col < jaggedArray[row].Length; col++)
                    {
                        jaggedArray[row][col] *= 2;
                        jaggedArray[row + 1][col] *= 2;
                    }
                }
                else
                {
                    for (int col = 0; col < jaggedArray[row].Length; col++)
                    {
                        jaggedArray[row][col] /= 2;
                    }
                    for (int col = 0; col < jaggedArray[row + 1].Length; col++)
                    {
                        jaggedArray[row + 1][col] /= 2;
                    }
                }
            }
        }

        private static void FillArray(int rows, double[][] jaggedArray)
        {
            for (int row = 0; row < rows; row++)
            {
                var input = Console.ReadLine().Split().Select(double.Parse).ToArray();
                jaggedArray[row] = new double[input.Length];
                jaggedArray[row] = input;
            }
        }
    }
}

using System;

namespace Knight_Game
{
    class Program
    {
        static void Main(string[] args)
        {

            var dimensions = int.Parse(Console.ReadLine());

            var matrix = new char[dimensions, dimensions];

            FillMatrix(dimensions, matrix);

            int removeKnight = 0;

            while (true)
            {

                int maxAttack = 0;
                int maxKnightIndexRow = 0;
                int maxKnightIndexCol = 0;
                for (int row = 0; row < dimensions; row++)
                {
                    for (int col = 0; col < dimensions; col++)
                    {
                        if (matrix[row, col] == 'K')
                        {
                            int count = 0;
                            count = CheckKnightAttack(dimensions, matrix, count, row - 1, col - 2);
                            count = CheckKnightAttack(dimensions, matrix, count, row - 1, col + 2);
                            count = CheckKnightAttack(dimensions, matrix, count, row - 2, col - 1);
                            count = CheckKnightAttack(dimensions, matrix, count, row - 2, col + 1);
                            count = CheckKnightAttack(dimensions, matrix, count, row + 1, col - 2);
                            count = CheckKnightAttack(dimensions, matrix, count, row + 1, col + 2);
                            count = CheckKnightAttack(dimensions, matrix, count, row + 2, col - 1);
                            count = CheckKnightAttack(dimensions, matrix, count, row + 2, col + 1);

                            if (count > maxAttack)
                            {
                                maxAttack = count;
                                maxKnightIndexRow = row;
                                maxKnightIndexCol = col;
                            }
                        }
                    }
                }

                if (maxAttack == 0)
                {
                    break;
                }
                else
                {
                    matrix[maxKnightIndexRow, maxKnightIndexCol] = '0';
                    removeKnight++;
                }
            }

            Console.WriteLine(removeKnight);

        }

        private static int CheckKnightAttack(int dimensions, char[,] matrix, int count, int row, int col)
        {
            if (CheckIndex(row, col, dimensions))
            {
                if (matrix[row, col] == 'K')
                {
                    count++;
                }
            }

            return count;
        }

        private static void FillMatrix(int dimensions, char[,] matrix)
        {
            for (int row = 0; row < dimensions; row++)
            {
                var input = Console.ReadLine().ToCharArray();
                for (int col = 0; col < dimensions; col++)
                {
                    matrix[row, col] = input[col];
                }
            }
        }

        private static bool CheckIndex(int row, int col, int dimensions)
        {
            return row >= 0 && row < dimensions && col >= 0 && col < dimensions;

        }
    }
}

using System;
using System.Linq;

namespace Bombs
{
    class Program
    {
        static void Main(string[] args)
        {
            int sizeMatrix = int.Parse(Console.ReadLine());

            var matrix = new int[sizeMatrix, sizeMatrix];
            FillMatrix(sizeMatrix, matrix);

            var indexBombs = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < indexBombs.Length; i++)
            {
                var curentRowEndCol = indexBombs[i].Split(",", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

                if (CheckIndex(matrix, curentRowEndCol[0], curentRowEndCol[1]))
                {
                    var row = curentRowEndCol[0];
                    var col = curentRowEndCol[1];
                    var bombValue = matrix[row, col];
                    if (bombValue > 0)
                    {
                        demageCell(matrix, bombValue, row - 1, col - 1);
                        demageCell(matrix, bombValue, row - 1, col);
                        demageCell(matrix, bombValue, row - 1, col + 1);
                        demageCell(matrix, bombValue, row, col - 1);
                        demageCell(matrix, bombValue, row, col + 1);
                        demageCell(matrix, bombValue, row + 1, col - 1);
                        demageCell(matrix, bombValue, row + 1, col);
                        demageCell(matrix, bombValue, row + 1, col + 1);
                        matrix[row, col] = 0;
                    }
                }

            }

            var aliveCells = 0;
            var sumCells = 0;
            foreach (var cell in matrix)
            {
                if (cell > 0)
                {
                    sumCells += cell;
                    aliveCells++;
                }
            }
            Console.WriteLine($"Alive cells: {aliveCells}");
            Console.WriteLine($"Sum: {sumCells}");
            PrintMatrix(matrix);

        }

        private static void PrintMatrix(int[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    Console.Write(matrix[row, col] + " ");
                }
                Console.WriteLine();
            }
        }
        private static void FillMatrix(int sizeMatrix, int[,] matrix)
        {
            for (int row = 0; row < sizeMatrix; row++)
            {
                var input = Console.ReadLine().Split().Select(int.Parse).ToArray();
                for (int col = 0; col < sizeMatrix; col++)
                {
                    matrix[row, col] = input[col];
                }
            }
        }
        private static bool CheckIndex(int[,] matrix, int row, int col)
        {

            return row >= 0 && row < matrix.GetLength(0) && col >= 0 && col < matrix.GetLength(1);
        }
        private static void demageCell(int[,] matrix, int bombValue, int row, int col)
        {
            if (CheckIndex(matrix, row, col) && matrix[row, col] > 0)
            {
                matrix[row, col] -= bombValue;
            }
        }
    }
}

using System;
using System.Linq;

namespace Miner
{
    class Program
    {
        static void Main(string[] args)
        {
            var sizeMatrix = int.Parse(Console.ReadLine());

            var matrix = new char[sizeMatrix, sizeMatrix];

            var commands = Console.ReadLine().Split();

            var allCoals = 0;
            var rowStart = 0;
            var colStart = 0;
            var rowEnd = 0;
            var colEnd = 0;

            bool check = true;

            // Fill matrix and find all coal, start index and  end index
            for (int row = 0; row < sizeMatrix; row++)
            {
                var input = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(char.Parse).ToArray();
                for (int col = 0; col < sizeMatrix; col++)
                {
                    matrix[row, col] = input[col];
                    if (input[col] == 'c')
                    {
                        allCoals++;
                    }
                    else if (input[col] == 'e')
                    {
                        rowEnd = row;
                        colEnd = col;
                    }
                    else if (input[col] == 's')
                    {
                        rowStart = row;
                        colStart = col;
                    }
                }
            }

            for (int i = 0; i < commands.Length; i++)
            {
                //Find position after command
                if (commands[i] == "up" && rowStart - 1 >= 0)
                {
                    rowStart -= 1;
                }
                else if (commands[i] == "down" && rowStart + 1 < sizeMatrix)
                {
                    rowStart += 1;
                }
                else if (commands[i] == "left" && colStart - 1 >= 0)
                {
                    colStart -= 1;
                }
                else if (commands[i] == "right" && colStart + 1 < sizeMatrix)
                {
                    colStart += 1;
                }

                //Check what is in position
                if (matrix[rowStart, colStart] == 'e')
                {
                    Console.WriteLine($"Game over! ({rowStart}, {colStart})");
                    check = false;
                    break;
                }
                else if (matrix[rowStart, colStart] == 'c')
                {
                    matrix[rowStart, colStart] = '*';
                    allCoals--;
                    if (allCoals == 0)
                    {
                        Console.WriteLine($"You collected all coals! ({rowStart}, { colStart})");
                        check = false;
                        break;
                    }
                }
            }
           
            //Print if coal left
            if (check)
            {
                Console.WriteLine($"{allCoals} coals left. ({rowStart}, {colStart})");
            }

        }
    }
}
