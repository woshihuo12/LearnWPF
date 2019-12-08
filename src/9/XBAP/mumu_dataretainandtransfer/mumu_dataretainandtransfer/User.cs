using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mumu_dataretainandtransfer
{
    public class User
    {
        private string _name;
        private string _password;
        private List<string> _favColors;

        public User() { }
        public User(string name,string password)
        {
            this._name = name;
            this._password = password;
            _favColors = new List<string>();
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public List<string> FavColors
        {
            get { return _favColors; }
            set { _favColors = value; }
        }
        public override string ToString()
        {
            return this._name;
        }

        

      
    }
}
