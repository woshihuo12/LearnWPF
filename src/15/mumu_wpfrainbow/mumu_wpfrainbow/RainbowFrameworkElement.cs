using System;
using System.Windows;
using System.Windows.Media;

namespace mumu_wpfrainbow
{
    public class RainbowFrameworkElement : FrameworkElement
    {
        // 颜色数组 在WinForm里使用的是System.Drawing.Color
        //          在WPF里使用的是System.Windows.Media.Color
        Color[] colors = { Colors.Red, Colors.Orange, 
                    Colors.Yellow, Colors.Green, Colors.Blue, 
                    Colors.Indigo, Colors.Violet };


        protected override void OnRender(DrawingContext dc)
        {
            // myBrush为填充的画刷 在WinForm里使用的是System.Drawing.SolidBrush
            //                     在WPF里使用的是System.Windows.Media.SolidColorBrush
            // SolidColorBrush myBrush = new SolidColorBrush();

            // rect为所绘制的区域 在WinForm里使用的是System.Drawing.Rectangle
            //                    在WPF里使用的是System.Windows.Rect
            Rect rect = 
                new Rect(0, 0, RenderSize.Width / colors.Length, 
                               RenderSize.Height);

            // 平均分成七列 进行循环绘制
            foreach (Color color in colors)
            {
                SolidColorBrush myBrush = new SolidColorBrush();
                myBrush.Color = color;
                dc.DrawRectangle(myBrush, null, rect);
                rect.X += RenderSize.Width / colors.Length;
            }
        }
    }
}

