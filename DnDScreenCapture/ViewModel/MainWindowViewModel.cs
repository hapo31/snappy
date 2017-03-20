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
            Win32API.GetClientRect(a, out targetRect);
            //var sc = new ScreenCaptureByRectangle(targetRect.Rectangle);
            var sc = new ScreenCaptureByRectangle(new Rectangle(-400, 400, 1200, 1200));
            ImageSrc = sc.capture().GetBitmapFrame();
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


        public void OnClickButton()
        {
            var sc = new ScreenCaptureByRectangle(new System.Windows.Rect(-100, -220, 200, 200));
            ImageSrc = sc.capture().GetBitmapFrame();
        }


        public void StartDrag(Point position)
        {

        }

        public void EndDrag(Point start, Point end)
        {

        }
    }
}
