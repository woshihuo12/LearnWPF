#include "resource.h"
#include <windows.h>
LRESULT CALLBACK About(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam);


int WINAPI WinMain(HINSTANCE hInstance,
    HINSTANCE hPrevInstance,
    LPSTR lpCmdLine,
    int nCmdShow)
{
	DialogBox(hInstance, (LPCTSTR)IDD_DATETIMEDLG, NULL, (DLGPROC)About);
    return 0;
}


namespace ManagedCode
{
    using namespace System;
    using namespace System::Windows;
    using namespace System::Windows::Interop;
    using namespace System::Windows::Media;
	

    HWND GetHwnd(HWND parent, int x, int y, int width, int height,int type) 
	{
        HwndSource^ source = gcnew HwndSource(
            0, 
            WS_VISIBLE | WS_CHILD, // style
            0, // exstyle
            x, y, width, height,
            "hi", // NAME
            IntPtr(parent)        // parent window 
            );
        UIElement^ page;
		if(type == 0)
			page = gcnew WPFClock::AnalogClock();
		else
			page = gcnew WPFClock::DigitalClock();

		WPFClock::IPosition^ pos = (WPFClock::IPosition^)page;
		pos->SetDesiredWidthAndHeight(width,height);
        source->RootVisual = page;
        return (HWND) source->Handle.ToPointer();
    }
}

HWND AnalogClock;
HWND DigitalClock;
LRESULT CALLBACK About(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
    switch (message)
    {
    case WM_INITDIALOG:
        { 
			HWND placeholder = GetDlgItem(hDlg, IDC_CLOCK);
			int result;
			RECT rectangle;
			GetWindowRect(placeholder, &rectangle);
			int width = rectangle.right - rectangle.left;
			int height = rectangle.bottom - rectangle.top;
			POINT point;
			point.x = rectangle.left;
			point.y = rectangle.top;
			result = MapWindowPoints(NULL, hDlg, &point, 1);
			ShowWindow(placeholder, SW_HIDE);
			AnalogClock= ManagedCode::GetHwnd(hDlg, point.x, point.y, width, height,0);
			DigitalClock= ManagedCode::GetHwnd(hDlg, point.x, point.y, width, height,1);
			ShowWindow(DigitalClock,SW_HIDE);

            return TRUE;
        }
	case WM_COMMAND: 
	   if (LOWORD(wParam) == IDOK || LOWORD(wParam)== IDCANCEL)
        {
            EndDialog(hDlg, LOWORD(wParam));
            return TRUE;
        }
	   else if(LOWORD(wParam) == ID_CHANGE)
	   {
		   BOOL isvisable = ::IsWindowVisible(AnalogClock);
			if(isvisable)
			{
				::ShowWindow(AnalogClock,SW_HIDE);
				::ShowWindow(DigitalClock,SW_SHOW);
			}
			else
			{
				::ShowWindow(DigitalClock,SW_HIDE);
				::ShowWindow(AnalogClock ,SW_SHOW);
			}
	   }
        break;
    case WM_CLOSE:
        {
            EndDialog(hDlg, LOWORD(wParam));
            return TRUE;
        }
        break;
    }
    return FALSE;
}
