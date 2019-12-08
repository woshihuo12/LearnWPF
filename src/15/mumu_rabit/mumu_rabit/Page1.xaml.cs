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


namespace mumu_rabit
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Samples.CustomControls.ColorPickerDialog cPicker
                   = new Microsoft.Samples.CustomControls.ColorPickerDialog();
            Rectangle rectangle = null;
            Button btn = sender as Button;
            if (btn == earcolorbtn)
            {
                rectangle = earcolor;
            }
            else if (btn == headcolorbtn)
            {
                rectangle = headcolor;
            }
            else
                return;

            SolidColorBrush fillbrush = rectangle.Fill as SolidColorBrush;
            if (fillbrush == null) return;
            Color fillcolor = fillbrush.Color;
            cPicker.StartingColor = fillcolor;
 

            bool? dialogResult = cPicker.ShowDialog();
            if (dialogResult != null && (bool)dialogResult == true)
            {

                rectangle.Fill = new SolidColorBrush(cPicker.SelectedColor);
                

            }
        }

        private void rabit_MouseEnter(object sender, MouseEventArgs e)
        {
            PathGeometry leftearpath2 = this.FindResource("leftearpath2") as PathGeometry;
            if (leftearpath2 == null) return;
            leftear.Data = leftearpath2;


            PathGeometry rightearpath2 = this.FindResource("rightearpath2") as PathGeometry;
            if (rightearpath2 == null) return;
            rightear.Data = rightearpath2;
            
        }

        private void rabit_MouseLeave(object sender, MouseEventArgs e)
        {
            PathGeometry leftearpath1 = this.FindResource("leftearpath1") as PathGeometry;
            if (leftearpath1 == null) return;
            leftear.Data = leftearpath1;

            PathGeometry rightearpath1 = this.FindResource("rightearpath1") as PathGeometry;
            if (rightearpath1 == null) return;
            rightear.Data = rightearpath1;
        }

       
    }
}
