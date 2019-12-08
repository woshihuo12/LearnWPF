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

namespace mumu_geometries
{
    /// <summary>
    /// Interaction logic for streamgeometryexample.xaml
    /// </summary>
    public partial class streamgeometryexample : Page
    {
        public streamgeometryexample()
        {
            InitializeComponent();

    
            myPath.Stroke = Brushes.Black;
            myPath.StrokeThickness = 1;

            // 创建一个StreamGeometry对象
            StreamGeometry geometry = new StreamGeometry();
            geometry.FillRule = FillRule.EvenOdd;

            // 通过Open方法获得StreamGeometryContext
            using (StreamGeometryContext ctx = geometry.Open())
            {

                // 绘制第一个PathFigure
                ctx.BeginFigure(new Point(10, 100), true /* is filled */, true /* is closed */);
                ctx.LineTo(new Point(100, 100), true /* is stroked */, false /* is smooth join */);
                ctx.LineTo(new Point(100, 50), true /* is stroked */, false /* is smooth join */);
            }

            geometry.Freeze();


            myPath.Data = geometry;


        }
    }
}
