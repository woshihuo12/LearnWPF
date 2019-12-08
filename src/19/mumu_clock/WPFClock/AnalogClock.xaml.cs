using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Media.Animation;


namespace WPFClock
{
    public interface IPosition
    {
        void SetDesiredWidthAndHeight(int width, int height);
    }
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AnalogClock : Page ,IPosition
    {
        System.Timers.Timer timer = new System.Timers.Timer(1000);

        public AnalogClock()
        {
            InitializeComponent();

            DateTime date = DateTime.Now;

            persianCalendar.Content = date.ToShortDateString();
            christianityCalendar.Content = date.ToShortTimeString();

            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            timer.Enabled = true;
        }
        public  void SetDesiredWidthAndHeight(int width,int height)
        {
            scale.CenterX = 0.5;
            scale.CenterY = 0.5;
            scale.ScaleX = width / (this.Width + 30);
            scale.ScaleY = width / (this.Height + 30);
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
           
            this.Dispatcher.Invoke(DispatcherPriority.Normal,
                 new Action(TimeChanged));
        }
        void TimeChanged()
        {
            secondHand.Angle = DateTime.Now.Second * 6;
            minuteHand.Angle = DateTime.Now.Minute * 6;
            hourHand.Angle = (DateTime.Now.Hour * 30) + (DateTime.Now.Minute * 0.5);
            persianCalendar.Content = DateTime.Now.ToShortDateString();
            christianityCalendar.Content = DateTime.Now.ToShortTimeString();
        }
    }
}
