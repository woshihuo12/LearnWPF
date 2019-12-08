using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mumu_whitebee
{
  
    class Program
    {
        
        static void Main(string[] args)
        {
            // 第一步 人物介绍
            XiaoLongnv longnv = new XiaoLongnv();   //小龙女
            LaoWantong wantong = new LaoWantong();  //老顽童
            Huangrong rong = new Huangrong();       //黄蓉
            YangGuo guo = new YangGuo();            //杨过

            // 第二步 订阅事件,唯独没有订阅杨过的ProcessBeeLetter;
            longnv.WhiteBeeEvent += wantong.ProcessBeeLetter;
            longnv.WhiteBeeEvent += rong.ProcessBeeLetter;
            // longnv.WhiteBeeEvent += guo.ProcessBeeLetter; //杨过是没有订阅小龙女的玉蜂事件

            // 第三步 小龙女玉蜂传信
            longnv.OnFlyBee();

            // 第四步 杨过叹息
            guo.Sign();

            Console.ReadKey();

        }
    }
}
