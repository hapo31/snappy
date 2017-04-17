using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DnDScreenCapture.Utils;
using DnDScreenCapture.Service;

namespace DnDScreenCapture.ViewModel
{
    class SettingWindowViewModel: INotifyPropertyChanged
    {

        private Twitter twitterInfo;
        public SettingWindowViewModel(Twitter twitterInfo)
        {

            this.twitterInfo = twitterInfo;
        }

        private string screenName = "";
        public string ScreenName
        {
            get
            {
                return screenName;
            }
            set
            {
                screenName = value;
                PropertyChanged.Notice(this);
            }
        }


        private string showName = "";
        public string ShowName
        {
            get { return showName; }
            set
            {
                showName = value;
                PropertyChanged.Notice(this);
            }
        }


        public void OpenOAuthWindow()
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

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
