using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace mumu_layout2
{
    public class CustomPanel : Panel
    {
        // Default public constructor
        public CustomPanel()
            : base()
        {
        }

        
        protected override Size MeasureOverride(Size availableSize)
        {
            double maxChildWidth = 0.0;
            double maxChildHeight = 0.0;
            foreach (UIElement child in InternalChildren)
            {
                child.Measure(availableSize);
                maxChildWidth = Math.Max(child.DesiredSize.Width, maxChildWidth);
                maxChildHeight = Math.Max(child.DesiredSize.Height, maxChildHeight);

            }
            double idealCircumference = maxChildWidth * InternalChildren.Count;
            double idealRadius = (idealCircumference / (Math.PI * 2) + maxChildHeight);

            Size ideal = new Size(idealRadius * 2, idealRadius * 2);
            Size desired = ideal;
            if (!double.IsInfinity(availableSize.Width))
            {
                if (availableSize.Width < desired.Width)
                    desired.Width = availableSize.Width;
            }
            if (!double.IsInfinity(availableSize.Height))
            {
                if (availableSize.Height < desired.Height)
                    desired.Height = availableSize.Height;
            }
            return desired;

        }
        protected override Size ArrangeOverride(Size finalSize)
        {
            Rect layoutRect;
            if (finalSize.Width > finalSize.Height)
            {
                layoutRect = new Rect((finalSize.Width - finalSize.Height) / 2, 0, finalSize.Height, finalSize.Height);
            }
            else
            {
                layoutRect = new Rect(0, (finalSize.Height - finalSize.Width) / 2, finalSize.Width, finalSize.Width);
            }
            double angleInc = 360 / InternalChildren.Count;
            double angle = 0;
            foreach (UIElement child in InternalChildren)
            {
                Point childLocation = new Point(layoutRect.Left + (layoutRect.Width - child.DesiredSize.Width) / 2, layoutRect.Top);

                child.RenderTransform = new RotateTransform(angle, child.DesiredSize.Width / 2, finalSize.Height / 2 - layoutRect.Top);
                angle += angleInc;
                child.Arrange(new Rect(childLocation, child.DesiredSize));
            }
            return finalSize;
        }
    }

}

