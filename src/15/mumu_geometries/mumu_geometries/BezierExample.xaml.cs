
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace mumu_geometries
{
    public partial class BezierExample : Page
    {

        public BezierExample()
        {
            InitializeComponent();
            canvas.SizeChanged += CanvasOnSizeChanged;
        }
        // Canvas发生变化 四个点需要重新调整
        protected virtual void CanvasOnSizeChanged(object sender, 
                                        SizeChangedEventArgs args)
        {
            ptStart.Center = new Point(args.NewSize.Width / 4, 
                                       args.NewSize.Height / 2);
            ptCtrl1.Center = new Point(args.NewSize.Width / 2, 
                                       args.NewSize.Height / 4);
            ptCtrl2.Center = new Point(args.NewSize.Width / 2, 
                                       3 * args.NewSize.Height / 4);
            ptEnd.Center = new Point(3 * args.NewSize.Width / 4, 
                                     args.NewSize.Height / 2);
        }
       
        protected override void OnMouseDown(MouseButtonEventArgs args)
        {
            base.OnMouseDown(args);
            Point pt = args.GetPosition(canvas);

            if (args.ChangedButton == MouseButton.Left)
                ptCtrl1.Center = pt;

            if (args.ChangedButton == MouseButton.Right)
                ptCtrl2.Center = pt;
        }

        protected override void OnMouseMove(MouseEventArgs args)
        {
            base.OnMouseMove(args);
            Point pt = args.GetPosition(canvas);

            if (args.LeftButton == MouseButtonState.Pressed)
                ptCtrl1.Center = pt;

            if (args.RightButton == MouseButtonState.Pressed)
                ptCtrl2.Center = pt;
        }
    }
}