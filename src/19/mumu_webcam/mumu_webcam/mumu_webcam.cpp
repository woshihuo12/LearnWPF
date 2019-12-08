#include "stdafx.h"

#include "Webcam.h"
#ifdef _MANAGED
#pragma managed(push, off)
#endif

HINSTANCE hInstance;	
BOOL APIENTRY DllMain( HINSTANCE hInst,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
					 )
{
    hInstance = hInst;
	return TRUE;
}

#ifdef _MANAGED
#pragma managed(pop)
#endif


INT_PTR CALLBACK About(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
	HWND hwnd;
	RECT rect;
	switch (message)
	{
	case WM_INITDIALOG:
		hwnd = ::GetDlgItem(hDlg,IDC_VIDEO);
		
		::GetWindowRect(hwnd,&rect);

		if (FAILED(Webcam::Initialize((rect.right - rect.left), (rect.bottom - rect.top))))
				::MessageBox(NULL, L"初始化摄像头设备失败", L"错误", 0);
			
		Webcam::AttachToWindow(hwnd);
		Webcam::Start();
		return (INT_PTR)TRUE;

	case WM_COMMAND:
		if (LOWORD(wParam) == ID_START)
		{
			Webcam::Start();
		}
		else if(LOWORD(wParam) == ID_STOP)
		{
			Webcam::Stop();
		}
		else if(LOWORD(wParam) == ID_PAUSE)
		{
			Webcam::Pause();
		}
		break;

	case WM_DESTROY:
		Webcam::Terminate();
		break;

	}
	return (INT_PTR)FALSE;
}

namespace ManagedCpp
{
	using namespace System;
	using namespace System::Windows;
    using namespace System::Windows::Interop;
    using namespace System::Windows::Input;
    using namespace System::Windows::Media;
    using namespace System::Runtime::InteropServices;

	HWND dialog;
    public ref class MyHwndHost : public HwndHost, IKeyboardInputSink {
    protected: 
        virtual HandleRef BuildWindowCore(HandleRef hwndParent) override {           
            dialog = CreateDialog(hInstance,  
                MAKEINTRESOURCE(IDD_VIDEODLG), 
                (HWND) hwndParent.Handle.ToPointer(),
                (DLGPROC) About); 
			
			

            return HandleRef(this, IntPtr(dialog));
        }

        virtual void DestroyWindowCore(HandleRef hwnd) override {
			
            ::DestroyWindow((HWND)hwnd.Handle.ToInt32());
        }
		public: 
        virtual bool TabInto(TraversalRequest^ request) override 
		{
			FocusNavigationDirection dir= request->FocusNavigationDirection;
			if (request->FocusNavigationDirection == FocusNavigationDirection::Last) 
			{
                HWND lastTabStop = GetDlgItem(dialog, ID_STOP);
                SetFocus(lastTabStop);
            }
            else 
			{
                HWND firstTabStop = GetDlgItem(dialog, IDC_EDIT1);
                SetFocus(firstTabStop);
            }
            return true;
        }

        ::MSG ConvertMessage(System::Windows::Interop::MSG% msg) 
		{
            ::MSG m;
            m.hwnd = (HWND) msg.hwnd.ToPointer();
            m.lParam = (LPARAM) msg.lParam.ToPointer();
            m.message = msg.message;
            m.wParam = (WPARAM) msg.wParam.ToPointer();
            
            m.time = msg.time;

            POINT pt;
            pt.x = msg.pt_x;
            pt.y = msg.pt_y;
            m.pt = pt;

            return m;
		}

		#undef TranslateAccelerator
        virtual bool TranslateAccelerator(System::Windows::Interop::MSG% msg, 
            ModifierKeys modifiers) override 
        {

			if(msg.message == WM_KEYDOWN && msg.wParam == IntPtr(VK_TAB))
			{
				HWND firstTabStop = GetDlgItem(dialog, IDC_EDIT1);
                HWND lastTabStop = GetDlgItem(dialog, ID_STOP);
                TraversalRequest^ request = nullptr;
				SHORT keystate = GetKeyState(VK_SHIFT);
				BYTE downstate = HIBYTE(keystate);
				BYTE togglestate = LOBYTE(keystate);
				if(downstate)
				{
					if(GetFocus()==firstTabStop)
					{
						request = gcnew TraversalRequest(FocusNavigationDirection::Previous);
						return ((IKeyboardInputSink^)this)->KeyboardInputSite->OnNoMoreTabStops(request);
					}
				}
				else
				{
					if(GetFocus()==lastTabStop)
					{
						request = gcnew TraversalRequest(FocusNavigationDirection::Next);
						return ((IKeyboardInputSink^)this)->KeyboardInputSite->OnNoMoreTabStops(request);
					}
				}
			}

			  
			 if (msg.message == WM_SYSKEYDOWN || msg.message == WM_KEYDOWN )
			 {
				 ::MSG m = ConvertMessage(msg);
				 switch (m.wParam) {
                    case VK_TAB:
                    case VK_LEFT:
                    case VK_UP:
                    case VK_RIGHT:
                    case VK_DOWN:
                    case VK_EXECUTE:
                    case VK_RETURN:
                    case VK_ESCAPE:
                    case VK_CANCEL:
					// case VK_SHIFT:
                        IsDialogMessage(dialog, &m);
                        // IsDialogMessage should be called ProcessDialogMessage --
                        // it processes messages without ever really telling you
                        // if it handled a specific message or not
                        return true;
                }
            }

            return false; // not a key we handled
        }

		virtual bool OnMnemonic(System::Windows::Interop::MSG% msg, ModifierKeys modifiers) override {
            ::MSG m = ConvertMessage(msg);

            // If it's one of our mnemonics, set focus to the appropriate hwnd
            if (msg.message == WM_SYSCHAR && GetKeyState(VK_MENU /*alt*/)) {
                int dialogitem = 9999;
                switch (m.wParam) {
                    case 'a': dialogitem = IDC_EDIT1; break;
                    case 'b': dialogitem = IDC_EDIT2; break;
                }
                if (dialogitem != 9999) {
                    HWND hwnd = GetDlgItem(dialog, dialogitem);
                    SetFocus(hwnd);
                    return true;
                }
            }
            return false; // key unhandled
        };
		
 };
}