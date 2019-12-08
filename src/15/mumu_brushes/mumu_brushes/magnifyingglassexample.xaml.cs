using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Input;

namespace mumu_brushes
{

    public partial class MagnifyingGlassExample : Page
    {

        private static readonly double distanceFromMouse = 5;
        
        public MagnifyingGlassExample()
        {
        }
        
        
        private void updateMagnifyingGlass(object sender, MouseEventArgs args)
        {
            Mouse.SetCursor(Cursors.Cross);
            
            Point currentMousePosition = args.GetPosition(this);
            
            if (this.ActualWidth - currentMousePosition.X > magnifyingGlassEllipse.Width + distanceFromMouse)
            {
                Canvas.SetLeft(magnifyingGlassEllipse, currentMousePosition.X + distanceFromMouse);
            }
            else
            {
                Canvas.SetLeft(magnifyingGlassEllipse, currentMousePosition.X - distanceFromMouse - magnifyingGlassEllipse.Width);
            }
            

            if (this.ActualHeight - currentMousePosition.Y > magnifyingGlassEllipse.Height + distanceFromMouse)
            {
                Canvas.SetTop(magnifyingGlassEllipse, currentMousePosition.Y + distanceFromMouse);
            }
            else
            {
                Canvas.SetTop(magnifyingGlassEllipse, currentMousePosition.Y - distanceFromMouse - magnifyingGlassEllipse.Height);
            }
            
            

            myVisualBrush.Viewbox = 
                new Rect(currentMousePosition.X - 10, currentMousePosition.Y  - 10, 20, 20);
            
        }
        
    }
}