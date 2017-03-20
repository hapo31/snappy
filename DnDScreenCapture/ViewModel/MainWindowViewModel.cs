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
            IntPtr a = Win32API.GetForegroundWindow();
            var targetRect = new Win32API.RECT();
            Win32API.GetClientRect(a, out targetRect);
            var sc = new ScreenCaptureByRectangle(targetRect.Rect);
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
            var sc = new ScreenCaptureByRectangle(new System.Windows.Rect(0, 0, 1000, 1000));
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
