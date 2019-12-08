using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
namespace mumu_windowcs
{
    public class MyWindow : Window
    {
        [STAThread]
        public static void Main()
        {
            MyWindow win = new MyWindow();
            win.Show();

            Application app = new Application();
            app.Run();
        }
        public MyWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Title = "XAML窗口";
            this.Width = 300;
            this.Height = 200;
            DockPanel panel = new DockPanel();

            Button btn = new Button();
            btn.Content = "Hello XAML";
            btn.Background = new SolidColorBrush(Colors.AliceBlue);
            btn.Margin = new Thickness(0, 5, 0, 10);
            DockPanel.SetDock(btn, Dock.Left);
            panel.Children.Add(btn);

            Button btn2 = new Button();
            btn2.Content = "Hello XAML";
            LinearGradientBrush brush = new LinearGradientBrush();
            brush.StartPoint = new Point(0, 0);
            brush.EndPoint = new Point(1, 1);

            brush.GradientStops.Add(new GradientStop(Colors.Yellow, 0));
            brush.GradientStops.Add(new GradientStop(Colors.Red, 0.25));
            brush.GradientStops.Add(new GradientStop(Colors.Blue, 0.75));
            brush.GradientStops.Add(new GradientStop(Colors.LimeGreen, 1));
            btn2.Background = brush;
            DockPanel.SetDock(btn2, Dock.Right);
            panel.Children.Add(btn2);
            this.Content = panel;

        }
    }
}
