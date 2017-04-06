using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using CoreTweet.Core;
using System.Xml.Serialization;
using DnDScreenCapture.Model;
using DnDScreenCapture.Service;

namespace DnDScreenCapture
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        [STAThread]
        public static void Main()
        {
            App app = new App();
            var twitter = new Twitter();

            {
                app.StartupUri = new Uri("View/OAuthWindow.xaml");
            }
            app.InitializeComponent();
            app.Run();
        }
    }
}
