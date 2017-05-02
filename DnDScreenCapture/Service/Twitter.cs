
using System.Xml.Serialization;

using CoreTweet;
using System;
using System.Threading.Tasks;

namespace DnDScreenCapture.Service
{
    public class Twitter
    {
        //インスタンスを持ち越す必要があるのでメンバで持つ
        private OAuth.OAuthSession session;

        private string ConsumerKey { get; set; }
        private string ConsumerSecret { get; set; }
        public Tokens Token { get; set; }

        /// <summary>
        /// OAuth認証が必要かどうかを返すプロパティ
        /// </summary>
        public bool AuthorizationRequired
        {
            get
            {
                return string.IsNullOrEmpty(Token.AccessToken) || string.IsNullOrEmpty(Token.AccessTokenSecret);
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="consumer_key">アプリのコンシューマキー</param>
        /// <param name="consumer_secret">アプリのコンシューマシークレットキー</param>
        public Twitter(string consumer_key, string consumer_secret)
        {
            ConsumerKey = consumer_key;
            ConsumerSecret = consumer_secret;
            Token = new Tokens();
            Token.ConsumerKey = consumer_key;
            Token.ConsumerSecret = consumer_secret;
        }

        /// <summary>
        /// PINコードによる認証を行います
        /// </summary>
        /// <param name="PINCode">ブラウザ上で取得したPINコード</param>
        /// <returns></returns>
        public bool OAuth(string PINCode = "")
        {
            if (string.IsNullOrEmpty(PINCode))
            {
                Token = null;
                try
                {
                    session = CoreTweet.OAuth.Authorize(ConsumerKey, ConsumerSecret);

                    var url = session.AuthorizeUri;
                    System.Diagnostics.Process.Start(url.ToString());
                }
                catch (TwitterException ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                Token = CoreTweet.OAuth.GetTokens(session, PINCode);
                SaveToken("config.xml");
                session = null;
            }

            return true;
        }

        /// <summary>
        /// OAuth認証のためのURLを取得します
        /// </summary>
        /// <param name="callbackUrl">コールバックURL</param>
        /// <returns></returns>
        public Uri GetOAuthUri(string callbackUrl = "oob")
        {
            session = CoreTweet.OAuth.Authorize(ConsumerKey, ConsumerSecret, callbackUrl);
            return session.AuthorizeUri;
        }

        /// <summary>
        /// OAuthVerifierを用いてアクセストークンを取得します
        /// </summary>
        /// <param name="verifier">OAuth認証で得られたverifier</param>
        /// <returns></returns>
        public async Task<Tokens> GetTokensByVerifierAsync(string verifier)
        {
            return await CoreTweet.OAuth.GetTokensAsync(session, verifier);
        }

        /// <summary>
        /// 取得したトークンを指定したファイル名で保存します
        /// </summary>
        /// <param name="filename">ファイル名</param>
        /// <param name="token">保存するトークン なければ内部で保持しているトークンを保存</param>
        /// <returns></returns>
        public void SaveToken(string filename, Tokens token = null)
        {
            try
            {
                using (var sw = new System.IO.StreamWriter(filename, false, new System.Text.UTF8Encoding(false)))
                {

                    var sr = new XmlSerializer(typeof(Model.Token));
                    var data = new Model.Token();
                    // トークンの取得もしくは何もしない
                    Token = token ?? Token;
                    data.AccessToken = Token.AccessToken;
                    data.AccessTokenSecret = Token.AccessTokenSecret;
                    sr.Serialize(sw, data);
                }

            }
            catch(InvalidOperationException e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 指定した名前のファイルからトークンを読み込みます
        /// </summary>
        /// <param name="filename">ファイル名</param>
        /// <returns></returns>
        public void LoadToken(string filename)
        {
            try
            {
                using (var st = new System.IO.StreamReader(filename, new System.Text.UTF8Encoding(false)))
                {
                    var sr = new XmlSerializer(typeof(Model.Token));
                    var data = (Model.Token)sr.Deserialize(st);

                    Token.AccessToken = data.AccessToken;
                    Token.AccessTokenSecret = data.AccessTokenSecret;
                }
            }
            catch(InvalidOperationException e)
            {
                throw e;
            }
        }
    }
}
