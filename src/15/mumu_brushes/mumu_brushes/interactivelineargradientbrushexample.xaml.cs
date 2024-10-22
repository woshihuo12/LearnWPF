using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace mumu_brushes
{

    /// <summary>
    /// Enables the user to configure a LinearGradientBrush interactively. 
    /// </summary>
    public partial class InteractiveLinearGradientBrushExample : Page
    {

        public InteractiveLinearGradientBrushExample()
        {
            InitializeComponent();
        }

        private void onPageLoaded(object sender, RoutedEventArgs s)
        {

            MappingModeComboBox.SelectionChanged += new SelectionChangedEventHandler(mappingModeChanged);
            onStartPointTextBoxKeyUp(StartPointTextBox, null);
            onEndPointTextBoxKeyUp(EndPointTextBox, null);
        }


        private void gradientDisplaySizeChanged(object sender, SizeChangedEventArgs e)
        {
            // The marker positions only need recalcutated if the brush's MappingMode
            // is RelativeToBoundingBox.
            if (InteractiveLinearGradientBrush.MappingMode == 
                    BrushMappingMode.RelativeToBoundingBox)
            {
                StartPointMarkerTranslateTransform.X =
                    InteractiveLinearGradientBrush.StartPoint.X * e.NewSize.Width;
                StartPointMarkerTranslateTransform.Y =
                    InteractiveLinearGradientBrush.StartPoint.Y * e.NewSize.Height;

                EndPointMarkerTranslateTransform.X =
                    InteractiveLinearGradientBrush.EndPoint.X * e.NewSize.Width;
                EndPointMarkerTranslateTransform.Y =
                   InteractiveLinearGradientBrush.EndPoint.Y * e.NewSize.Height;
            }
        }

        private void onStartPointTextBoxKeyUp(object sender, KeyEventArgs args)
        {
            TextBox t = (TextBox)sender;
            try
            {
                Point p = Point.Parse(t.Text);
                if (InteractiveLinearGradientBrush.MappingMode == BrushMappingMode.RelativeToBoundingBox)
                {
                    StartPointMarkerTranslateTransform.X = p.X * GradientDisplayElement.ActualWidth;
                    StartPointMarkerTranslateTransform.Y = p.Y * GradientDisplayElement.ActualHeight;
                }
                else
                {
                    StartPointMarkerTranslateTransform.X = p.X;
                    StartPointMarkerTranslateTransform.Y = p.Y;

                }
            }
            catch (InvalidOperationException ex)
            {
                // Ignore errors.
            }
            catch (FormatException formatEx)
            {
                // Ignore errors.
            }

        }

        private void onEndPointTextBoxKeyUp(object sender, KeyEventArgs args)
        {
            TextBox t = (TextBox)sender;
            try
            {
                Point p = Point.Parse(t.Text);
                if (InteractiveLinearGradientBrush.MappingMode == BrushMappingMode.RelativeToBoundingBox)
                {
                    EndPointMarkerTranslateTransform.X = p.X * GradientDisplayElement.ActualWidth;
                    EndPointMarkerTranslateTransform.Y = p.Y * GradientDisplayElement.ActualHeight;
                }
                else
                {
                    EndPointMarkerTranslateTransform.X = p.X;
                    EndPointMarkerTranslateTransform.Y = p.Y;

                }

            }
            catch (InvalidOperationException ex)
            {
                // Ignore errors.
            }
            catch (FormatException formatEx)
            {
                // Ignore errors.
            }

        }

        // Determine whether the user clicked a marker.
        private void gradientDisplayMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is Shape)
            {
                SetValue(SelectedMarkerProperty, (Shape)e.OriginalSource);
            }
            else
                SetValue(SelectedMarkerProperty, null);
        }
 
        private void gradientDisplayMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Point clickPoint = e.GetPosition(GradientDisplayElement);
            Shape s = (Shape)GetValue(SelectedMarkerProperty);
            if (s == EndPointMarker || s == StartPointMarker)
            {
                TranslateTransform translation = (TranslateTransform)s.RenderTransform;
                translation.X = clickPoint.X;
                translation.Y = clickPoint.Y;
                SetValue(SelectedMarkerProperty, null);
                Mouse.Synchronize();

                Point p;
                if (InteractiveLinearGradientBrush.MappingMode == BrushMappingMode.RelativeToBoundingBox)
                {
                    p = new Point(clickPoint.X / GradientDisplayElement.ActualWidth,
                        clickPoint.Y / GradientDisplayElement.ActualHeight);
                }
                else
                {

                    p = clickPoint;

                }

                if (s == StartPointMarker)
                {

                    InteractiveLinearGradientBrush.StartPoint = p;
                    StartPointTextBox.Text = p.X.ToString("F4") + "," + p.Y.ToString("F4");
                }
                else
                {
                    InteractiveLinearGradientBrush.EndPoint = p;
                    EndPointTextBox.Text = p.X.ToString("F4") + "," + p.Y.ToString("F4");
                }
            }
        }


        private void gradientDisplayMouseMove(object sender, MouseEventArgs e)
        {
            Point currentPoint = e.GetPosition(GradientDisplayElement);
            Shape s = (Shape)GetValue(SelectedMarkerProperty);
            

            if (s == EndPointMarker || s == StartPointMarker)
            {
                

                TranslateTransform translation = (TranslateTransform)s.RenderTransform;
                translation.X = currentPoint.X;
                translation.Y = currentPoint.Y;
                Mouse.Synchronize();

                Point p;


                if (InteractiveLinearGradientBrush.MappingMode == 
                        BrushMappingMode.RelativeToBoundingBox)
                {

                    p = new Point(currentPoint.X / GradientDisplayElement.ActualWidth,
                        currentPoint.Y / GradientDisplayElement.ActualHeight);
                }
                else
                {

                    p = currentPoint;

                }

                if (s == StartPointMarker)
                {

                    InteractiveLinearGradientBrush.StartPoint = p;
                    StartPointTextBox.Text = p.X.ToString("F4") + "," + p.Y.ToString("F4");
                }
                else
                {

                    InteractiveLinearGradientBrush.EndPoint = p;
                    EndPointTextBox.Text = p.X.ToString("F4") + "," + p.Y.ToString("F4");
                }
            }
        }

        private void mappingModeChanged(object sender, SelectionChangedEventArgs e)
        {
            
            Point oldStartPoint = InteractiveLinearGradientBrush.StartPoint;
            Point newStartPoint = new Point();
            Point oldEndPoint = InteractiveLinearGradientBrush.EndPoint;
            Point newEndPoint = new Point();

           
            if (InteractiveLinearGradientBrush.MappingMode == 
                    BrushMappingMode.RelativeToBoundingBox)
            {
               
               
                newStartPoint.X = oldStartPoint.X / GradientDisplayElement.ActualWidth;
                newStartPoint.Y = oldStartPoint.Y / GradientDisplayElement.ActualHeight;
                InteractiveLinearGradientBrush.StartPoint = newStartPoint;

                newEndPoint.X = oldEndPoint.X / GradientDisplayElement.ActualWidth;
                newEndPoint.Y = oldEndPoint.Y / GradientDisplayElement.ActualHeight;
                InteractiveLinearGradientBrush.EndPoint = newEndPoint;

            }
            else
            {
               
                newStartPoint.X = oldStartPoint.X * GradientDisplayElement.ActualWidth;
                newStartPoint.Y = oldStartPoint.Y * GradientDisplayElement.ActualHeight;
                InteractiveLinearGradientBrush.StartPoint = newStartPoint;

                newEndPoint.X = oldEndPoint.X * GradientDisplayElement.ActualWidth;
                newEndPoint.Y = oldEndPoint.Y * GradientDisplayElement.ActualHeight;

                InteractiveLinearGradientBrush.EndPoint = newEndPoint;

            }

         
            StartPointTextBox.Text = newStartPoint.X.ToString("F4") +
                "," + newStartPoint.Y.ToString("F4");
            EndPointTextBox.Text = newEndPoint.X.ToString("F4") +
                "," + newEndPoint.Y.ToString("F4");
        }

       
        private void onInteractiveLinearGradientBrushChanged(object sender, EventArgs e)
        {
            if (GradientDisplayElement != null)
            {
                markupOutputTextBlock.Text =
                    generateLinearGradientBrushMarkup(InteractiveLinearGradientBrush);
            }
        }

        private static string generateLinearGradientBrushMarkup(LinearGradientBrush theBrush)
        {
            System.Text.StringBuilder sBuilder = new System.Text.StringBuilder();
            sBuilder.Append("<" + theBrush.GetType().Name + "\n" +
            "  StartPoint=\"" + theBrush.StartPoint.ToString() + "\"" +
            "  EndPoint=\"" + theBrush.EndPoint.ToString() + "\" \n" +
            "  MappingMode=\"" + theBrush.MappingMode.ToString() + "\"" +
            "  SpreadMethod=\"" + theBrush.SpreadMethod.ToString() + "\"\n" +
            "  ColorInterpolationMode=\"" + theBrush.ColorInterpolationMode.ToString() + "\"" +
            "  Opacity=\"" + theBrush.Opacity.ToString() + "\"" + ">\n");

            foreach (GradientStop stop in theBrush.GradientStops)
            {
                sBuilder.Append
                (
                    "  <GradientStop Offset=\"" + stop.Offset.ToString("F4")
                    + "\" Color=\"" + stop.Color.ToString() + "\" />\n"
                );

            }
            sBuilder.Append("</LinearGradientBrush>");
            return sBuilder.ToString();
        }

        public static readonly DependencyProperty SelectedMarkerProperty =
            DependencyProperty.Register
            ("SelectedMarker", typeof(Shape), typeof(InteractiveLinearGradientBrushExample),
            new PropertyMetadata(null));

    }


    [ValueConversion(typeof(object), typeof(string[]))]
    public class EnumPossibleValuesToStringArrayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {


            return new System.Collections.ArrayList(Enum.GetNames(value.GetType()));

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    [ValueConversion(typeof(Point), typeof(string))]
    public class PointToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            Point p = (Point)value;
            return p.X.ToString("F4") + "," + p.Y.ToString("F4");

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                return Point.Parse((string)value);
            }
            catch (System.InvalidOperationException ex)
            {
                return null;
            }
        }
    }

    [ValueConversion(typeof(double), typeof(string))]
    public class DoubleToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            double d = (double)value;
            return d.ToString("F4");

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                return Double.Parse((string)value);
            }
            catch (System.InvalidOperationException ex)
            {
                return null;
            }
        }
    }


}