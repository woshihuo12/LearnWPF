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
using System.Windows.Threading;

namespace mumu_buttonchange
{
	/// <summary>
	/// Interaction logic for RadarExample.xaml
	/// </summary>
	public partial class RadarExample : UserControl
	{
		private DispatcherTimer _timer;

		public RadarExample()
		{
			InitializeComponent();
			Loaded += OnExampleLoaded;
		}

		private void OnExampleLoaded(object sender, RoutedEventArgs e)
		{
			_timer = new DispatcherTimer(DispatcherPriority.Normal);
			_timer.Interval = TimeSpan.FromSeconds(0.25);
			_timer.Tick += MoveEnemy;
			_timer.Start();
		}

		private void MoveEnemy(object sender, EventArgs e)
		{
			foreach (Enemy enemy in _enemies.Items)
			{
				if (enemy.Location.X < 0 || enemy.Location.X > _enemies.ActualWidth)
				{
					enemy.Velocity = new Vector(-1*enemy.Velocity.X, enemy.Velocity.Y);
				}
				if (enemy.Location.Y < 0 || enemy.Location.Y > _enemies.ActualHeight)
				{
					enemy.Velocity = new Vector(enemy.Velocity.X, -1*enemy.Velocity.Y);
				}

				enemy.Location = Point.Add(enemy.Location, enemy.Velocity);
			}
		}
	}
}
