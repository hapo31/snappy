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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DnDScreenCapture.View
{
    /// <summary>
    /// OAuthWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class OAuthWindow : Window
    {
        public OAuthWindow(Uri oauthUri)
        {
            InitializeComponent();
            var vm = new OAuthWindowViewModel();
            this.DataContext = vm;
            vm.Initialized(oauthUri);
        }
    }
}
