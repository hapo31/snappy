using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DnDScreenCapture.Utils
{
    class Win32API
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int top;
            public int left;
            public int right;
            public int bottom;

            public int Width { get { return right - left; } }

            public int Height { get { return bottom - top; } }

            public System.Drawing.Rectangle Rectangle
            {
                get { return new System.Drawing.Rectangle(left, top, Width, Height); }
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Point
        {
            public long x;
            public long y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MONITORINFOEX
        {
            public uint cbSize;
            public RECT rcMonitor;
            public RECT rcWork;
            public uint dwFlags;
            public string szDevice;
        }


        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hwnd, out RECT rect);

        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hwnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        public static extern bool ClientToScreen(IntPtr hwnd, out Point result);

        [DllImport("user32.dll")]
        public static extern bool PrintWindow(IntPtr hwnd, IntPtr hdcBlt, int nFlag);

        const int MONITOR_DEFAULTTONULL = 0x00000000;
        const int MONITOR_DEFAULTTOPRIMARY = 0x00000001;
        const int MONITOR_DEFAULTTONEAREST = 0x00000002;

        [DllImport("user32.dll", CharSet=CharSet.Unicode)]
        public static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags = MONITOR_DEFAULTTONEAREST);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern bool GetMonitorInfo(IntPtr hMonitor, out MONITORINFOEX lpmi);
        
        public static MONITORINFOEX GetMonitorInfomation(IntPtr hwnd)
        {
            var result = new MONITORINFOEX();
            var hmonitor = MonitorFromWindow(hwnd);

            if (GetMonitorInfo(hmonitor, out result))
            {
                return result;
            }
            else
            {
                throw new InvalidOperationException();
            }
            
        }

    }
}
