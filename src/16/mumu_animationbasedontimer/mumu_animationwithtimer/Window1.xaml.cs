using System;
using System.Windows;
using System.Windows.Threading;


namespace mumu_animationwithtimer
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private double maxWidth = 300;
        private double startWidth = 0;
        public Window1()
        {
            InitializeComponent();
            startWidth = rectangle.Width;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DispatcherTimer tmr = new DispatcherTimer();
            tmr.Interval = TimeSpan.FromSeconds(0.1);
            tmr.Tick += TimerOnTick;
            tmr.Start();
            btn.IsEnabled = false;
        }

        void TimerOnTick(object sender, EventArgs args)
        {
            rectangle.Width += 10;
            if (rectangle.Width >= maxWidth)
            {
                rectangle.Width = startWidth;
                (sender as DispatcherTimer).Stop();
                btn.IsEnabled = true;
            }
        }

    }
}
