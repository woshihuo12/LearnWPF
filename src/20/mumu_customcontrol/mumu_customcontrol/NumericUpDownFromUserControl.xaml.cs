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

namespace mumu_customcontrol
{
    /// <summary>
    /// Interaction logic for NumericUpDownFromUserControl.xaml
    /// </summary>
    public partial class NumericUpDownFromUserControl : UserControl
    {
        public NumericUpDownFromUserControl()
        {
            InitializeComponent();
        }
        private const decimal MinValue = 0, MaxValue = 100;

        #region 依赖属性
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                "Value", typeof(decimal), typeof(NumericUpDownFromUserControl),
                new FrameworkPropertyMetadata(MinValue, new PropertyChangedCallback(OnValueChanged),
                                              new CoerceValueCallback(CoerceValue)));

        public decimal Value
        {
            get { return (decimal)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        private static object CoerceValue(DependencyObject element, object value)
        {
            decimal newValue = (decimal)value;
            NumericUpDownFromUserControl control = (NumericUpDownFromUserControl)element;

            newValue = Math.Max(MinValue, Math.Min(MaxValue, newValue));

            return newValue;
        }


        #endregion
        #region 路由事件
        public static readonly RoutedEvent ValueChangedEvent = EventManager.RegisterRoutedEvent(
            "ValueChanged", RoutingStrategy.Bubble,
            typeof(RoutedPropertyChangedEventHandler<decimal>), typeof(NumericUpDownFromUserControl));


        public event RoutedPropertyChangedEventHandler<decimal> ValueChanged
        {
            add { AddHandler(ValueChangedEvent, value); }
            remove { RemoveHandler(ValueChangedEvent, value); }
        }
        protected virtual void OnValueChanged(RoutedPropertyChangedEventArgs<decimal> args)
        {
            RaiseEvent(args);
        }

        private static void OnValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            NumericUpDownFromUserControl control = (NumericUpDownFromUserControl)obj;

            RoutedPropertyChangedEventArgs<decimal> e = new RoutedPropertyChangedEventArgs<decimal>(
                (decimal)args.OldValue, (decimal)args.NewValue, ValueChangedEvent);
            control.OnValueChanged(e);
        }
        #endregion

        private void upButton_Click(object sender, EventArgs e)
        {
            Value++;
        }

        private void downButton_Click(object sender, EventArgs e)
        {
            Value--;
        }

    }
}
