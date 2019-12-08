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

using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using _3DTools;

namespace EarthDemo
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

            CompositionTarget.Rendering += new EventHandler(CompositionTarget_Rendering);

        }
        private bool isstop = false;
        void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            YRotate.Angle++;
            if (YRotate.Angle > 360)
                YRotate.Angle = 0;
            if (isstop)
            {
                CompositionTarget.Rendering -= new EventHandler(CompositionTarget_Rendering);
                isstop = true;
            }
        }

        // private bool isstop = false;
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.WindowStyle = WindowStyle.SingleBorderWindow;
                menu.Background = new SolidColorBrush( SystemColors.ControlColor);
                //MenuItem menuitem = (MenuItem)menu.Items[0];
                //int count  = menuitem.Items.Count;
                //for (int i = 0; i < count; i++)
                //{
                //    MenuItem item = (MenuItem)menuitem.Items[i];
                //    item.Background = new SolidColorBrush(SystemColors.ControlColor);
                //}

            }
            else if (e.Key == Key.F1)
            {
                this.WindowStyle = WindowStyle.None;
                menu.Background = Brushes.Transparent;
                //MenuItem menuitem = (MenuItem)menu.Items[0];
                //int count = menuitem.Items.Count;
                
                //for (int i = 0; i < count; i++)
                //{
                //    MenuItem item = (MenuItem)menuitem.Items[i];
                //    item.Background = Brushes.Transparent;
                //}
            }
            else if (e.Key == Key.S)
            {
                if (isstop)
                {
                    CompositionTarget.Rendering += new EventHandler(CompositionTarget_Rendering);
                    isstop = false;
                }
                else isstop = true;

                //if (isstop)
                //    rotatestory.Resume(this);
                //else
                //    rotatestory.Pause(this);

                //isstop = !isstop;
            }
        }

        private void earthmodel_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            double z = cam.Position.Z;

            if (z > 100)
            {
                z = 99;
                cam.Position = new Point3D(0, 0, z);
                return;
            }
            if (z < 4)
            {
                z = 5;
                cam.Position = new Point3D(0, 0, z);
                return;
            }
            z = z - (double)(e.Delta/60);
            cam.Position = new Point3D(0, 0, z);
            
            //if(e.Delta 
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            NameScope.SetNameScope(this, new NameScope());
            this.RegisterName("YRotate", YRotate);
            this.RegisterName("earthoffset", earthoffset);
            this.RegisterName("cam", cam);
        }

        private void Storyboard_Completed(object sender, EventArgs e)
        {
            cam.Position = new Point3D(0, 0, 7);
            
        }

        private void earthmodel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show("EarthDemo");
        }

       

    }
}
