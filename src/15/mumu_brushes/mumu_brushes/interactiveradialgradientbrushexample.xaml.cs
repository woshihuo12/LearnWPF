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

namespace mumu_brushes
{
    /// <summary>
    /// Interaction logic for interactiveradialgradientbrushexample.xaml
    /// </summary>
    public partial class interactiveradialgradientbrushexample : Page
    {
        public interactiveradialgradientbrushexample()
        {
            InitializeComponent();
        }


        private void onPageLoaded(object sender, RoutedEventArgs s)
        {

            MappingModeComboBox.SelectionChanged += new SelectionChangedEventHandler(mappingModeChanged);
            onCenterPointTextBoxKeyUp(StartPointTextBox, null);
            onGradientOriginPointTextBoxKeyUp(EndPointTextBox, null);
        }



        private void gradientDisplaySizeChanged(object sender, SizeChangedEventArgs e)
        {

            if (InteractiveRadialGradientBrush.MappingMode ==
                    BrushMappingMode.RelativeToBoundingBox)
            {
                CenterPointMarkerTranslateTransform.X =
                    InteractiveRadialGradientBrush.Center.X * e.NewSize.Width;
                CenterPointMarkerTranslateTransform.Y =
                    InteractiveRadialGradientBrush.Center.Y * e.NewSize.Height;

                GradientOriginPointMarkerTranslateTransform.X =
                    InteractiveRadialGradientBrush.GradientOrigin.X * e.NewSize.Width;
                GradientOriginPointMarkerTranslateTransform.Y =
                   InteractiveRadialGradientBrush.GradientOrigin.Y * e.NewSize.Height;
            }
        }

        private void onCenterPointTextBoxKeyUp(object sender, KeyEventArgs args)
        {
            TextBox t = (TextBox)sender;
            try
            {
                Point p = Point.Parse(t.Text);
                if (InteractiveRadialGradientBrush.MappingMode == BrushMappingMode.RelativeToBoundingBox)
                {
                    CenterPointMarkerTranslateTransform.X = p.X * GradientDisplayElement.ActualWidth;
                    CenterPointMarkerTranslateTransform.Y = p.Y * GradientDisplayElement.ActualHeight;
                }
                else
                {
                    CenterPointMarkerTranslateTransform.X = p.X;
                    CenterPointMarkerTranslateTransform.Y = p.Y;

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

        private void onGradientOriginPointTextBoxKeyUp(object sender, KeyEventArgs args)
        {
            TextBox t = (TextBox)sender;
            try
            {
                Point p = Point.Parse(t.Text);
                if (InteractiveRadialGradientBrush.MappingMode == BrushMappingMode.RelativeToBoundingBox)
                {
                    GradientOriginPointMarkerTranslateTransform.X = p.X * GradientDisplayElement.ActualWidth;
                    GradientOriginPointMarkerTranslateTransform.Y = p.Y * GradientDisplayElement.ActualHeight;
                }
                else
                {
                    GradientOriginPointMarkerTranslateTransform.X = p.X;
                    GradientOriginPointMarkerTranslateTransform.Y = p.Y;

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

        private void RadiusY_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox t = (TextBox)sender;
            try
            {
                double p = double.Parse(t.Text);
                if (InteractiveRadialGradientBrush.MappingMode == BrushMappingMode.RelativeToBoundingBox)
                {
                    InteractiveRadialGradientBrush.RadiusY = p;
                }
                else
                {
                    InteractiveRadialGradientBrush.RadiusY = GradientDisplayElement.ActualHeight * p;

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

        private void RadiusX_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox t = (TextBox)sender;
            try
            {
                double p = double.Parse(t.Text);
                if (InteractiveRadialGradientBrush.MappingMode == BrushMappingMode.RelativeToBoundingBox)
                {
                    InteractiveRadialGradientBrush.RadiusX = p;
                }
                else
                {
                    InteractiveRadialGradientBrush.RadiusX = GradientDisplayElement.ActualWidth * p;

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

        // 鼠标左键按下消息
        private void gradientDisplayMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is Shape)
            {
                SetValue(SelectedMarkerProperty, (Shape)e.OriginalSource);
            }
            else
                SetValue(SelectedMarkerProperty, null);
        }

        
        // 鼠标抬起消息
        private void gradientDisplayMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Point clickPoint = e.GetPosition(GradientDisplayElement);
            Shape s = (Shape)GetValue(SelectedMarkerProperty);
            if (s == GradientOriginPointMarker || s == CenterPointMarker)
            {
                TranslateTransform translation = (TranslateTransform)s.RenderTransform;
                translation.X = clickPoint.X;
                translation.Y = clickPoint.Y;
                SetValue(SelectedMarkerProperty, null);
                Mouse.Synchronize();

                Point p;
                if (InteractiveRadialGradientBrush.MappingMode == BrushMappingMode.RelativeToBoundingBox)
                {
                    p = new Point(clickPoint.X / GradientDisplayElement.ActualWidth,
                        clickPoint.Y / GradientDisplayElement.ActualHeight);
                }
                else
                {

                    p = clickPoint;

                }

                if (s == CenterPointMarker)
                {

                    InteractiveRadialGradientBrush.Center = p;
                    StartPointTextBox.Text = p.X.ToString("F4") + "," + p.Y.ToString("F4");
                }
                else
                {
                    InteractiveRadialGradientBrush.GradientOrigin = p;
                    EndPointTextBox.Text = p.X.ToString("F4") + "," + p.Y.ToString("F4");
                }
            }
        }

        // 鼠标移动消息 
        private void gradientDisplayMouseMove(object sender, MouseEventArgs e)
        {
            Point currentPoint = e.GetPosition(GradientDisplayElement);
            Shape s = (Shape)GetValue(SelectedMarkerProperty);

            
            if (s == GradientOriginPointMarker || s == CenterPointMarker)
            {


                TranslateTransform translation = (TranslateTransform)s.RenderTransform;
                translation.X = currentPoint.X;
                translation.Y = currentPoint.Y;
                Mouse.Synchronize();

                Point p;


                if (InteractiveRadialGradientBrush.MappingMode ==
                        BrushMappingMode.RelativeToBoundingBox)
                {

                    p = new Point(currentPoint.X / GradientDisplayElement.ActualWidth,
                        currentPoint.Y / GradientDisplayElement.ActualHeight);
                }
                else
                {

                    p = currentPoint;

                }

                if (s == CenterPointMarker)
                {

                    InteractiveRadialGradientBrush.Center = p;
                    StartPointTextBox.Text = p.X.ToString("F4") + "," + p.Y.ToString("F4");
                }
                else
                {

                    InteractiveRadialGradientBrush.GradientOrigin = p;
                    EndPointTextBox.Text = p.X.ToString("F4") + "," + p.Y.ToString("F4");
                }
            }
        }


        private void mappingModeChanged(object sender, SelectionChangedEventArgs e)
        {

            Point oldCenterPoint = InteractiveRadialGradientBrush.Center;
            Point newCenterPoint = new Point();
            Point oldRadialGradientPoint = InteractiveRadialGradientBrush.GradientOrigin;
            Point newRadialGradientPoint = new Point();

            double oldRadiusX = InteractiveRadialGradientBrush.RadiusX;
            double newRadiusX;
            double oldRadiusY = InteractiveRadialGradientBrush.RadiusY;
            double newRadiusY;

            if (InteractiveRadialGradientBrush.MappingMode ==
                    BrushMappingMode.RelativeToBoundingBox)
            {

                newCenterPoint.X = oldCenterPoint.X / GradientDisplayElement.ActualWidth;
                newCenterPoint.Y = oldCenterPoint.Y / GradientDisplayElement.ActualHeight;
                InteractiveRadialGradientBrush.Center = newCenterPoint;

                newRadialGradientPoint.X = oldRadialGradientPoint.X / GradientDisplayElement.ActualWidth;
                newRadialGradientPoint.Y = oldRadialGradientPoint.Y / GradientDisplayElement.ActualHeight;
                InteractiveRadialGradientBrush.GradientOrigin = newRadialGradientPoint;

                newRadiusX = oldRadiusX / GradientDisplayElement.ActualWidth;
                newRadiusY = oldRadiusY / GradientDisplayElement.ActualHeight;

                InteractiveRadialGradientBrush.RadiusY = newRadiusY;
                InteractiveRadialGradientBrush.RadiusX = newRadiusX;

            }
            else
            {

                newCenterPoint.X = oldCenterPoint.X * GradientDisplayElement.ActualWidth;
                newCenterPoint.Y = oldCenterPoint.Y * GradientDisplayElement.ActualHeight;
                InteractiveRadialGradientBrush.Center = newCenterPoint;

                newRadialGradientPoint.X = oldRadialGradientPoint.X * GradientDisplayElement.ActualWidth;
                newRadialGradientPoint.Y = oldRadialGradientPoint.Y * GradientDisplayElement.ActualHeight;
                InteractiveRadialGradientBrush.GradientOrigin = newRadialGradientPoint;


                newRadiusX = oldRadiusX * GradientDisplayElement.ActualWidth;
                newRadiusY = oldRadiusY * GradientDisplayElement.ActualHeight;
                InteractiveRadialGradientBrush.RadiusY = newRadiusY;
                InteractiveRadialGradientBrush.RadiusX = newRadiusX;
            }


            StartPointTextBox.Text = newCenterPoint.X.ToString("F4") +
                "," + newCenterPoint.Y.ToString("F4");
            EndPointTextBox.Text = newRadialGradientPoint.X.ToString("F4") +
                "," + newRadialGradientPoint.Y.ToString("F4");
            RadiusX.Text = newRadiusX.ToString("F4");
            RadiusY.Text = newRadiusY.ToString("F4");
        }


        private static string generateLinearGradientBrushMarkup(RadialGradientBrush theBrush)
        {
            System.Text.StringBuilder sBuilder = new System.Text.StringBuilder();
            sBuilder.Append("<" + theBrush.GetType().Name + "\n" +
            "  CenterPoint=\"" + theBrush.Center.ToString() + "\"" +
            "RadiusX=\"" + theBrush.RadiusX.ToString() + "\"" +
            "RadiusY=\"" + theBrush.RadiusY.ToString() + "\"" +
            "  GradientOriginPoint=\"" + theBrush.GradientOrigin.ToString() + "\" \n" +
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
            sBuilder.Append("</RadialGradientBrush>");
            return sBuilder.ToString();
        }

        public static readonly DependencyProperty SelectedMarkerProperty =
            DependencyProperty.Register
            ("SelectedMarker", typeof(Shape), typeof(interactiveradialgradientbrushexample),
            new PropertyMetadata(null));


        private void onInteractiveRadialGradientBrushChanged(object sender, EventArgs e)
        {
            if (GradientDisplayElement != null)
            {
                markupOutputTextBlock.Text =
                    generateLinearGradientBrushMarkup(InteractiveRadialGradientBrush);
            }
        }

       
    }
}
