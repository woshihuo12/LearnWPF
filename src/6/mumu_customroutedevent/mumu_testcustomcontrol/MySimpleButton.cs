using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
namespace mumu_testcustomcontrol
{
    public class MySimpleButton : Button
    {
        static MySimpleButton()
        {      
            // 将CustomClickEvent和一个Class Handler关联起来
            EventManager.RegisterClassHandler(typeof(MySimpleButton), CustomClickEvent, new RoutedEventHandler(CustomClickClassHandler), false);
        }

        

        // 创建和注册该事件，该事件路由策略为Bubble
        public static readonly RoutedEvent CustomClickEvent = EventManager.RegisterRoutedEvent(
            "CustomClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(MySimpleButton));


        // CLR事件的包装器
        public event RoutedEventHandler CustomClick
        {
            add { AddHandler(CustomClickEvent, value); }
            remove { RemoveHandler(CustomClickEvent, value); }
        }

        // 触发CustomClickEvent
        void RaiseCustomClickEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(MySimpleButton.CustomClickEvent);
            RaiseEvent(newEventArgs);
        }

        // OnClice触发CustomClickEvent
        protected override void OnClick()
        {
            RaiseCustomClickEvent();
        }

        // 普通CLR事件
        public event EventHandler ClassHandlerProcessed; 

        public static void CustomClickClassHandler(object sender, RoutedEventArgs e)
        {
            MySimpleButton simpleBtn = sender as MySimpleButton;
            EventArgs args = new EventArgs();
            simpleBtn.ClassHandlerProcessed(simpleBtn, args);   
        }
 
    }
}
