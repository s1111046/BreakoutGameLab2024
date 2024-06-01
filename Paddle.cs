namespace BreakoutGameLab001
{
    // 擋板類別
    class Paddle
    {
        // 屬性
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Color Color { get; set; }

        // 建構子
        public Paddle(int x, int y, int width, int height, Color color)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Color = color;
        }

        // 繪製擋板
        internal void Draw(Graphics gr)
        {
            gr.FillRectangle(new SolidBrush(Color.Blue), X, Y, Width, Height);
        }

        // 左右移動擋板，并確保擋板不會超出遊戲區域
        public void Move(int vx, int gameWidth)
        {
            X += vx;

            // 確保擋板不會超出遊戲區域的左邊界
            if (X < 0)
            {
                X = 0;
            }
            // 確保擋板不會超出遊戲區域的右邊界
            else if (X + Width > gameWidth)
            {
                X = gameWidth - Width;
            }
        }
    }
}
