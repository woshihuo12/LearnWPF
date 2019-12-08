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
    /// Interaction logic for Page5.xaml
    /// </summary>
    public partial class Page5 : Page
    {
        public Page5()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {   
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
            else if (btn == eyecolorbtn)
            {
                rectangle = eyecolor;
            }
            else return;

            Microsoft.Samples.CustomControls.ColorPickerDialog cPicker
                   = new Microsoft.Samples.CustomControls.ColorPickerDialog();

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
            ChangeRabitHead(true);
            ChangeRabitEar(true);
            ChangeRabitEye(true);
/*
            PathGeometry = this.FindResource(

            PathGeometry leftearpath2 = this.FindResource("leftearpath2") as PathGeometry;
            if (leftearpath2 == null) return;
            leftear.Data = leftearpath2;


            PathGeometry rightearpath2 = this.FindResource("rightearpath2") as PathGeometry;
            if (rightearpath2 == null) return;
            rightear.Data = rightearpath2;
*/

        }

        private void ChangeRabitHead(bool change)
        {
            if (change)
            {
                PathGeometry Geometryhead = this.FindResource("geometryhead2") as PathGeometry;
                if (Geometryhead == null) return;
                geometrydrawinghead.Geometry = Geometryhead;
            }
            else
            {
                PathGeometry Geometryhead = this.FindResource("geometryhead1") as PathGeometry;
                if (Geometryhead == null) return;
                geometrydrawinghead.Geometry = Geometryhead;
            }
        }
        private void ChangeRabitEar(bool change)
        {
            if (change)
            {
                PathGeometry Geometryear = this.FindResource("geometryear2") as PathGeometry;
                if (Geometryear == null) return;
                drawinggroupear.Geometry = Geometryear;
            }
            else
            {
                PathGeometry Geometryear = this.FindResource("geometryear1") as PathGeometry;
                if (Geometryear == null) return;
                drawinggroupear.Geometry = Geometryear;
            }
        }
        private void ChangeRabitEye(bool change)
        {
            if (change)
            {
                // 眼眶
                PathGeometry geometryeyeorbit = this.FindResource("geometryeyeorbit2") as PathGeometry;
                if (geometryeyeorbit == null) return;
                geometrydrawingeyeorbit.Geometry = geometryeyeorbit;
                // 眼睑
                PathGeometry geometryeyelid = this.FindResource("geometryeyelid2") as PathGeometry;
                if (geometryeyelid == null) return;
                geoetrydrawingeyelid.Geometry = geometryeyelid;
                SolidColorBrush solidcolorbrush = headcolor.Fill as SolidColorBrush;
                if(solidcolorbrush == null) return;
                eyelidbrushgradientstop.Color = solidcolorbrush.Color;
                // 眼珠
                PathGeometry geometryeyeball = this.FindResource("geometryeyeball2") as PathGeometry;
                if (geometryeyeball == null) return;
                geometrydrawingeyeball.Geometry = geometryeyeball;
                // 嘴
                Geometry geometrymouth = this.FindResource("geometrymouth2") as Geometry;
                if (geometrymouth == null) return;
                geometrydrawingmouth.Geometry = geometrymouth;
                // 舌头
                Geometry geometrylongue = this.FindResource("geometrylongue2") as Geometry;
                if (geometrylongue == null) return;
                geometrydrawinglongue.Geometry = geometrylongue;
            }
            else
            {
                // 眼眶
                PathGeometry geometryeyeorbit = this.FindResource("geometryeyeorbit1") as PathGeometry;
                if (geometryeyeorbit == null) return;
                geometrydrawingeyeorbit.Geometry = geometryeyeorbit;

                // 眼睑
                PathGeometry geometryeyelid = this.FindResource("geometryeyelid1") as PathGeometry;
                if (geometryeyelid == null) return;
                geoetrydrawingeyelid.Geometry = geometryeyelid;
               
                // 眼珠
                GeometryGroup geometryeyeball = this.FindResource("geometryeyeball1") as GeometryGroup;
                if (geometryeyeball == null) return;
                geometrydrawingeyeball.Geometry = geometryeyeball;

                // 嘴
                Geometry geometrymouth = this.FindResource("geometrymouth1") as Geometry;
                if (geometrymouth == null) return;
                geometrydrawingmouth.Geometry = geometrymouth;
                // 舌头
                Geometry geometrylongue = this.FindResource("geometrylongue1") as Geometry;
                if (geometrylongue == null) return;
                geometrydrawinglongue.Geometry = geometrylongue;
            }
        }
        private void rabit_MouseLeave(object sender, MouseEventArgs e)
        {
            ChangeRabitHead(false);
            ChangeRabitEar(false);
            ChangeRabitEye(false);
/*
            PathGeometry leftearpath1 = this.FindResource("leftearpath1") as PathGeometry;
            if (leftearpath1 == null) return;
            leftear.Data = leftearpath1;

            PathGeometry rightearpath1 = this.FindResource("rightearpath1") as PathGeometry;
            if (rightearpath1 == null) return;
            rightear.Data = rightearpath1;
 */ 
        }

    }
}
