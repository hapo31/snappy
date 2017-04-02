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

            Win32API.GetWindowRect(a, out targetRect);
            var sc = new ScreenCaptureByRectangle(targetRect.Rectangle);
            var windowText = new StringBuilder(128);

            Win32API.GetWindowText(a, windowText, windowText.Capacity);

            Console.WriteLine($"TargetWindow: [{windowText.ToString()}]({targetRect.Width},{targetRect.Height})");

            ImageSrc = sc.capture().GetBitmapFrame();
            ScreenSize = targetRect.Rectangle;

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

        private Rectangle screenSize;
        public Rectangle ScreenSize
        {
            get
            {
                return screenSize;
            }
            set
            {
                screenSize = value;
                PropertyChanged.Notice(this);
            }
        }

        private String tweetText;
        public String TweetText
        {
            get { return tweetText; }
            set
            {
                tweetText = value;
                PropertyChanged.Notice(this);
            }
        }

        private Point StartClickPoint
        {
            get; set;
        } = new Point(-1, -1);

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
