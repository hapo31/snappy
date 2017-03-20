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

            public System.Windows.Rect Rect
            {
                get { return new System.Windows.Rect(left, top, Width, Height); }
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Point
        {
            public long x;
            public long y;
        }


        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hwnd, out RECT rect);

        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hwnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        public static extern bool ClientToScreen(IntPtr hwnd, out Point result);

        //EnumMonitorFunc
    }
}
