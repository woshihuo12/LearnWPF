using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
namespace mumu_rongerwithdatabinding
{
    public class Person : INotifyPropertyChanged 
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void Notify(string propName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        // 姓名属性
        string name;
        public string Name
        {
            get { return this.name; }
            set
            {
                if (this.name == value) { return; }
                this.name = value;
                Notify("Name");
            }
        }

        // 年龄属性
        int age;
        public int Age
        {
            get { return this.age; }
            set
            {
                if (this.age == value) { return; }
                this.age = value;
                Notify("Age");
               
            }
        }
        // 构造函数
        public Person() { }
        public Person(string name, int age)
        {
            this.name = name;
            this.age = age;
        }
        //public override string ToString()
        //{
        //    return name.ToString();
        //}



    }

    public class People : ObservableCollection<Person>
    {
        public People()
            : base()
        {
            Add(new Person("木木", 22));
            Add(new Person("黄蓉", 20));
            Add(new Person("黄药师", 40));
            Add(new Person("士兵甲", 22));
            Add(new Person("士兵乙", 20));
            Add(new Person("士兵丙", 28));

        }
    }
}
