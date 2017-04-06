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
using System.Text;

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

            var con = DnDScreenCapture.Properties.Resources.ConsumerKey;
            var cons = DnDScreenCapture.Properties.Resources.ConsumerSecret;

            var twitter = new Twitter(con, cons);

            if (!twitter.LoadToken("token.xml"))
            {
                app.StartupUri = new Uri("View/OAuthWindow.xaml");
            }
            else
            {

            }
            app.InitializeComponent();
            app.Run();
        }
    }
}
