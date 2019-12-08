using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mumu_whitebee
{
    // 黄蓉类
    class Huangrong
    {
        public void ProcessBeeLetter(object sender, WhiteBeeEventArgs e)
        {
            Console.WriteLine("黄蓉：\"{0}\",莫非......",e._msg);
        }
    }
}
