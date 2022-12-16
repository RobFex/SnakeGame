using System;
using System.Diagnostics;
namespace Snake
{
    internal class Program
    {
        private const int MapWidth = 30;
        private const int MapHeight = 20;

        private const int ScreenWidth = MapWidth * 3;
        private const int ScreenHeight = MapHeight * 3;

        private const ConsoleColor Border = ConsoleColor.Gray;
        private const ConsoleColor HeadColor = ConsoleColor.Black;
        private const ConsoleColor BodyColor = ConsoleColor.DarkGreen;
        private const ConsoleColor FoodColor = ConsoleColor.Green;

        private const int FrameMs = 200;

        private static int bestScore = 0;

        private static readonly Random random = new Random();
        static void Main()
        {
            Console.SetWindowSize(ScreenWidth, ScreenHeight);
            Console.SetBufferSize(ScreenWidth, ScreenHeight);
            Console.CursorVisible = false;
            StartGame();
            Console.ReadKey();
        }
        static void StartGame()
        {
            DrawBorder();

            Direction currentMove = Direction.Right;
            Snake snake = new Snake(10, 5, HeadColor, BodyColor);
            Stopwatch sw = new Stopwatch();

            Pixel food = GenFood(snake);
            food.Draw();

            int score = 0;
            int lagMs = 0;

            while (true)
            {
                sw.Restart();
                Direction oldMove = currentMove;
                while (sw.ElapsedMilliseconds <= FrameMs - lagMs)
                {
                    if (currentMove == oldMove)
                        currentMove = ReadMovement(currentMove);
                }

                sw.Restart();

                if (snake.Head.X == food.X && snake.Head.Y == food.Y)
                {
                    snake.Move(currentMove, true);
                    food = GenFood(snake);
                    food.Draw();
                    score++;

                    Task.Run(() => Console.Beep(1200, 200));
                }

                else
                {
                    snake.Move(currentMove);
                }

                if (snake.Head.X == MapWidth - 1
                    || snake.Head.X == 0
                    || snake.Head.Y == MapHeight - 1
                    || snake.Head.Y == 0
                    || snake.Body.Any(part => part.X == snake.Head.X && part.Y == snake.Head.Y))
                {
                    break;
                }
                lagMs = (int)sw.ElapsedMilliseconds;
            }
            snake.Clear();
            Console.SetCursorPosition(ScreenWidth / 5 * 2 + 5, ScreenHeight / 5 * 2 + 5);
            Console.WriteLine("Game Over");
            Console.SetCursorPosition(ScreenWidth / 5 * 2 + 5, ScreenHeight / 5 * 2 + 6);
            Console.WriteLine("Scores: {0}", score);
            if (score > bestScore)
            {
                bestScore = score;
            }
            Console.SetCursorPosition(ScreenWidth / 5 * 2 + 5, ScreenHeight / 5 * 2 + 7);
            Console.WriteLine("Best score: {0}", bestScore);
            Task.Run(() => Console.Beep(200, 600));
        }
        static Pixel GenFood(Snake snake)
        {
            Pixel food;
            do
            {
                food = new Pixel(random.Next(1, MapWidth - 2), random.Next(1, MapHeight - 2), FoodColor);
            }
            while (snake.Head.X == food.X && snake.Head.Y == food.Y
            || snake.Body.Any(part => part.X == food.X && part.Y == food.Y));
            return food;
        }
        static Direction ReadMovement(Direction currentDir)
        {
            if (!Console.KeyAvailable)
                return currentDir;

            ConsoleKey key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (currentDir != Direction.Down)
                        currentDir = Direction.Up;
                    return currentDir;
                case ConsoleKey.DownArrow:
                    if (currentDir != Direction.Up)
                        currentDir = Direction.Down;
                    return currentDir;
                case ConsoleKey.RightArrow:
                    if (currentDir != Direction.Left)
                        currentDir = Direction.Right;
                    return currentDir;
                case ConsoleKey.LeftArrow:
                    if (currentDir != Direction.Right)
                        currentDir = Direction.Left;
                    return currentDir;
            }
            return currentDir;
        }
        static void DrawBorder()
        {
            for (int x = 0; x < MapWidth; x++)
            {
                Pixel pxOne = new Pixel(x, 0, Border);
                pxOne.Draw();
                Pixel pxTwo = new Pixel(x, MapHeight - 1, Border);
                pxTwo.Draw();
            }

            for (int y = 0; y < MapHeight; y++)
            {
                Pixel pxOne = new Pixel(0, y, Border);
                pxOne.Draw();
                Pixel pxTwo = new Pixel(MapWidth - 1, y, Border);
                pxTwo.Draw();
            }
        }
    }
}