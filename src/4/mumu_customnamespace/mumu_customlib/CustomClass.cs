using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Markup;
using System.ComponentModel;
using System.Globalization;

namespace mumu_customlib
{
    [ContentProperty("Text")]
    public class Book
    {
        public Book() { }
        public string Name { get; set; }

        public MoneyType Price { get; set; }

       
        public string Text { get; set; }
        public override string ToString()
        {
            string str = Name + "售价为：" + Price + "元" + "\n" + Text;

            return str;
        }
    }

    [TypeConverter(typeof(MoneyConverter))]
    public class MoneyType
    {
        private double _value;
        public MoneyType() { _value=0;}
        public MoneyType(double value) { _value = value; }

        public override string ToString()
        {
            return _value.ToString();
        }
        public static MoneyType Parse(string value)
        {
            string str = (value as string).Trim();
            if (str[0] == '$')
            {
                string newprice = str.Remove(0, 1);
                double price = double.Parse(newprice);
                return new MoneyType(price * 8);
            }
            else
            {
                double price = double.Parse(str);
                return new MoneyType(price);
            }
        }
    }
    public struct BookStruct
    {
        
        public string Name { get; set; }
        public double Price { get; set; }
        
        public override string ToString()
        {
            string str =  Name + "售价为：" + Price + "元";
            return str;
        }

    }


    public class MoneyConverter : TypeConverter
    {
     
        public override bool CanConvertFrom(ITypeDescriptorContext context,
                                            Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;

            return base.CanConvertFrom(context, sourceType);
        }

       
        public override bool CanConvertTo(ITypeDescriptorContext context,
                                          Type destinationType)
        {
            if (destinationType == typeof(string))
                return true;

            return base.CanConvertTo(context, destinationType);
        }

       
        public override object ConvertFrom(ITypeDescriptorContext context,
                                           CultureInfo culture,
                                           object value)
        {
            if (value.GetType() != typeof(string))
                return base.ConvertFrom(context, culture, value);


            return MoneyType.Parse((string)value);
            
        }


        public override object ConvertTo(ITypeDescriptorContext context,
                                         CultureInfo culture,
                                         object value,
                                         Type destinationType)
        {
            if (destinationType == typeof(string))
                return base.ConvertTo(context, culture, value, destinationType);

            return value.ToString();
        }
    }




  
}