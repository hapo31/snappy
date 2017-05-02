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
using System.Windows;
using DnDScreenCapture.View;

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
            var target = new WindowInfo(a);
            var sc = new ScreenCaptureByRectangle(target.WinodwRect);
            
            DataSrc = sc.capture();
            ImageSrc = DataSrc.GetBitmapFrame();
            ScreenSize = target.WinodwRect;
        }

        public async Task<bool> UpdateStatus()
        {
            Console.WriteLine("UpdateStatus()");
            try
            {
                SendButtonEnabled = false;
                var twitter = App.applicationSetting.Twitter;
                var isAuthorized = false;

                // 認証が必要かどうか見て認証ウインドウを開く
                if (twitter.AuthorizationRequired)
                {
                    Console.WriteLine("Authorize require.");
                    var oauth = new OAuthWindow(twitter.GetOAuthUri(DnDScreenCapture.Properties.Resources.CallbackScheme));
                    oauth.oauthCallbackHandler += async oauthResult =>
                    {                        
                        if (oauthResult.Verified)
                        {
                            // トークン保存
                            // この処理いくつかの箇所で同じこと書いてあるから共通化したい
                            var tokens = await twitter.GetTokensByVerifierAsync(oauthResult.Verifier);
                            // ファイル名も
                            twitter.SaveToken("token.xml", tokens);
                            isAuthorized = true;
                        }
                        else
                        {
                            // ウインドウのバッテン押されるか認証してもらえなかったらダイアログを出す
                            MessageBox.Show("認証に失敗しました…。");
                            isAuthorized = false;
                        }
                    };
                    // いきなり飛ばすと行儀悪いかなと思ったので確認ダイアログを出す
                    var ans = MessageBox.Show("Twitterへ投稿するには認証する必要があります。認証しますか？", "認証確認", MessageBoxButton.YesNo, MessageBoxImage.Information);
                    if (ans == MessageBoxResult.Yes)
                    {
                        MessageBox.Show("OAuth認証のため、oauth.twitter.comをWebViewで開きます。準備はよろしいですね？", "確認");
                        oauth.ShowDialog();
                    }
                    else
                    {
                        // しないならいいや
                        return false;
                    }
                }
                else
                {
                    isAuthorized = true;
                }
                if (isAuthorized)
                {
                    Console.WriteLine("Upload start...");
                    var upload = new MediaUploader(twitter);
                    var data = upload.ConvertToPNGBytes(DataSrc);
                    var id = await upload.MediaUpload(data);

                    Console.Write($"media id:{id.MediaId}");
                    var s = await twitter.Token.Statuses.UpdateAsync(
                            status: TweetText,
                            media_ids: new long[] { id.MediaId }
                        );

                    Console.WriteLine($"Upload result: {s.Id}");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                SendButtonEnabled = true;
                Console.WriteLine("operation exit.");
            }
        }

        public Bitmap DataSrc;

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

        private bool sendButtonEnabled = true;
        public bool SendButtonEnabled
        {
            get { return sendButtonEnabled; }
            set
            {
                sendButtonEnabled = value;
                PropertyChanged.Notice(this);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
