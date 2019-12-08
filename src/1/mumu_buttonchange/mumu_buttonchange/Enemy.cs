using System;
using System.ComponentModel;
using System.Windows;

namespace mumu_buttonchange
{
	public class Enemy : INotifyPropertyChanged
	{
		private Point _location;
		private Vector _velocity;
		private string _type;
		private double _angle;

		public Point Location
		{
			get { return _location; }
			set
			{
				_location = value;
				NotifyPropertyChanged("Location");
			}
		}

		public Vector Velocity
		{
			get { return _velocity; }
			set
			{
				_velocity = value;
				Angle = Math.Atan2(_velocity.Y, _velocity.X) * 180/Math.PI;
				NotifyPropertyChanged("Velocity");
			}
		}

		public double Angle
		{
			get
			{
				return _angle;
			}
			private set
			{
				_angle = value;
				NotifyPropertyChanged("Angle");
			}
		}

		public string Type
		{
			get { return _type; }
			set
			{
				_type = value;
				NotifyPropertyChanged("Type");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void NotifyPropertyChanged(string propName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propName));
			}
		}
	}
}