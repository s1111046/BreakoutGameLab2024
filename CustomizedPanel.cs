using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timer = System.Windows.Forms.Timer;

namespace BreakoutGameLab001
{
    class CustomizedPanel : Panel
    {
        // 定義 Timer 控制項
        private Timer timer = new Timer();

        public CustomizedPanel(int width, int height)
        {
            // this.DoubleBuffered = true;
            this.BackColor = Color.Yellow;
            this.Size = new Size(width, height);
        }

        public void Initialize()
        {
            // Timer : 每 20 毫秒觸發一次 Timer_Tick 事件 ==> 更新遊戲畫面
            // 也可以利用 Thread 類別來實現 類似的功能!!
            timer.Interval = 20;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // 更新遊戲狀態
            // 移動球的位置, 檢查碰撞事件 ...

            // 要求遊戲畫面重繪
            Invalidate(); // --> 觸發 OnPaint 事件
        }

        // 處理畫面的重繪事件
        protected override void OnPaint(PaintEventArgs e)
        {
            // 呼叫基底類別的 OnPaint 方法 --> 這樣才能正確繪製 Panel 控制項
            base.OnPaint(e);

            // 取得 Graphics 物件
            Graphics gr = e.Graphics;

            // 繪製遊戲畫面
            gr.FillEllipse(new SolidBrush(Color.Red), 100, 100, 50, 50);
        }
    }
}
