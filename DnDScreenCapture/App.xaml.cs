﻿using System;
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
                Uri oauthUri = twitter.GetOAuthUri(DnDScreenCapture.Properties.Resources.CallbackScheme);
                var oauth = new View.OAuthWindow(oauthUri);
                oauth.oauthCallbackHandler += async (e) =>
                {
                    var tokens = await twitter.GetTokensByVerifierAsync(e.Verifier);
                    twitter.SaveToken("token.xml", tokens);
                    oauth.Close();
                };
                oauth.ShowDialog();
            }
            else
            {

            }
            app.InitializeComponent();
            app.Run();
        }
    }
}
