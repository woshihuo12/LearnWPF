using System;
using System.Windows;
using System.Windows.Media;

namespace mumu_customcontrol
{
	public class Guage : FrameworkElement
	{
		public int Ticks { get; set; }
		public Size TickSize { get; set; }
		public Brush TickBrush { get; set; }

		protected override void OnRender(DrawingContext dc)
		{
			double radius = Math.Min((RenderSize.Width - TickSize.Width) / 2,
										(RenderSize.Height - TickSize.Height) / 2);
			Point center = new Point((RenderSize.Width - TickSize.Width) / 2,
										(RenderSize.Height - TickSize.Height) / 2);

			for (int i = 0; i <= Ticks; i++)
			{
				double ratio = (double)i / Ticks;
				double x = center.X + radius * Math.Cos(Math.PI * ratio);
				double y = center.Y - radius * Math.Sin(Math.PI * ratio);

				Rect currRect = new Rect(TickSize);
				currRect.Offset(x, y);
				Point strokeCenter = new Point(currRect.X + 0.5 * currRect.Width,
												currRect.Y + 0.5 * currRect.Height);

				RotateTransform rotation = new RotateTransform(90 - 180 * ratio,
													strokeCenter.X, strokeCenter.Y);
				dc.PushTransform(rotation);
				dc.DrawRectangle(TickBrush, null, currRect);
				dc.Pop();
			}
		}
	}
}