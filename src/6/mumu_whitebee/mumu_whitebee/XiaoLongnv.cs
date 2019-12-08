using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mumu_whitebee
{
    public delegate void WhiteBee(string param); //声明了玉蜂的委托
    // 小龙女类
    class XiaoLongnv
    {
        public event WhiteBee WhiteBeeEvent;    //玉蜂事件
        public void OnFlyBee()
        {
            Console.WriteLine("小龙女在谷底日复一日地放着玉蜂，希望杨过有一天能看到.....");
            WhiteBeeEvent(msg);
        }
        private string msg = "我在绝情谷底";
    }
}
