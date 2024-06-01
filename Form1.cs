using System.Reflection.Metadata;

namespace BreakoutGameLab001
{
    public partial class Form1 : Form
    {
        // 遊戲面板控制項
        BrickGamePanel gamePanel;

        public Form1()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            // 移除 測試用 panel2 控制項
            Controls.Remove(panel2);

            gamePanel = new BrickGamePanel(panel2.Width, panel2.Height);
            gamePanel.Dock = DockStyle.Fill;
            gamePanel.Location = new Point(0, 61);
            gamePanel.Name = "BrickoutGamePanel";
            gamePanel.Size = new Size(panel2.Width, panel2.Height);
            gamePanel.TabIndex = 1;

            gamePanel.Initialize(OnGameOver);

            Controls.Add(gamePanel);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    gamePanel.paddleMoveLeft();
                    break;
                case Keys.Right:
                    gamePanel.paddleMoveRight();
                    break;
                default:
                    break;
            }
        }

        // 在遊戲結束時顯示消息並關閉遊戲
        private void OnGameOver(string message)
        {
            MessageBox.Show(message, "游戲結束！");
            Close();
        }
    }
}
