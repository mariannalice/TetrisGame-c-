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
    static void Main()
    {
        //Game Loop
        while (isRunning)
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

            Update();
            Render();
            Thread.Sleep(100); // Controls the speed of the loop (100 ms per frame)
        }

        Console.WriteLine("Game over. Thanks for playing! :)");
    }

    static void Update()
    {
       
    }

    static void Render()
    {
        // This is where we'll render the game screen
        Console.Clear();
        Console.WriteLine("Tetris Game");
        Console.WriteLine("Press 'Q' to quit.");
    }
}
