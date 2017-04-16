using DnDScreenCapture.Service;
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
    /// SettingWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class SettingWindow : Window
    {
        private Twitter twitterInfo;

        public SettingWindow(Twitter twitterInfo)
        {

            this.twitterInfo = twitterInfo;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Uri oauthUri = twitterInfo.GetOAuthUri(DnDScreenCapture.Properties.Resources.CallbackScheme);
            var oauth = new View.OAuthWindow(oauthUri);
            oauth.oauthCallbackHandler += async (oauthResult) =>
            {
                var tokens = await twitterInfo.GetTokensByVerifierAsync(oauthResult.Verifier);
                twitterInfo.SaveToken("token.xml", tokens);
                oauth.Close();
            };
            oauth.ShowDialog();
        }
    }
}
