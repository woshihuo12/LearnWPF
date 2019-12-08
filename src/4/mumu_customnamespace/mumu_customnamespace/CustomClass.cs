using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Markup;
using System.ComponentModel;

namespace mumu_customnamespace
{
    [ContentProperty("Name")]
    public class Book
    {
        //public Book() { }
        public Book(string name, double price) { }

        [TypeConverter(typeof(StringConverter))]
        public string Name { get; set; }

        public double Price { get; set; }

        public override string ToString()
        {
            string str = Name + "售价为：" + Price + "元";
            return str;
        }
    }




    public struct BookStruct2
    {
        
        public string Name { get; set; }
        public double Price { get; set; }
        
        public override string ToString()
        {
            string str =  Name + "售价为：" + Price + "元";
            return str;
        }

    }
}