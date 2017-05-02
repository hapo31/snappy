using DnDScreenCapture.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDScreenCapture.Service
{
    class WindowInfo
    {
        /// <summary>
        /// ウインドウハンドルから各種情報を収集
        /// </summary>
        /// <param name="windowHandle">ウインドウハンドル</param>
        public WindowInfo(IntPtr windowHandle)
        {
            this.windowHandle = windowHandle;
            Win32API.GetWindowRect(windowHandle, out rect);
            var text = new StringBuilder(128);
            Win32API.GetWindowText(windowHandle, text, text.Capacity);
            windowText = text.ToString();
        }

        private IntPtr windowHandle;
        private string windowText;
        private Win32API.RECT rect;

        public IntPtr Handle
        {
            get
            {
                return windowHandle;
            }
        }

        public Rectangle WinodwRect
        {
            get
            {
                return rect.Rectangle;
            }
        }

        public string WindowText
        {
            get
            {
                return windowText;
            }
        }
    }
}
