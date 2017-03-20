using System;
using System.Windows;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing.Imaging;

namespace DnDScreenCapture.Service
{
    using Point = System.Drawing.Point;
    class ScreenCaptureByRectangle
    {
        private Rect targetRect;
        public ScreenCaptureByRectangle(Rect target)
        {
            this.targetRect = target;
        }

        public Bitmap capture()
        {
            Bitmap bitmap = new Bitmap((int)targetRect.Width, (int)targetRect.Height);
            Graphics g = Graphics.FromImage(bitmap);
            g.CopyFromScreen(new Point(0, 0),
                            new Point(0, 0),
                            bitmap.Size);

            g.Dispose();
            return bitmap;
        }
    }
}
