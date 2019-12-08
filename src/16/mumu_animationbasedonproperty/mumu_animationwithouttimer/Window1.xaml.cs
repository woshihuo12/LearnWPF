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
namespace mumu_animationwithouttimer
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation doubleanimation = new DoubleAnimation();
            doubleanimation.From = 30;
            doubleanimation.To = 300;
            // doubleanimation.Duration = TimeSpan.FromSeconds(2.7);
            doubleanimation.Duration = TimeSpan.Parse("0:0:2.7");
            doubleanimation.FillBehavior = FillBehavior.Stop;
            // rectangle.BeginAnimation(Rectangle.WidthProperty,doubleanimation);
            rectangle.ApplyAnimationClock(Rectangle.WidthProperty,doubleanimation.CreateClock());
            
        }




    }
}
