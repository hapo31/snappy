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
        
        public SettingWindowViewModel()
        {

        }

        private string screenName = "";
        public string ScreenName
        {
            get
            {
                return screenName.Length > 0 ? $"@{screenName}" : "...";
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

        private string iconPath = "";
        public string IconPath {
            get
            {
                return iconPath;
            }
            set
            {
                iconPath = value;
                PropertyChanged.Notice(this);
            }
        }


        public void OpenOAuthWindow()
        {
            Uri oauthUri = App.applicationSetting.Twitter.GetOAuthUri(DnDScreenCapture.Properties.Resources.CallbackScheme);
            var oauth = new View.OAuthWindow(oauthUri);

            oauth.oauthCallbackHandler += async (oauthResult) =>
            {
                if (oauthResult.Verified)
                {
                    var tokens = await App.applicationSetting.Twitter.GetTokensByVerifierAsync(oauthResult.Verifier);
                    var cred = await tokens.Account.VerifyCredentialsAsync();
                    this.setUITextFromCredentials(cred);
                    App.applicationSetting.Twitter.SaveToken("token.xml", tokens);
                    oauth.Close();
                }
            };
            oauth.ShowDialog();
        }

        /// <summary>
        /// 認証状態をチェックしてUIを更新する
        /// </summary>
        public async void LoadProfile()
        {
            if(App.applicationSetting.Twitter.AuthorizationRequired)
            {
                this.ShowName = "未認証";
            }
            else
            {
                var cred = await App.applicationSetting.Twitter.Token.Account.VerifyCredentialsAsync();
                this.setUITextFromCredentials(cred);
            }
        }

        private void setUITextFromCredentials(CoreTweet.UserResponse cred)
        {
            this.ShowName = cred.Name;
            this.ScreenName = cred.ScreenName;
            this.IconPath = cred.ProfileImageUrl;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
