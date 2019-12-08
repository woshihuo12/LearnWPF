using System;
using System.Windows;

class mumu_hellowpf
{
	[STAThread]
	public static void Main()
	{
		Window win = new Window();
		win.Title = "Hello mumu!";
      		 win.Width = 300;
       		 win.Height = 200;
        		win.Show();
		
		Application app = new Application();
		app.Run();
	}
}