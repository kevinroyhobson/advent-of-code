using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day4
    {
        private const string InputPath = "input/2021-12-04.txt";

        public int Puzzle1()
        {
            var input = File.ReadLines(InputPath)
                            .ToList();

            var numbersToDraw = input.First()
                                     .Split(',')
                                     .Select(int.Parse)
                                     .ToList();

            var boards = GetBingoBoardsFromInput(input);

            var numbersDrawnSoFar = new HashSet<int>();
            for (int numNumbersDrawnSoFar = 0; numNumbersDrawnSoFar < numbersToDraw.Count; numNumbersDrawnSoFar++)
            {
                int thisNumber = numbersToDraw[numNumbersDrawnSoFar];
                numbersDrawnSoFar.Add(thisNumber);

                foreach (var board in boards)
                {
                    if (board.DoesBoardWin(numbersDrawnSoFar))
                    {
                        return board.GetBoardScore(numbersDrawnSoFar, thisNumber);
                    }
                }
            }

            return 0;
        }

        public int Puzzle2()
        {
            var input = File.ReadLines(InputPath)
                            .ToList();

            var numbersToDraw = input.First()
                                     .Split(',')
                                     .Select(int.Parse)
                                     .ToList();

            var boards = GetBingoBoardsFromInput(input);

            var numbersDrawnSoFar = new HashSet<int>();
            for (int numNumbersDrawnSoFar = 0; numNumbersDrawnSoFar < numbersToDraw.Count; numNumbersDrawnSoFar++)
            {
                int thisNumber = numbersToDraw[numNumbersDrawnSoFar];
                numbersDrawnSoFar.Add(thisNumber);

                if (boards.Count == 1 && boards.Single().DoesBoardWin(numbersDrawnSoFar))
                {
                    return boards.Single().GetBoardScore(numbersDrawnSoFar, thisNumber);
                }

                boards = boards.Where(board => !board.DoesBoardWin(numbersDrawnSoFar))
                               .ToList();
            }

            return 0;
        }
        
        private List<BingoBoard> GetBingoBoardsFromInput(List<string> input)
        {
            var boards = new List<BingoBoard>();

            input = input.Skip(2).ToList();
            while (input.Any())
            {
                var nextBoardInput = input.Take(5);
                boards.Add(new BingoBoard(nextBoardInput));

                input = input.Skip(6).ToList();
            }

            return boards;
        }

        private class BingoBoard
        {
            private int[,] _board;

            public BingoBoard(IEnumerable<string> boardInput)
            {
                _board = new int[5,5];
                int row = 0;
                foreach (var rowInput in boardInput)
                {
                    var rowNumbers = rowInput.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                             .Select(int.Parse);

                    int column = 0;
                    foreach (var number in rowNumbers)
                    {
                        _board[row, column] = number;
                        column++;
                    }
                    row++;
                }
            }

            public bool DoesBoardWin(HashSet<int> numbersDrawnSoFar)
            {
                var boardIndexes = Enumerable.Range(0, 5);
                for (int row = 0; row < 5; row++)
                {
                    if (boardIndexes.All(index => numbersDrawnSoFar.Contains(_board[row, index])))
                    {
                        return true;
                    }
                }
                
                for (int column = 0; column < 5; column++)
                {
                    if (boardIndexes.All(index => numbersDrawnSoFar.Contains(_board[index, column])))
                    {
                        return true;
                    }
                }
                
                return false;
            }

            public int GetBoardScore(HashSet<int> drawnNumbers, int winningNumber)
            {
                int unmarkedNumberSum = 0;
                foreach (var number in _board)
                {
                    if (!drawnNumbers.Contains(number))
                    {
                        unmarkedNumberSum += number;
                    }
                }

                return unmarkedNumberSum * winningNumber;
            }
        }
    }
}
