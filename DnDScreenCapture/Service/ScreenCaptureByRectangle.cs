using System;
using System.Windows;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing.Imaging;
using DnDScreenCapture.Utils;
using System.Windows.Forms;

namespace DnDScreenCapture.Service
{
    using Point = System.Drawing.Point;
    class ScreenCaptureByRectangle
    {
        private Rectangle targetRect;
        public ScreenCaptureByRectangle(Rect target)
        {
            this.targetRect = new Rectangle((int)target.Left, (int)target.Bottom, (int)target.Width, (int)target.Height);
        }

        public ScreenCaptureByRectangle(Rectangle target)
        {
            this.targetRect = target;
        }

        public Bitmap capture()
        {
            var bitmap = new Bitmap((int)targetRect.Width, (int)targetRect.Height);
            var g = Graphics.FromImage(bitmap);
            g.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
            g.Dispose();
            return bitmap;
        }

        public Bitmap capture(IntPtr Hwnd)
        {
            var bitmap = new Bitmap((int)targetRect.Width, (int)targetRect.Height);
            var g = Graphics.FromImage(bitmap);
            g.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
            
            g.Dispose();
            return bitmap;
        }

        public static Bitmap[] CaptureAllScreen()
        {
            return Screen.AllScreens.Select(s =>
            {
                
                var bmp = new Bitmap(s.Bounds.Width, s.Bounds.Height);
                var g = Graphics.FromImage(bmp);
                
                //g.CopyFromScreen(s.Bounds.Left, s.Bounds.Top, s.Bounds.Width, s.Bounds.Height, bmp.Size, CopyPixelOperation.PatCopy);
                g.CopyFromScreen(0, 0, 0, 0, bmp.Size, CopyPixelOperation.CaptureBlt);
                g.Dispose();
                return bmp;
            }).ToArray();
        }
    }
}
