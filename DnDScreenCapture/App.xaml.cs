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
using DnDScreenCapture.View;

namespace DnDScreenCapture
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {

        static public ApplicationSetting applicationSetting { get; set; }

        App()
        {
            applicationSetting = new ApplicationSetting();
            var con = DnDScreenCapture.Properties.Resources.ConsumerKey;
            var cons = DnDScreenCapture.Properties.Resources.ConsumerSecret;

            applicationSetting.Twitter = new Twitter(con, cons);
        }

        [STAThread]
        public static void Main()
        {
            App app = new App();
            try
            {
                App.applicationSetting.Twitter.LoadToken("token.xml");
            }
            catch {}

            Console.WriteLine(applicationSetting.Twitter.Token.ScreenName);
            // 設定ウインドウを開く
            app.InitializeComponent();
            app.Run();
        }
    }
}
