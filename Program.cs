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
    static int[,] I_Piece = new int[,] {{1,1,1,1,}}; // Horizontal line
    static int[,] O_Piece = new int[,] {{1,1}, {1,1}}; // Squere
    static int[,] T_Piece = new int[,] {{0,1,0}, {1,1,1}}; // T-shape

    static void Main()
    {
        InitializeBoard();

        int[,] currentPiece = T_Piece;


        //Game Loop
        while (isRunning)
        {
            Update();
            Render(currentPiece);
            Thread.Sleep(100); // Controls the speed of the loop (100 ms per frame)
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
        // Check if a key is pressed
        if (Console.KeyAvailable)
        {
            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.Q)
            {
                isRunning = false; // Stop the game loop
            }
        }
    }

   
    static void Render(int[,] currentPiece)
    {
        Console.Clear();

        // Render the board
        for (int row =0; row < height; row++)
        {
            for (int col =0; col < width; col++)
            {
                if (board[row, col] == 0)
                {
                    Console.Write(".");
                }
                else
                {
                    Console.Write("#");
                }
            }
            Console.WriteLine();
        }

        RenderPiece(currentPiece, 0, 0);


        Console.WriteLine("\nPress 'Q' to quit the game.");
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
