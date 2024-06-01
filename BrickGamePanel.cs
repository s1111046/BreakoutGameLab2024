using System;
using System.Collections.Generic;
using System.Drawing; // 确保使用正确的命名空间
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timer = System.Windows.Forms.Timer;

namespace BreakoutGameLab001
{
    internal class BrickGamePanel : Panel
    {
        // 定義遊戲元件
        private List<Ball> balls = new List<Ball>();
        private Paddle paddle;
        private List<Brick> bricks;
        // 定義 Timer 控制項
        private Timer timer = new Timer();
        private bool isGameOver = false;
        private bool isWin = false;
        private Action<string> onGameOver; // 更改为带参数的委托

        public BrickGamePanel(int width, int height)
        {
            this.DoubleBuffered = true;
            this.BackColor = Color.Yellow;
            this.Size = new Size(width, height);
        }

        public void Initialize(Action<string> onGameOverCallback = null)
        {
            onGameOver = onGameOverCallback;

            // 初始化遊戲元件
            balls.Add(new Ball(Width / 2, Height / 2, 15, 3, -3, Color.Red));
            //balls.Add(new Ball(Width / 3, Height / 2, 30, -2, -2, Color.Green));
            //balls.Add(new Ball(Width / 2, Height / 3, 10, -10, 10, Color.Blue));

            paddle = new Paddle(Width / 2 - 50, Height - 50, 120, 20, Color.Blue);

            bricks = new List<Brick>();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    bricks.Add(new Brick(25 + j * 80, 25 + this.Location.Y + i * 30, 80, 30, Color.Green));
                }
            }

            // 設定遊戲的背景控制流程: 每 20 毫秒觸發一次 Timer_Tick 事件 ==> 更新遊戲畫面
            timer.Interval = 20;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!isGameOver && !isWin)
            {
                // 定時移動球的位置, 檢查碰撞事件
                foreach (Ball ball in balls)
                {
                    ball.Move(0, 61, Width, Height);
                    ball.CheckCollision(paddle, bricks, out isGameOver, out isWin);
                }

                // 重繪遊戲畫面
                Invalidate(); // --> 觸發 OnPaint 事件
            }

            if (isGameOver || isWin)
            {
                timer.Stop();
                if (isGameOver)
                {
                    onGameOver?.Invoke("遊戲結束！");
                }
                else if (isWin)
                {
                    onGameOver?.Invoke("恭喜清空了！");
                }
            }
        }

        // 處理畫面的重繪事件
        protected override void OnPaint(PaintEventArgs e)
        {
            // 呼叫基底類別的 OnPaint 方法 --> 這樣才能正確繪製 Panel 控制項
            base.OnPaint(e);

            // 取得 Graphics 物件
            Graphics gr = e.Graphics;

            // 繪製球、擋板
            foreach (Ball ball in balls)
            {
                ball.Draw(gr);
            }

            paddle.Draw(gr);

            // 繪製磚塊
            foreach (Brick brick in bricks)
            {
                brick.Draw(gr);
            }
        }

        // 左移動擋板
        public void paddleMoveLeft()
        {
            paddle.Move(-30, Width);
        }

        // 右移動擋板
        public void paddleMoveRight()
        {
            paddle.Move(30, Width);
        }
    }
}
