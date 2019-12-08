using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TryRainbowAttempt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // 一个颜色的数组
        System.Drawing.Color[] colors = { System.Drawing.Color.Red, System.Drawing.Color.Orange, 
                    System.Drawing.Color.Yellow, System.Drawing.Color.Green, System.Drawing.Color.Blue, 
                    System.Drawing.Color.Indigo, System.Drawing.Color.Violet };

        // OnPaint函数负责处理WM_PAINT消息，在WinForm窗口里面进行绘制
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // rect为所绘制的区域
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, this.Size.Width / colors.Length, this.Size.Height);
            // myBrush为填充的画刷
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            // GDI+绘制的图形句柄，类似于GDI当中的HDC
            System.Drawing.Graphics formGraphics;

            formGraphics = this.CreateGraphics();
            // 平均分成七列 进行循环绘制
            foreach (Color color in colors)
            {
                myBrush.Color = color;
                formGraphics.FillRectangle(myBrush, rect);
                rect.X += this.Size.Width / colors.Length;
            }

            myBrush.Dispose();
            formGraphics.Dispose();
        }
    }
}
