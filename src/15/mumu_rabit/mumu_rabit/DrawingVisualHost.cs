using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace mumu_rabit
{
    public class DrawingVisualHost : FrameworkElement
    {
        // Create a collection of child visual objects.
        private VisualCollection _children;
        private DrawingVisual headdrawingvisual;
        private DrawingVisual eardrawingvisual;
        private DrawingVisual eyedrawingvisual;
        private DrawingVisual mousedrawingvisual;

        public DrawingVisualHost()
        {
            _children = new VisualCollection(this);
            headdrawingvisual = CreateHeadDrawingVisual(true);
            eardrawingvisual = CreateEarDrawingVisual(true);
            eyedrawingvisual = CreateEyeDrawingVisual(true);
            mousedrawingvisual = CreateMouseDrawingVisual(true);

            _children.Add(headdrawingvisual);
            _children.Add(eardrawingvisual);
            _children.Add(eyedrawingvisual);
            _children.Add(mousedrawingvisual);

            this.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(MyVisualHost_MouseLeftButtonUp);
            this.MouseRightButtonDown += new System.Windows.Input.MouseButtonEventHandler(DrawingVisualHost_MouseRightButtonDown);
        }

        void DrawingVisualHost_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _children.RemoveRange(0, 4);
            headdrawingvisual = CreateHeadDrawingVisual(true);
            eardrawingvisual = CreateEarDrawingVisual(true);
            eyedrawingvisual = CreateEyeDrawingVisual(true);
            mousedrawingvisual = CreateMouseDrawingVisual(true);
            _children.Add(headdrawingvisual);
            _children.Add(eardrawingvisual);
            _children.Add(eyedrawingvisual);
            _children.Add(mousedrawingvisual);
        }

        void MyVisualHost_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
          
            System.Windows.Point pt = e.GetPosition((UIElement)sender);

           
            HitTestResult hittestresult = VisualTreeHelper.HitTest(this, pt);

            if (headdrawingvisual == hittestresult.VisualHit)
            {
                // MessageBox.Show("击中了兔子头部！");
                _children.Remove(headdrawingvisual);
                headdrawingvisual = CreateHeadDrawingVisual(false);
                _children.Insert(0, headdrawingvisual);

            }
            else if (eardrawingvisual == hittestresult.VisualHit)
            {
                // MessageBox.Show("击中了兔子耳朵！");
                _children.Remove(eardrawingvisual);
                eardrawingvisual = CreateEarDrawingVisual(false);
                _children.Insert(1, eardrawingvisual);
            }
            else if (eyedrawingvisual == hittestresult.VisualHit)
            {
                // MessageBox.Show("击中了兔子眼部！");
                _children.Remove(eyedrawingvisual);
                eyedrawingvisual = CreateEyeDrawingVisual(false);
                _children.Insert(2, eyedrawingvisual);
            }
            else if (mousedrawingvisual == hittestresult.VisualHit)
            {
        
                _children.Remove(mousedrawingvisual);
                mousedrawingvisual = CreateMouseDrawingVisual(false);
                _children.Insert(3, mousedrawingvisual);
            }
          
            
           VisualTreeHelper.HitTest(this, null, new HitTestResultCallback(myCallback), new PointHitTestParameters(pt));
        }

        public HitTestResultBehavior myCallback(HitTestResult result)
        {
            if (result.VisualHit.GetType() == typeof(DrawingVisual))
            {
                if (headdrawingvisual == result.VisualHit)
                {
                    _children.Remove(headdrawingvisual);
                    headdrawingvisual = CreateHeadDrawingVisual(false);
                    _children.Insert(0, headdrawingvisual);

                }
                else if (eardrawingvisual == result.VisualHit)
                {
                    _children.Remove(eardrawingvisual);
                    eardrawingvisual = CreateEarDrawingVisual(false);
                    _children.Insert(1, eardrawingvisual);
                }
                else if (eyedrawingvisual == result.VisualHit)
                {
                    _children.Remove(eyedrawingvisual);
                    eyedrawingvisual = CreateEyeDrawingVisual(false);
                    _children.Insert(2, eyedrawingvisual);
                }

                else if (mousedrawingvisual == result.VisualHit)
                {
                   
                    _children.Remove(mousedrawingvisual);
                    mousedrawingvisual = CreateMouseDrawingVisual(false);
                    _children.Insert(3, mousedrawingvisual);
                }
            }
            else
            {
                _children.RemoveRange(0, 4);
                headdrawingvisual = CreateHeadDrawingVisual(true);
                eardrawingvisual = CreateEarDrawingVisual(true);
                eyedrawingvisual = CreateEyeDrawingVisual(true);
                mousedrawingvisual = CreateMouseDrawingVisual(true);

                _children.Add(headdrawingvisual);
                _children.Add(eardrawingvisual);
                _children.Add(eyedrawingvisual);
                _children.Add(mousedrawingvisual);

            }
            // Stop the hit test enumeration of objects in the visual tree.
            return HitTestResultBehavior.Continue;
        }


        private DrawingVisual CreateHeadDrawingVisual(bool change)
        {
            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();
            GeometryDrawing headdrawing = new GeometryDrawing();
            SolidColorBrush solidcolorbrush = this.FindResource("headcolor") as SolidColorBrush;
            if (solidcolorbrush == null)
            {
                drawingContext.Close();
                return null;
            }

            if (change)
            {
                Geometry headgeometry = this.FindResource("geometryhead1") as Geometry;
                if (headgeometry == null)
                {
                    drawingContext.Close();
                    return null;
                }
                headdrawing.Brush = solidcolorbrush;
                headdrawing.Geometry = headgeometry;      
            }
            else
            {
                Geometry headgeometry = this.FindResource("geometryhead2") as Geometry;
                if (headgeometry == null)
                {
                    drawingContext.Close();
                    return null;
                }
                headdrawing.Brush = solidcolorbrush;
                headdrawing.Geometry = headgeometry;
            }
           
            drawingContext.DrawDrawing(headdrawing);
            drawingContext.Close();
            return drawingVisual;
        }

        private DrawingVisual CreateEarDrawingVisual(bool change)
        {
            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();
            GeometryDrawing eardrawing = new GeometryDrawing();
            SolidColorBrush solidcolorbrush = this.FindResource("headcolor") as SolidColorBrush;
            if (solidcolorbrush == null)
            {
                drawingContext.Close();
                return null;
            }

            if (change)
            {
                
                eardrawing.Brush = solidcolorbrush;
                Geometry eargeometry = this.FindResource("geometryear1") as Geometry;
                eardrawing.Geometry = eargeometry;
            }
            else
            {
                
                eardrawing.Brush = solidcolorbrush;
                Geometry eargeometry = this.FindResource("geometryear2") as Geometry;
                eardrawing.Geometry = eargeometry;
            }

            drawingContext.DrawDrawing(eardrawing);
            drawingContext.Close();
            return drawingVisual;

        }
        private DrawingVisual CreateEyeDrawingVisual(bool change)
        {
            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();
            if (change)
            {
                OuterGlowBitmapEffect bitmapeffect = new OuterGlowBitmapEffect();
                bitmapeffect.GlowColor = Colors.Black;
                bitmapeffect.GlowSize = 10;
                drawingContext.PushEffect(bitmapeffect, null);

                // 兔眼眶
                DrawingGroup eyedrawing = new DrawingGroup();
                GeometryDrawing eyeorbitdrawing = new GeometryDrawing();
                eyeorbitdrawing.Brush = new SolidColorBrush(Colors.White);
                Geometry eyegorbiteometry = this.FindResource("geometryeyeorbit1") as Geometry;
                eyeorbitdrawing.Geometry = eyegorbiteometry;
                eyedrawing.Children.Add(eyeorbitdrawing);
               
                

                // 兔眼珠

                GeometryDrawing eyeballdrawing = new GeometryDrawing();
                eyeballdrawing.Brush = new SolidColorBrush(Colors.Black);
                Geometry eyeballgeometry = this.FindResource("geometryeyeball1") as Geometry;
                eyeballdrawing.Geometry = eyeballgeometry;
                eyedrawing.Children.Add(eyeballdrawing);
                drawingContext.DrawDrawing(eyedrawing);

              
                drawingContext.Pop();

            }
            else
            {

                // 兔眼眶
                DrawingGroup eyedrawing = new DrawingGroup();
                GeometryDrawing eyeorbitdrawing = new GeometryDrawing();
                eyeorbitdrawing.Brush = new SolidColorBrush(Colors.White);
                Geometry eyegorbiteometry = this.FindResource("geometryeyeorbit2") as Geometry;
                eyeorbitdrawing.Geometry = eyegorbiteometry;
                eyedrawing.Children.Add(eyeorbitdrawing);


                // 兔眼珠
                GeometryDrawing eyeballdrawing = new GeometryDrawing();
                eyeballdrawing.Brush = new SolidColorBrush(Colors.Black);
                Geometry eyeballgeometry = this.FindResource("geometryeyeball2") as Geometry;
                eyeballdrawing.Geometry = eyeballgeometry;
                eyedrawing.Children.Add(eyeballdrawing);
                drawingContext.DrawDrawing(eyedrawing);
            }


            drawingContext.Close();
            return drawingVisual;
        }
        private DrawingVisual CreateMouseDrawingVisual(bool change)
        {
           

            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();
            if (change)
            {

                GeometryDrawing mouthdrawing = new GeometryDrawing();
                mouthdrawing.Brush = new SolidColorBrush(Colors.Black);
                mouthdrawing.Pen = new Pen(new SolidColorBrush(Colors.Black), 1);
                Geometry mouthgeometry = this.FindResource("geometrymouth1") as Geometry;
                mouthdrawing.Geometry = mouthgeometry;
                drawingContext.DrawDrawing(mouthdrawing);

            }
            else
            {
                GeometryDrawing mouthdrawing = new GeometryDrawing();
                mouthdrawing.Brush = new SolidColorBrush(Colors.Black);
                mouthdrawing.Pen = new Pen(new SolidColorBrush(Colors.Black), 1);
                Geometry mouthgeometry = this.FindResource("geometrymouth2") as Geometry;
                mouthdrawing.Geometry = mouthgeometry;
                drawingContext.DrawDrawing(mouthdrawing);

                GeometryDrawing longuedrawing = new GeometryDrawing();
                longuedrawing.Brush = new SolidColorBrush(Colors.Red);
                longuedrawing.Pen = new Pen(new SolidColorBrush(Colors.Red), 1);
                Geometry longuegeometry = this.FindResource("geometrylongue2") as Geometry;
                longuedrawing.Geometry = longuegeometry;
                drawingContext.DrawDrawing(longuedrawing);
                
            }


            drawingContext.Close();
            return drawingVisual;

            //return null;
        }
/*
        private DrawingVisual CreateHeadDrawingVisual(bool change)
        {
            if (change)
            {
                #region 兔头
                DrawingVisual drawingVisual = new DrawingVisual();
                DrawingContext drawingContext = drawingVisual.RenderOpen();
                GeometryDrawing headdrawing = new GeometryDrawing();

                SolidColorBrush solidcolorbrush = this.FindResource("headcolor") as SolidColorBrush;
                if (solidcolorbrush == null)
                {
                    drawingContext.Close();
                    return null;
                }

                Geometry headgeometry = this.FindResource("geometryhead1") as Geometry;
                if (headgeometry == null)
                {
                    drawingContext.Close();
                    return null;
                }
                headdrawing.Brush = solidcolorbrush;
                headdrawing.Geometry = headgeometry;


                drawingContext.PushEffect(new DropShadowBitmapEffect(), null);
                drawingContext.DrawDrawing(headdrawing);

                #endregion
                #region 兔耳
                GeometryDrawing eardrawing = new GeometryDrawing();
                eardrawing.Brush = solidcolorbrush;
                Geometry eargeometry = this.FindResource("geometryear1") as Geometry;
                eardrawing.Geometry = eargeometry;
                drawingContext.DrawDrawing(eardrawing);
                #endregion

                #region 兔眼
                drawingContext.Pop();
                OuterGlowBitmapEffect bitmapeffect = new OuterGlowBitmapEffect();
                bitmapeffect.GlowColor = Colors.Black;
                bitmapeffect.GlowSize = 10;
                drawingContext.PushEffect(bitmapeffect, null);
                // 兔眼眶
                DrawingGroup eyedrawing = new DrawingGroup();
                GeometryDrawing eyeorbitdrawing = new GeometryDrawing();
                eyeorbitdrawing.Brush = new SolidColorBrush(Colors.White);
                Geometry eyegorbiteometry = this.FindResource("geometryeyeorbit1") as Geometry;
                eyeorbitdrawing.Geometry = eyegorbiteometry;
                eyedrawing.Children.Add(eyeorbitdrawing);



                // 兔眼珠

                GeometryDrawing eyeballdrawing = new GeometryDrawing();
                eyeballdrawing.Brush = new SolidColorBrush(Colors.Black);
                Geometry eyeballgeometry = this.FindResource("geometryeyeball1") as Geometry;
                eyeballdrawing.Geometry = eyeballgeometry;
                eyedrawing.Children.Add(eyeballdrawing);

                drawingContext.DrawDrawing(eyedrawing);

                drawingContext.Pop();

                #endregion

                #region 兔嘴
                GeometryDrawing mouthdrawing = new GeometryDrawing();
                mouthdrawing.Brush = new SolidColorBrush(Colors.Black);
                mouthdrawing.Pen = new Pen(new SolidColorBrush(Colors.Black), 1);
                Geometry mouthgeometry = this.FindResource("geometrymouth1") as Geometry;
                mouthdrawing.Geometry = mouthgeometry;
                drawingContext.DrawDrawing(mouthdrawing);
                #endregion


                drawingContext.Close();
                return drawingVisual;
            }
                       
            
            
           
                        if (grayregion == null) 
                        {
                            drawingContext.Close();
                            return null;
                        }


                        drawingContext.DrawDrawing(grayregion);

                        drawingContext.Close();

                        return drawingVisual;
 return null;
            
            
        }
    */




        


        // Provide a required override for the VisualChildrenCount property.
        protected override int VisualChildrenCount
        {
            get { return _children.Count; }
        }

        // Provide a required override for the GetVisualChild method.
        protected override Visual GetVisualChild(int index)
        {
            if (index < 0 || index >= _children.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            return _children[index];
        }

    }
}
