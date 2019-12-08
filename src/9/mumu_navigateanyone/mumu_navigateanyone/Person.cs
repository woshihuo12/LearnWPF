using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mumu_navigateanyone
{
    class Person
    {
        private string _name;
        private string _sex;
        private int _age;
        public Person(string name, string sex,int age)
        {
            this._name = name;
            this._sex = sex;
            this._age = age;
        }
        public override string ToString()
        {
            return "姓名：" + this._name + "\n" + "性别：" + this._sex + "\n" + "年龄：" + this._age;
        }
    }
}
