using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mumu_withoutdatabinding
{
    public class Person 
    {
        // 姓名属性
        string name;
        public string Name
        {
            get { return this.name; }
            set
            {
                if (this.name == value) { return; }
                this.name = value;

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
               
            }
        }
        // 构造函数
        public Person() { }
        public Person(string name, int age)
        {
            this.name = name;
            this.age = age;
        }
    }
}
