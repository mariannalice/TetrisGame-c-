﻿using System.Net;
using System.Runtime.InteropServices.Marshalling;

namespace Tetris_c_;

class Program
{
    // Board dimensions
    static int width = 10;
    static int height = 20;

    // 2D array to represent the Tetris board
    static int[,] board = new int[height, width];
    static bool isRunning = true;

    // Define pieces as 2D arrays
   // static int[,] I_Piece = new int[,] {{1,1,1,1,}}; // Horizontal line
   // static int[,] O_Piece = new int[,] {{1,1}, {1,1}}; // Squere
    static int[,] T_Piece = new int[,] {{0,1,0}, {1,1,1}}; // T-shape

    // Current piece and position 
    static int [,]currentPiece;
    static int pieceX = 4; // Horizontal position (middle of the board)
    static int pieceY = 0; // Vertical position ( Top of board)

    static void Main()
    {
        InitializeBoard();
        currentPiece = T_Piece;


        //Game Loop
        while (isRunning)
        {
            Update();
            Render();
            Thread.Sleep(500); // Game speed (gravity)
        }

        Console.WriteLine("Game over. Thanks for playing! :)");
    }

    static void InitializeBoard()
    {
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                board[row, col] = 0;
            }
        }
    }

    static void Update()
    {
        HandleInput();

        if(CanMove(currentPiece, pieceX, pieceY + 1))
        {
            pieceY++; // Move piece down 
        }
        else
        {
            // Lock the piece into the board when it can't move down
            LockPiece();
            SpawnNewPiece();
        }
    }

    static void HandleInput()
    {
        if(Console.KeyAvailable)
        {
            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.LeftArrow && CanMove(currentPiece, pieceX - 1, pieceY))
            {
                pieceX--; // Move left
            }
            else if (key == ConsoleKey.RightArrow && CanMove(currentPiece, pieceX + 1, pieceY))
            {
                pieceX++; // Move right
            }
            else if (key == ConsoleKey.Q)
            {
                isRunning = false; // Quite game
            }
        }
    }

   
    static void Render()
    {
        Console.Clear();

        // Render the board
        for (int row =0; row < height; row++)
        {
            for (int col =0; col < width; col++)
            {
                if (board[row, col] == 1)
                {
                    Console.Write("#");
                }
                else if(IsPieceAt(row, col))
                {
                    Console.Write("#"); // Current piece
                }
                else
                {
                    Console.Write(".");
                }
            }
            Console.WriteLine();
        }

        Console.WriteLine("\nPress 'Q' to quit the game.");
        
    }

    static bool IsPieceAt(int row, int col)
    {
        for (int pieceRow = 0; pieceRow < currentPiece.GetLength(0); pieceRow++)
        {
            for (int pieceCol = 0; pieceCol < currentPiece.GetLength(1); pieceCol++)
            {
                if (currentPiece[pieceRow, pieceCol] == 1)
                {
                    int boardRow = pieceY + pieceRow;
                    int boardCol = pieceX + pieceCol;

                    if (boardRow == row && boardCol == col)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    static bool CanMove(int [,] piece, int newX, int newY)
    {
        for (int row = 0; row < piece.GetLength(0); row++)
        {
            for (int col = 0; col < piece.GetLength(1); col++)
            {
                if (piece[row, col] == 1)
                {
                    int boardRow = newY + row;
                    int boardCol = newX + col;

                    // Check for bouanderies and collisions
                    if (boardRow >= height || boardCol < 0 || boardCol >= width || board[boardRow, boardCol] == 1)
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    static void LockPiece()
    {
        for (int row = 0; row < currentPiece.GetLength(0); row++)
        {
            for (int col = 0; col < currentPiece.GetLength(1); col++)
            {
                if (currentPiece[row, col] == 1)
                {
                    board [pieceY + row, pieceX + col] + 1;
                }
            }
        }
    }

    static void SpawnNewPiece()
    {
        currentPiece = T_Piece;
        pieceX = 4;
        pieceY = 0;

        if (!CanMove(currentPiece, pieceX, pieceY))
        {
            isRunning = false;
        }
    }

    static void RenderPiece(int[,] piece, int offsetX, int offsetY)
    {
        for(int row = 0; row < piece.GetLength(0); row++)
        {
            for(int col = 0; col < piece.GetLength(1); col++)
            {
                if(piece [row, col] == 1)
                {
                    int boardRow = row + offsetY;
                    int boardCol = col + offsetX;

                    if(boardRow < height && boardCol > width)
                    {
                        board[boardRow,boardCol] =1;
                    }
                }
            }
        }
    }

}
