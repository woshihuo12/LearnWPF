using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.Media3D.Converters;

namespace WPFClock
{
	public partial class DigitalClock : Page,IPosition
	{
        public DigitalClock()
        {
            System.Timers.Timer timer = new System.Timers.Timer(1000);
            InitializeComponent();

            DateTime date = DateTime.Now;
            TimeZone time = TimeZone.CurrentTimeZone;
            TimeSpan difference = time.GetUtcOffset(date);

            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            timer.Enabled = true;


        }
        public void SetDesiredWidthAndHeight(int width, int height)
        {
            scale.CenterX = 0.5;
            scale.CenterY = 0.5;
            scale.ScaleX = width / (this.Width + 30);
            scale.ScaleY = width / (this.Height + 30);
        }
        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
            {
                int Second01 = DateTime.Now.Second - (DateTime.Now.Second / 10) * 10;
                int Second10 = DateTime.Now.Second / 10;
                int Minute01 = DateTime.Now.Minute - (DateTime.Now.Minute / 10) * 10;
                int Minute10 = DateTime.Now.Minute / 10;
                int Hour01 = DateTime.Now.Hour - (DateTime.Now.Hour / 10) * 10;
                int Hour10 = DateTime.Now.Hour / 10;

                switch (Hour10)
                {
                    case 1:
                        GeometryModel3DGeometry_10h.Geometry = (Geometry3D)TryFindResource("Digit_1");
                        break;
                    case 2:
                        GeometryModel3DGeometry_10h.Geometry = (Geometry3D)TryFindResource("Digit_2");
                        break;
                    case 3:
                        GeometryModel3DGeometry_10h.Geometry = (Geometry3D)TryFindResource("Digit_3");
                        break;
                    case 4:
                        GeometryModel3DGeometry_10h.Geometry = (Geometry3D)TryFindResource("Digit_4");
                        break;
                    case 5:
                        GeometryModel3DGeometry_10h.Geometry = (Geometry3D)TryFindResource("Digit_5");
                        break;
                    case 6:
                        GeometryModel3DGeometry_10h.Geometry = (Geometry3D)TryFindResource("Digit_6");
                        break;
                    case 7:
                        GeometryModel3DGeometry_10h.Geometry = (Geometry3D)TryFindResource("Digit_7");
                        break;
                    case 8:
                        GeometryModel3DGeometry_10h.Geometry = (Geometry3D)TryFindResource("Digit_8");
                        break;
                    case 9:
                        GeometryModel3DGeometry_10h.Geometry = (Geometry3D)TryFindResource("Digit_9");
                        break;
                    case 0:
                        GeometryModel3DGeometry_10h.Geometry = (Geometry3D)TryFindResource("Digit_0");
                        break;
                }                
                switch (Hour01)
                {
                    case 1:
                        GeometryModel3DGeometry_1h.Geometry = (Geometry3D)TryFindResource("Digit_1");
                        break;
                    case 2:
                        GeometryModel3DGeometry_1h.Geometry = (Geometry3D)TryFindResource("Digit_2");
                        break;
                    case 3:
                        GeometryModel3DGeometry_1h.Geometry = (Geometry3D)TryFindResource("Digit_3");
                        break;
                    case 4:
                        GeometryModel3DGeometry_1h.Geometry = (Geometry3D)TryFindResource("Digit_4");
                        break;
                    case 5:
                        GeometryModel3DGeometry_1h.Geometry = (Geometry3D)TryFindResource("Digit_5");
                        break;
                    case 6:
                        GeometryModel3DGeometry_1h.Geometry = (Geometry3D)TryFindResource("Digit_6");
                        break;
                    case 7:
                        GeometryModel3DGeometry_1h.Geometry = (Geometry3D)TryFindResource("Digit_7");
                        break;
                    case 8:
                        GeometryModel3DGeometry_1h.Geometry = (Geometry3D)TryFindResource("Digit_8");
                        break;
                    case 9:
                        GeometryModel3DGeometry_1h.Geometry = (Geometry3D)TryFindResource("Digit_9");
                        break;
                    case 0:
                        GeometryModel3DGeometry_1h.Geometry = (Geometry3D)TryFindResource("Digit_0");
                        break;
                }

                switch (Minute10)
                {
                    case 1:
                        GeometryModel3DGeometry_10m.Geometry = (Geometry3D)TryFindResource("Digit_1");
                        break;
                    case 2:
                        GeometryModel3DGeometry_10m.Geometry = (Geometry3D)TryFindResource("Digit_2");
                        break;
                    case 3:
                        GeometryModel3DGeometry_10m.Geometry = (Geometry3D)TryFindResource("Digit_3");
                        break;
                    case 4:
                        GeometryModel3DGeometry_10m.Geometry = (Geometry3D)TryFindResource("Digit_4");
                        break;
                    case 5:
                        GeometryModel3DGeometry_10m.Geometry = (Geometry3D)TryFindResource("Digit_5");
                        break;
                    case 6:
                        GeometryModel3DGeometry_10m.Geometry = (Geometry3D)TryFindResource("Digit_6");
                        break;
                    case 7:
                        GeometryModel3DGeometry_10m.Geometry = (Geometry3D)TryFindResource("Digit_7");
                        break;
                    case 8:
                        GeometryModel3DGeometry_10m.Geometry = (Geometry3D)TryFindResource("Digit_8");
                        break;
                    case 9:
                        GeometryModel3DGeometry_10m.Geometry = (Geometry3D)TryFindResource("Digit_9");
                        break;
                    case 0:
                        GeometryModel3DGeometry_10m.Geometry = (Geometry3D)TryFindResource("Digit_0");
                        break;
                }
                switch (Minute01)
                {
                    case 1:
                        GeometryModel3DGeometry_1m.Geometry = (Geometry3D)TryFindResource("Digit_1");
                        break;
                    case 2:
                        GeometryModel3DGeometry_1m.Geometry = (Geometry3D)TryFindResource("Digit_2");
                        break;
                    case 3:
                        GeometryModel3DGeometry_1m.Geometry = (Geometry3D)TryFindResource("Digit_3");
                        break;
                    case 4:
                        GeometryModel3DGeometry_1m.Geometry = (Geometry3D)TryFindResource("Digit_4");
                        break;
                    case 5:
                        GeometryModel3DGeometry_1m.Geometry = (Geometry3D)TryFindResource("Digit_5");
                        break;
                    case 6:
                        GeometryModel3DGeometry_1m.Geometry = (Geometry3D)TryFindResource("Digit_6");
                        break;
                    case 7:
                        GeometryModel3DGeometry_1m.Geometry = (Geometry3D)TryFindResource("Digit_7");
                        break;
                    case 8:
                        GeometryModel3DGeometry_1m.Geometry = (Geometry3D)TryFindResource("Digit_8");
                        break;
                    case 9:
                        GeometryModel3DGeometry_1m.Geometry = (Geometry3D)TryFindResource("Digit_9");
                        break;
                    case 0:
                        GeometryModel3DGeometry_1m.Geometry = (Geometry3D)TryFindResource("Digit_0");
                        break;
                }


                switch (Second10)
                {
                    case 1:
                        GeometryModel3DGeometry_10s.Geometry = (Geometry3D)TryFindResource("Digit_1");
                        break;
                    case 2:
                        GeometryModel3DGeometry_10s.Geometry = (Geometry3D)TryFindResource("Digit_2");
                        break;
                    case 3:
                        GeometryModel3DGeometry_10s.Geometry = (Geometry3D)TryFindResource("Digit_3");
                        break;
                    case 4:
                        GeometryModel3DGeometry_10s.Geometry = (Geometry3D)TryFindResource("Digit_4");
                        break;
                    case 5:
                        GeometryModel3DGeometry_10s.Geometry = (Geometry3D)TryFindResource("Digit_5");
                        break;
                    case 6:
                        GeometryModel3DGeometry_10s.Geometry = (Geometry3D)TryFindResource("Digit_6");
                        break;
                    case 7:
                        GeometryModel3DGeometry_10s.Geometry = (Geometry3D)TryFindResource("Digit_7");
                        break;
                    case 8:
                        GeometryModel3DGeometry_10s.Geometry = (Geometry3D)TryFindResource("Digit_8");
                        break;
                    case 9:
                        GeometryModel3DGeometry_10s.Geometry = (Geometry3D)TryFindResource("Digit_9");
                        break;
                    case 0:
                        GeometryModel3DGeometry_10s.Geometry = (Geometry3D)TryFindResource("Digit_0");
                        break;
                }
                switch (Second01)
                {
                    case 1:
                        GeometryModel3DGeometry_1s.Geometry = (Geometry3D)TryFindResource("Digit_1");
                        break;
                    case 2:
                        GeometryModel3DGeometry_1s.Geometry = (Geometry3D)TryFindResource("Digit_2");
                        break;
                    case 3:
                        GeometryModel3DGeometry_1s.Geometry = (Geometry3D)TryFindResource("Digit_3");
                        break;
                    case 4:
                        GeometryModel3DGeometry_1s.Geometry = (Geometry3D)TryFindResource("Digit_4");
                        break;
                    case 5:
                        GeometryModel3DGeometry_1s.Geometry = (Geometry3D)TryFindResource("Digit_5");
                        break;
                    case 6:
                        GeometryModel3DGeometry_1s.Geometry = (Geometry3D)TryFindResource("Digit_6");
                        break;
                    case 7:
                        GeometryModel3DGeometry_1s.Geometry = (Geometry3D)TryFindResource("Digit_7");
                        break;
                    case 8:
                        GeometryModel3DGeometry_1s.Geometry = (Geometry3D)TryFindResource("Digit_8");
                        break;
                    case 9:
                        GeometryModel3DGeometry_1s.Geometry = (Geometry3D)TryFindResource("Digit_9");
                        break;
                    case 0:
                        GeometryModel3DGeometry_1s.Geometry = (Geometry3D)TryFindResource("Digit_0");
                        break;
                }

            }));
        }

        private void GeometryModel3D_1sChanged(object sender, EventArgs e)
        {
            Storyboard Storyboard1s = (Storyboard)TryFindResource("Storyboard_1s");
            Storyboard1s.Begin();

        }

        private void GeometryModel3D_10sChanged(object sender, EventArgs e)
        {
            Storyboard Storyboard10s = (Storyboard)TryFindResource("Storyboard_10s");
            Storyboard10s.Begin();
        }

        private void GeometryModel3D_1mChanged(object sender, EventArgs e)
        {
            Storyboard Storyboard1m = (Storyboard)TryFindResource("Storyboard_1m");
            Storyboard1m.Begin();
        }

        private void GeometryModel3D_10mChanged(object sender, EventArgs e)
        {
            Storyboard Storyboard10m = (Storyboard)TryFindResource("Storyboard_10m");
            Storyboard10m.Begin();
        }

        private void GeometryModel3D_1hChanged(object sender, EventArgs e)
        {
            Storyboard Storyboard1h = (Storyboard)TryFindResource("Storyboard_1h");
            Storyboard1h.Begin();
        }

        private void GeometryModel3D_10hChanged(object sender, EventArgs e)
        {
            Storyboard Storyboard10h = (Storyboard)TryFindResource("Storyboard_10h");
            Storyboard10h.Begin();
        }


     

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.Cursor = Cursors.Hand;
            
            this.Cursor = Cursors.Arrow;
        }

        private void WinSizeChanded(object sender, SizeChangedEventArgs e)
        {
            ClockGrid.Width = (Window.Width);
            ClockGrid.Height = 0.9 * (Window.Height); 
            viewport3D1.Width = (Window.Width);
            viewport3D1.Height = 0.9*(Window.Height);
        }

        private void TitleLabelMouseEnter(object sender, MouseEventArgs e)
        {
            SolidColorBrush mySolidColorBrush3 = new SolidColorBrush(Colors.Black);
            // TitleLabel.Foreground = mySolidColorBrush3;
            mySolidColorBrush3.Opacity = 1;
            // TitleLabel.Opacity = 1;
        }
        private void TitleLabelMouseLeave(object sender, MouseEventArgs e)
        {
            SolidColorBrush mySolidColorBrush3 = new SolidColorBrush(Colors.Transparent);
            // TitleLabel.Background = mySolidColorBrush3;
            mySolidColorBrush3.Opacity = 0.5;
            // TitleLabel.Opacity = 0.5;
        }

        private void Shadow_0(object sender, RoutedEventArgs e)
        {
            DigitDropShadow.ShadowDepth = 0;
        }
        private void Shadow_1(object sender, RoutedEventArgs e)
        {
            DigitDropShadow.ShadowDepth = 1;
        }
        private void Shadow_2(object sender, RoutedEventArgs e)
        {
            DigitDropShadow.ShadowDepth = 2;
        }
        private void Shadow_3(object sender, RoutedEventArgs e)
        {
            DigitDropShadow.ShadowDepth = 3;
        }
        private void Shadow_4(object sender, RoutedEventArgs e)
        {
            DigitDropShadow.ShadowDepth = 4;
        }
        private void Shadow_5(object sender, RoutedEventArgs e)
        {
            DigitDropShadow.ShadowDepth = 5;
        }



    }

}