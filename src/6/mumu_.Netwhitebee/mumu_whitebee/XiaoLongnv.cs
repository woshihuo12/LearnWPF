using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mumu_whitebee
{
    public class WhiteBeeEventArgs : EventArgs
    {
        public readonly string _msg;
        public WhiteBeeEventArgs(string msg)
        {
            this._msg = msg;
        }
    }

    public delegate void WhiteBeeEventHandler(object sender,WhiteBeeEventArgs e); //声明了玉蜂的委托
    // 小龙女类
    class XiaoLongnv
    {
        public event WhiteBeeEventHandler WhiteBee;    //玉蜂事件
        public void OnFlyBee()
        {
            Console.WriteLine("小龙女在谷底日复一日地放着玉蜂，希望杨过有一天能看到.....");
            WhiteBeeEventArgs args = new WhiteBeeEventArgs(msg);
            WhiteBee(this, args);
        }
        private string msg = "我在绝情谷底";
    }
}
