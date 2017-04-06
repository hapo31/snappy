
using System.Xml.Serialization;

using CoreTweet;
using System;

namespace DnDScreenCapture.Service
{
    public class Twitter
    {
        //インスタンスを持ち越す必要があるのでメンバで持つ
        private OAuth.OAuthSession session;

        private string ConsumerKey { get; set; }
        private string ConsumerSecret { get; set; }
        public Tokens Token { get; set; }

        public bool AuthorizationRequired
        {
            get
            {
                return string.IsNullOrEmpty(Token.AccessToken) || string.IsNullOrEmpty(Token.AccessTokenSecret);
            }
        }

        public Twitter(string consumer_key, string consumer_secret)
        {
            ConsumerKey = consumer_key;
            ConsumerSecret = consumer_secret;
            Token = new Tokens();
            Token.ConsumerKey = consumer_key;
            Token.ConsumerSecret = consumer_secret;
        }

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

        public Uri GetOAuthUri()
        {
            session = CoreTweet.OAuth.Authorize(ConsumerKey, ConsumerSecret);
            return session.AuthorizeUri;
        }

        public bool SaveToken(string filename)
        {
            try
            {
                using (var sw = new System.IO.StreamWriter(filename, false, new System.Text.UTF8Encoding(false)))
                {

                    var sr = new XmlSerializer(typeof(TokenData));
                    var data = new TokenData();

                    data.access_token = Token.AccessToken;
                    data.access_token_secret = Token.AccessTokenSecret;
                    sr.Serialize(sw, data);
                }

            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool LoadToken(string filename)
        {
            try
            {
                using (var st = new System.IO.StreamReader(filename, new System.Text.UTF8Encoding(false)))
                {
                    var sr = new XmlSerializer(typeof(TokenData));
                    var data = (TokenData)sr.Deserialize(st);

                    Token.AccessToken = data.access_token;
                    Token.AccessTokenSecret = data.access_token_secret;
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
    }

    public class TokenData
    {
        public string access_token;
        public string access_token_secret;
    }
}
