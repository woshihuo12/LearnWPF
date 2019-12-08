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

namespace mumu_rongerwithdatabinding
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AddPerson : Window
    {
        

        public AddPerson()
        {
            InitializeComponent();

            FrameworkPropertyMetadata metadata = TextBox.TextProperty.GetMetadata(nameTextBox) as FrameworkPropertyMetadata;
            bool res = metadata.BindsTwoWayByDefault;
           UpdateSourceTrigger updatesourcetrigger =  metadata.DefaultUpdateSourceTrigger;
           
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Person person = (Person)this.FindResource("mumu");
            MessageBox.Show( string.Format(
          "姓名{0}, 年龄 {1}!",person.Name,person.Age));

            BindingExpression be = nameTextBox.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();

        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            Person person = (Person)this.FindResource("mumu");
            person.Name = "木木";
            person.Age = 22;
        }
    }


    public class AgeToForegroundConverter : IValueConverter
    {
        // Called when converting the Age to a Foreground brush
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // Only convert to brushes...
            if (targetType != typeof(Brush)) { return null; }

            // DANGER! After 25, it's all down hill...
            int age = int.Parse(value.ToString());
            return (age < 18 ? Brushes.Red : Brushes.Black);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class NumberRangeRule : ValidationRule
    {
        int _min;
        public int Min
        {
            get { return _min; }
            set { _min = value; }
        }

        int _max;
        public int Max
        {
            get { return _max; }
            set { _max = value; }
        }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            int number;
            if (!int.TryParse((string)value, out number))
            {
                return new ValidationResult(false, "非数值性字符");
            }

            if (number < _min || number > _max)
            {
                string s = string.Format("超出正常人的年龄范围 ({0}-{1})", _min, _max);
                return new ValidationResult(false, s);
            }

            return ValidationResult.ValidResult; // static validation result to save on garbage
        }
    }
}
