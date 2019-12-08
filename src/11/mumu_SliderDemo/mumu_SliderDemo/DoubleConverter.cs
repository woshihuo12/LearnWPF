using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace mumu_SliderDemo
{
    public class DoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
        System.Globalization.CultureInfo culture)
        {
            Byte rgbValue = System.Convert.ToByte(value) ;
            string txtRGBValue = rgbValue.ToString();
            return txtRGBValue;
        }
        public object ConvertBack(object value, Type targetType, object parameter,
System.Globalization.CultureInfo culture)
        {
            string str = (string)value;
            double dbValue = double.Parse(str);
            return dbValue;
        }
    }
}
