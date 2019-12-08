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

namespace mumu_animationbasedonframe
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

            
        }

        void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            rectangle.Width += 10;
            if (rectangle.Width >= maxWidth)
            {
                rectangle.Width = startWidth;
                CompositionTarget.Rendering -= new EventHandler(CompositionTarget_Rendering);
                btn.IsEnabled = true;
            }
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            CompositionTarget.Rendering += new EventHandler(CompositionTarget_Rendering);
        }
    }
}
