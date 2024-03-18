using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Minesweeper!");
            Console.WriteLine("Enter the size of the board (e.g. 5 for a 5x5 board):");
            int size = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the density of mines:");
            int mines = int.Parse(Console.ReadLine());
            int mineCount, mistakes = 0;
            int[,] board = new int[size, size];
            int[,] playBoard = new int[size, size];
            int playerCordsX = 0, playerCordsY = 0;

            GenerateMines(board, size, mines, playBoard, out mineCount);
            PrintBoard(board, size, playerCordsX, playerCordsY, playBoard);
            Move(board, size, playerCordsX, playerCordsY, playBoard, mineCount, mistakes);
        }

        static void GenerateMines(int[,] board, int size, int mines, int[,] playBoard, out int mineCount)
        {

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    board[i, j] = 0;
                    playBoard[i, j] = -1;
                }
            }

            double totalMines = (size * size) * (mines / 100.0);
            mineCount = Convert.ToInt16(totalMines);
            Random random = new Random();
            for (int i = 0; i < mineCount; i++)
            {
                int x = random.Next(0, size);
                int y = random.Next(0, size);
                if (board[x, y] == 0)
                {
                    board[x, y] = 9;
                }
                else
                {
                    i--;
                }
            }
            CountMines(board, size);
        }
        static void CountMines(int[,] board, int size)
        {
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    int x1 = x - 1;
                    int y1 = y - 1;
                    int x2 = x + 1;
                    int y2 = y + 1;

                    if (board[x, y] == 9)
                    {
                        if (x1 > -1 && y1 > -1)
                        {
                            if (board[x1, y1] != 9)
                                board[x1, y1]++;
                        }

                        if (y1 > -1)
                        {
                            if (board[x, y1] != 9)
                                board[x, y1]++;
                        }

                        if (x2 < size && y1 > -1)
                        {
                            if (board[x2, y1] != 9)
                                board[x2, y1]++;
                        }

                        if (x1 > -1)
                        {
                            if (board[x1, y] != 9)
                                board[x1, y]++;
                        }

                        if (x2 < size)
                        {
                            if (board[x2, y] != 9)
                                board[x2, y]++;
                        }

                        if (x1 > -1 && y2 < size)
                        {
                            if (board[x1, y2] != 9)
                                board[x1, y2]++;
                        }

                        if (y2 < size)
                        {
                            if (board[x, y2] != 9)
                                board[x, y2]++;
                        }

                        if (x2 < size && y2 < size)
                        {
                            if (board[x2, y2] != 9)
                                board[x2, y2]++;
                        }

                    }
                }
            }
        }
        static void PrintBoard(int[,] board, int size, int playerCordsX, int playerCordsY, int[,] playBoard)
        {
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (playerCordsX == x && playerCordsY == y)
                        Console.BackgroundColor = ConsoleColor.Magenta;

                    if (playBoard[x, y] == -1)
                    {
                        Console.Write(" ");
                    }
                    else if (playBoard[x, y] == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("0");
                    }
                    else if (playBoard[x, y] == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("1");
                    }
                    else if (playBoard[x, y] == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("2");
                    }
                    else if (playBoard[x, y] == 3)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("3");
                    }
                    else if (playBoard[x, y] == 4)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.Write("4");
                    }
                    else if (playBoard[x, y] == 5)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("5");
                    }
                    else if (playBoard[x, y] == 6)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write("6");
                    }
                    else if (playBoard[x, y] == 7)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write("7");
                    }
                    else if (playBoard[x, y] == 8)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write("8");
                    }
                    else if (playBoard[x, y] == 9)
                    {
                        Console.Write("*");
                    }
                    Console.ResetColor();

                    if (y < size - 1)
                    {
                        Console.Write("|");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        static void Move(int[,] board, int size, int playerCordsX, int playerCordsY, int[,] playBoard, int mineCount, int mistakes)
        {
            int wrongFlag = 0, flagged = 0;
            int size1 = size - 1;
            while (true)
            {
                var ch = Console.ReadKey(false).Key;
                switch (ch)
                {
                    case ConsoleKey.UpArrow:
                        if (playerCordsX != 0)
                        {
                            playerCordsX -= 1;
                            //Console.WriteLine("up");
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (playerCordsX != size1)
                        {
                            playerCordsX += 1;
                            //Console.WriteLine("down");
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (playerCordsY != size1)
                        {
                            playerCordsY += 1;
                            //Console.WriteLine("right");
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (playerCordsY != 0)
                        {
                            playerCordsY -= 1;
                            //Console.WriteLine("left");
                        }
                        break;
                    case ConsoleKey.Spacebar:
                        //Console.WriteLine("space");
                        if (board[playerCordsX, playerCordsY] < 9)
                        {
                            if (playBoard[playerCordsX, playerCordsY] == 9)
                                wrongFlag--;
                            playBoard[playerCordsX, playerCordsY] = board[playerCordsX, playerCordsY];
                            WinGame(flagged, wrongFlag, mineCount, mistakes);
                            if (board[playerCordsX, playerCordsY] == 0)
                            {
                                revealZeros(playerCordsX, playerCordsY, board, size, playBoard);
                            }
                        }
                        else
                        {
                            EndGame(flagged, wrongFlag);
                            mistakes++;
                        }
                            
                        break;
                    case ConsoleKey.Enter:
                        //Console.WriteLine("Enter");
                        if (playBoard[playerCordsX, playerCordsY] == 9)
                        {
                            WinGame(flagged, wrongFlag, mineCount, mistakes);
                        }
                        else if (board[playerCordsX, playerCordsY] == 9)
                        {
                            flagged++;
                            WinGame(flagged, wrongFlag, mineCount, mistakes);
                        }
                        else
                            wrongFlag++;
                        playBoard[playerCordsX, playerCordsY] = 9;
                        break;
                }
                Console.Clear();
                PrintBoard(board, size, playerCordsX, playerCordsY, playBoard);
            }
        }
        static void revealZeros(int x, int y, int[,] board, int size, int[,] playBoard)
        {
            int[] checkNext = new int[10];
            checkNext[0] = x;
            checkNext[1] = y;

            for (int i = 0; i < 10; i += 2)
            {
                if (i != 0)
                {
                    x = checkNext[i];
                    y = checkNext[i + 1];
                }

                int x1 = x - 1;
                int y1 = y - 1;
                int x2 = x + 1;
                int y2 = y + 1;

                if (x1 > -1 && y1 > -1)
                {
                    if (board[x1, y1] == 0)
                    {
                        playBoard[x1, y1] = 0;
                        if (i == 0)
                        {
                            checkNext[2] = x1;
                            checkNext[3] = y1;
                        }
                    }
                    playBoard[x1, y1] = board[x1, y1];
                }

                if (y1 > -1)
                {
                    if (board[x, y1] == 0)
                    {
                        playBoard[x, y1] = 0;
                        if (i == 0)
                        {
                            //checkNext[2] = x;
                            //checkNext[3] = y1;
                        }
                    }
                    playBoard[x, y1] = board[x, y1];
                }

                if (x2 < size && y1 > -1)
                {
                    if (board[x2, y1] == 0)
                    {
                        playBoard[x2, y1] = 0;
                        if (i == 0)
                        {
                            checkNext[4] = x2;
                            checkNext[5] = y1;
                        }
                    }
                    playBoard[x2, y1] = board[x2, y1];
                }

                if (x1 > -1)
                {
                    if (board[x1, y] == 0)
                    {
                        playBoard[x1, y] = 0;
                        if (i == 0)
                        {
                            //checkNext[6] = x1;
                            //checkNext[7] = y;
                        }
                    }
                    playBoard[x1, y] = board[x1, y];
                }

                if (x2 < size)
                {
                    if (board[x2, y] == 0)
                    {
                        playBoard[x2, y] = 0;
                        if (i == 0)
                        {
                            //checkNext[6] = x2;
                            //checkNext[7] = y;
                        }
                    }
                    playBoard[x2, y] = board[x2, y];
                }

                if (x1 > -1 && y2 < size)
                {
                    if (board[x1, y2] == 0)
                    {
                        playBoard[x1, y2] = 0;
                        if (i == 0)
                        {
                            checkNext[6] = x1;
                            checkNext[7] = y2;
                        }
                    }
                    playBoard[x1, y2] = board[x1, y2];
                }

                if (y2 < size)
                {
                    if (board[x, y2] == 0)
                    {
                        playBoard[x, y2] = 0;
                        if (i == 0)
                        {
                            //checkNext[8] = x;
                            //checkNext[9] = y2;
                        }
                    }
                    playBoard[x, y2] = board[x, y2];
                }

                if (x2 < size && y2 < size)
                {
                    if (board[x2, y2] == 0)
                    {
                        playBoard[x2, y2] = 0;
                        if (i == 0)
                        {
                            checkNext[8] = x2;
                            checkNext[9] = y2;
                        }
                    }
                    playBoard[x2, y2] = board[x2, y2];
                }
            }
        }
        static void EndGame(int flagged, int wrongFlag)
        {
            Console.Clear();
            Console.WriteLine("Game Over");
            Console.WriteLine("Mines Found: " + flagged);
            Console.WriteLine("Misplaced flags: " + wrongFlag);
            Console.ReadLine();
            return;
        }
        static void WinGame(int flagged, int wrongFlag, int mineCount, int mistakes)
        {
            if (mineCount == flagged && wrongFlag == 0)
            {
                Console.WriteLine("You Win!");
                Console.WriteLine("mistakes: " + mistakes);
                Console.ReadLine();
                Environment.Exit(0);
            }
        }
    }
}
