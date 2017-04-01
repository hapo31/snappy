using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DnDScreenCapture.Utils;
using DnDScreenCapture.Service;
using System.IO;
using System.Windows.Media.Imaging;

namespace DnDScreenCapture.ViewModel
{
    using Point = System.Windows.Point;
    class MainWindowViewModel: INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
        }

        public void Initialized()
        {
            // 雑に最前面ウインドウを撮影する
            IntPtr a = Win32API.GetForegroundWindow();
            var targetRect = new Win32API.RECT();
            //var monitor = Win32API.GetMonitorInfomation(a);

            Win32API.GetClientRect(a, out targetRect);
            var sc = new ScreenCaptureByRectangle(targetRect.Rectangle);
            ImageSrc = sc.capture().GetBitmapFrame();

            int i = 0;
            ScreenCaptureByRectangle.CaptureAllScreen().ToList().ForEach(b =>
            {
                b.Save($"cap_{i}.bmp");
                ++i;
            });
        }

        private BitmapFrame imageSrc;
        public BitmapFrame ImageSrc {
            get
            {
                return imageSrc;
            }
            set
            {
                imageSrc = value;
                PropertyChanged.Notice(this);
            }
        }

        private Point StartClickPoint
        {
            get; set;
        } = new Point(-1, -1);

        public event PropertyChangedEventHandler PropertyChanged;

        public void StartDrag(Point position)
        {

        }

        public void EndDrag(Point start, Point end)
        {

        }
    }
}
