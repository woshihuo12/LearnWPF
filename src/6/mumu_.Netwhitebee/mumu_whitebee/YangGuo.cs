using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mumu_whitebee
{
    // 杨过类
    class YangGuo
    {
        public void ProcessBeeLetter(object sender, WhiteBeeEventArgs e)
        {
            // 真的是龙儿吗？
            XiaoLongnv longnv = sender as XiaoLongnv;
            if(longnv != null)
                Console.WriteLine("杨过：\"{0}\",我一定会找她！", e._msg);
        }
        public void Sign()
        {
            Console.WriteLine("杨过叹息：龙儿，你在哪儿....");
        }
    }

    

}
