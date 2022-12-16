namespace Snake
{
    public class Snake
    {
        private readonly ConsoleColor _headColor;
        private readonly ConsoleColor _bodyColor;
        public Pixel Head { get; private set; }
        public Queue<Pixel> Body { get; private set; } = new Queue<Pixel>();

        public Snake(
            int initX,
            int initY,
            ConsoleColor headColor,
            ConsoleColor bodyColor,
            int bodyLength = 3)
        {
            _headColor = headColor;
            _bodyColor = bodyColor;
            Head = new Pixel(initX, initY, _headColor);
            for (int i = bodyLength; i >= 0; i--)
            {
                Body.Enqueue(new Pixel(Head.X - i - 1, initY, _bodyColor));
            }
            Draw();
        }
        
        public void Move(Direction dir, bool isEating = false)
        {
            Clear();

            Body.Enqueue(new Pixel(Head.X, Head.Y, _bodyColor));
            if (!isEating)
                Body.Dequeue();

            switch (dir)
            {
                case Direction.Up:
                    Head = new Pixel(Head.X, Head.Y - 1, _headColor);
                    break;
                case Direction.Down:
                    Head = new Pixel(Head.X, Head.Y + 1, _headColor);
                    break;
                case Direction.Left:
                    Head = new Pixel(Head.X - 1, Head.Y, _headColor);
                    break;
                case Direction.Right:
                    Head = new Pixel(Head.X + 1, Head.Y, _headColor);
                    break;
                default:
                    Head = Head;
                    break;
            }
            Draw();
        }
        public void Draw()
        {
            Head.Draw();
            foreach (Pixel px in Body)
            {
                px.Draw();
            }
        }
        public void Clear()
        {
            Head.Clear();
            foreach (Pixel px in Body)
            {
                px.Clear();
            }
        }
    }
}