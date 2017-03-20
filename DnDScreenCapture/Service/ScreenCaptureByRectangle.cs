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
            Bitmap bitmap = new Bitmap((int)targetRect.Width, (int)targetRect.Height);
            Graphics g = Graphics.FromImage(bitmap);
            g.CopyFromScreen(new Point((int)targetRect.Left, (int)targetRect.Top),
                            new Point((int)targetRect.Right, (int)targetRect.Bottom),
                            bitmap.Size);

            g.Dispose();
            return bitmap;
        }
    }
}
