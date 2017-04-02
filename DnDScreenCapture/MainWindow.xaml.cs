using DnDScreenCapture.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DnDScreenCapture
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel vm;

        private Point StartClickPoint
        {
            get; set;
        } = new Point(-1, -1);

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = vm = new MainWindowViewModel();
            vm.Initialized();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            StartClickPoint = e.GetPosition(this);
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {

        }

        const int WM_PRINT = 0x0317;
        const int WM_PRINTCLIENT = 0x0318;
        const int WM_SYSCOMMAND = 0x0112;
        const int SC_MOVE = 0xF010;
        const int SC_MASK = 0xFFF0;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //HwndSource source = HwndSource.FromHwnd(new WindowInteropHelper(this).Handle);
            //source.AddHook(new HwndSourceHook(WndProc));
        }

        private static IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch(msg)
            {
                case WM_SYSCOMMAND:
                    if((wParam.ToInt32() & SC_MASK) == SC_MOVE)
                    { 
                        handled = true;
                    }
                    break;

                case WM_PRINT:
                case WM_PRINTCLIENT:

                    break;
            }

            return IntPtr.Zero;
        }
    }
}
