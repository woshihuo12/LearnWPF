using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace mumu_customcontrol
{
    public class NumericUpDownFromControl : Control
    {

        public NumericUpDownFromControl()
        {
            
        }

        static NumericUpDownFromControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericUpDownFromControl),
                       new FrameworkPropertyMetadata(typeof(NumericUpDownFromControl)));
            InitializeCommands();
        }


        private const decimal MinValue = 0, MaxValue = 100;

        #region 依赖属性
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                "Value", typeof(decimal), typeof(NumericUpDownFromControl),
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
            NumericUpDownFromControl control = (NumericUpDownFromControl)element;

            newValue = Math.Max(MinValue, Math.Min(MaxValue, newValue));

            return newValue;
        }


        #endregion
        #region 路由事件
        public static readonly RoutedEvent ValueChangedEvent = EventManager.RegisterRoutedEvent(
            "ValueChanged", RoutingStrategy.Bubble,
            typeof(RoutedPropertyChangedEventHandler<decimal>), typeof(NumericUpDownFromControl));


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
            NumericUpDownFromControl control = (NumericUpDownFromControl)obj;

            RoutedPropertyChangedEventArgs<decimal> e = new RoutedPropertyChangedEventArgs<decimal>(
                (decimal)args.OldValue, (decimal)args.NewValue, ValueChangedEvent);
            control.OnValueChanged(e);
        }
        #endregion

        #region 命令
        private static RoutedCommand _increaseCommand;
        private static RoutedCommand _decreaseCommand;

        public static RoutedCommand IncreaseCommand
        {
            get
            {
                return _increaseCommand;
            }
        }
        public static RoutedCommand DecreaseCommand
        {
            get
            {
                return _decreaseCommand;
            }
        }

        private static void InitializeCommands()
        {
            _increaseCommand = new RoutedCommand("IncreaseCommand", typeof(NumericUpDownFromControl));
            CommandManager.RegisterClassCommandBinding(typeof(NumericUpDownFromControl),
                                    new CommandBinding(_increaseCommand, OnIncreaseCommand));
            CommandManager.RegisterClassInputBinding(typeof(NumericUpDownFromControl),
                                    new InputBinding(_increaseCommand, new KeyGesture(Key.Up)));

            _decreaseCommand = new RoutedCommand("DecreaseCommand", typeof(NumericUpDownFromControl));
            CommandManager.RegisterClassCommandBinding(typeof(NumericUpDownFromControl),
                                    new CommandBinding(_decreaseCommand, OnDecreaseCommand));
            CommandManager.RegisterClassInputBinding(typeof(NumericUpDownFromControl),
                                    new InputBinding(_decreaseCommand, new KeyGesture(Key.Down)));
        }

        private static void OnIncreaseCommand(object sender, ExecutedRoutedEventArgs e)
        {
            NumericUpDownFromControl control = sender as NumericUpDownFromControl;
            if (control != null)
            {
                control.OnIncrease();
            }
        }
        private static void OnDecreaseCommand(object sender, ExecutedRoutedEventArgs e)
        {
            NumericUpDownFromControl control = sender as NumericUpDownFromControl;
            if (control != null)
            {
                control.OnDecrease();
            }
        }

        protected virtual void OnIncrease()
        {
            Value++;
        }
        protected virtual void OnDecrease()
        {
            Value--;
        }
        #endregion
    }
}
