namespace Snake
{
    public readonly struct Pixel
    {
        public int X { get; }
        public int Y { get; }

        private const char _pix = '█';
        public readonly int Size;
        public ConsoleColor Color { get; }

        public Pixel(int x, int y, ConsoleColor color, int size = 3)
        {
            X = x;
            Y = y;
            Color = color;
            Size = size;
        }

        public void Draw()
        {
            Console.ForegroundColor = Color;
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    Console.SetCursorPosition(X * Size + x, Y * Size + y);
                    Console.Write(_pix);
                }
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        public void Clear()
        {
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    Console.SetCursorPosition(X * Size + x, Y * Size + y);
                    Console.Write(' ');
                }
            }
        }
    }
}