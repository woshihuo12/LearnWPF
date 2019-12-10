using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mumu_Button01
{
    class BindingData
    {
        public BindingData()
        {
            ColorName = "Red";
        }

        private string name = "Red";

        public string ColorName
        {
            get { return name; }
            set
            {
                name = value;
            }
        }

    }

}
