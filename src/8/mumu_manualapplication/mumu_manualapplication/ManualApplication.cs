using System;
using System.Windows;

namespace mumu_manualapplication
{
    class ManualApplication
    {
        [STAThread]
        public static void Main()
        {
            Window win = new Window();
            win.Title = "手动创建一个应用程序";
            win.Show();
            Application app = new Application();
            app.Run();
        }
    }
}

